using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace PtNumSearch
{
    public partial class dxgfileStatus : System.Web.UI.Page
    {
        FileInfo fileData;
        string filename;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["dxgfileStatus"] != null)
            {
                String file = Request.QueryString["dxgfileStatus"].ToString();

                String[] dxfFiles = Directory.GetFiles("W:\\test\\Access\\Planos\\" + file.Substring(0,6) + "\\", "*.dxf");

                foreach (String filePath in dxfFiles)
                {
                    if (filePath.Contains(file))
                    {
                        filename = new FileInfo(filePath).Name;

                    }
                }
                iframe.Attributes.Add("src", "https://beta.sharecad.org/cadframe/load?url=10.0.0.5:180/W://test/Access/Planos/" + file.Substring(0, 6) + "/" + filename);
            }
        }
    }
}