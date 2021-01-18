using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.OleDb;
using System.Drawing;
using System.Data;
using System.IO;
using System.Web.Services;
using System.Collections;
using System.Text.RegularExpressions;
namespace PtNumSearch
{
    public partial class Default : System.Web.UI.Page
    {
        String connectionString = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;

        DataTable data = new DataTable();
        String CustomerPoDateValue, Location;
        public String FinishDate;
        List<String> articleList = new List<String>();
        int rowcount;
        protected void Page_Load(object sender, EventArgs e)
        {
            TotalRowLbl.Text = "";
            if (Request.QueryString["location"] != null)
            {
                locationBtn.Enabled = false;
                Location = Request.QueryString["location"].ToString();
                if (Location == "1")
                {
                    locationBtn.Text = "AWS 1 (-AWT)";
                    System.Drawing.Color col = System.Drawing.ColorTranslator.FromHtml("#800080");
                    locationBtn.ForeColor = Color.White;
                    locationBtn.BackColor = col;
                }
                if (Location == "2")
                {
                    locationBtn.Text = "AWS 2 (-AWT)";
                    System.Drawing.Color col = System.Drawing.ColorTranslator.FromHtml("#ffa500");
                    locationBtn.ForeColor = Color.White;
                    locationBtn.BackColor = Color.Orange;
                }
            }

            if (!IsPostBack)
            {
                string file = Server.MapPath("~/.starredUid/uid.txt");
                List<string> uidlist = new List<string>();

                if (File.Exists(file)==true)
                {
                    FileInfo info = new FileInfo(file);
                    if (info.Length > 0)
                    {
                        string[] uids = File.ReadAllLines(file);

                        foreach (string uiddata in uids)
                        {
                            string uid = uiddata.Split(' ').GetValue(0).ToString();
                            if (uid != "")
                            {
                                OleDbConnection con = new OleDbConnection(connectionString);
                                OleDbCommand cmd = new OleDbCommand("SELECT NumOrd, FinOrd, Location FROM [Ordenes de fabricación] WHERE FinOrd IS NULL AND NumOrd = " + uid, con);
                                con.Open();
                                OleDbDataReader dr = cmd.ExecuteReader();

                                if (dr.HasRows == true)
                                {
                                    while (dr.Read())
                                    {
                                        uidlist.Add(dr["NumOrd"].ToString()+" "+dr["Location"].ToString());
                                    }
                                }
                                con.Close();
                            }
                        }

                        if(File.Exists(file)==true)
                        File.Delete(file);

                        if (File.Exists(file) == false)
                        {
                            var myFile = File.Create(file);
                            myFile.Close();
                            if (uidlist.Count() > 0)
                            {
                                foreach (String uidData in uidlist)
                                {
                                    File.AppendAllText(@file,uidData+"\n");
                                    var hideFile = new DirectoryInfo(file);
                                    hideFile.Attributes = FileAttributes.Hidden;
                                }
                            }
                        }

                    }

                    //redcolorCheckbox.Checked = true;
                }

                fromorderdate.Text = DateTime.Today.AddDays(-50).ToString("dd-MMM-yyyy");

                toorderdate.Text = DateTime.Today.AddDays(-7).ToString("dd-MMM-yyyy");
            }
        }
        


        protected void SearchBtn_Click(object sender, EventArgs e)
        {
            data.Columns.Add("UID");
            data.Columns.Add("Cust_PO Date");
            data.Columns.Add("PT Date");
            data.Columns.Add("OC Status");
            data.Columns.Add("Remarks");
            //data.Columns.Add("A* Casing");
            data.Columns.Add("Casing Drawing Status");
            data.Columns.Add("Send To HT");
            data.Columns.Add("Receive To HT");
            //data.Columns.Add("B* Carbide");
            data.Columns.Add("Carbide Inq Date");
            data.Columns.Add("Carbide Recv Date");
            data.Columns.Add("Job Card Status");
            data.Columns.Add("External Grinding");
            data.Columns.Add("Dxf Ready");
            data.Columns.Add("Wirecut");
            data.Columns.Add("EDM");
            data.Columns.Add("Send To Coating");
            data.Columns.Add("Recieve To Coating");
            data.Columns.Add("End Date/Delivery Date");
            data.Columns.Add("Observation");

            fromTxtBox.Text = "";
            toTxtBox.Text = "";

            //if (PtNumTxt.Text != "")
            //{
                String SqlQuery = "";

                if (dispatchCheckBox.Checked != true)
                {
                    //SqlQuery = "SELECT [Ordenes de fabricación].NumOrd,[Ordenes de fabricación].LanOrd,[Ordenes de fabricación].EntOrd, [Ordenes de fabricación].FinOrd, [Ordenes de fabricación].RefCorte, [Ordenes de fabricación].ArtOrd, [Ordenes de fabricación].Datos, [Ordenes de fabricación].Observaciones,[Pedidos de clientes].PedPed,[Pedidos de clientes].FecPed,[Pedidos de clientes].OcDate FROM ([Artículos de clientes] INNER JOIN ([Pedidos de clientes] INNER JOIN [Ordenes de fabricación] ON [Pedidos de clientes].[NumPed] = [Ordenes de fabricación].[PinOrd]) ON [Artículos de clientes].[CodArt] = [Ordenes de fabricación].[ArtOrd]) WHERE [Ordenes de fabricación].PinOrd=" + PtNumTxt.Text + " AND [Ordenes de fabricación].Location=" + Location + "AND [Ordenes de fabricación].FinOrd IS Null";

                    SqlQuery = "SELECT [Ordenes de fabricación].NumOrd,[Ordenes de fabricación].LanOrd,[Ordenes de fabricación].EntOrd, [Ordenes de fabricación].Observaciones, [Ordenes de fabricación].FinOrd, [Ordenes de fabricación].RefCorte, [Ordenes de fabricación].ArtOrd, [Ordenes de fabricación].Datos,  [Pedidos de clientes].PedPed,[Pedidos de clientes].FecPed,[Pedidos de clientes].OcDate FROM ([Artículos de clientes] INNER JOIN ([Pedidos de clientes] INNER JOIN [Ordenes de fabricación] ON [Pedidos de clientes].[NumPed] = [Ordenes de fabricación].[PinOrd]) ON [Artículos de clientes].[CodArt] = [Ordenes de fabricación].[ArtOrd]) WHERE [Pedidos de clientes].FecPed BETWEEN #" + fromorderdate.Text + "# AND #" + toorderdate.Text + "# AND [Ordenes de fabricación].Location="+Location+" AND [Ordenes de fabricación].FinOrd IS NULL AND (([Ordenes de fabricación].Datos Not Like'%AWT%' AND [Ordenes de fabricación].Datos Not Like'%CALL OFF%'))";

                }
                else
                {
                    //SqlQuery = "SELECT [Ordenes de fabricación].NumOrd,[Ordenes de fabricación].LanOrd,[Ordenes de fabricación].EntOrd, [Ordenes de fabricación].FinOrd, [Ordenes de fabricación].RefCorte, [Ordenes de fabricación].ArtOrd, [Ordenes de fabricación].Datos, [Ordenes de fabricación].Observaciones, [Pedidos de clientes].PedPed,[Pedidos de clientes].FecPed,[Pedidos de clientes].OcDate FROM ([Artículos de clientes] INNER JOIN ([Pedidos de clientes] INNER JOIN [Ordenes de fabricación] ON [Pedidos de clientes].[NumPed] = [Ordenes de fabricación].[PinOrd]) ON [Artículos de clientes].[CodArt] = [Ordenes de fabricación].[ArtOrd]) WHERE [Ordenes de fabricación].PinOrd=" + PtNumTxt.Text + " AND [Ordenes de fabricación].Location=" + Location + "AND [Ordenes de fabricación].FinOrd IS NOT Null";

                    SqlQuery = "SELECT [Ordenes de fabricación].NumOrd,[Ordenes de fabricación].LanOrd,[Ordenes de fabricación].EntOrd, [Ordenes de fabricación].Observaciones, [Ordenes de fabricación].FinOrd, [Ordenes de fabricación].RefCorte, [Ordenes de fabricación].ArtOrd, [Ordenes de fabricación].Datos,  [Pedidos de clientes].PedPed,[Pedidos de clientes].FecPed,[Pedidos de clientes].OcDate FROM ([Artículos de clientes] INNER JOIN ([Pedidos de clientes] INNER JOIN [Ordenes de fabricación] ON [Pedidos de clientes].[NumPed] = [Ordenes de fabricación].[PinOrd]) ON [Artículos de clientes].[CodArt] = [Ordenes de fabricación].[ArtOrd]) WHERE [Pedidos de clientes].FecPed BETWEEN #" + fromorderdate.Text + "# AND #" + toorderdate.Text + "# AND [Ordenes de fabricación].Location=" + Location + " AND [Ordenes de fabricación].FinOrd IS NOT NULL  AND ([Ordenes de fabricación].Datos Not Like'%AWT%' AND [Ordenes de fabricación].Datos Not Like'%@CALL OFF%')";

                }
                getAllData(SqlQuery);
            //}
            //else
            //{
            //    Response.Write("<script>alert('Please enter PT Number...!');</script>");
            //    PtNumTxt.Focus();
            //    PtNumTxt.BackColor = Color.Red;
            //}

                if (redcolorCheckbox.Checked == true)
                {
                    //redcolorCheckbox_changed(null, null);
                    redcolorCheckbox.Checked = false;
                }
        }

