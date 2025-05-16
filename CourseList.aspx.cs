using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI.WebControls;

namespace E_Learning_Project
{
    public partial class CourseList : System.Web.UI.Page
    {
        string cs = ConfigurationManager.ConnectionStrings["Dbconn"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCourseList();
            }
        }

        private void BindCourseList(string search = "")
        {
            var allCourses = new List<dynamic>();

            using (SqlConnection conn = new SqlConnection(cs))
            {
                string query = "SELECT * FROM MasterCourse";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        int courseId = Convert.ToInt32(rdr["CourseID"]);
                        string courseName = rdr["CourseName"].ToString();
                        string courseThumbnail = rdr["Thumbnail"].ToString();

                        var subCourses = GetSubCourses(courseId);

                        allCourses.Add(new
                        {
                            CourseID = courseId,
                            CourseName = courseName,
                            Thumbnail = courseThumbnail,
                            SubCourses = subCourses
                        });
                    }
                }
            }

            // Apply search filter
            if (!string.IsNullOrEmpty(search))
            {
                search = search.ToLower();
                allCourses = allCourses.Where(c =>
                {
                    var courseName = (string)c.CourseName;
                    var subCourses = (IEnumerable<dynamic>)c.SubCourses;

                    return courseName.ToLower().Contains(search) ||
                           subCourses.Any(sc =>
                           {
                               var subCourseName = (string)sc.SubCourseName;
                               var price = (string)sc.Price;
                               var topics = (IEnumerable<dynamic>)sc.Topics;

                               return subCourseName.ToLower().Contains(search) ||
                                      price.ToLower().Contains(search) ||
                                      topics.Any(t =>
                                      {
                                          var topicName = (string)t.TopicName;
                                          return topicName.ToLower().Contains(search);
                                      });
                           });
                }).ToList();
            }

            rptCourses.DataSource = allCourses;
            rptCourses.DataBind();
        }

        private List<dynamic> GetSubCourses(int courseId)
        {
            var subList = new List<dynamic>();

            using (SqlConnection conn = new SqlConnection(cs))
            {
                string query = "SELECT * FROM SubCourse WHERE CourseID=@CourseID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CourseID", courseId);
                conn.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        int subCourseId = Convert.ToInt32(rdr["SubCourseID"]);
                        string name = rdr["SubCourseName"].ToString();
                        string price = rdr["Price"].ToString();
                        string thumb = rdr["Thumbnail"].ToString();
                        var topics = GetTopics(subCourseId);

                        subList.Add(new
                        {
                            SubCourseID = subCourseId,
                            SubCourseName = name,
                            Price = price,
                            Thumbnail = thumb,
                            Topics = topics
                        });
                    }
                }
            }

            return subList;
        }

        private List<dynamic> GetTopics(int subCourseId)
        {
            var topicList = new List<dynamic>();

            using (SqlConnection conn = new SqlConnection(cs))
            {
                string query = "SELECT TopicName, VideoURL, DurationSeconds FROM Topic WHERE SubCourseID=@SubCourseID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@SubCourseID", subCourseId);
                conn.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        int seconds = rdr["DurationSeconds"] != DBNull.Value ? Convert.ToInt32(rdr["DurationSeconds"]) : 0;
                        int minutes = seconds / 60;
                        int remSeconds = seconds % 60;
                        string durationFormatted = $"{minutes} min {remSeconds} sec";

                        topicList.Add(new
                        {
                            TopicName = rdr["TopicName"].ToString(),
                            VideoURL = rdr["VideoURL"].ToString(),
                            DurationFormatted = durationFormatted
                        });
                    }
                }
            }

            return topicList;
        }

        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            int courseId = Convert.ToInt32(e.CommandArgument);

            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("sp_DeleteMasterCourse", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CourseID", courseId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }

            BindCourseList(txtSearch.Text.Trim());
        }

        protected void btnEdit_Command(object sender, CommandEventArgs e)
        {
            int courseId = Convert.ToInt32(e.CommandArgument);
            Response.Redirect("EditCourse.aspx?CourseID=" + courseId);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtSearch.Text.Trim();
            BindCourseList(searchTerm);
        }
    }
}
