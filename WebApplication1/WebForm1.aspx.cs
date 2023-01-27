using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            byte[] bytes = null;
            HttpPostedFile postedfile = FileUpload1.PostedFile;
            string filename = Path.GetFileName(postedfile.FileName);
            string fileextension = Path.GetExtension(postedfile.FileName);
            int filesize = postedfile.ContentLength;

            if (fileextension.ToLower() == ".jpg" || fileextension.ToLower() == ".bmp" ||
                fileextension.ToLower() == ".gif" || fileextension.ToLower() == ".png")
            {
                Stream stream = postedfile.InputStream;
                BinaryReader binaryreader = new BinaryReader(stream);
                bytes = binaryreader.ReadBytes((int)stream.Length);
            }
            else
            {
                //else code for not image type goes here
            }

            //now code for database
            string connectionstring = "Data Source=localhost\\MSSQLSERVER04;Database=master;Integrated Security=SSPI";
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                string query = "insert into imagedata values (@code,@filename,@fileextension,@filesize,@filecontent)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@code", txtcode.Text.Trim());
                cmd.Parameters.AddWithValue("@filename", filename);
                cmd.Parameters.AddWithValue("@fileextension", fileextension);
                cmd.Parameters.AddWithValue("@filesize", filesize);
                cmd.Parameters.AddWithValue("@filecontent", bytes);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
        protected void btnshow_Click(object sender, EventArgs e) 
        {
            string connectionstring = "Data Source=localhost\\MSSQLSERVER04;Database=master;Integrated Security=SSPI";
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                string query = "select * from imagedata where id='" + txtcode.Text.Trim() + "'";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.SelectCommand = cmd;
                DataTable dt = new DataTable();
                da.Fill(dt);
                if(dt != null && dt.Rows.Count > 0)
                {
                    //first convert the fieldcontent to bytearray and then in base64string
                    byte[] bytes = (byte[])dt.Rows[0]["filecontent"];
                    string str = Convert.ToBase64String(bytes);
                    Image1.ImageUrl = "data:Image/png;base64," + str;
                }
            }
        }
    }
}