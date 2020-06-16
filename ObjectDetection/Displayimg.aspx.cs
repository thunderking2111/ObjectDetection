using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.IO;

namespace ObjectDetection
{
    public partial class Displayimg : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
             string mainconn = ConfigurationManager.ConnectionStrings["Images"].ConnectionString;
                SqlConnection sqlconn = new SqlConnection(mainconn);
                sqlconn.Open();
                string sqlquery = "select [ImgPath] from [dbo].[ImageSave] where ImgId="+TextBox1.Text;
                SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);
                SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                GridView1.DataSource = dt;
                GridView1.DataBind();
                sqlconn.Close();

          
            string filePath = Path.Combine(Server.MapPath("~/data"));
           // string cropFileName = "";
            //string cropFilePath = "";
            if (File.Exists(filePath))
            {
                System.Drawing.Image orgImg = System.Drawing.Image.FromFile(filePath);
                Rectangle CropArea = new Rectangle(Convert.ToInt32(X.Value), Convert.ToInt32(Y.Value), Convert.ToInt32(W.Value), Convert.ToInt32(H.Value));
                try
                {
                    Bitmap bitMap = new Bitmap(CropArea.Width, CropArea.Height);
                    using (Graphics g = Graphics.FromImage(bitMap))
                    {
                        g.DrawImage(orgImg, new Rectangle(0, 0, bitMap.Width, bitMap.Height), CropArea, GraphicsUnit.Pixel);
                    }
                    //cropFileName = "crop_" + fileName;
                   // cropFilePath = Path.Combine(Server.MapPath("~/data"), cropFileName);
                  


                }
                catch (Exception ex)
                {
                    throw;
                }


            }


        }

        protected void Unnamed1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Admin\Downloads\ObjectDetection\Images.mdf;Integrated Security=True;Connect Timeout=30");
            con.Open();
          

            SqlCommand cmd = new SqlCommand("insert into Coordinates values('" + X.Value + "','" + Y.Value + "','" + H.Value + "','" + W.Value + "','" + DropDownList1.SelectedValue + "')", con);
            cmd.ExecuteNonQuery();

            con.Close();

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string lable = TextBox2.Text;
            if(!string.IsNullOrEmpty(lable))
            {
                DropDownList1.Items.Add(new ListItem(lable));
            }
        }
    }
}