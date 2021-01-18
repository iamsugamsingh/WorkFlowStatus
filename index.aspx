<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="PtNumSearch.index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Workflow</title>
    <link rel="icon" href="images/ANU LOGO.jpg" type="image/jpg">
    <style type="text/css">
        *
        {
            margin:0px;  
            padding:0px;              
            font-family:Segoe UI Historic;
        }
        .Container
        {
            width:80%;
            margin:auto;
            padding:25px;
        }
        .row
        {
            width:100%;
            margin:25px;
            margin:auto;  
        }
        .left-side
        {
            width:48%;
            float:left;
        }
        .right-side
        {
            width:48%;
            float:right;
        }
        .location1
        {
            width:296px;
            height:148px;
            background:#800080;
            color:White;
            margin-top:50px;
            line-height:148px;
            float:right;
            box-shadow:5px 5px 10px black;
            border-radius:5px;
        }
        .location2
        {
            width:296px;
            height:148px;
            background:#FFA500;
            color:White;
            margin-top:50px;
            line-height:148px;
            float:left;
            box-shadow:5px 5px 10px black;
            border-radius:5px;
        }
        .location1:hover
        {
            border:2px solid black;    
        }
        .location2:hover
        {
            border:2px solid black;    
        }
        .content
        {
            font-size:x-large;
            text-align:center;
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
        .message
        {
            text-align:center;
            background:#d61a19;
            color:White;
            width:20%;
            margin:auto;             
            margin-top:75px;
            padding:5px; 
            border-radius:5px;
            box-shadow:2px 2px 5px black;  
        }
        .footer
        {
            position:fixed;
            bottom:0;
            left:0;
            width:100%;
            height:50px;
            background:#3390ff;
            color:White;
            text-align:center;
            line-height:50px;
            font-weight:bold;
            font-family:Segoe UI Semibold;    
        }
        .version
        {
            color:White;
            float:right;
            margin-top:-35px;
            font-weight:bold;
            margin-right:25px;    
        }
    </style>
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
    </div>
    <div class="version">
        <p>
            Version: 1.0
        </p>
    </div>

    <form runat="server">
        <div class="Container">

            <div class="message">
                <h2>
                    Choose Location
                </h2>
            </div>

        <div class="row">
            <div class="left-side">
                <asp:LinkButton ID="loaction1" runat="server" onclick="loaction1_Click">
                    <div class="location1">
                        <h3 class="content">
                            AWS 1
                        </h3>
                    </div>
                </asp:LinkButton>
            </div>
            <div class="right-side">
                <asp:LinkButton ID="loaction2" runat="server" onclick="loaction2_Click">
                    <div class="location2">
                        <h3 class="content">
                            AWS 2
                        </h3>
                    </div>
                </asp:LinkButton>
            </div>
        </div>        
    </div>
    </form>

    <div class="footer">
        <p>
            Anu Worles Solutions &#169; 2020
        </p>
    </div>
</body>
</html>