        protected void EnterBtn_Click(object sender, EventArgs e)
        {
            fromorderdate.Text = "";
            toorderdate.Text = "";
            if (redcolorCheckbox.Checked == true)
            {
                redcolorCheckbox.Checked = false;
            }

            data.Columns.Add("UID");
            data.Columns.Add("Cust_PO Date");
            data.Columns.Add("PT Date");
            data.Columns.Add("OC Status");
            data.Columns.Add("Remarks");
            //data.Columns.Add("A* Casing");
            data.Columns.Add("Casing Drawing Status");
            data.Columns.Add("Send To HT");
            data.Columns.Add("Receive To HT");
            //data.Columns.Add("B* Carbide");
            data.Columns.Add("Carbide Inq Date");
            data.Columns.Add("Carbide Recv Date");
            data.Columns.Add("Job Card Status");
            data.Columns.Add("External Grinding");
            data.Columns.Add("Dxf Ready");
            data.Columns.Add("Wirecut");
            data.Columns.Add("EDM");
            data.Columns.Add("Send To Coating");
            data.Columns.Add("Recieve To Coating");
            data.Columns.Add("End Date/Delivery Date");
            data.Columns.Add("Observation");
            PtNumTxt.Text = "";

            if (fromTxtBox.Text == "")
            {
                Response.Write("<script>alert('Please enter `From Date`...!');</script>");
                fromTxtBox.Focus();
                fromTxtBox.BackColor = Color.Red;
            }
            else if (toTxtBox.Text == "")
            {
                Response.Write("<script>alert('Please enter `To Date`...!');</script>");
                toTxtBox.Focus();
                toTxtBox.BackColor = Color.Red;
            }
            else
            {
                fromTxtBox.BackColor = Color.White;
                toTxtBox.BackColor = Color.White;
                String SqlQuery = "";
                if (dispatchCheckBox.Checked != true)
                {
                    SqlQuery = "SELECT [Ordenes de fabricación].NumOrd,[Ordenes de fabricación].LanOrd,[Ordenes de fabricación].EntOrd, [Ordenes de fabricación].Observaciones, [Ordenes de fabricación].FinOrd, [Ordenes de fabricación].RefCorte, [Ordenes de fabricación].ArtOrd, [Ordenes de fabricación].Datos,  [Pedidos de clientes].PedPed,[Pedidos de clientes].FecPed,[Pedidos de clientes].OcDate FROM ([Artículos de clientes] INNER JOIN ([Pedidos de clientes] INNER JOIN [Ordenes de fabricación] ON [Pedidos de clientes].[NumPed] = [Ordenes de fabricación].[PinOrd]) ON [Artículos de clientes].[CodArt] = [Ordenes de fabricación].[ArtOrd]) WHERE [Ordenes de fabricación].EntOrd BETWEEN #" + fromTxtBox.Text + "# AND #" + toTxtBox.Text + "# AND [Ordenes de fabricación].Location=" + Location + " AND [Ordenes de fabricación].FinOrd IS NULL AND (([Ordenes de fabricación].Datos Not Like'%AWT%' AND [Ordenes de fabricación].Datos Not Like'%CALL OFF%'))";
                }
                else
                {
                    SqlQuery = "SELECT [Ordenes de fabricación].NumOrd,[Ordenes de fabricación].LanOrd,[Ordenes de fabricación].EntOrd, [Ordenes de fabricación].Observaciones, [Ordenes de fabricación].FinOrd, [Ordenes de fabricación].RefCorte, [Ordenes de fabricación].ArtOrd, [Ordenes de fabricación].Datos,  [Pedidos de clientes].PedPed,[Pedidos de clientes].FecPed,[Pedidos de clientes].OcDate FROM ([Artículos de clientes] INNER JOIN ([Pedidos de clientes] INNER JOIN [Ordenes de fabricación] ON [Pedidos de clientes].[NumPed] = [Ordenes de fabricación].[PinOrd]) ON [Artículos de clientes].[CodArt] = [Ordenes de fabricación].[ArtOrd]) WHERE [Ordenes de fabricación].EntOrd BETWEEN #" + fromTxtBox.Text + "# AND #" + toTxtBox.Text + "# AND [Ordenes de fabricación].Location=" + Location + " AND [Ordenes de fabricación].FinOrd IS NOT NULL AND (([Ordenes de fabricación].Datos Not Like'%AWT%' AND [Ordenes de fabricación].Datos Not Like'%CALL OFF%'))";                    
                }
                getAllData(SqlQuery);
            }
        }

        protected void starBtn_Click(object sender, EventArgs e)
        {
            data.Columns.Add("UID");
            data.Columns.Add("Cust_PO Date");
            data.Columns.Add("PT Date");
            data.Columns.Add("OC Status");
            data.Columns.Add("Remarks");
            //data.Columns.Add("A* Casing");
            data.Columns.Add("Casing Drawing Status");
            data.Columns.Add("Send To HT");
            data.Columns.Add("Receive To HT");
            //data.Columns.Add("B* Carbide");
            data.Columns.Add("Carbide Inq Date");
            data.Columns.Add("Carbide Recv Date");
            data.Columns.Add("Job Card Status");
            data.Columns.Add("External Grinding");
            data.Columns.Add("Dxf Ready");
            data.Columns.Add("Wirecut");
            data.Columns.Add("EDM");
            data.Columns.Add("Send To Coating");
            data.Columns.Add("Recieve To Coating");
            data.Columns.Add("End Date/Delivery Date");
            data.Columns.Add("Observation");

            string filepath = Server.MapPath("~/.starredUid/uid.txt");
            if (File.Exists(filepath))
            {
                FileInfo info = new FileInfo(filepath);
                if (info.Length > 0)
                {
                    string[] uidData = File.ReadAllLines(filepath);
                    
                    foreach (string uidvalue in uidData)
                    {
                        string[] starredUid = uidvalue.Split(' ');

                        int loccount = 0;

                        foreach (string locationItem in uidData)
                        {
                            string loc = locationItem.Split(' ').GetValue(1).ToString();
                            
                            if (loc== Location)
                            {
                                loccount++;
                            }
                        }

                        if (loccount > 1)
                        {
                            TotalRowLbl.Text = "";
                        }


                        if (starredUid[1] == Location)
                        {
                            String SqlQuery = "SELECT [Ordenes de fabricación].NumOrd,[Ordenes de fabricación].LanOrd,[Ordenes de fabricación].EntOrd, [Ordenes de fabricación].FinOrd, [Ordenes de fabricación].RefCorte, [Ordenes de fabricación].ArtOrd, [Ordenes de fabricación].Datos, [Ordenes de fabricación].Observaciones,[Pedidos de clientes].PedPed,[Pedidos de clientes].FecPed,[Pedidos de clientes].OcDate FROM ([Artículos de clientes] INNER JOIN ([Pedidos de clientes] INNER JOIN [Ordenes de fabricación] ON [Pedidos de clientes].[NumPed] = [Ordenes de fabricación].[PinOrd]) ON [Artículos de clientes].[CodArt] = [Ordenes de fabricación].[ArtOrd]) WHERE [Ordenes de fabricación].NumOrd = " + starredUid[0] + " AND [Ordenes de fabricación].FinOrd IS Null AND [Ordenes de fabricación].Location = " + Location;
                            getAllData(SqlQuery);
                        }
                    }

                }
                else
                {
                    Response.Write("<script>alert('Data Not Found...!')</script>");
                }
            }
        }

