<%@ Page Language="C#" AutoEventWireup="true"  MaintainScrollPositionOnPostback="true" CodeBehind="Default.aspx.cs" Inherits="PtNumSearch.Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Workflow</title>

    <link rel="icon" href="images/ANU LOGO.jpg" type="image/jpg"/>

    
    <!--To Create ToolTip-->

    <link type="text/css"href="https://code.jquery.com/ui/1.10.4/themes/ui-lightness/jquery-ui.css" rel="stylesheet"/>  
    <script type="text/javascript" src="https://code.jquery.com/jquery-1.10.2.js"></script>  
    <script type="text/javascript" src="https://code.jquery.com/ui/1.10.4/jquery-ui.js"></script>  
    
    
    <script type="text/javascript" src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js"></script>
    <script type="text/javascript" src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js"></script>
    <link rel="Stylesheet" type="text/css" href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css" />

    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.14.0/js/all.min.js"></script>

    <style>
        *{
            margin:0px;
            padding:0px;
            font-family:Century Schoolbook;
         }
        .container 
        {
            width:98%;
            margin:auto;
        }
        
        .form-area{
            margin:auto;
        }
        h1{
            margin-top:25px;
            text-align:center;
            font-size:32px;
        }
        #form1{
            margin-top:50px;
        }
        hr{
            width:30%;
            height:2px;
            background:black;
            border:none;
            margin-top:0px;
            margin:auto;

        }
        .lbl{
            font-weight:bold;
            font-size:16px;
            margin-right:10px;
        }
        .txtbox{
            width:25%;
            height:30px;
            border:2px solid black;
            border-radius:5px;
            text-align:center;            
            margin-right:10px;
            font-size:16px;
            font-weight:bold;
        }
        .grid-item 
        {
            width:95%;
        }
        .btn{
            height:35px;
            width:60px;
            background:#0069d9;
            color:white;
            border-radius:5px;
            font-size:14px;
            border:2px solid black;
        }
        
        #mydivclass
        {          
            scrollbar-width: thin;
            width:100%;   
            overflow-y: scroll;
        }
        
        
        .gridviewArea
        {
            margin-top:25px;    
        }

        
        #GridView1
        {  
            text-align:center;
        }
        .row
        {
            width:100%; 
        }
        .LinkStyle
        {
            text-decoration:none;
            display:block;
        }
        .logo img
        {
            width:40px;  
            line-height:80px;
            margin:5px;
        }
        .navbar
        {
            background-color:#3390FF;
            width:100%;
            padding:0px;
            margin:0px;   
            height:48px;  
            box-shadow:2px 2px 4px;       
        }
        .logo
        {
            float:left;    
        }
        .softwareName
        {
            line-height:50px;
            color:White;
        }
        .backBtn a
        {
            float:right;
            color:Black;    
            z-index:10;
            background:#FFC107;
            width:70px;
            padding:5px;
            text-align:center;
            border-radius:5px;
            text-decoration:none;
            margin-top:-40px;
            margin-right:20px;
            display:block;
        }
        .checkboxarea
        {
            border:2px solid Black;
            padding:7px;  
            font-weight:bold;  
            border-radius:5px;
            background:#00ff00;
            
        }
        .redcolorCheckbox
        {
            border:2px solid Black;
            padding:7px;  
            font-weight:bold;  
            border-radius:5px;
            background:red;  
            color:White;
        }
        #locationBtn
        {
            border:2px solid Black;
            padding:8px;  
            font-weight:bold;  
            border-radius:5px; 
        }
        #starbtn
        {
            border:2px solid Black;
            font-weight:bold;  
            border-radius:5px;   
            font-size:18px;
            padding:4px;
            line-height:23px;
            color:Yellow;
            background:#0069d9;
            width:35px;
        }
        .ui-tooltip
        {
            background:#5D5D5D;
            font-size:14px;
            color:White;
            box-shadow:none;
            border:none;
        }
        .ui-tooltip-content::after, .ui-tooltip-content::before {
            content: "";
            position: absolute;
            border-style: solid;
            display: block;
            left: 90px;
         }
         .ui-tooltip-content::before {
            bottom: -10px;
            border-color: #5D5D5D transparent;
            border-width: 10px 10px 0;
         }
         .ui-tooltip-content::after {
            bottom: -7px;
            border-color: #5D5D5D transparent;
            border-width: 10px 10px 0;
         }
         td
        {
            cursor: pointer;
        }
        .hover_row
        {
            background-color: #A1DCF2;
        }
        input[type=search] {
        background: none;
        font-weight: bold;
        border-color: #2e2e2e;
        border-style: solid;
        border-width: 2px 2px 2px 2px;
        outline: none;
        padding:10px 20px 10px 20px;
        width: 250px;
        }
        .icon
        {
            font-size:x-large;
            -webkit-text-stroke-width: 0.5px;
            -webkit-text-stroke-color: black;
        }
        .icon:hover
        {
            color:#5d5d5d;   
        }
        #LinkButton1
        {
            position: absolute;
            top: 0;
            left: 0;
            opacity: 0; 
        }
        
        
        #Panel2
        {
            border:1px solid gray; 
            padding:20px 5px 5px 5px; 
            border-radius:5px;
            margin-top:-8px;
            width:98%;            
            margin-left:5%;
        }
        #Panel1
        {
            margin-top:-8px;
            border:1px solid gray; 
            padding:20px 5px 5px 5px;
            border-radius:5px;
            width:98%;
            margin-left:5%;
        }
        #Panel3
        {
            border:1px solid gray; 
            padding:20px 5px 3px 5px;           
            border-radius:5px;
            margin-top:10px;
            margin-left:5%;
        }
        .secionlbl
        {
             font-weight:bold;
             color:Red;
             background:white; 
             margin-left:8%;
        }
        
        .grid-container 
        {
          display: grid;
          grid-template-columns: 33% 33% 33%;
        }
        .mainFilter
        {
            padding:7px;
            border:2px solid black;
            border-radius:5px;
            margin-left:10px;
            width:75px;
        }
        
        .fixeheader
        {
            background:#0066FF;
            color:White;
        }
        #TotalRowLbl
        {
            font-weight:bold;
            margin-bottom:10px;
            float:right;
            margin-right:25px;
            color:red;    
        }
       #callOfflbl
       {
            font-weight:bold;
            margin-bottom:10px;
            float:left;
            font-size:14px; 
       }
       
       .popup
       {
            margin-top:15px;   
       }
       
       #popupHeading
       { 
            font-size:18px;
            font-weight:bold; 
            float:left;
            margin-left:30px;  
       }
       
       #notetxtbox
       {
            margin-top:10px;
            border-radius:5px;
            resize:none;
            font-size:14px; 
            border: 2px solid black;   
       }
       
       .popupbtn
       {
            margin-top:15px;
            height:30px;
            border-radius:5px;
            font-size:14px;
            margin-bottom:15px;
            font-weight:bold;
            border:2px solid black;
            color:White;
       }
       #Panel4
       {
            background: #f5f5f5;
            text-align:center;
            border:1px solid gray;
            border-radius:5px;
            width:35%;
            box-shadow:0px 0px 50px black;    
       }
       
       .noteLinkNBtn
       {
            color:#ffffff;
            text-decoration:none;
            font-size:large;     
            text-decoration:none;
            -webkit-text-stroke-width: 0.5px;
            -webkit-text-stroke-color: black;
       }
       .noteLinkNBtn:hover
       {
             color:#9a9a9a; 
       }
    </style>

    <!--ToolTip Code-->

    <script>
        $(function () {
            $(".checkboxarea").tooltip({
                position: {
                    my: "center bottom",
                    at: "center top-10",
                    collision: "none"
                }
            });
        });  
    </script>
     
    <script type="text/javascript">
        $(function () {
            $("#PtNumTxt").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: 'Default.aspx/GetPtNum',
                        data: "{ 'prefix': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            if (data.d.length > 0) {
                                response($.map(data.d, function (item) {
                                    return { label: item.Text, val: item.Value };
                                }))
                            } else {
                                response([{ label: 'No results found.', val: -1}]);
                            }
                        }
                    });
                },
                select: function (e, u) {
                    if (u.item.val == -1) {
                        $(this).val("");
                        return false;
                    }
                }
            });
        });
