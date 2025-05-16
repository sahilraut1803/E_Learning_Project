using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;

namespace E_Learning_Project
{
    public partial class AddCourse : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Disable browser autocomplete suggestions
                txtMasterCourse.Attributes["autocomplete"] = "off";
            }
        }

        protected void btnAddMasterCourse_Click(object sender, EventArgs e)
        {
            string courseName = txtMasterCourse.Text.Trim();
            string imagePath = "";

            if (fuThumbnail.HasFile)
            {
                string filename = Path.GetFileName(fuThumbnail.FileName);
                string folder = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
                fuThumbnail.SaveAs(Path.Combine(folder, filename));
                imagePath = "Uploads/" + filename;
            }

            string cs = ConfigurationManager.ConnectionStrings["Dbconn"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("InsertMasterCourse", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CourseName", courseName);
                cmd.Parameters.AddWithValue("@Thumbnail", imagePath);

                SqlParameter outputIdParam = new SqlParameter("@NewCourseID", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outputIdParam);

                conn.Open();
                cmd.ExecuteNonQuery();

                int courseId = (int)outputIdParam.Value;

                // Redirect to subcourse page with CourseID
                Response.Redirect("AddSubCourse.aspx?courseId=" + courseId);
            }
        }
    }
}
