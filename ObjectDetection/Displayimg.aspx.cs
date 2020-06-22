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
        private static DataTable mainDt = new DataTable();
        private static int imageNo = 0,initH,initW,newH,newW;
        private static int totalImages=-1;
        private static String[,] cordinates = new String[20,5];
        private static int no_Of_Objects = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());

                string mainconn = ConfigurationManager.ConnectionStrings["Images"].ConnectionString;
                SqlConnection sqlconn = new SqlConnection(mainconn);
                string sqlquery = "select [ImgId],[ImgName],[ImgPath] from [dbo].[ImageSave] where Status='No'";
                SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);
                SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);
                sda.Fill(mainDt);
                totalImages = mainDt.Rows.Count;
                getInitialCordinates(mainDt.Rows[imageNo].ItemArray[2].ToString());
            }
            
            DispImage.ImageUrl = mainDt.Rows[imageNo].ItemArray[2].ToString();
            //cropImage(sender, e);
        }

        protected void getInitialCordinates(string filename)
        {
            string path = Path.Combine(Server.MapPath(filename));
            System.Drawing.Image img = System.Drawing.Image.FromFile(path);
            initH = img.Height;
            initW = img.Width;
        }
        
        protected override void OnPreRender(EventArgs e)
        {
            ViewState["update"] = Session["update"];
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["Images"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);
            sqlconn.Open();
            string sqlquery = "select [ImgId],[ImgName],[ImgPath] from [dbo].[ImageSave] where ImgId=" + TextBox1.Text;
            SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);
            SqlDataReader sdr = sqlcomm.ExecuteReader();
            string imagePath="";
            while (sdr.Read())
            {
                imagePath = sdr[2].ToString();
            }
            getInitialCordinates(imagePath);
            DispImage.ImageUrl = imagePath;
            

            sqlconn.Close();
            //cropImage(sender, e);
        }

        //protected void cropImage(Object sender, EventArgs e)
        //{
        //    string filename = mainDt.Rows[imageNo].ItemArray[1].ToString();
        //    string filePath = Path.Combine(Server.MapPath("~/data"),filename);
        //    // string cropFileName = "";
        //    //string cropFilePath = "";
        //    if (File.Exists(filePath))
        //    {
        //        System.Drawing.Image orgImg = System.Drawing.Image.FromFile(filePath);
        //        Rectangle CropArea = new Rectangle(Convert.ToInt32(X.Value), Convert.ToInt32(Y.Value), Convert.ToInt32(W.Value), Convert.ToInt32(H.Value));
        //        try
        //        {
        //            Bitmap bitMap = new Bitmap(CropArea.Width, CropArea.Height);
        //            using (Graphics g = Graphics.FromImage(bitMap))
        //            {
        //                g.DrawImage(orgImg, new Rectangle(0, 0, bitMap.Width, bitMap.Height), CropArea, GraphicsUnit.Pixel);
        //            }
        //            //cropFileName = "crop_" + fileName;
        //            // cropFilePath = Path.Combine(Server.MapPath("~/data"), cropFileName);
        //        }
        //        catch (Exception ex)
        //        {
        //            throw;
        //        }
        //    }
        //}

        protected void Load_Previous_Image(object sender,EventArgs e)
        {
            if (Session["update"].ToString() == ViewState["update"].ToString())
            {
                if (no_Of_Objects > 0)
                {
                    Add_Data_To_Database(sender, e);
                }

                if (imageNo > 0)
                {
                    imageNo--;
                    getInitialCordinates(mainDt.Rows[imageNo].ItemArray[2].ToString());
                    DispImage.ImageUrl = mainDt.Rows[imageNo].ItemArray[2].ToString();
                }
                else
                {
                    Response.Write("<script>alert('Reached First Image')</script>");
                }

                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }

        protected void Load_Next_Image(object sender, EventArgs e)
        {
            
            if (Session["update"].ToString() == ViewState["update"].ToString())
            {
                if (no_Of_Objects > 0)
                {
                    Add_Data_To_Database(sender, e);
                }

                if (imageNo < totalImages - 1)
                {

                    imageNo = imageNo + 1;
                    getInitialCordinates(mainDt.Rows[imageNo].ItemArray[2].ToString());
                    DispImage.ImageUrl = mainDt.Rows[imageNo].ItemArray[2].ToString();
                }
                else
                {
                    Response.Write("<script>alert('Reached Last Image')</script>");
                }

                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }

        protected void Add_Data_To_Database(object sender, EventArgs e)
        {
            if (Session["update"].ToString() == ViewState["update"].ToString())
            {
                Add_Another_Data(sender, e);
                OnPreRender(e);
                
                if(no_Of_Objects > 0)
                {
                    
                    int id = (Int32)mainDt.Rows[imageNo].ItemArray[0];
                    
                    SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Admin\Desktop\ObjectDetection\Images.mdf;Integrated Security=True;Connect Timeout=30");
                    con.Open();

                    // To add Data of all the objects into database...
                    for (int i = 0; i < no_Of_Objects; i++)
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO Coordinates ([ImgId],[X-Dimension],[Y-Dimension],[Height],[Width],[Label]) VALUES(@Id,@X,@Y,@H,@W,@Label)", con);

                        cmd.Parameters.Add("@Id", System.Data.SqlDbType.Int);
                        cmd.Parameters.Add("@X", System.Data.SqlDbType.Int);
                        cmd.Parameters.Add("@Y", System.Data.SqlDbType.Int);
                        cmd.Parameters.Add("@H", System.Data.SqlDbType.Int);
                        cmd.Parameters.Add("@W", System.Data.SqlDbType.Int);

                        cmd.Parameters["@Id"].Value = id;
                        cmd.Parameters["@X"].Value = int.Parse(cordinates[i, 0]);
                        cmd.Parameters["@Y"].Value = int.Parse(cordinates[i, 1]);
                        cmd.Parameters["@H"].Value = int.Parse(cordinates[i, 2]);
                        cmd.Parameters["@W"].Value = int.Parse(cordinates[i, 3]);
                        cmd.Parameters.AddWithValue("@Label", cordinates[i, 4]);
                        cmd.ExecuteNonQuery();
                    }

                    SqlCommand cmd2 = new SqlCommand("UPDATE IMAGESAVE SET Status = 'Yes' WHERE ImgId = @Id", con);
                    cmd2.Parameters.Add("@Id", System.Data.SqlDbType.Int);
                    cmd2.Parameters["@Id"].Value = id;
                    cmd2.ExecuteNonQuery();

                    con.Close();
                    no_Of_Objects = 0;
                    Array.Clear(cordinates, 0, cordinates.Length);
                    cordinates = new String[20, 5];

                }
                

                Load_Next_Image(sender, e);

                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());

            }
        }

        protected void Add_Another_Data(object sender, EventArgs e)
        {
            if (Session["update"].ToString() == ViewState["update"].ToString())
            {
                cordinates[no_Of_Objects, 0] = X.Value;
                cordinates[no_Of_Objects, 1] = X.Value;
                cordinates[no_Of_Objects, 2] = H.Value;
                cordinates[no_Of_Objects, 3] = W.Value;
                cordinates[no_Of_Objects, 4] = DropDownList1.SelectedValue;
                if (H.Value != "" && W.Value != "")
                {
                    no_Of_Objects++;
                }

                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }

        protected void Add_New_Label(object sender, EventArgs e)
        {
            if (Session["update"].ToString() == ViewState["update"].ToString())
            {
                string label = NewLabelBox.Text;
                if (!string.IsNullOrEmpty(label) && !DropDownList1.Items.Contains(new ListItem(label)))
                {
                    DropDownList1.Items.Add(new ListItem(label));
                }

                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());

            }
        }

        
    }
}