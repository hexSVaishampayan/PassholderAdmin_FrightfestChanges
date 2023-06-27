<%@ Page Title="" Language="vb" AutoEventWireup="false" CodeBehind="lookuppass.aspx.vb" Inherits="PassholderAdmin.lookuppass" %>
<!DOCTYPE html>

<html>
<head runat="server">
    <title></title>
    <meta name="viewport" content="initial-scale=.90, maximum-scale=.90" />
    <link type="text/css" rel="stylesheet" href="https://mypass.sixflags.com/styles/corporate.css" media="all" />
    <link type="text/css" rel="stylesheet" href="https://mypass.sixflags.com/styles/dialog.css" media="all" />
    <link type="text/css" rel="stylesheet" href="adminstyle.css" media="all" />
    <style>
        html, body {
    background: #f9f9f9!important;
}
        div#pnlErrors {
    width: 100%;
    max-width: 1000px;
    margin-left: auto;
    margin-right: auto;
}
        div#pnlErrors {
    width: 100%;
    max-width: 1000px;
    margin-left: auto;
    margin-right: auto;
    margin-bottom: -50px;
}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <telerik:radscriptmanager ID="RadScriptManager1" runat="server">
		<Scripts>
			<asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
			<asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
			<asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js" />
		</Scripts>
	</telerik:radscriptmanager>
    <telerik:radajaxmanager ID="RadAjaxManager1" runat="server">
	</telerik:radajaxmanager>
        <div class="displayPage">
            <div style="display:block;">
                </div>
            <asp:Panel ID="pnlErrors" runat="server" Visible="false" >
                <telerik:RadLabel ID="lblErrors" runat="server"></telerik:RadLabel>
            </asp:Panel>
    <style>
        .holdbox {
            margin-left: 20px;
            margin-top: 73px;
            display: block;
        }
        table.infoblock {
            font-size: 16pt;
            width: 400px;
        }

        h1 {
            font-size: 20pt;
        }

        input#ContentPlaceHolder1_cmdPause,
        input#ContentPlaceHolder1_cmdSearch {
            font-size: 21px;
            padding: 8px;
            background: green;
            color: white;
            box-shadow: 0px 2px 5px -2px #888;
            border: none;
            padding-left: 17px;
            padding-right: 17px;
        }
        select#ContentPlaceHolder1_fldSearchType {
            font-size: 22px;
            margin-bottom: 4px;
            padding-left: 5px;
            padding-right: 5px;
            padding: 2px;
        }

        input#ContentPlaceHolder1_fldAccessoOrderID,
        input#ContentPlaceHolder1_fldSearchText {
            font-size: 22pt;
            width: 351px;
        }
        .infoblock td {
            padding: 3px;
        }

        table.visitlist {
            width: 596px;
            text-align: center;
            margin-top: 31px;
            font-size: 13pt;
        }
    </style>
    <asp:Panel ID="pnlContent" runat="server" class="holdbox">
        <h1>Lookup Season Pass Visits</h1>

        <p><asp:DropDownList ID="fldSearchType" runat="server">
            <asp:ListItem Value="1">Media Number</asp:ListItem>
            </asp:DropDownList><br />
        <asp:TextBox ID="fldSearchText" runat="server"></asp:TextBox></p>
        <p><asp:Button ID="cmdSearch" runat="server" Text="Search" /></p>

        <p><asp:Label ID="lblResults" runat="server" Text=""></asp:Label></p>
    </asp:Panel>


</div>

        <asp:Literal ID="lblUserStatus" runat="server"></asp:Literal>
        </div>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server"></telerik:RadWindowManager>

        <script>
                (function (global, undefined) {
            var demo = {};

            function alertCallBackFn(arg) {
            }

            Sys.Application.add_load(function () {
                //attach a handler to radio buttons to update global variable holding image url
                $telerik.$('input:radio').bind('click', function () {
                    demo.imgUrl = $telerik.$(this).val();
                });
            });

            global.alertCallBackFn = alertCallBackFn;
            global.$dialogsDemo = demo;
        })(window);


    </script>

<script type="text/javascript" src="https://s3.amazonaws.com/assets.freshdesk.com/widget/freshwidget.js"></script>
<script type="text/javascript">
    FreshWidget.init("", { "queryString": "&widgetType=popup", "utf8": "✓", "widgetType": "popup", "buttonType": "text", "buttonText": "Marketing Help Desk", "buttonColor": "white", "buttonBg": "#FF0000", "alignment": "2", "offset": "235px", "formHeight": "750px", "url": "https://sixflagsinteractive.freshdesk.com" });
</script>
    </form>
</body>
</html>



