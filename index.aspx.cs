using System;

namespace PtNumSearch
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void loaction1_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx?location=1");
        }

        protected void loaction2_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx?location=2");
        }
    }
}