</script>


<script>
    $(window).load(function() {
        var scrollpos = localStorage.getItem('scrollpos');
        $('#mydivclass').scrollTop(scrollpos);
    });

    window.onbeforeunload = function (e) {
        scrollpos = $('#mydivclass').scrollTop();
        localStorage.setItem('scrollpos', scrollpos);
    };
</script>

<script type="text/javascript">
    $(function () {
        $("[id*=GridView1] td").hover(function () {
            $("td", $(this).closest("tr")).addClass("hover_row");
        }, function () {
            $("td", $(this).closest("tr")).removeClass("hover_row");
        });
    });
</script> 

</head>

<body>    
    <div class="navbar">
        <div class="logo">
            <img src="images/ANU LOGO.jpg" alt="Alternate Text" />
        </div>
        <div class="softwareName">
            <center>
                <h2>
                    WorkFlow
                </h2>
            </center>
        </div>

        <div class="backBtn">
            <a href="index.aspx">Back</a>
        </div>
    </div>
    <div class="container">   
        <div class="row">
            <div class="form-area">
                <form id="form1" runat="server">
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

                <div class="grid-container">
                    <div class="grid-item">
                        <asp:Label ID="orderdatelbl" runat="server" Text="Via Order Date" CssClass="secionlbl"></asp:Label>
                                <asp:Panel ID="Panel2" runat="server" DefaultButton="SearchBtn">                               
                                    <!--<asp:Label ID="Label1" CssClass="lbl" runat="server" Text="PT Number: "></asp:Label>
                                    <asp:TextBox ID="PtNumTxt" CssClass="txtbox" runat="server"></asp:TextBox>-->
                                    <asp:Label ID="Label3" CssClass="lbl" runat="server" Text="From: "></asp:Label>
                                    <asp:TextBox ID="fromorderdate" CssClass="txtbox" runat="server"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="fromorderdate" Format="dd-MMM-yyyy"/>

                                    <asp:Label ID="Label4" CssClass="lbl" runat="server" Text="To: "></asp:Label>
                                    <asp:TextBox ID="toorderdate" CssClass="txtbox" runat="server"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="toorderdate" Format="dd-MMM-yyyy"/>

                                    <asp:Button ID="SearchBtn" CssClass="btn" runat="server" Text="Enter" 
                                        onclick="SearchBtn_Click" CausesValidation="False" UseSubmitBehavior="true" />

                         </asp:Panel>                  
                    </div>


                    <div class="grid-item">              
                        <asp:Label ID="deliverylbl" runat="server" Text="Via Delivery Date" CssClass="secionlbl"></asp:Label>         
                                <asp:Panel ID="Panel1" runat="server" DefaultButton="EnterBtn">  
                                                              
                                    <asp:Label ID="fromDateLbl" CssClass="lbl" runat="server" Text="From: "></asp:Label>
                                        <asp:TextBox ID="fromTxtBox" CssClass="txtbox" runat="server" TextMode="DateTime"></asp:TextBox>
                                
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="toTxtBox" Format="dd-MMM-yyyy"/>

                                        <asp:Label ID="toDateLbl" CssClass="lbl" runat="server" Text="To: "></asp:Label>
                                        <asp:TextBox ID="toTxtBox" CssClass="txtbox" runat="server"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="fromTxtBox" Format="dd-MMM-yyyy"/>
                                        <asp:Button ID="EnterBtn" CssClass="btn" runat="server" Text="Enter" onclick="EnterBtn_Click"/>                               
                         </asp:Panel>
                    </div>
                    <div class="grid-item">
                        <asp:Panel runat="server" ID="Panel3" class="grid-item">
                            <span class="checkboxarea" Title="Check, To See Dispatch Records"> <asp:CheckBox ID="dispatchCheckBox" runat="server" Text="  Dispatch" /></span>
                            <span class="location"><asp:Button ID="locationBtn" runat="server" disable="true"/></span>
                            <span>
                                <%--<asp:DropDownList ID="mainFilterDropDown" CssClass="mainFilter" runat="server" OnSelectedIndexChanged="mainFilterDropDownChanged"
                                                    AutoPostBack="true" AppendDataBoundItems="true">
                                                    <asp:ListItem Text="Filter" Value="-1"></asp:ListItem>
                                                    <asp:ListItem Text="Red Color" Value="Red Color"></asp:ListItem> 
                                                </asp:DropDownList>--%>
                                <asp:CheckBox ID="redcolorCheckbox" class="redcolorCheckbox" runat="server" Text=" Red Color" AutoPostBack="true" OnCheckedChanged="redcolorCheckbox_changed"/>
                            </span>
                            <span><asp:Button ID="starbtn" runat="server" Text="★" OnClick="starBtn_Click"/></span>
                        </asp:Panel>
                    </div>

                </div>
                
                            <div id="Div1" class="gridviewArea" runat="server">
                            <asp:Label ID="callOfflbl" runat="server"></asp:Label>
                            <asp:Label ID="TotalRowLbl" runat="server"></asp:Label>
                            <asp:Button ID="exportBtn" runat="server" Text="Export" OnClick="ExportAllData" style="float:right; background:#00FF00; height:25px; border:2px solid black; width:70px; border-radius:5px; margin-right:25px; margin-top:-5px;"/>

                      <asp:LinkButton ID="Linkbtn" runat="server"></asp:LinkButton> 
                            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" TargetControlID="Linkbtn" CancelControlID="btnclose" PopupControlID="Panel4"  runat="server">  
        </ajaxToolkit:ModalPopupExtender> 

                               <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" ShowHeader="true" GridLines="both" HeaderStyle-Height="68px" HeaderStyle-HorizontalAlign="Center" RowStyle-HorizontalAlign="Center" RowStyle-VerticalAlign="Middle" EmptyDataText="Data Not Found...!" EmptyDataRowStyle-ForeColor="Red" EmptyDataRowStyle-Font-Size="X-Large" RowStyle-Height="25px" Font-Size="12px" OnRowDataBound="GridView1_RowDataBound" ShowHeaderWhenEmpty="true" BorderColor="Black" BorderStyle="Solid" BorderWidth="2px" HeaderStyle-CssClass="fixeheader">
                                    <Columns>
                                        <%--<asp:BoundField HeaderText="UID" DataField="UID"/>--%>


                                        <asp:TemplateField HeaderText = "UID" ItemStyle-Width="80px" HeaderStyle-CssClass="xyz" HeaderStyle-Width="80px">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="stautsLinkBtn" runat="server" CommandArgument='<%# Eval("UID") %>'  OnCommand="ArticleShow_Command" CssClass="LinkStyle" Text='<%# Eval("UID")%>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <%--<asp:BoundField HeaderText="Cust_PO Date" DataField="Cust_PO Date"/>--%>

                                        <asp:TemplateField ItemStyle-Width="65px" HeaderStyle-Width="65px">
                                            <HeaderTemplate>
                                                Cust_PO Date
                                                <br />
                                                <asp:DropDownList ID="Cust_PODate" runat="server" OnSelectedIndexChanged="Cust_PODateChanged"
                                                    AutoPostBack="true" AppendDataBoundItems="true" Width="20px">
                                                    <asp:ListItem Text="Filter" Value="Apply Filter"></asp:ListItem>
                                                    <asp:ListItem Text="Show All" Value="Show All"></asp:ListItem>
                                                    <asp:ListItem Text="Non Pending" Value="Non Pending"></asp:ListItem> 
                                                    <asp:ListItem Text="N/A" Value="N/A"></asp:ListItem>                                                    
                                                    <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
                                                </asp:DropDownList>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="custPoDateLbl" runat="server" Text='<%# Eval("Cust_PO Date")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <%--<asp:BoundField HeaderText="PT Date" DataField="PT Date"/>--%>

                                        <asp:TemplateField ItemStyle-Width="65px" HeaderStyle-Width="65px">
                                            <HeaderTemplate>
                                                PT Date
                                                <br />
                                                <br />
                                                <asp:DropDownList ID="PtDateList" runat="server" OnSelectedIndexChanged="PtDateListChanged" AutoPostBack="true" AppendDataBoundItems="true" Width="20px" CssClass="dropdownlist">
                                                <asp:ListItem Text="Filter" Value="Apply Filter"></asp:ListItem>
                                                    <asp:ListItem Text="Show All" Value="Show All"></asp:ListItem>
                                                    <asp:ListItem Text="Non Pending" Value="Non Pending"></asp:ListItem>                                                    
                                                    <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
                                                    <asp:ListItem Text="N/A" Value="N/A"></asp:ListItem>                                                    
                                                </asp:DropDownList>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="ptDateLbl" runat="server" Text='<%# Eval("PT Date")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-Width="65px" HeaderStyle-Width="65px">
                                            <HeaderTemplate>
                                                OC Status
                                                <br />
                                                <asp:DropDownList ID="OcStatusList" runat="server" OnSelectedIndexChanged="OcStatusListChanged" 
                                                    AutoPostBack="true" AppendDataBoundItems="true" Width="20px" >
                                                    <asp:ListItem Text="Filter" Value="Apply Filter"></asp:ListItem>
                                                    <asp:ListItem Text="Show All" Value="Show All"></asp:ListItem>
                                                    <asp:ListItem Text="Non Pending" Value="Non Pending"></asp:ListItem>                                                    
                                                    <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
                                                    <asp:ListItem Text="N/A" Value="N/A"></asp:ListItem>                                                    
                                                </asp:DropDownList>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="ocStatusLbl" runat="server" Text='<%# Eval("OC Status")%>'></asp:Label>  
                                            </ItemTemplate>
                                        </asp:TemplateField>




                                        <%--<asp:BoundField HeaderText="Remarks" DataField="Remarks" HeaderStyle-CssClass="observation" HeaderStyle-Width="65px"/>--%>

                                        <asp:TemplateField ItemStyle-Width="65px" HeaderStyle-Width="65px">
                                            <HeaderTemplate>
                                                Remarks
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="RemarksLbl" runat="server" Text='<%# Eval("Remarks")%>'></asp:Label>  
                                            </ItemTemplate>
                                        </asp:TemplateField>



                                        <%--<asp:BoundField HeaderText="A* Casing" DataField="A* Casing"/>--%>

                                        <%--<asp:TemplateField>
                                            <HeaderTemplate>
                                                A* Casing
                                                <asp:DropDownList ID="aCasinglist" runat="server" OnSelectedIndexChanged="aCasinglistChanged" AutoPostBack="true" AppendDataBoundItems="true" Width="20px" CssClass="dropdownlist">
                                                <asp:ListItem Text="Filter" Value="Apply Filter"></asp:ListItem>
                                                    <asp:ListItem Text="Show All" Value="Show All"></asp:ListItem>
                                                    <asp:ListItem Text="Non Pending" Value="Non Pending"></asp:ListItem>                                                    
                                                    <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
                                                    <asp:ListItem Text="N/A" Value="N/A"></asp:ListItem> 
                                                </asp:DropDownList>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# Eval("A* Casing")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>

                                        <asp:TemplateField ItemStyle-Width="65px" HeaderStyle-Width="65px">
                                            <HeaderTemplate>
                                                Casing Drawing Status
                                                <br />
                                                <asp:DropDownList ID="CasingDrawingStatusList" runat="server" OnSelectedIndexChanged="CasingDrawingStatusListChanged" AutoPostBack="true" AppendDataBoundItems="true" Width="20px" CssClass="dropdownlist">
                                                <asp:ListItem Text="Filter" Value="Apply Filter"></asp:ListItem>
                                                    <asp:ListItem Text="Show All" Value="Show All"></asp:ListItem>
                                                    <asp:ListItem Text="Non Pending" Value="Non Pending"></asp:ListItem>                                                    
                                                    <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
                                                    <asp:ListItem Text="N/A" Value="N/A"></asp:ListItem> 
                                                </asp:DropDownList>
                                            </HeaderTemplate>
                                            <ItemTemplate>                                                
                                                        <asp:LinkButton ID="uidLinkBtn" runat="server" CommandArgument='<%# Eval("Casing Drawing Status") %>'  OnCommand="Drawingstauts_Command" CssClass="LinkStyle" Text='<%# Eval("Casing Drawing Status")%>'><%# Eval("Casing Drawing Status")%> </asp:LinkButton>                                                   
                                             </ItemTemplate>
                                        </asp:TemplateField>

                                        <%--<asp:BoundField HeaderText="Send To HT" DataField="Send To HT"/>--%>

                                        <asp:TemplateField ItemStyle-Width="65px" HeaderStyle-Width="65px">
                                            <HeaderTemplate>
                                                Send To HT
                                                <br />
                                                <asp:DropDownList ID="SendToHTList" runat="server" OnSelectedIndexChanged="SendToHTListChanged"
                                                    AutoPostBack="true" AppendDataBoundItems="true" Width="20px">
                                                    <asp:ListItem Text="Filter" Value="Apply Filter"></asp:ListItem>
                                                    <asp:ListItem Text="Show All" Value="Show All"></asp:ListItem>
                                                    <asp:ListItem Text="Non Pending" Value="Non Pending"></asp:ListItem>                                                    
                                                    <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
                                                    <asp:ListItem Text="N/A" Value="N/A"></asp:ListItem> 
                                                </asp:DropDownList>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="sendToHTLbl" runat="server" Text='<%# Eval("Send To HT")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <%--<asp:BoundField HeaderText="Receive To HT" DataField="Receive To HT"/>--%>

                                         <asp:TemplateField ItemStyle-Width="65px" HeaderStyle-Width="65px">
                                            <HeaderTemplate>
                                                Received From HT
                                                <br />
                                                <asp:DropDownList ID="ReceiveToHT" runat="server" OnSelectedIndexChanged="ReceiveToHTChanged"
                                                    AutoPostBack="true" AppendDataBoundItems="true" Width="20px">
                                                    <asp:ListItem Text="Filter" Value="Apply Filter"></asp:ListItem>
                                                    <asp:ListItem Text="Show All" Value="Show All"></asp:ListItem>
                                                    <asp:ListItem Text="Non Pending" Value="Non Pending"></asp:ListItem>                                                    
                                                    <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
                                                    <asp:ListItem Text="N/A" Value="N/A"></asp:ListItem> 
                                                </asp:DropDownList>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="recvToHTLbl" runat="server" Text='<%# Eval("Receive To HT")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <%--<asp:BoundField HeaderText="B* Carbide" DataField="B* Carbide" />--%>

                                        <%--<asp:TemplateField>
                                            <HeaderTemplate>
                                                B* Carbide
                                                <asp:DropDownList ID="BCarbidelist" runat="server" OnSelectedIndexChanged="BCarbidelistChanged"
                                                    AutoPostBack="true" AppendDataBoundItems="true" Width="20px">
                                                    <asp:ListItem Text="Filter" Value="Apply Filter"></asp:ListItem>
                                                    <asp:ListItem Text="Show All" Value="Show All"></asp:ListItem>
                                                    <asp:ListItem Text="Non Pending" Value="Non Pending"></asp:ListItem>                                                    
                                                    <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
                                                    <asp:ListItem Text="N/A" Value="N/A"></asp:ListItem> 
                                                </asp:DropDownList>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# Eval("B* Carbide")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>

                                        <%--<asp:BoundField HeaderText="Carbide Inq Date" DataField="Carbide Inq Date" />--%>

                                        <asp:TemplateField ItemStyle-Width="65px" HeaderStyle-Width="65px">
                                            <HeaderTemplate>
                                                Carbide Inq Date<br />
                                                <asp:DropDownList ID="CarbideInqDate" runat="server" OnSelectedIndexChanged="CarbideInqDateChanged"
                                                    AutoPostBack="true" AppendDataBoundItems="true" Width="20px">
                                                    <asp:ListItem Text="Filter" Value="Apply Filter"></asp:ListItem>
                                                    <asp:ListItem Text="Show All" Value="Show All"></asp:ListItem>
                                                    <asp:ListItem Text="Non Pending" Value="Non Pending"></asp:ListItem>                                                    
                                                    <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
                                                    <asp:ListItem Text="N/A" Value="N/A"></asp:ListItem> 
                                                </asp:DropDownList>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="carbideInqLbl" runat="server" Text='<%# Eval("Carbide Inq Date")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>



                                        <%--<asp:BoundField HeaderText="Carbide Recv Date" DataField="Carbide Recv Date" />--%>

                                        <asp:TemplateField ItemStyle-Width="65px" HeaderStyle-Width="65px">
                                            <HeaderTemplate>
                                                Carbide Recv Date
                                                <br />
                                                <asp:DropDownList ID="CarbideRecvDateList" runat="server" OnSelectedIndexChanged="CarbideRecvDateListChanged"
                                                    AutoPostBack="true" AppendDataBoundItems="true" Width="20px">
                                                    <asp:ListItem Text="Filter" Value="Apply Filter"></asp:ListItem>
                                                    <asp:ListItem Text="Show All" Value="Show All"></asp:ListItem>
                                                    <asp:ListItem Text="Non Pending" Value="Non Pending"></asp:ListItem>                                                    
                                                    <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
                                                    <asp:ListItem Text="N/A" Value="N/A"></asp:ListItem> 
                                                </asp:DropDownList>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="carbideRecvLbl" runat="server" Text='<%# Eval("Carbide Recv Date")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <%--<asp:BoundField HeaderText="Job Card Status" DataField="Job Card Status" />--%>

                                        <asp:TemplateField ItemStyle-Width="65px" HeaderStyle-Width="65px">
                                            <HeaderTemplate>
                                                Job <br />Card <br /> Status
                                                <br />
                                                <asp:DropDownList ID="JobCardStatus" runat="server" OnSelectedIndexChanged="JobCardStatusChanged"
                                                    AutoPostBack="true" AppendDataBoundItems="true" Width="20px">
                                                    <asp:ListItem Text="Filter" Value="Apply Filter"></asp:ListItem>
                                                    <asp:ListItem Text="Show All" Value="Show All"></asp:ListItem>
                                                    <asp:ListItem Text="Non Pending" Value="Non Pending"></asp:ListItem>                                                    
                                                    <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
                                                    <asp:ListItem Text="N/A" Value="N/A"></asp:ListItem> 
                                                </asp:DropDownList>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="jobCardStatusLbl" runat="server" Text='<%# Eval("Job Card Status")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <%--<asp:BoundField HeaderText="External Grinding" DataField="External Grinding" />--%>

                                        <asp:TemplateField ItemStyle-Width="65px" HeaderStyle-Width="65px">
                                            <HeaderTemplate>
                                                External Grinding<br></br>
                                                <asp:DropDownList ID="ExternalGrindingList" runat="server" OnSelectedIndexChanged="ExternalGrindingListChanged" 
                                                    AutoPostBack="true" AppendDataBoundItems="true" Width="20px">
                                                    <asp:ListItem Text="Filter" Value="Apply Filter"></asp:ListItem>
                                                    <asp:ListItem Text="Show All" Value="Show All"></asp:ListItem>
                                                    <asp:ListItem Text="Non Pending" Value="Non Pending"></asp:ListItem>                                                    
                                                    <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
                                                    <asp:ListItem Text="N/A" Value="N/A"></asp:ListItem> 
                                                </asp:DropDownList>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="externalGrindingLbl" runat="server" Text='<%# Eval("External Grinding")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <%--<asp:BoundField HeaderText="Dxf Ready" DataField="Dxf Ready" />--%>

                                        <asp:TemplateField ItemStyle-Width="65px" HeaderStyle-Width="65px">
                                            <HeaderTemplate>
                                                Dxf Ready<br />
                                                <asp:DropDownList ID="DxfReady" runat="server" OnSelectedIndexChanged="DxfReadyChanged" 
                                                    AutoPostBack="true" AppendDataBoundItems="true" Width="20px">
                                                    <asp:ListItem Text="Filter" Value="Apply Filter"></asp:ListItem>
                                                    <asp:ListItem Text="Show All" Value="Show All"></asp:ListItem>
                                                    <asp:ListItem Text="Non Pending" Value="Non Pending"></asp:ListItem>                                                    
                                                    <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
                                                    <asp:ListItem Text="N/A" Value="N/A"></asp:ListItem> 
                                                </asp:DropDownList>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="DxfReadyLbl" runat="server" Text='<%# Eval("Dxf Ready")%>'></asp:Label>
                                            </ItemTemplate>


                                            <%--<ItemTemplate>                                                
                                                        <asp:LinkButton ID="DxfReadylinkBtn" runat="server" CommandArgument='<%# Eval("Dxf Ready") %>'  OnCommand="dxfFileStatus_Command" CssClass="LinkStyle"><%# Eval("Dxf Ready")%> </asp:LinkButton>                                                   
                                             </ItemTemplate>--%>

                                        </asp:TemplateField>

                                        <%--<asp:BoundField HeaderText="Wirecut" DataField="Wirecut" />--%>

                                        <asp:TemplateField ItemStyle-Width="65px" HeaderStyle-Width="65px">
                                            <HeaderTemplate>
                                                Wirecut (Step 26)
                                                <br />
                                                <asp:DropDownList ID="WirecutList" runat="server" OnSelectedIndexChanged="WirecutListChanged" 
                                                    AutoPostBack="true" AppendDataBoundItems="true" Width="20px">
                                                    <asp:ListItem Text="Filter" Value="Apply Filter"></asp:ListItem>
                                                    <asp:ListItem Text="Show All" Value="Show All"></asp:ListItem>
                                                    <asp:ListItem Text="Non Pending" Value="Non Pending"></asp:ListItem>                                                    
                                                    <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
                                                    <asp:ListItem Text="N/A" Value="N/A"></asp:ListItem> 
                                                </asp:DropDownList>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="WirecutLbl" runat="server" Text='<%# Eval("Wirecut")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        

                                        <%--<asp:BoundField HeaderText="EDM" DataField="EDM" />--%>

                                         <asp:TemplateField ItemStyle-Width="65px" HeaderStyle-Width="65px">
                                            <HeaderTemplate>
                                                EDM (Step 80)<br />
                                                <asp:DropDownList ID="EDM" runat="server" OnSelectedIndexChanged="EDMChanged" 
                                                    AutoPostBack="true" AppendDataBoundItems="true" Width="20px">
                                                    <asp:ListItem Text="Filter" Value="Apply Filter"></asp:ListItem>
                                                    <asp:ListItem Text="Show All" Value="Show All"></asp:ListItem>
                                                    <asp:ListItem Text="Non Pending" Value="Non Pending"></asp:ListItem>                                                    
                                                    <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
                                                    <asp:ListItem Text="N/A" Value="N/A"></asp:ListItem> 
                                                </asp:DropDownList>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="EDMLbl" runat="server" Text='<%# Eval("EDM")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <%--<asp:BoundField HeaderText="Send To Coating" DataField="Send To Coating" />--%>
                                        <asp:TemplateField ItemStyle-Width="65px" HeaderStyle-Width="65px">
                                            <HeaderTemplate>
                                                Send To Coating<br />
                                                <asp:DropDownList ID="SendToCoating" runat="server" OnSelectedIndexChanged="SendToCoatingChanged" 
                                                    AutoPostBack="true" AppendDataBoundItems="true" Width="20px" style="postion:absolute">
                                                    <asp:ListItem Text="Filter" Value="Apply Filter"></asp:ListItem>
                                                    <asp:ListItem Text="Show All" Value="Show All"></asp:ListItem>
                                                    <asp:ListItem Text="Non Pending" Value="Non Pending"></asp:ListItem>                                                    
                                                    <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
                                                    <asp:ListItem Text="N/A" Value="N/A"></asp:ListItem> 
                                                </asp:DropDownList>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="SendToCoatingLbl" runat="server" Text='<%# Eval("Send To Coating")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <%--<asp:BoundField HeaderText="Recieve To Coating" DataField="Recieve To Coating" />--%>

                                        <asp:TemplateField ItemStyle-Width="65px" HeaderStyle-Width="65px">
                                            <HeaderTemplate>
                                                Recieved From Coating<br />
                                                <asp:DropDownList ID="RecieveToCoatingList" runat="server" OnSelectedIndexChanged="RecieveToCoatingChanged" 
                                                    AutoPostBack="true" AppendDataBoundItems="true" Width="20px">
                                                    <asp:ListItem Text="Filter" Value="Apply Filter"></asp:ListItem>
                                                    <asp:ListItem Text="Show All" Value="Show All"></asp:ListItem>
                                                    <asp:ListItem Text="Non Pending" Value="Non Pending"></asp:ListItem>                                                    
                                                    <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
                                                    <asp:ListItem Text="N/A" Value="N/A"></asp:ListItem> 
                                                </asp:DropDownList>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="RecieveToCoatingLbl" runat="server" Text='<%# Eval("Recieve To Coating")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <%--<asp:BoundField HeaderText="Delivery Date" DataField="Delivery Date" />--%>

                                        <asp:TemplateField ItemStyle-Width="65px" HeaderStyle-Width="65px">
                                            <HeaderTemplate>
                                                <%if (dispatchCheckBox.Checked == true)
                                                  { %>
                                                  <%="End Date<br/>"%>
                                                  <%}
                                                  else
                                                  { %>
                                                    <%="Delivery Date"%>
                                                    <%} %>
                                                <br />
                                                <asp:DropDownList ID="DeliveryDateList" runat="server" OnSelectedIndexChanged="DeliveryDateListChanged" 
                                                    AutoPostBack="true" AppendDataBoundItems="true" Width="20px" >
                                                    <asp:ListItem Text="Filter" Value="Apply Filter"></asp:ListItem>
                                                    <asp:ListItem Text="Show All" Value="Show All"></asp:ListItem>
                                                    <asp:ListItem Text="Non Pending" Value="Non Pending"></asp:ListItem>                                                    
                                                    <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
                                                    <asp:ListItem Text="N/A" Value="N/A"></asp:ListItem> 
                                                </asp:DropDownList>
                                            </HeaderTemplate>
                                            <ItemTemplate>                                                                                            
                                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("End Date/Delivery Date")%>'></asp:Label>                                         
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        <%--<asp:BoundField HeaderText="Observation" DataField="Observation" HeaderStyle-CssClass="observation" HeaderStyle-Width="65px"/>--%>

                                        <asp:TemplateField ItemStyle-Width="65px" HeaderStyle-Width="65px">
                                            <HeaderTemplate>
                                                Observ.
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="ObservationLbl" runat="server" Text='<%# Eval("Observation")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>



                                        <asp:TemplateField ItemStyle-Width="65px" HeaderStyle-Width="65px">
                                            <HeaderTemplate>
                                                Mark As Star
                                                <%--<br />
                                              <asp:CheckBox ID="checkAll" runat="server" onclick = "checkAll(this);"/>--%>
                                            </HeaderTemplate>
                                           <ItemTemplate>
                                            <%-- <asp:CheckBox ID="CheckBox1" runat="server" onclick = "Check_Click(this)" />--%>
                                               <asp:LinkButton ID="LinkButton1" runat="server" Style="color:#ffffff; text-decoration:none;" CommandArgument='<%# Eval("UID") %>'  OnCommand="MarkAsStar_Command"><span class="icon">★</asp:LinkButton>
                                           </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-Width="50px" HeaderStyle-Width="50px">
                                            <HeaderTemplate>
                                                Add Note
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                               <asp:LinkButton ID="noteLinkNBtn" class="noteLinkNBtn" runat="server" CommandName="Select">⬤</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
 
                                    </Columns>
                                </asp:GridView>

                            </div>


                        <asp:Panel ID="Panel4" runat="server"> 
                        
                            <div class="popup" >
                                <asp:Label ID="popupHeading" runat="server" Text="Label"></asp:Label>
                            </div>
                            
                            <div>
                                <asp:TextBox ID="notetxtbox" runat="server" TextMode="MultiLine" Rows="10" Columns="50" placeholder="Write your note..."></asp:TextBox><br />
                                <asp:Button ID="btnclose" class="popupbtn" style="width:30%;float:left; margin-left:17%; background:#00ff00; color:Black;" runat="server" BackColor="#99CCFF" Text="Cancel"/>  
                                <span><asp:Button ID="NoteSaveBtn" class="popupbtn" 
                                    style="width:30%; margin-right:12%; background:#3390ff;" runat="server" 
                                    Text="Save" onclick="NoteSaveBtn_Click" /></span> 
                            </div>
                        </asp:Panel>
                          
                    
                    <asp:Label ID="hiddenlbl" runat="server" Visible="false"></asp:Label>
                </form>
                </div>
            </div>
        </div>
    

    <!--<script src="Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>-->
    <script src="Scripts/ScrollableGridPlugin.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#<%=GridView1.ClientID%>').Scrollable();
        });
      </script>
</body>
</html>






