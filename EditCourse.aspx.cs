// EditCourse.aspx.cs - Code-behind file for EditCourse.aspx
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace E_Learning_Project
{
    public partial class EditCourse : System.Web.UI.Page
    {
        string cs = ConfigurationManager.ConnectionStrings["Dbconn"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (int.TryParse(Request.QueryString["CourseID"], out int courseId))
                {
                    hfCourseID.Value = courseId.ToString();
                    LoadCourse(courseId);
                }
                else
                {
                    Response.Redirect("CourseList.aspx");
                }
            }
        }

        private void LoadCourse(int courseId)
        {
            using (SqlConnection conn = new SqlConnection(cs))
            {
                conn.Open();

                // Load Master Course
                SqlCommand cmd = new SqlCommand("SELECT CourseName, Thumbnail FROM MasterCourse WHERE CourseID = @CourseID", conn);
                cmd.Parameters.AddWithValue("@CourseID", courseId);
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        txtCourseName.Text = rdr["CourseName"].ToString();
                        txtThumbnail.Text = rdr["Thumbnail"].ToString();
                    }
                }

                // Load SubCourses
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM SubCourse WHERE CourseID = @CourseID", conn);
                da.SelectCommand.Parameters.AddWithValue("@CourseID", courseId);
                DataTable subCoursesTable = new DataTable();
                da.Fill(subCoursesTable);

                List<dynamic> subCoursesWithTopics = new List<dynamic>();

                foreach (DataRow row in subCoursesTable.Rows)
                {
                    int subCourseId = Convert.ToInt32(row["SubCourseID"]);

                    SqlDataAdapter topicAdapter = new SqlDataAdapter("SELECT * FROM Topic WHERE SubCourseID = @SubCourseID", conn);
                    topicAdapter.SelectCommand.Parameters.AddWithValue("@SubCourseID", subCourseId);
                    DataTable topicsTable = new DataTable();
                    topicAdapter.Fill(topicsTable);

                    subCoursesWithTopics.Add(new
                    {
                        SubCourseID = subCourseId,
                        SubCourseName = row["SubCourseName"].ToString(),
                        Price = row["Price"].ToString(),
                        Thumbnail = row["Thumbnail"].ToString(),
                        Topics = topicsTable
                    });
                }

                rptSubCourses.DataSource = subCoursesWithTopics;
                rptSubCourses.DataBind();
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int courseId = int.Parse(hfCourseID.Value);
            using (SqlConnection conn = new SqlConnection(cs))
            {
                conn.Open();

                // Update MasterCourse
                SqlCommand updateMasterCmd = new SqlCommand("UPDATE MasterCourse SET CourseName = @CourseName, Thumbnail = @Thumbnail WHERE CourseID = @CourseID", conn);
                updateMasterCmd.Parameters.AddWithValue("@CourseName", txtCourseName.Text.Trim());
                updateMasterCmd.Parameters.AddWithValue("@Thumbnail", txtThumbnail.Text.Trim());
                updateMasterCmd.Parameters.AddWithValue("@CourseID", courseId);
                updateMasterCmd.ExecuteNonQuery();

                // Loop SubCourses
                foreach (RepeaterItem subItem in rptSubCourses.Items)
                {
                    int subCourseId = int.Parse(((HiddenField)subItem.FindControl("hfSubCourseID")).Value);
                    string subName = ((TextBox)subItem.FindControl("txtSubCourseName")).Text.Trim();
                    decimal price = decimal.Parse(((TextBox)subItem.FindControl("txtPrice")).Text.Trim());
                    string subThumb = ((TextBox)subItem.FindControl("txtSubThumbnail")).Text.Trim();

                    SqlCommand updateSub = new SqlCommand("UPDATE SubCourse SET SubCourseName = @Name, Price = @Price, Thumbnail = @Thumb WHERE SubCourseID = @ID", conn);
                    updateSub.Parameters.AddWithValue("@Name", subName);
                    updateSub.Parameters.AddWithValue("@Price", price);
                    updateSub.Parameters.AddWithValue("@Thumb", subThumb);
                    updateSub.Parameters.AddWithValue("@ID", subCourseId);
                    updateSub.ExecuteNonQuery();

                    Repeater rptTopics = (Repeater)subItem.FindControl("rptTopics");
                    foreach (RepeaterItem topicItem in rptTopics.Items)
                    {
                        int topicId = int.Parse(((HiddenField)topicItem.FindControl("hfTopicID")).Value);
                        string topicName = ((TextBox)topicItem.FindControl("txtTopicName")).Text.Trim();
                        string videoURL = ((TextBox)topicItem.FindControl("txtVideoURL")).Text.Trim();
                        int duration = int.Parse(((TextBox)topicItem.FindControl("txtDuration")).Text.Trim());

                        SqlCommand updateTopic = new SqlCommand("UPDATE Topic SET TopicName = @Name, VideoURL = @Video, DurationSeconds = @Duration WHERE TopicID = @ID", conn);
                        updateTopic.Parameters.AddWithValue("@Name", topicName);
                        updateTopic.Parameters.AddWithValue("@Video", videoURL);
                        updateTopic.Parameters.AddWithValue("@Duration", duration);
                        updateTopic.Parameters.AddWithValue("@ID", topicId);
                        updateTopic.ExecuteNonQuery();
                    }
                }

                lblMessage.Text = "Course, SubCourses and Topics updated successfully.";
            }
        }
    }
}