        public void getAllData(String sqlquery)
        {
            String UID = "";
            try
            {                
                OleDbConnection con = new OleDbConnection(connectionString);
                OleDbCommand command = new OleDbCommand(sqlquery, con);
                con.Open();
                OleDbDataReader dr = command.ExecuteReader();

                String article = "", PtDate = "", remarks = "", aCasingData = "", casingDrawing = "", sendToHeatTreatmentData = "";
                String receiveToHeatTreatmentData = "", bCasingData = "", JobCardStatus = "", carbideInquiryDate="", carbideReceiveDate="", externalGrinding="";
                String DeliveryDate = "", wirecutData = "", sendToCoating = "", receiveToCoating = "", dxfFileSatus = "", EDM = "", OcStatus = "",EndDateDeliveryDate="", observation="";

                if (dr.HasRows == true)
                {
                    while (dr.Read())
                    {
                        UID = dr["NumOrd"].ToString();
                        article = dr["ArtOrd"].ToString();

                        String UidData = UID + "<br/>(" + article + ")";

                        CustomerPoDateValue = CustomerPoDate(UID);


                        
                        articleList.Add(article);

                        

                        Session["articleList"] = articleList;

                        if(dr["FecPed"].ToString()!="")
                        {
                            PtDate = Convert.ToDateTime(dr["FecPed"]).ToString("dd-MMM-yyyy") + "<br/>(" + (Convert.ToDateTime(dr["FecPed"]) - Convert.ToDateTime(CustomerPoDateValue)).Duration().Days.ToString() + " Days To Make)";
                        }

                        remarks = dr["Datos"].ToString();

                        if (remarks == "" || remarks == " ")
                        {
                            remarks = "N/A";
                        }

                        if (remarks.Contains('$'))
                        {
                            wirecutData = getWireCutData(UID);
                            string folderName = article.Substring(0, 6);

                            String[] dxfFiles = Directory.GetFiles("W:\\test\\Access\\Planos\\" + folderName + "\\", "*.dxf");

                            String dxfFileinfo = "";
                            foreach (String filePath in dxfFiles)
                            {
                                if (filePath.Contains(article))
                                {
                                    FileInfo fileData = new FileInfo(filePath);
                                    var fileCreatedDate = fileData.CreationTime;

                                    int days = (Convert.ToDateTime(fileCreatedDate) - Convert.ToDateTime(CustomerPoDateValue)).Days;

                                    if (days > 0)
                                    {
                                        dxfFileinfo = "File Created<br/><br/>(" + days.ToString() + " Days To Make)";
                                    }
                                    else
                                    {
                                        dxfFileinfo = "Old File Exist<br/><br/>(" + (days * (-1)).ToString() + " Days To Make)";
                                    }
                                }
                            }

                            if (dxfFileinfo != "")
                            {
                                dxfFileSatus = dxfFileinfo;
                            }
                            else
                            {
                                dxfFileSatus = "Pending";
                            }
                        }
                        else
                        {
                            wirecutData = "N/A";
                            dxfFileSatus = "N/A";
                        }

                        if (remarks.Contains('?'))
                        {
                            sendToCoating = getSendToCoatingData(UID);

                            if (sendToCoating.Contains("N/A"))
                            {
                                sendToCoating = "Pending";
                            }

                            if (sendToCoating.Contains("Days"))
                            {
                                receiveToCoating = "Pending";
                            }
                            else
                            {
                                receiveToCoating = getReceiveToCoating(UID);
                            }
                        }
                        else
                        {
                            sendToCoating = "N/A";
                            receiveToCoating = "N/A";
                        }

                        if (remarks.Contains('#'))
                        {
                            EDM = getEdmData(UID);

                        }
                        else
                        {
                            EDM = "N/A";
                        }

                        aCasingData = "";
                        aCasingData = getACasing(UID);

                        if (aCasingData.Contains("(A)"))
                        {
                            sendToHeatTreatmentData = sendToHeatTreatment(UID);
                            if (sendToHeatTreatmentData.Contains("Days"))
                            {
                                receiveToHeatTreatmentData = "Pending";
                            }
                            else 
                            {
                                receiveToHeatTreatmentData = receiveToHeatTreatment(UID);
                            }
                        }
                        else if (aCasingData == "N/A")
                        {
                            sendToHeatTreatmentData = "N/A";
                            receiveToHeatTreatmentData = "N/A";
                        }
                        else
                        {
                            sendToHeatTreatmentData = "Carbide Only";
                            receiveToHeatTreatmentData = "Carbide Only";
                        }

                        if (dr["RefCorte"].ToString() == "0")
                        {
                            casingDrawing = "N/A";
                        }
                        else
                        {
                            String fechaDate = getFechaDate(UID);
                            if (fechaDate != "")
                            {
                                casingDrawing = dr["RefCorte"].ToString() + " <br/>(" + Convert.ToDateTime(fechaDate).ToString("dd-MMM-yyyy") + ")<br/>(" + (Convert.ToDateTime(fechaDate) - Convert.ToDateTime(CustomerPoDateValue)).Duration().Days.ToString() + " Days To Make)";
                            }
                        }

                        //sendToHeatTreatmentData = sendToHeatTreatment(UID);

                        //receiveToHeatTreatmentData = receiveToHeatTreatment(UID);

                        bCasingData = "";

                        bCasingData = getBCasing(UID);

                        if (bCasingData.Contains('B'))
                        {
                            carbideInquiryDate = getCarbideInquiryDate(UID);
                            if (carbideInquiryDate.Contains("Days"))
                            {
                                carbideReceiveDate = "Pending";
                            }
                            else
                            {
                                carbideReceiveDate = getCarbideReceiveDate(UID);
                            }
                        }
                        else if(bCasingData=="N/A")
                        {
                            carbideInquiryDate = "N/A";
                            carbideReceiveDate = "N/A";
                        }
                        else
                        {
                            carbideInquiryDate = "Only Steel";
                            carbideReceiveDate = "Only Steel";
                        }

                        

                        if (dr["LanOrd"].ToString() != "")
                        {
                            JobCardStatus = "Printed" + "<br/>(After " + (Convert.ToDateTime(dr["LanOrd"]) - Convert.ToDateTime(CustomerPoDateValue)).Duration().Days.ToString() + " Days)";
                        }
                        else
                        {
                            JobCardStatus = "Pending";
                        }

                        externalGrinding = getExternalGrinding(UID);
                        FinishDate="";
                        if (dispatchCheckBox.Checked == true)
                        {
                            if(dr["FinOrd"].ToString()!="")
                            {
                                EndDateDeliveryDate = Convert.ToDateTime(dr["FinOrd"]).ToString("dd-MMM-yyyy") + "<br/>(After " + (Convert.ToDateTime(dr["FinOrd"]) - Convert.ToDateTime(CustomerPoDateValue)).Duration().Days.ToString() + " Days)";
                                
                            }
                            else
                            {
                                EndDateDeliveryDate = "N/A";
                            }
                        }
                        else
                        {
                            if (dr["EntOrd"].ToString() != "")
                            {
                                EndDateDeliveryDate = Convert.ToDateTime(dr["EntOrd"]).ToString("dd-MMM-yyyy") + "<br/>(After " + (Convert.ToDateTime(dr["EntOrd"]) - Convert.ToDateTime(CustomerPoDateValue)).Duration().Days.ToString() + " Days)";
                            }
                            else
                            {
                                EndDateDeliveryDate = "Pending";
                            }
                        }

                        if (dr["OcDate"].ToString() != "")
                        {
                            OcStatus = Convert.ToDateTime(dr["OcDate"]).ToString("dd-MMM-yyy") + "<br/>(After " + (Convert.ToDateTime(dr["OcDate"]) - Convert.ToDateTime(CustomerPoDateValue)).Duration().Days.ToString() + " Days)";
                        }
                        else
                        {
                            OcStatus = "Pending";
                        }

                        observation = dr["Observaciones"].ToString();
                        if (observation == "")
                        {
                            observation = "N/A";
                        }

                        if (dispatchCheckBox.Checked == true)
                        {
                            data.Rows.Add(UidData, CustomerPoDateValue, PtDate, OcStatus, remarks, casingDrawing, sendToHeatTreatmentData, receiveToHeatTreatmentData, carbideInquiryDate, carbideReceiveDate, JobCardStatus, externalGrinding, dxfFileSatus, wirecutData, EDM, sendToCoating, receiveToCoating, EndDateDeliveryDate, observation);
                        }
                        else
                        {
                            data.Rows.Add(UidData, CustomerPoDateValue, PtDate, OcStatus, remarks, casingDrawing, sendToHeatTreatmentData, receiveToHeatTreatmentData, carbideInquiryDate, carbideReceiveDate, JobCardStatus, externalGrinding, dxfFileSatus, wirecutData, EDM, sendToCoating, receiveToCoating, EndDateDeliveryDate, observation);
                        }
                    }
                }
                con.Close();              

                DataTable finalTable = data.Clone();
                
                List<string> custcodeList = new List<string>();

                foreach (DataRow r in data.Rows)
                {
                    string getData = r[0].ToString();
                    string[] multidata = getData.Split('<','(',')');
                    string articleNum = multidata[2];
                    string custcode = articleNum.Substring(0,6);
                    int count = 1;
                    if (finalTable.Rows.Count == 0)
                    {
                        hiddenlbl.Text = custcode;
                        foreach (DataRow drow in data.Rows)
                        {
                            if (drow[0].ToString().Contains(custcode))
                            {
                                if (count == 1)
                                    finalTable.Rows.Add(custcode, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");

                                finalTable.Rows.Add(drow.ItemArray);
                                count = 2;
                            }
                        }

                        custcodeList.Add(custcode);
                    }
                    else
                    {
                        string custdata="";
                        if(hiddenlbl.Text!=custcode)
                        {
                            foreach (DataRow drow in data.Rows)
                            {
                                for (int i = 0; i < custcodeList.Count; i++)
                                {
                                    if (custcodeList[i] == custcode)
                                    {
                                        custdata = "Exist";
                                    }
                                }
                                
                                if(custdata!="Exist")
                                {
                                    if (drow[0].ToString().Contains(custcode))
                                    {
                                        if (count == 1)
                                            finalTable.Rows.Add(custcode, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");

                                        finalTable.Rows.Add(drow.ItemArray);
                                        count = 2;
                                    }
                                }
                            }
                            hiddenlbl.Text = custcode;
                            custcodeList.Add(custcode);

                        }
                    }
                    
                }

                List<int> sortedlist = new List<int>();

                foreach (DataRow ro in finalTable.Rows)
                {
                    if (ro[0].ToString().Length == 6)
                    {
                        string custcode = ro[0].ToString();
                        sortedlist.Add(Convert.ToInt32(custcode));
                    }
                }

                sortedlist.Sort();



                DataTable sortedfinalTable = finalTable.Clone();



                for (int i = 0; i < sortedlist.Count; i++)
                {
                    foreach (DataRow r in finalTable.Rows)
                    {
                        if (r[0].ToString().Length == 6)
                        {
                            if (sortedlist[i].ToString() == r[0].ToString())
                            {
                                sortedfinalTable.Rows.Add(r[0].ToString(), "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
                                foreach (DataRow rows in finalTable.Rows)
                                {
                                    if (rows[0].ToString().Length != 6)
                                    {
                                        if (r[0].ToString() == (rows[0].ToString().Split('<','(',')').GetValue(2).ToString().Substring(0, 6)))
                                        {
                                            sortedfinalTable.Rows.Add(rows.ItemArray);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                GridView1.DataSource = sortedfinalTable;
                GridView1.DataBind();

                Session["dataTable"] = data;
                if (Session["ViewAllDataTable"] == null)
                {
                    Session["ViewAllDataTable"] = data;
                }

            }
            catch (Exception ex)
            {
                Response.Write("getAllData for uid   " + UID + "  " + ex.Message+"<br/><br/>" );
            }
        }

        public String getEdmData(String uid)
        {

            String getEdmData = "";
            try
            {
                OleDbConnection con = new OleDbConnection(connectionString);
                OleDbCommand cmmd = new OleDbCommand("SELECT NumFas, HorFin from [Ordenes de fabricación (historia/taller)] where NumOrd="+uid, con);
                con.Open();
                OleDbDataReader rdr = cmmd.ExecuteReader();
                if (rdr.HasRows == true)
                {
                    while (rdr.Read())
                    {
                        if (rdr["NumFas"].ToString() == "80")
                        {
                            if (rdr["HorFin"].ToString() != "")
                            {
                                getEdmData = Convert.ToDateTime(rdr["HorFin"]).ToString("dd-MMM-yyyy") + "<br/>(After " + (Convert.ToDateTime(rdr["HorFin"]) - Convert.ToDateTime(CustomerPoDateValue)).Duration().Days.ToString() + " Days)";
                                break;
                            }
                            else
                            {
                                getEdmData = "N/A"; 
                            }
                        }
                        else
                        {
                            getEdmData = "N/A";
                        }
                    }
                }
                else
                {
                    getEdmData = "N/A";
                }

                con.Close();
            }
            catch(Exception ex)
            {
                Response.Write("getEDMData for uid   "+uid+" <br/> "+ex.Message +"<br/><br/>"+"<br/><br/>");
            }
            return getEdmData;
        }

        public String getReceiveToCoating(String uid)
        {
            String ReceiveToCoatingData = "";

            try
            {
                OleDbConnection connec = new OleDbConnection(connectionString);
                OleDbCommand com = new OleDbCommand("SELECT NumOrd, NumFas, CodPie, CanPie, FecAlbExt, CodProExt, PrePieExt from [Ordenes de fabricación (historia/exterior)] where NumOrd=" + uid + " Order By NumOrd Desc", connec);
                connec.Open();
                OleDbDataReader readr = com.ExecuteReader();

                if (readr.HasRows == true)
                {
                    while (readr.Read())
                    {
                        if (readr["NumFas"].ToString() == "97")
                        {
                            ReceiveToCoatingData = Convert.ToDateTime(readr["FecAlbExt"]).ToString("dd-MMM-yyyy") + "<br/>(" + (Convert.ToDateTime(readr["FecAlbExt"]) - Convert.ToDateTime(CustomerPoDateValue)).Duration().Days.ToString() + " Days After)";
                            break;
                        }
                        else
                        {
                            ReceiveToCoatingData = "N/A";
                        }
                    }
                }
                else
                {
                    ReceiveToCoatingData = "N/A";
                }
                connec.Close();
            }
            catch (Exception ex)
            {
                Response.Write("getReceiveToCoating for uid    "+uid+"     "+ex.Message +"<br/><br/>");
            }

            return ReceiveToCoatingData;
        }

        public String getSendToCoatingData(String uid)
        {
            string sendtocoating = "";

            try
            {
                OleDbConnection con = new OleDbConnection(connectionString);
                OleDbCommand cmd = new OleDbCommand("SELECT  [Pedidos a proveedor (líneas)].CodPie,[Pedidos a proveedor (líneas)].NumFas,[Pedidos a proveedor (cabeceras)].FecPed FROM (( [Pedidos a proveedor (líneas)] INNER JOIN [Pedidos a proveedor (cabeceras)]   ON  [Pedidos a proveedor (líneas)].NumPed = [Pedidos a proveedor (cabeceras)].NumPed) INNER JOIN [Proveedores] ON [Pedidos a proveedor (cabeceras)].ProPed = [Proveedores].CodPro ) WHERE [Pedidos a proveedor (líneas)].NumOrd =" + uid,con);
                con.Open();
                OleDbDataReader r = cmd.ExecuteReader();

                if (r.HasRows == true)
                {
                    while (r.Read())
                    {
                        if (r["Numfas"].ToString() == "97")
                        {
                            sendtocoating = Convert.ToDateTime(r["FecPed"]).ToString("dd-MMM-yyyy") + "<br/>(" + (Convert.ToDateTime(DateTime.Today) - Convert.ToDateTime(r["FecPed"])).Duration().Days.ToString() + " Days Ago)";
                            break;
                        }
                        else
                        {
                            sendtocoating = "N/A";
                        }
                    }
                }
                else
                {
                    sendtocoating = "N/A";
                }
                con.Close();
            }
            catch (Exception ex)
            {
                Response.Write("getSendToCoatingData for uid    " + uid + "     " + ex.Message + "<br/><br/>");
            }

            return sendtocoating;
        }

        public String getWireCutData(String uid)
        {
            String wirecutdata = "";
            try
            {
                OleDbConnection c = new OleDbConnection(connectionString);
                OleDbCommand cm = new OleDbCommand("SELECT NumFas, HorFin from [Ordenes de fabricación (historia/taller)] where NumOrd="+uid, c);
                c.Open();
                OleDbDataReader r = cm.ExecuteReader();
                if (r.HasRows == true)
                {
                    while (r.Read())
                    {   
                        if (r["NumFas"].ToString() == "26")
                        {
                            if (r["NumFas"].ToString() != "")
                            {
                                if (r["HorFin"].ToString() != "")
                                {
                                    wirecutdata = Convert.ToDateTime(r["HorFin"]).ToString("dd-MMM-yyyy") + "<br/><br/>(After " + (Convert.ToDateTime(r["HorFin"]) - Convert.ToDateTime(CustomerPoDateValue)).Duration().Days.ToString() + " Days)";
                                    break;
                                }
                                else
                                {
                                    wirecutdata = "Work In Process";
                                    break;
                                }
                            }
                            else
                            {
                                wirecutdata = "not found.....!";
                            }
                        }
                        else
                        {
                            wirecutdata = "Pending";
                        }
                    }
                }
                else
                {
                    wirecutdata = "N/A";                    
                }
            }
            catch (Exception ex)
            {
                Response.Write("getWireCutData for uid    " + uid + "     " + ex.Message +"<br/><br/>");
            }
            return wirecutdata;
        }

        public string getFechaDate(String UID)
        {
            String fechaDateValue = "";
            try
            {
                OleDbConnection conct = new OleDbConnection(connectionString);
                OleDbCommand com = new OleDbCommand("SELECT Fecha2 FROM [Mecanizados (altas) guardar PML] where NumOrd2=" + UID, conct);
                conct.Open();
                OleDbDataReader reder = com.ExecuteReader();
                if (reder.Read())
                {
                    if (reder["Fecha2"].ToString() != "")
                    {
                        string date = reder["Fecha2"].ToString();
                        fechaDateValue = Convert.ToDateTime(date).ToString("dd-MMM-yyyy");  //----------- This is Casing Section column data-------------------
                    }
                }
                conct.Close();
            }
            catch (Exception ex)
            {
                Response.Write("getFechaDate for uid    " + UID + "     " + ex.Message +"<br/><br/>");
            }

            return fechaDateValue;
        }

        public String getExternalGrinding(String uid)
        {
            String ExternalGrindingValue = "";

            try
            {
                OleDbConnection conection = new OleDbConnection(connectionString);
                OleDbCommand cm = new OleDbCommand("SELECT NumFas, HorFin from [Ordenes de fabricación (historia/taller)] where NumOrd="+uid,conection);
                conection.Open();

                OleDbDataReader r = cm.ExecuteReader();

                if (r.HasRows == true)
                {
                    while (r.Read())
                    {
                        if (r["NumFas"].ToString() == "35")
                        {
                            if (r["HorFin"].ToString() != "")
                            {
                                ExternalGrindingValue = Convert.ToDateTime(r["HorFin"]).ToString("dd-MMM-yyyy") + "<br/>(" + (Convert.ToDateTime(r["HorFin"]) - Convert.ToDateTime(CustomerPoDateValue)).Duration().Days.ToString() + " Days Ago)";
                            }
                            else
                            {
                                ExternalGrindingValue = "Work In Process";
                            }
                        }
                        else
                        {
                            ExternalGrindingValue = "N/A";
                        }
                    }
                }
                else
                {
                    ExternalGrindingValue = "N/A";
                }

                conection.Close();
            }
            catch (Exception ex)
            {
                Response.Write("getExternalGrinding for uid    " + uid + "     " + ex.Message +"<br/><br/>");
            }
            return ExternalGrindingValue;
        }

        public String getCarbideReceiveDate(String uid)
        {
            String CarbideReceiveDateValue = "";
            try
            {
                OleDbConnection conns = new OleDbConnection(connectionString);
                OleDbCommand cmnd = new OleDbCommand("SELECT NumOrd, NumFas, CodPie, CanPie, FecAlbExt, CodProExt, PrePieExt from [Ordenes de fabricación (historia/exterior)] where NumOrd=" + uid,conns);
                conns.Open();

                OleDbDataReader dread = cmnd.ExecuteReader();
                if (dread.HasRows == true)
                {
                    while (dread.Read())
                    {
                        if (dread["CodPie"].ToString().ToUpper().Contains('B'))
                        {
                            if (dread["NumFas"].ToString() == "" || dread["NumFas"].ToString() == " ")
                            {
                                if (dread["FecAlbExt"].ToString() != "")
                                {
                                    CarbideReceiveDateValue = Convert.ToDateTime(dread["FecAlbExt"]).ToString("dd-MMM-yyyy") + "<br/>(After " + (Convert.ToDateTime(dread["FecAlbExt"]) - Convert.ToDateTime(CustomerPoDateValue)).Duration().Days.ToString() + " Days)";
                                }
                                else
                                {
                                    CarbideReceiveDateValue = "N/A";
                                }
                                break;
                            }
                            else
                            {
                                CarbideReceiveDateValue = "N/A";
                            }
                        }
                        else
                        {
                            CarbideReceiveDateValue = "N/A";
                        }
                    }
                }
                else
                {
                    CarbideReceiveDateValue = "N/A";
                }
                conns.Close();
            }
            catch (Exception ex)
            {
                Response.Write("getCarbideReceiveDate for uid    " + uid + "     " + ex.Message +"<br/><br/>");
            }
            return CarbideReceiveDateValue;
        }

        public String getCarbideInquiryDate(String uid)
        {
            string CarbideInquiryDateValue = "";
            try
            {
                OleDbConnection connections = new OleDbConnection(connectionString);
                OleDbCommand commnd = new OleDbCommand("SELECT  [Pedidos a proveedor (líneas)].CodPie,[Pedidos a proveedor (líneas)].NumFas,[Pedidos a proveedor (cabeceras)].FecPed FROM (( [Pedidos a proveedor (líneas)] INNER JOIN [Pedidos a proveedor (cabeceras)]   ON  [Pedidos a proveedor (líneas)].NumPed = [Pedidos a proveedor (cabeceras)].NumPed) INNER JOIN [Proveedores] ON [Pedidos a proveedor (cabeceras)].ProPed = [Proveedores].CodPro ) WHERE [Pedidos a proveedor (líneas)].NumOrd =" + uid,connections);
                connections.Open();

                OleDbDataReader dataReader = commnd.ExecuteReader();

                if (dataReader.HasRows == true)
                {
                    while (dataReader.Read())
                    {
                        if (dataReader["CodPie"].ToString().ToUpper().Contains('B'))
                        {
                            if (dataReader["FecPed"].ToString() != "")
                            {
                                CarbideInquiryDateValue = Convert.ToDateTime(dataReader["FecPed"]).ToString("dd-MMM-yyyy") + "<br/>(" + (Convert.ToDateTime(DateTime.Today) - Convert.ToDateTime(dataReader["FecPed"])).Duration().Days.ToString() + " Days Ago)";
                            }
                        }
                        else
                        {
                            CarbideInquiryDateValue = "N/A";
                        }
                    }
                }
                else
                {
                    CarbideInquiryDateValue = "N/A";
                }
                connections.Close();
            }
            catch (Exception ex)
            {
                Response.Write("getCarbideInquiryDate for uid    " + uid + "     " + ex.Message +"<br/><br/>");
            }
            return CarbideInquiryDateValue;
        }

        public String receiveToHeatTreatment(String uid)
        {
            String recvToHT = "";
            try
            {
                OleDbConnection con = new OleDbConnection(connectionString);
                OleDbCommand cmd = new OleDbCommand("SELECT NumOrd, NumFas, CodPie, CanPie, FecAlbExt, CodProExt, PrePieExt from [Ordenes de fabricación (historia/exterior)] where NumOrd=" + uid,con);
                con.Open();

                OleDbDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows == true)
                {
                    while (dr.Read())
                    {
                        if (dr["CodPie"].ToString().ToUpper().Contains('A'))
                        {
                            if (dr["NumFas"].ToString() == "6") //step 6 column according uid status software-------------------
                            {
                                //---------Recieved date according to uid status software's green table

                                recvToHT = Convert.ToDateTime(dr["FecAlbExt"]).ToString("dd-MMM-yyyy") + "<br/>(After " + (Convert.ToDateTime(dr["FecAlbExt"]) - Convert.ToDateTime(CustomerPoDateValue)).Duration().Days.ToString() + " Days)";
                                break;
                            }
                            else
                            {
                                recvToHT = "N/A";    //------if step 6 is not existing than show blank---------
                            }
                        }
                        else
                        {
                            recvToHT = "N/A"; 
                        }
                    }
                }
                else
                {
                    recvToHT = "N/A"; 
                }
                con.Close();
            }
            catch(Exception ex)
            {
                Response.Write("receiveToHeatTreatment for uid    " + uid + " <br/>" + ex.Message +"<br/><br/>");
            }

            return recvToHT;
        }

        public String sendToHeatTreatment(String uid)
        {
            String sendToHT = "";
            try
            {
                OleDbConnection conn = new OleDbConnection(connectionString);
                OleDbCommand cmd = new OleDbCommand("SELECT [Pedidos a proveedor (líneas)].CodPie,[Pedidos a proveedor (líneas)].NumFas,[Pedidos a proveedor (cabeceras)].FecPed FROM (( [Pedidos a proveedor (líneas)] INNER JOIN [Pedidos a proveedor (cabeceras)]   ON  [Pedidos a proveedor (líneas)].NumPed = [Pedidos a proveedor (cabeceras)].NumPed) INNER JOIN [Proveedores] ON [Pedidos a proveedor (cabeceras)].ProPed = [Proveedores].CodPro ) WHERE [Pedidos a proveedor (líneas)].NumOrd =" + uid, conn);
                conn.Open();

                OleDbDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows == true)
                {
                    while (dr.Read())
                    {
                        if (dr["CodPie"].ToString().ToUpper().Contains('A'))
                        {
                            if (dr["NumFas"].ToString() == "6")
                            {
                                if (dr["FecPed"].ToString() != "")
                                {
                                    sendToHT = Convert.ToDateTime(dr["FecPed"]).ToString("dd-MMM-yyyy") + "<br/>(" + (Convert.ToDateTime(DateTime.Today) - Convert.ToDateTime(dr["FecPed"])).Duration().Days.ToString() + " Days Ago)";
                                }
                                else
                                {
                                    sendToHT = "N/A";
                                }
                                break;
                            }
                            else
                            {
                                sendToHT = "N/A";
                            }
                        }
                        else
                        {
                            sendToHT = "N/A";
                        }

                    }
                }
                else
                {
                    sendToHT = "N/A";
                }
               
                conn.Close();
            }
            catch (Exception ex)
            {
                Response.Write("sendToHeatTreatment for uid    " + uid + "<br/>" + ex.Message +"<br/><br/>");
            }
            return sendToHT;
        }

        public String getBCasing(string uid)
        {
            string bCasing = "";
            try
            {
                OleDbConnection connect = new OleDbConnection(connectionString);
                OleDbCommand command = new OleDbCommand("SELECT [Ordenes de fabricación].NumOrd, [Artículos de clientes (piezas)].CodPie FROM ([Artículos de clientes] INNER JOIN [Ordenes de fabricación] ON [Artículos de clientes].[CodArt] = [Ordenes de fabricación].[ArtOrd]) INNER JOIN [Artículos de clientes (piezas)] ON [Artículos de clientes].[CodArt] = [Artículos de clientes (piezas)].[CodArt] WHERE [Ordenes de fabricación].NumOrd=" + uid, connect);
                connect.Open();
                OleDbDataReader dr = command.ExecuteReader();

                if (dr.HasRows == true)
                {
                    while (dr.Read())
                    {
                        if (dr["CodPie"].ToString().Contains('B') == true)            //-----------'A' is stand for 'Steel'------------
                        {
                            bCasing = bCasing + " (" + dr["CodPie"].ToString() + ")";
                            break;
                        }
                        else
                        {
                            bCasing = "Steel Only";
                        }
                    }
                }
                else
                {
                    bCasing = "N/A";
                }
                connect.Close();
            }
            catch (Exception ex)
            {
                Response.Write("getBCasing for uid "+uid+"<br/>" + ex.Message + "<br/><br/>");
            }
            return bCasing;
        }

        public String getACasing(string uid)
        {
            string aCasing = "";
            try
            {
                OleDbConnection connect = new OleDbConnection(connectionString);
                OleDbCommand command = new OleDbCommand("SELECT [Ordenes de fabricación].NumOrd, [Artículos de clientes (piezas)].CodPie FROM ([Artículos de clientes] INNER JOIN [Ordenes de fabricación] ON [Artículos de clientes].[CodArt] = [Ordenes de fabricación].[ArtOrd]) INNER JOIN [Artículos de clientes (piezas)] ON [Artículos de clientes].[CodArt] = [Artículos de clientes (piezas)].[CodArt] WHERE [Ordenes de fabricación].NumOrd= " + uid, connect);
                connect.Open();
                OleDbDataReader dr = command.ExecuteReader();

                if (dr.HasRows == true)
                {
                    while (dr.Read())
                    {
                        if (dr["CodPie"].ToString().Contains('A') == true)            //-----------'A' is stand for 'Steel'------------
                        {
                            aCasing = aCasing + " (" + dr["CodPie"].ToString() + ")";
                            break;
                        }
                    }
                }
                else
                {
                    aCasing = "N/A";
                }
                connect.Close();
            }
            catch (Exception ex)
            {
                Response.Write("getACasing for uid " +uid+"<br/>"+ ex.Message + "<br/><br/>");
            }
            return aCasing;
        }

        public String CustomerPoDate(String UID)
        {
            String custPoDate = "";

            try
            {
                OleDbConnection conn = new OleDbConnection(connectionString);
                OleDbCommand comm = new OleDbCommand("SELECT [Pedidos de clientes].PedPed From [Pedidos de clientes] INNER JOIN [Ordenes de fabricación] On [Ordenes de fabricación].PinOrd=[Pedidos de clientes].NumPed Where [Ordenes de fabricación].NumOrd=" + UID, conn);
                conn.Open();
                OleDbDataReader dr = comm.ExecuteReader();

                if (dr.HasRows == true)
                {
                    while (dr.Read())
                    {
                        int pedped = dr["PedPed"].ToString().LastIndexOf(' ');
                        custPoDate = dr["PedPed"].ToString().Substring(pedped + 1);
                    }
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                Response.Write("CustomerPoDate for uid    " + UID + "<br/>" + ex.Message +"<br/><br/>");
            }
            return custPoDate;
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    LinkButton stautsLinkBtn = (LinkButton)e.Row.FindControl("stautsLinkBtn");
                    Label custPoDateLbl = (Label)e.Row.FindControl("custPoDateLbl");
                    Label ptDateLbl = (Label)e.Row.FindControl("ptDateLbl");
                    Label ocStatusLbl = (Label)e.Row.FindControl("ocStatusLbl");
                    LinkButton uidLinkBtn = (LinkButton)e.Row.FindControl("uidLinkBtn");
                    Label sendToHTLbl = (Label)e.Row.FindControl("sendToHTLbl");
                    Label recvToHTLbl = (Label)e.Row.FindControl("recvToHTLbl");
                    Label carbideInqLbl = (Label)e.Row.FindControl("carbideInqLbl");
                    Label carbideRecvLbl = (Label)e.Row.FindControl("carbideRecvLbl");
                    Label jobCardStatusLbl = (Label)e.Row.FindControl("jobCardStatusLbl");
                    Label externalGrindingLbl = (Label)e.Row.FindControl("externalGrindingLbl");
                    Label DxfReadyLbl = (Label)e.Row.FindControl("DxfReadyLbl");
                    Label WirecutLbl = (Label)e.Row.FindControl("WirecutLbl");
                    Label EDMLbl = (Label)e.Row.FindControl("EDMLbl");
                    Label SendToCoatingLbl = (Label)e.Row.FindControl("SendToCoatingLbl");
                    Label RecieveToCoatingLbl = (Label)e.Row.FindControl("RecieveToCoatingLbl");
                    Label DeliveryDateLbl = (Label)e.Row.FindControl("DeliveryDateLbl");
                    LinkButton LinkButton1 = (LinkButton)e.Row.FindControl("LinkButton1");
                    LinkButton noteLinkNBtn = (LinkButton)e.Row.FindControl("noteLinkNBtn");

                    rowcount = 0;
                    
                        //if (e.Row.RowType == DataControlRowType.DataRow)
                        //{
                        //    if (e.Row.RowIndex == 0)
                        //        e.Row.Style.Add("height", "50px");
                        //}

                    if (stautsLinkBtn.Text.Length == 6)
                    {
                        stautsLinkBtn.Font.Bold = true;
                        stautsLinkBtn.ForeColor = Color.Black;

                        for (int i = 0; i < 21; i++)
                        {
                            e.Row.Cells[i].BackColor = Color.BurlyWood;
                            e.Row.Cells[i].Font.Size = 14;
                            e.Row.Cells[i].BorderStyle = BorderStyle.None;

                            if (i == 19)
                                e.Row.Cells[i].Text = "";
                            if (i == 20)
                                e.Row.Cells[i].Text = "";
                        }
                    }
                    else
                    {
                        if (ocStatusLbl.Text == "Pending")
                        {
                            e.Row.Cells[3].BackColor = Color.Red;
                            e.Row.Cells[3].ForeColor = Color.White;
                        }

                        if (sendToHTLbl.Text.Contains("Days"))
                        {
                            string[] data = sendToHTLbl.Text.Split('<', '>', ' ');
                            string day = data[2].Replace("(", "").Trim();
                            if (Convert.ToInt32(day) > 5)
                            {
                                e.Row.Cells[6].BackColor = Color.Red;
                                e.Row.Cells[6].ForeColor = Color.White;
                            }
                        }

                        if (recvToHTLbl.Text.Contains("Pending"))
                        {
                            e.Row.Cells[7].BackColor = Color.Red;
                            e.Row.Cells[7].ForeColor = Color.White;
                        }

                        if (carbideInqLbl.Text.Contains("Days"))
                        {
                            string[] data = carbideInqLbl.Text.Split('<', '>', ' ');
                            string day = data[2].Replace("(", "").Trim();
                            if (Convert.ToInt32(day) > 10)
                            {
                                e.Row.Cells[8].BackColor = Color.Red;
                                e.Row.Cells[8].ForeColor = Color.White;
                            }
                        }

                        if (carbideRecvLbl.Text.Contains("Pending"))
                        {
                            e.Row.Cells[9].BackColor = Color.Red;
                            e.Row.Cells[9].ForeColor = Color.White;
                        }

                        if (jobCardStatusLbl.Text.Contains("Pending"))
                        {
                            e.Row.Cells[10].BackColor = Color.Red;
                            e.Row.Cells[10].ForeColor = Color.White;
                        }

                        if (DxfReadyLbl.Text.Contains("Pending"))
                        {
                            e.Row.Cells[12].BackColor = Color.Red;
                            e.Row.Cells[12].ForeColor = Color.White;
                        }

                        if (SendToCoatingLbl.Text.Contains("Days"))
                        {
                            string[] data = SendToCoatingLbl.Text.Split('<', '>', ' ');
                            string day = data[2].Replace("(", "").Trim();
                            if (Convert.ToInt32(day) > 5)
                            {
                                e.Row.Cells[15].BackColor = Color.Red;
                                e.Row.Cells[15].ForeColor = Color.White;
                            }
                        }

                        if (RecieveToCoatingLbl.Text.Contains("Pending"))
                        {
                            e.Row.Cells[16].BackColor = Color.Red;
                            e.Row.Cells[16].ForeColor = Color.White;
                        }

                        if (WirecutLbl.Text.Contains("Pending"))
                        {
                            e.Row.Cells[13].BackColor = Color.Red;
                            e.Row.Cells[13].ForeColor = Color.White;
                        }

                        if (SendToCoatingLbl.Text.Contains("Pending"))
                        {
                            e.Row.Cells[15].BackColor = Color.Red;
                            e.Row.Cells[15].ForeColor = Color.White;
                        }

                        if (dispatchCheckBox.Checked == true)
                        {
                            e.Row.Cells[19].Visible = false;
                            this.GridView1.Columns[19].Visible = false;
                            e.Row.Cells[20].Visible = false;
                            this.GridView1.Columns[20].Visible = false;
                        }
                        else
                        {
                            e.Row.Cells[19].Visible = true;
                            this.GridView1.Columns[19].Visible = true;
                            e.Row.Cells[20].Visible = true;
                            this.GridView1.Columns[20].Visible = true;
                        }


                        string file = Server.MapPath("~/.starredUid/uid.txt");
                        if (File.Exists(file))
                        {
                            FileInfo info = new FileInfo(file);
                            if (info.Length > 0)
                            {
                                string[] getUid = stautsLinkBtn.Text.Split('<');
                                if (getUid[0] != "")
                                {
                                    string[] starreduid = File.ReadAllLines(file);

                                    foreach (string uidvalue in starreduid)
                                    {
                                        string[] uids = uidvalue.Split(' ');
                                        if (uids[0] == getUid[0])
                                        {
                                            var color = LinkButton1.Style.Value;
                                            if (color.ToString().Contains("color:#ffffff;"))
                                            {
                                                LinkButton1.Style["color"] = "yellow";
                                            }
                                        }
                                    }
                                }
                            }
                            if (TotalRowLbl.Text == "")
                            {
                                TotalRowLbl.Text = "Total Rows: " + 1;
                            }
                            else
                            {
                                rowcount = Convert.ToInt32((TotalRowLbl.Text.Split(' ')).GetValue(2));
                                TotalRowLbl.Text = "Total Rows: "+(rowcount + 1).ToString();
                            }

                            if ((rowcount + 1) <= 7)
                            {
                                GridView1.Style.Add("width", "98.7%");
                            }
                            else
                            {
                                GridView1.Style.Add("width", "100%");
                            }

                            callOfflbl.Text = "(-@ CALL OFF)";
                        }


                        OleDbConnection con = new OleDbConnection(connectionString);

                        OleDbCommand cmd = new OleDbCommand("Select Note From [Ordenes de fabricación] WHERE [NumOrd]=" + stautsLinkBtn.Text.Split('<').GetValue(0).ToString(), con);

                        con.Open();

                        OleDbDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows == true)
                        {
                            while (dr.Read())
                            {
                                if (dr["Note"].ToString() != "")
                                {
                                    noteLinkNBtn.Style["color"] = "#5BC236";
                                }
                                else
                                {
                                    noteLinkNBtn.Style["color"] = "white";                              
                                }

                            }
                        }

                        con.Close();

                    }

                    

                }
            }
            catch (Exception ex)
            {
                Response.Write("Gridview1 bound error:-   <br/>"+ ex.Message);
            }
        }

        protected void Drawingstauts_Command(Object sender, CommandEventArgs e)
        {
            
            String RefStillCutData = e.CommandArgument.ToString();

            String[] refStillCutValue = RefStillCutData.Split('<');

            string pdfpath = "W:\\Technical Section\\Casing\\" + refStillCutValue[0].Trim() + ".pdf";

            if (RefStillCutData.Contains("N/A")==false)
            {
                if (File.Exists(pdfpath))
                {
                    string strPopup = "<script language='javascript' ID='script1'>"

                         // Passing intId to popup window.
                         + "window.open('RefSteelCut.aspx?RefStillCut=" + refStillCutValue[0] + "','new window', 'top=70, left=250, width=470, height=590, dependant=no, location=0, alwaysRaised=no, menubar=no, resizeable=no, scrollbars=n, toolbar=no, status=no, center=yes')"

                         + "</script>";

                    ScriptManager.RegisterStartupScript((Page)HttpContext.Current.Handler, typeof(Page), "Script1", strPopup, false);
                }
                else
                {
                    Response.Write("<script>alert('Casing Drawing Not Available!!!');</script>");
                }
            }

            foreach (GridViewRow r in GridView1.Rows)
            {
                int rowcounter = 0;
                string stautsLinkBtn = ((LinkButton)r.FindControl("stautsLinkBtn")).Text;
                if (stautsLinkBtn.Length == 6)
                {
                    r.Cells[19].Text = "";
                    r.Cells[20].Text = "";
                }
                else
                {
                    rowcounter = rowcount++;
                }
                TotalRowLbl.Text = "Total Row: " + (rowcounter + 1).ToString();

            }
        }

        protected void MarkAsStar_Command(Object sender, CommandEventArgs e)
        {
            string[] uid = e.CommandArgument.ToString().Split('<');
            LinkButton btn = (LinkButton)sender;
            var color = btn.Style.Value;
            if (color.ToString().Contains("color:#ffffff;"))
            {
                btn.Style["color"] = "yellow";
            }

            string filepath = Server.MapPath("~/.starredUid/uid.txt");
            Boolean isUidExist = false;

            if (File.Exists(filepath)==false)
            {
                var file=File.Create(filepath);
                file.Close();
            }


            if (File.Exists(filepath))
            {
                FileInfo info = new FileInfo(filepath);
                if (info.Length > 0)
                {
                    if (uid[0] != "")
                    {
                        string[] starreduid = File.ReadAllLines(filepath);

                        foreach (string uidvalue in starreduid)
                        {
                            string[] uids = uidvalue.Split(' ');
                            if (uids[0] == uid[0])
                            {
                                isUidExist = true;                                
                            }
                        }

                        if (isUidExist == false)
                        {
                            File.AppendAllText(@filepath, uid[0]+ " "+Location+"\n");
                            var hideFile = new DirectoryInfo(filepath);
                            hideFile.Attributes = FileAttributes.Hidden;
                            //Response.Write("<script>alert('" + uid[0] + " marked as star')</script>");
                        }
                    }
                }
                else
                {
                    File.AppendAllText(@filepath, uid[0] + " " + Location + "\n");
                    var hideFile = new DirectoryInfo(filepath);
                    hideFile.Attributes = FileAttributes.Hidden;
                    //Response.Write("<script>alert('" + uid[0] + " marked as star')</script>");
                }
            }

            foreach (GridViewRow r in GridView1.Rows)
            {
                int rowcounter = 0;
                string stautsLinkBtn = ((LinkButton)r.FindControl("stautsLinkBtn")).Text;
                LinkButton notelinkbtn = ((LinkButton)r.FindControl("noteLinkNBtn"));
                if (stautsLinkBtn.Length==6)
                {
                    r.Cells[19].Text = "";
                    r.Cells[20].Text = "";
                    rowcounter = rowcount++;
                }
                TotalRowLbl.Text = "Total Row: " + (rowcounter + 1).ToString();
            }
        }


        protected void dxfFileStatus_Command(Object sender, CommandEventArgs e)
        {
            String dxfStatus = e.CommandArgument.ToString();

            if (dxfStatus != " " || dxfStatus != "")
            {
                LinkButton btn = (LinkButton)sender;

                //Get the row that contains this button
                GridViewRow gvr = (GridViewRow)btn.NamingContainer;
                string data = "";
                //Get rowindex
                int rowindex = gvr.RowIndex;

                if (Session["articleList"] != null)
                {
                    var list = Session["articleList"] as List<string>;
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (i == rowindex)
                        {
                            data = list[i].ToString();
                        }
                    }
                }

                if (data != "")
                {
                    string[] splitingdata = data.Split(' ');
                    string artnum = splitingdata[0];

                    string folderName = artnum.Substring(0, 6);

                    String[] dxfFiles = Directory.GetFiles("W:\\test\\Access\\Planos\\" + folderName + "\\", "*.dxf");

                    foreach (String filePath in dxfFiles)
                    {
                        if (filePath.Contains(artnum))
                        {
                            //FileInfo fileData = new FileInfo(filePath);
                            //string filename = filePath;
                            string strPopup = "<script language='javascript' ID='script1'>"

                                 // Passing intId to popup window.
                                 + "window.open('dxgfileStatus.aspx?dxgfileStatus=" + artnum + "','new window', 'top=70, left=250, width=470, height=590, dependant=no, location=0, alwaysRaised=no, menubar=no, resizeable=no, scrollbars=n, toolbar=no, status=no, center=yes')"

                                 + "</script>";

                            ScriptManager.RegisterStartupScript((Page)HttpContext.Current.Handler, typeof(Page), "Script1", strPopup, false);
                        }
                        else
                        {
                            // Response.Write("<script>alert('Casing Drawing Not Available!!!');</script>");
                        }
                    }
                }
            }


        }

        protected void ArticleShow_Command(Object sender, CommandEventArgs e)
        {
            String UIDData = e.CommandArgument.ToString();

            String[] test = UIDData.Split('<','(');

            String uid = test[0];

            Response.Write("<script>window.open('http://10.0.0.5:8989/uidstatus/MyPage.aspx?workFlowUid=" + uid + "','_blank');</script>");

            //string[] articleNum = test[1].Split(')');

            //string pdfpath = "W:\\test\\Access\\Planos\\" + articleNum[0].Substring(0, 6).Trim() + "\\" + articleNum[0].Trim() + ".PC.pdf";
            //if (File.Exists(pdfpath))
            //{
            //    string strPopup = "<script language='javascript' ID='script1'>"

            //         // Passing intId to popup window.
            //         + "window.open('articleViewer.aspx?article=" + articleNum[0] + "','new window', 'top=70, left=250, width=470, height=590, dependant=no, location=0, alwaysRaised=no, menubar=no, resizeable=no, scrollbars=n, toolbar=no, status=no, center=yes')"

            //         + "</script>";

            //    ScriptManager.RegisterStartupScript((Page)HttpContext.Current.Handler, typeof(Page), "Script1", strPopup, false);
            //}
            //else
            //{
            //    Response.Write("<script>alert('Article Not Available!!!');</script>");
            //}

            foreach (GridViewRow r in GridView1.Rows)
            {
                int rowcounter = 0;
                string stautsLinkBtn = ((LinkButton)r.FindControl("stautsLinkBtn")).Text;
                if (stautsLinkBtn.Length == 6)
                {
                    r.Cells[19].Text = "";
                    r.Cells[20].Text = "";
                }
                else
                {
                    rowcounter = rowcount++;
                }
                TotalRowLbl.Text = "Total Row: " + (rowcounter + 1).ToString();

            }
        }

        [WebMethod]
        public static List<ListItem> GetPtNum(string prefix)
        {
            List<ListItem> PtNum = new List<ListItem>();
            string str = "Provider=Microsoft.ACE.OLEDB.12.0;DATA SOURCE=W:\\test\\Access\\Tablas.mdb;Persist Security Info = False; ";
            OleDbConnection con = new OleDbConnection(str);
            OleDbCommand cmd = new OleDbCommand("SELECT distinct top 10 PinOrd FROM [Ordenes de fabricación] WHERE PinOrd Like + @PtNum + '%' Order By PinOrd Desc", con);
            cmd.Parameters.AddWithValue("@PtNum", prefix);
            con.Open();
            OleDbDataReader odr = cmd.ExecuteReader();
            while (odr.Read())
            {
                PtNum.Add(new ListItem { Text = odr["PinOrd"].ToString().Trim() });
            }
            con.Close();

            return PtNum;
        }


        //protected void mainFilterDropDownChanged(object sender, EventArgs e)
        //{
        protected void redcolorCheckbox_changed(object sender, EventArgs e)
        {
            DataTable dtfilter = null;

            if (Session["dataTable"] != null)
            {
                dtfilter = (Session["dataTable"] as DataTable).Clone();                
            }

            string stautsLinkBtn = "";
            string custPoDateLbl = "";
            string ptDateLbl = "";
            string ocStatusLbl = "";
            string RemarksLbl = "";
            string uidLinkBtn = "";
            string sendToHTLbl = "";
            string recvToHTLbl = "";
            string carbideInqLbl = "";
            string carbideRecvLbl = "";
            string jobCardStatusLbl = "";
            string externalGrindingLbl = "";
            string DxfReadyLbl = "";
            string WirecutLbl = "";
            string EDMLbl = "";
            string SendToCoatingLbl = "";
            string RecieveToCoatingLbl = "";
            string DeliveryDateLbl = "";
            string ObservationLbl = "";

            if (redcolorCheckbox.Checked == true)
            {
                foreach (GridViewRow row in GridView1.Rows)
                {
                    for (int i = 0; i < row.Cells.Count; i++)
                    {
                         stautsLinkBtn = ((LinkButton)row.FindControl("stautsLinkBtn")).Text;
                         custPoDateLbl = ((Label)row.FindControl("custPoDateLbl")).Text;
                         ptDateLbl = ((Label)row.FindControl("ptDateLbl")).Text;
                         ocStatusLbl = ((Label)row.FindControl("ocStatusLbl")).Text;
                         RemarksLbl = ((Label)row.FindControl("RemarksLbl")).Text;
                         uidLinkBtn = ((LinkButton)row.FindControl("uidLinkBtn")).Text;
                         sendToHTLbl = ((Label)row.FindControl("sendToHTLbl")).Text;
                         recvToHTLbl = ((Label)row.FindControl("recvToHTLbl")).Text;
                         carbideInqLbl = ((Label)row.FindControl("carbideInqLbl")).Text;
                         carbideRecvLbl = ((Label)row.FindControl("carbideRecvLbl")).Text;
                         jobCardStatusLbl = ((Label)row.FindControl("jobCardStatusLbl")).Text;
                         externalGrindingLbl = ((Label)row.FindControl("externalGrindingLbl")).Text;
                         DxfReadyLbl = ((Label)row.FindControl("DxfReadyLbl")).Text;
                         WirecutLbl = ((Label)row.FindControl("WirecutLbl")).Text;
                         EDMLbl = ((Label)row.FindControl("EDMLbl")).Text;
                         SendToCoatingLbl = ((Label)row.FindControl("SendToCoatingLbl")).Text;
                         RecieveToCoatingLbl = ((Label)row.FindControl("RecieveToCoatingLbl")).Text;
                         DeliveryDateLbl = ((Label)row.FindControl("Label2")).Text;
                         ObservationLbl = ((Label)row.FindControl("ObservationLbl")).Text;
                        // LinkButton1 = ((LinkButton)row.FindControl("LinkButton1")).Text;

                        if (row.Cells[i].BackColor == Color.Red)
                        {
                            
                            dtfilter.Rows.Add(stautsLinkBtn, custPoDateLbl, ptDateLbl, ocStatusLbl, RemarksLbl, uidLinkBtn, sendToHTLbl, recvToHTLbl, carbideInqLbl, carbideRecvLbl, jobCardStatusLbl, externalGrindingLbl, DxfReadyLbl, WirecutLbl, EDMLbl, SendToCoatingLbl, RecieveToCoatingLbl, DeliveryDateLbl, ObservationLbl);
                            break;
                        }
                    }

                }

                DataTable mainfilterboundtable = dtfilter.Clone();

                List<string> filterlist = new List<string>();
                foreach (DataRow r in dtfilter.Rows)
                {
                    string dataexist = "";
                    string getData = r[0].ToString();
                    string[] multidata = getData.Split('<', '(', ')');
                    string articleNum = multidata[2];
                    string custcode = articleNum.Substring(0, 6);
                    int count = 1;
                    foreach (DataRow drow in dtfilter.Rows)
                    {
                        if (filterlist.Count > 0)
                        {
                            for (int i = 0; i < filterlist.Count; i++)
                            {
                                if (filterlist[i] == custcode)
                                {
                                    dataexist = "true";
                                }
                            }
                        }


                        if(dataexist!="true")
                        {
                            if (drow[0].ToString().Contains(custcode))
                            {
                                if (count == 1)
                                    mainfilterboundtable.Rows.Add(custcode, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");

                                mainfilterboundtable.Rows.Add(drow.ItemArray);
                                count = 2;
                            }
                        }
                    }
                    filterlist.Add(custcode);
                }

                GridView1.DataSource = mainfilterboundtable;
                GridView1.DataBind();
            }
        }

        protected void JobCardStatusChanged(object sender, EventArgs e)
        {
            DropDownList JobCardStatus = (DropDownList)sender;

            filter(JobCardStatus, 10);
        }

        protected void CarbideInqDateChanged(object sender, EventArgs e)
        {
            DropDownList CarbideList = (DropDownList)sender;

            filter(CarbideList, 8);
        }

        protected void SendToCoatingChanged(object sender, EventArgs e)
        {
            DropDownList sendToCoatingList = (DropDownList)sender;

            filter(sendToCoatingList, 15);
        }

        protected void Cust_PODateChanged(object sender, EventArgs e)
        {
            DropDownList Cust_PODateList = (DropDownList)sender;

            filter(Cust_PODateList, 1);
        }

        protected void PtDateListChanged(object sender, EventArgs e)
        {
            DropDownList PtDateListvalues = (DropDownList)sender;

            filter(PtDateListvalues, 2);
        }


        //protected void aCasinglistChanged(object sender, EventArgs e)
        //{
        //    DropDownList aCasinglistlistValue = (DropDownList)sender;
        //    filter(aCasinglistlistValue,4);
        //}


        protected void CasingDrawingStatusListChanged(object sender, EventArgs e)
        {
            DropDownList CasingDrawingStatusListChangedvalue = (DropDownList)sender;
            filter(CasingDrawingStatusListChangedvalue, 5);
        }

        protected void SendToHTListChanged(object sender, EventArgs e)
        {
            DropDownList SendToHTListChangedValue = (DropDownList)sender;
            filter(SendToHTListChangedValue, 6);
        }

        protected void ReceiveToHTChanged(object sender, EventArgs e)
        {
            DropDownList ReceiveToHTChangedValue = (DropDownList)sender;
            filter(ReceiveToHTChangedValue, 7);
        }

        //protected void BCarbidelistChanged(object sender, EventArgs e)
        //{
        //    DropDownList BCarbidelistChangedValue = (DropDownList)sender;
        //    filter(BCarbidelistChangedValue, 8);
        //}


        protected void CarbideRecvDateListChanged(object sender, EventArgs e)
        {
            DropDownList CarbideRecvDateListChangedValue = (DropDownList)sender;
            filter(CarbideRecvDateListChangedValue, 9);
        }


        protected void ExternalGrindingListChanged(object sender, EventArgs e)
        {
            DropDownList ExternalGrindingListChangedValue = (DropDownList)sender;
            filter(ExternalGrindingListChangedValue, 11);
        }

        protected void WirecutListChanged(object sender, EventArgs e)
        {
            DropDownList WirecutListChangedValue = (DropDownList)sender;
            filter(WirecutListChangedValue, 13);
        }

        protected void DxfReadyChanged(object sender, EventArgs e)
        {
            DropDownList DxfReadyChangedValue = (DropDownList)sender;
            filter(DxfReadyChangedValue, 12);
        }

        protected void EDMChanged(object sender, EventArgs e)
        {
            DropDownList EDMChangedValue = (DropDownList)sender;
            filter(EDMChangedValue, 14);
        }

        protected void RecieveToCoatingChanged(object sender, EventArgs e)
        {
            DropDownList RecieveToCoatingChangedValue = (DropDownList)sender;
            filter(RecieveToCoatingChangedValue, 16);
        }

        protected void DeliveryDateListChanged(object sender, EventArgs e)
        {
            DropDownList DeliveryDateListChangedValue = (DropDownList)sender;
            filter(DeliveryDateListChangedValue, 17);
        }

        protected void OcStatusListChanged(object sender, EventArgs e)
        {
            DropDownList OcStatusListChangedValue = (DropDownList)sender;
            filter(OcStatusListChangedValue, 3);
        }


        public void filter(DropDownList ddl, int GridViewColumnIndex)
        {
            DataTable copyTable = new DataTable();

            if (Session["dataTable"] != null)
            {
                DataTable dt = Session["dataTable"] as DataTable;


                foreach (DataColumn dtcolumn in dt.Columns)
                {
                    copyTable.Columns.Add(dtcolumn.ColumnName);
                }


                foreach (DataRow row in dt.Rows)
                {
                    string dd = row.ItemArray[GridViewColumnIndex].ToString();

                    if (ddl.SelectedValue == "Pending")
                    {
                        if (dd == " " || dd.Contains("Pending") || dd == "")
                        {
                            copyTable.Rows.Add(row.ItemArray);
                        }
                    }

                    if (ddl.SelectedValue == "Non Pending")
                    {
                        if (dd != "N/A" && dd != " " && dd != "Pending" && dd != "")
                        {
                            copyTable.Rows.Add(row.ItemArray);
                        }
                    }
                    if (ddl.SelectedValue == "N/A")
                    {
                        if (dd == "N/A")
                        {
                            copyTable.Rows.Add(row.ItemArray);
                        }
                    }
                    
                    if (ddl.SelectedValue == "Show All")
                    {
                        copyTable.Rows.Add(row.ItemArray);
                    }
                }


                DataTable filterboundtable = copyTable.Clone();

                List<string> filterboundlist = new List<string>();

                foreach (DataRow r in copyTable.Rows)
                {
                    string dataexist = "";
                    string getData = r[0].ToString();
                    string[] multidata = getData.Split('<', '(', ')');
                    string articleNum = multidata[2];
                    string custcode = articleNum.Substring(0, 6);
                    int count = 1;
                    foreach (DataRow drow in copyTable.Rows)
                    {
                        if (filterboundlist.Count > 0)
                        {
                            for (int i = 0; i < filterboundlist.Count; i++)
                            {
                                if (filterboundlist[i] == custcode)
                                {
                                    dataexist = "true";
                                }
                            }
                        }

                            if(dataexist!="true")
                            {
                                if (drow[0].ToString().Contains(custcode))
                                {
                                    if (count == 1)
                                        filterboundtable.Rows.Add(custcode, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");

                                    filterboundtable.Rows.Add(drow.ItemArray);
                                    count = 2;
                                }
                            }
                    }
                    filterboundlist.Add(custcode);
                }


                List<int> sortedlist = new List<int>();

                foreach (DataRow ro in filterboundtable.Rows)
                {
                    if (ro[0].ToString().Length == 6)
                    {
                        string custcode = ro[0].ToString();
                        sortedlist.Add(Convert.ToInt32(custcode));
                    }
                }

                sortedlist.Sort();



                DataTable sortedfilterboundtable = filterboundtable.Clone();



                for (int i = 0; i < sortedlist.Count; i++)
                {
                    foreach (DataRow r in filterboundtable.Rows)
                    {
                        if (r[0].ToString().Length == 6)
                        {
                            if (sortedlist[i].ToString() == r[0].ToString())
                            {
                                sortedfilterboundtable.Rows.Add(r[0].ToString(), "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
                                foreach (DataRow rows in filterboundtable.Rows)
                                {
                                    if (rows[0].ToString().Length != 6)
                                    {
                                        if (r[0].ToString() == (rows[0].ToString().Split('<', '(', ')').GetValue(2).ToString().Substring(0, 6)))
                                        {
                                            sortedfilterboundtable.Rows.Add(rows.ItemArray);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }



                GridView1.DataSource = sortedfilterboundtable;
                GridView1.DataBind();
            }
        }


        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                popupHeading.Text = "Adding Note For UID " + (GridView1.SelectedRow.FindControl("stautsLinkBtn") as LinkButton).Text.Split('<').GetValue(0).ToString();


                foreach (GridViewRow r in GridView1.Rows)
                {
                    int rowcounter = 0;
                    string stautsLinkBtn = ((LinkButton)r.FindControl("stautsLinkBtn")).Text;
                    if (stautsLinkBtn.Length == 6)
                    {
                        r.Cells[19].Text = "";
                        r.Cells[20].Text = "";
                    }
                    else
                    {
                        rowcounter = rowcount++;
                    }
                    TotalRowLbl.Text = "Total Row: " + (rowcounter + 1).ToString();
                }

                OleDbConnection con = new OleDbConnection(connectionString);

                OleDbCommand cmd = new OleDbCommand("Select Note From [Ordenes de fabricación] Where NumOrd = " + popupHeading.Text.Split(' ').GetValue(4).ToString(), con);

                con.Open();

                OleDbDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows == true)
                {
                    while (dr.Read())
                    {
                        if (dr["Note"].ToString() != "")
                        {
                            notetxtbox.Text = dr["Note"].ToString();
                            notetxtbox.Focus();
                        }
                        else
                        {
                            notetxtbox.Focus();
                        }
                    }
                }


                ModalPopupExtender1.Show();
            }
            catch (Exception ex)
            {
                Response.Write("Exception from Gridview_SelectedIndexChanged  <br/>"+ex.Message);
            }
        }

        protected void NoteSaveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                int rowcounter = 0;
                String uid = popupHeading.Text.Split(' ').GetValue(4).ToString();

                OleDbConnection con = new OleDbConnection(connectionString);

                OleDbCommand cmd = new OleDbCommand("UPDATE [Ordenes de fabricación] SET [Note] = '" + notetxtbox.Text.Trim() + "' WHERE [NumOrd] = " + uid, con);

                con.Open();

                int rowaffected = cmd.ExecuteNonQuery();

                if (rowaffected > 0)
                {
                    foreach (GridViewRow r in GridView1.Rows)
                    {
                        string stautsLinkBtn = ((LinkButton)r.FindControl("stautsLinkBtn")).Text;
                        LinkButton notelinkbtn = ((LinkButton)r.FindControl("noteLinkNBtn"));
                        if (stautsLinkBtn.Split('<').GetValue(0).ToString() == uid)
                        {
                            if (notetxtbox.Text.Trim() != "")
                            {
                                notelinkbtn.Style["color"] = "#5BC236";
                            }
                            else
                            {
                                notelinkbtn.Style["color"] = "white";
                            }
                            rowcounter = rowcount++;
                        }
                        else if (stautsLinkBtn.Length == 6)
                        {
                            r.Cells[19].Text = "";
                            r.Cells[20].Text = "";
                        }
                        else
                        {
                            rowcounter = rowcount++;
                        }
                        TotalRowLbl.Text = "Total Row: " + (rowcounter + 1).ToString();
                    }

                }
                else
                {
                    Response.Write("<script>alert('Error: Failed to save note...!');</script>");
                }
                con.Close();
            }
            catch(Exception ex)
            {
                Response.Write("Exception From NoteSaveBtn_Click Method: <br/> " + ex.Message);                
            }
        }

        protected void ExportAllData(object sender, EventArgs e)
        {
            DataTable data = new DataTable();

            data.Columns.Add("UID");
            data.Columns.Add("Cust_PO Date");
            data.Columns.Add("PT Date");
            data.Columns.Add("OC Status");
            data.Columns.Add("Remarks");
            //data.Columns.Add("A* Casing");
            data.Columns.Add("Casing Drawing Status");
            data.Columns.Add("Send To HT");
            data.Columns.Add("Receive To HT");
            //data.Columns.Add("B* Carbide");
            data.Columns.Add("Carbide Inq Date");
            data.Columns.Add("Carbide Recv Date");
            data.Columns.Add("Job Card Status");
            data.Columns.Add("External Grinding");
            data.Columns.Add("Dxf Ready");
            data.Columns.Add("Wirecut");
            data.Columns.Add("EDM");
            data.Columns.Add("Send To Coating");
            data.Columns.Add("Recieve To Coating");
            data.Columns.Add("End Date/Delivery Date");
            data.Columns.Add("Observation");

            string stautsLinkBtn = "";
            string custPoDateLbl = "";
            string ptDateLbl = "";
            string ocStatusLbl = "";
            string RemarksLbl = "";
            string uidLinkBtn = "";
            string sendToHTLbl = "";
            string recvToHTLbl = "";
            string carbideInqLbl = "";
            string carbideRecvLbl = "";
            string jobCardStatusLbl = "";
            string externalGrindingLbl = "";
            string DxfReadyLbl = "";
            string WirecutLbl = "";
            string EDMLbl = "";
            string SendToCoatingLbl = "";
            string RecieveToCoatingLbl = "";
            string DeliveryDateLbl = "";
            string ObservationLbl = "";           

            foreach (GridViewRow row in GridView1.Rows)
            {
                stautsLinkBtn = ((LinkButton)row.FindControl("stautsLinkBtn")).Text;
                custPoDateLbl = ((Label)row.FindControl("custPoDateLbl")).Text;
                ptDateLbl = ((Label)row.FindControl("ptDateLbl")).Text;
                ocStatusLbl = ((Label)row.FindControl("ocStatusLbl")).Text;
                RemarksLbl = ((Label)row.FindControl("RemarksLbl")).Text;
                uidLinkBtn = ((LinkButton)row.FindControl("uidLinkBtn")).Text;
                sendToHTLbl = ((Label)row.FindControl("sendToHTLbl")).Text;
                recvToHTLbl = ((Label)row.FindControl("recvToHTLbl")).Text;
                carbideInqLbl = ((Label)row.FindControl("carbideInqLbl")).Text;
                carbideRecvLbl = ((Label)row.FindControl("carbideRecvLbl")).Text;
                jobCardStatusLbl = ((Label)row.FindControl("jobCardStatusLbl")).Text;
                externalGrindingLbl = ((Label)row.FindControl("externalGrindingLbl")).Text;
                DxfReadyLbl = ((Label)row.FindControl("DxfReadyLbl")).Text;
                WirecutLbl = ((Label)row.FindControl("WirecutLbl")).Text;
                EDMLbl = ((Label)row.FindControl("EDMLbl")).Text;
                SendToCoatingLbl = ((Label)row.FindControl("SendToCoatingLbl")).Text;
                RecieveToCoatingLbl = ((Label)row.FindControl("RecieveToCoatingLbl")).Text;
                DeliveryDateLbl = ((Label)row.FindControl("Label2")).Text;
                ObservationLbl = ((Label)row.FindControl("ObservationLbl")).Text;

                if(stautsLinkBtn.Length!=6)
                {
                    string uid = stautsLinkBtn.Replace("<br/>", " ");
                    string custPoDate = custPoDateLbl.Replace("<br/>", " ");
                    string ptDate = ptDateLbl.Replace("<br/>"," ");
                    string ocStatus = ocStatusLbl.Replace("<br/>", " ");
                    string Remarks = RemarksLbl.Replace("<br/>", " ");
                    string uidLink = uidLinkBtn.Replace("<br/>", " ");
                    string sendToHT = sendToHTLbl.Replace("<br/>", " ");
                    string recvToHT = recvToHTLbl.Replace("<br/>", " ");
                    string carbideInq = carbideInqLbl.Replace("<br/>", " ");
                    string carbideRecv = carbideRecvLbl.Replace("<br/>", " ");
                    string jobCardStatus = jobCardStatusLbl.Replace("<br/>", " ");
                    string externalGrinding = externalGrindingLbl.Replace("<br/>", " ");
                    string DxfReady = DxfReadyLbl.Replace("<br/>", " ");
                    string Wirecut = WirecutLbl.Replace("<br/>", " ");
                    string EDM = EDMLbl.Replace("<br/>", " ");
                    string SendToCoating = SendToCoatingLbl.Replace("<br/>", " ");
                    string RecieveToCoating = RecieveToCoatingLbl.Replace("<br/>", " ");
                    string DeliveryDate = DeliveryDateLbl.Replace("<br/>", " ");
                    string Observation = ObservationLbl.Replace("<br/>", " ");


                    data.Rows.Add(uid, custPoDate, ptDate, ocStatus, Remarks, uidLink, sendToHT, recvToHT, carbideInq, carbideRecv, jobCardStatus, externalGrinding, DxfReady, Wirecut, EDM, SendToCoating, RecieveToCoating, DeliveryDate, Observation);
                }
            }

            Session["exportTable"] = data;
            Response.Redirect("ExcelExport.aspx");


            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string FileName = "WorkFlow Data (" + DateTime.Now + ").xls";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
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


            //GridView1.RenderControl(htmltextwrtter);  
            Response.Write(strwritter.ToString());
            Response.End();    
        }
    }
}