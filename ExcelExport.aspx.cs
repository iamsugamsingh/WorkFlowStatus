using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Drawing;
using System.Data;

namespace PtNumSearch
{
    public partial class ExcelExport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dt = Session["exportTable"] as DataTable;

            GridView1.DataSource = dt;
            GridView1.DataBind();

            Response.Clear();  
            Response.Buffer = true;  
            Response.ClearContent();  
            Response.ClearHeaders();  
            Response.Charset = "";  
            string FileName ="WorkFlow Data ("+DateTime.Now+").xls";  //... Give here file name which is going to save....
            StringWriter strwritter = new StringWriter();  
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);        
            Response.Cache.SetCacheability(HttpCacheability.NoCache);  
            Response.ContentType ="application/vnd.ms-excel";    
            Response.AddHeader("Content-Disposition","attachment;filename=" + FileName);  
            GridView1.GridLines = GridLines.Both;  
            GridView1.HeaderStyle.Font.Bold = true;

            GridView1.RenderBeginTag(htmltextwrtter);
            GridView1.HeaderRow.RenderControl(htmltextwrtter);
            foreach (GridViewRow row in GridView1.Rows)
            {
                row.RenderControl(htmltextwrtter);
            }
            GridView1.FooterRow.RenderControl(htmltextwrtter);
            GridView1.RenderEndTag(htmltextwrtter);  
            Response.Write(strwritter.ToString());  
            Response.End();
        }
    }
}