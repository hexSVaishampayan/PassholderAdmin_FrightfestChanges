<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="onlinesales.aspx.vb" Inherits="PassholderAdmin.onlinesales" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="shortcut icon" href="https://www.sixflags.com/sites/all/themes/sixflags_corp/favicon.ico" type="image/vnd.microsoft.icon" />
    <title>Daily Online Sales Report</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="initial-scale=1" />
    <meta http-equiv="refresh" content="600">
    <style>
        body {
            font-family: sans-serif;
            background: #ddd;
        }
        .pgReport {
            width: 1430px;
            margin-left: auto;
            margin-right: auto;
            background: white;
            padding: 11px;
            padding-top: 1px;
            padding-bottom: 1px;
            box-shadow: 0px 0px 14px -3px #000;
        }

        table.dailymemreport {
            font-family: sans-serif;
            font-size: 9pt;
            text-align: right;
            border-collapse: collapse;
            min-width:1080px;
        }
        td {
            border-bottom: 1px solid #ddd;
            padding-right: 4px;
        }
        th.topHeader {
            text-align: center;
            background: #ddd;
            border-left: 3px solid #fff;
            border-bottom: none;
            padding-bottom: 3px;
            padding-top: 3px;
        }
        th.topHeader.col1 {background:#ffaaaa}
        th.topHeader.col2 {background:#9ddeff}
        th.topHeader.col3 {background:#75f56e}
        th.topHeader.col4 {background:#ffd27b}
        th.topHeader.col5 {background:#cacaca}

        td.descCol {
            text-align: left;
            width: 105px;
            font-weight: bold;
            white-space:nowrap;
        }
        td.itemNo, td.itemdif {
            width: 45px;
            font-size: 7pt;
        }
        th {
            border-bottom: 1px solid #000;
            padding-top: 4px;
        }
        td.itemdif {
            background: #f0f0f0;
        }
        td.itemdifp {
            width: 35px;
            font-size: 7pt;
            background: #f0f0f0;
        }
        td.leftcol {
            border-right:1px solid #ddd;
        }
        th.descName {
            text-align: left;
        }
        .isNeg {
            color: red;
        }
        .isPos {
            color: #003ef3;
        }
        h1 {
            font-size: 16pt;
            font-family: sans-serif;
            background: #626262;
            color: white;
            padding-left: 4px;
            padding-top: 2px;
            padding-bottom: 2px;
        }
        p.updaterow {
            font-size: 10pt;
            font-style: italic;
        }
        p.dateRange {
            margin-top: 2px;
            font-size: 9pt;
            font-style: italic;
            color: #666666;
        }
        tr:nth-child(4) {
            border-bottom: 3px solid #888;
        }
        tr:nth-child(2) td {
            background: #ddd;
        }
        tr:nth-child(19),
        tr:nth-child(16),
        tr:nth-child(13),
        tr:nth-child(10),
        tr:nth-child(7) {
            border-bottom: 2px solid #888;
        }

        tr:nth-child(21) {
            border-bottom: 2px solid #000;
        }

        th.blank {
            border: none;
        }

        h3 {
            margin-bottom: 0px;
        }

        p.broken {
            margin-top: 4px;
            font-size: 10pt;
        }

        input#cmdReRun {
            border: none;
            background: red;
            color: white;
            padding: 10px;
            padding-left: 17px;
            padding-right: 17px;
            margin-top: -6px;
            box-shadow: 0px 2px 6px -3px #000;
        }

    </style>
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server"></telerik:RadScriptManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server"></telerik:RadAjaxManager>
        <div class="pgReport">
            <asp:Label ID="lblReport1" runat="server" Text="Error"></asp:Label>
            <asp:Label ID="lblReport3" runat="server" Text="Error"></asp:Label>
        </div>
        <h3>Regenerate Broken Report</h3>
        <p class="broken">This will stop the report if it is jammed and begin again. Only press this button if it appears the report is broken. It can take 2-4 minutes to update. Data is refreshed no more often than every 10 minutes no matter how often you press the button.</p>
        <asp:Button ID="cmdReRun" CssClass="cmdReRun" runat="server" Text="Force Refresh" />
    </form>
</body>
</html>
