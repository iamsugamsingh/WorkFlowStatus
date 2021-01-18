using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Net;

namespace PtNumSearch
{
    public partial class articleViewer : System.Web.UI.Page
    {
        string pdfpath;
        protected void Page_Load(object sender, EventArgs e)
        {

            String article = Request.QueryString["article"].ToString();

            String[] refstill = article.Split(' ');


            pdfpath = "W:\\test\\Access\\Planos\\"+article.Substring(0,6)+"\\"+article+".PC.pdf";

            if (File.Exists(pdfpath))
            {

                string path = pdfpath;
                WebClient client = new WebClient();
                Byte[] buffer = client.DownloadData(path);
                if (buffer != null)
                {
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-length", buffer.Length.ToString());
                    Response.BinaryWrite(buffer);
                    Response.End();
                    Response.Flush();
                }
            }
        }
    }
}