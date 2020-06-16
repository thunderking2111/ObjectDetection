using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace ObjectDetection
{
    public partial class ImageUpload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            foreach (HttpPostedFile p in FileUpload1.PostedFiles)
            {
                p.SaveAs(MapPath("~/data/" + p.FileName));
                string mainconn = ConfigurationManager.ConnectionStrings["Images"].ConnectionString;
                SqlConnection sqlconn = new SqlConnection(mainconn);
                sqlconn.Open();
                string sqlquery = " insert into [dbo].[ImageSave] ([ImgName],[ImgPath]) values (@ImgName,@ImgPath)";
                SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);
                sqlcomm.Parameters.AddWithValue("@ImgName", p.FileName);
                sqlcomm.Parameters.AddWithValue("@ImgPath", "data/" + p.FileName);
                sqlcomm.ExecuteNonQuery();

            }
            Response.Write(FileUpload1.PostedFiles.Count + " Fill Upload Successfully");

            show_data();
        }

        private void show_data()
        {
            DirectoryInfo d = new DirectoryInfo(MapPath("~/data/"));
            FileInfo[] f = d.GetFiles();
            Repeater1.DataSource = f;
            Repeater1.DataBind();


        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            foreach (HttpPostedFile p in FileUpload1.PostedFiles)
            {
                p.SaveAs(MapPath("~/data/" + p.FileName));
               


                    }
        }

        }
    }
