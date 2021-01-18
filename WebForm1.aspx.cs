using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.OleDb;
using System.Data;
using System.Web.Services;
using System.Web.Script.Services;

namespace PtNumSearch
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        static string connection = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=D:Tablas.mdb";
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!this.IsPostBack)
            //{
            //    //string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            //    using (OleDbConnection con = new OleDbConnection(connection))
            //    {
            //        using (OleDbCommand cmd = new OleDbCommand("SELECT NumOrd,PinOrd, ArtOrd, DiscountAllowed FROM [Ordenes de fabricación] where "))
            //        {
            //            using (OleDbDataAdapter sda = new OleDbDataAdapter())
            //            {
            //                DataTable dt = new DataTable();
            //                cmd.CommandType = CommandType.Text;
            //                cmd.Connection = con;
            //                sda.SelectCommand = cmd;
            //                sda.Fill(dt);
            //                gvUsers.DataSource = dt;
            //                gvUsers.DataBind();
            //            }
            //        }
            //    }
            //}
        }

        [WebMethod]
        [ScriptMethod]
        public static void SaveUser(User user)
        {
            //string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (OleDbConnection con = new OleDbConnection(connection))
            {
                using (OleDbCommand cmd = new OleDbCommand("Update [Ordenes de fabricación] SET [DiscountAllowed] = @Username Where [NumOrd] = @Password"))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Username", user.Username);
                    cmd.Parameters.AddWithValue("@Password", user.Password);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
    }

    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}