using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace E_Learning_Project
{
    public partial class AddTopic : System.Web.UI.Page
    {
        string cs = ConfigurationManager.ConnectionStrings["Dbconn"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadMasterCourses();
            }
        }

        private void LoadMasterCourses()
        {
            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("SELECT CourseID, CourseName FROM MasterCourse", conn);
                conn.Open();
                ddlMasterCourse.DataSource = cmd.ExecuteReader();
                ddlMasterCourse.DataTextField = "CourseName";
                ddlMasterCourse.DataValueField = "CourseID";
                ddlMasterCourse.DataBind();
                ddlMasterCourse.Items.Insert(0, new ListItem(" Select Master Course ", ""));
            }
        }

        protected void ddlMasterCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            int courseId;
            if (int.TryParse(ddlMasterCourse.SelectedValue, out courseId))
            {
                using (SqlConnection conn = new SqlConnection(cs))
                {
                    SqlCommand cmd = new SqlCommand("SELECT SubCourseID, SubCourseName FROM SubCourse WHERE CourseID=@CourseID", conn);
                    cmd.Parameters.AddWithValue("@CourseID", courseId);
                    conn.Open();
                    ddlSubCourse.DataSource = cmd.ExecuteReader();
                    ddlSubCourse.DataTextField = "SubCourseName";
                    ddlSubCourse.DataValueField = "SubCourseID";
                    ddlSubCourse.DataBind();
                    ddlSubCourse.Items.Insert(0, new ListItem(" Select Subcourse ", ""));
                }
            }
            else
            {
                ddlSubCourse.Items.Clear();
                ddlSubCourse.Items.Insert(0, new ListItem(" Select Subcourse ", ""));
            }
        }

        protected void btnAddTopic_Click(object sender, EventArgs e)
        {
            int subCourseId;
            if (!int.TryParse(ddlSubCourse.SelectedValue, out subCourseId))
            {
                Response.Write("<script>alert('Please select a subcourse');</script>");
                return;
            }

            string topicName = txtTopicName.Text.Trim();
            string videoUrl = txtVideoURL.Text.Trim();
            int duration;

            if (!int.TryParse(txtDurationSeconds.Text.Trim(), out duration) || duration <= 0)
            {
                Response.Write("<script>alert('Please enter a valid duration in seconds');</script>");
                return;
            }

            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("InsertTopic", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SubCourseID", subCourseId);
                cmd.Parameters.AddWithValue("@TopicName", topicName);
                cmd.Parameters.AddWithValue("@VideoURL", videoUrl);
                cmd.Parameters.AddWithValue("@DurationSeconds", duration);
                conn.Open();
                cmd.ExecuteNonQuery();

                Response.Write("<script>alert('Topic added successfully');</script>");
            }

            ClearForm();
        }

        private void ClearForm()
        {
            ddlMasterCourse.SelectedIndex = 0;
            ddlSubCourse.Items.Clear();
            ddlSubCourse.Items.Insert(0, new ListItem(" Select Subcourse ", ""));
            txtTopicName.Text = "";
            txtVideoURL.Text = "";
            txtDurationSeconds.Text = "";
        }
    }
}
