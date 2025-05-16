using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI.WebControls;

namespace E_Learning_Project
{
    public partial class AddSubCourse : System.Web.UI.Page
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

        protected void btnAddSubCourse_Click(object sender, EventArgs e)
        {
            if (ddlMasterCourse.SelectedIndex == 0)
            {
                Response.Write("<script>alert('Please select a master course');</script>");
                return;
            }

            int courseId = int.Parse(ddlMasterCourse.SelectedValue);
            string subCourseName = txtSubCourseName.Text.Trim();
            decimal price;
            if (!decimal.TryParse(txtPrice.Text.Trim(), out price))
            {
                Response.Write("<script>alert('Enter a valid price');</script>");
                return;
            }

            string thumbnailPath = "";
            if (fuSubThumbnail.HasFile)
            {
                string filename = Path.GetFileName(fuSubThumbnail.FileName);
                string folder = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
                string fullPath = Path.Combine(folder, filename);
                fuSubThumbnail.SaveAs(fullPath);
                thumbnailPath = "Uploads/" + filename;
            }

            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("InsertSubCourse", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CourseID", courseId);
                cmd.Parameters.AddWithValue("@SubCourseName", subCourseName);
                cmd.Parameters.AddWithValue("@Price", price);
                cmd.Parameters.AddWithValue("@Thumbnail", thumbnailPath);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            Response.Write("<script>alert('Subcourse added successfully');</script>");
            ClearForm();
        }

        private void ClearForm()
        {
            ddlMasterCourse.SelectedIndex = 0;
            txtSubCourseName.Text = "";
            txtPrice.Text = "";
        }
    }
}
