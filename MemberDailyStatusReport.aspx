<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MemberDailyStatusReport.aspx.vb" Inherits="PassholderAdmin.MemberDailyStatusReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="shortcut icon" href="https://www.sixflags.com/sites/all/themes/sixflags_corp/favicon.ico" type="image/vnd.microsoft.icon" />
    <title>Daily Online Sales Report</title>
    <link href="https://unpkg.com/tabulator-tables/dist/css/tabulator.min.css" rel="stylesheet">
    <script type="text/javascript" src="https://unpkg.com/tabulator-tables/dist/js/tabulator.min.js"></script>
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="initial-scale=1" />
    <meta http-equiv="refresh" content="600">
    <style>
        body {
            font-family: sans-serif;
            background: #ddd;
        }
        .pgReport {
            width: 90%;
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
            font-size: 20pt;
            font-family: sans-serif;
            padding-left: 4px;
            padding-top: 2px;
            padding-bottom: 2px;
            margin: 0px;
            margin-bottom: 3px;
            margin-top: 9px;
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

        .dateranges {
            font-size: 9pt;
            margin-bottom: 20px;
            font-style: italic;
            margin-top: 5px;
        }

    </style>
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server"></telerik:RadScriptManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server"></telerik:RadAjaxManager>
        <div class="pgReport">

            <h1>Daily (Since Midnight)</h1>
            <div id="table1"></div>
            <asp:Label ID="lblReport1" runat="server" Text="Error"></asp:Label>

            <h1>Last Seven Days</h1>
            <div id="table2"></div>
            <asp:Label ID="lblReport2" runat="server" Text="Error"></asp:Label>

            <h1>Cyber Sale 2021</h1>
            <div id="table3"></div>
            <asp:Label ID="lblReport3" runat="server" Text="Error"></asp:Label>

        </div>

<style>
    .tabulator {
            font-size: 8pt;
            font-family: sans-serif;
            background:white!important;
            border:none!important;
            height:auto!important;
        }
    .tabulator .tabulator-header {
        background: white;
        border: none;
    }

.tabulator-row:last-child {
    border-bottom: 1px solid #999999;
}

    .tabulator-row {
        height: 10px;
        min-height: 18px;
    }
    .tabulator-row .tabulator-cell {
        padding: 0px;
        padding-top: 3px;
        padding-right: 5px;
        padding-left: 5px;
        min-height:18px;
    }
    .tabulator-col-content {
        text-align: center;
    }
    .tabulator .tabulator-header .tabulator-col.tabulator-sortable .tabulator-col-title {
        padding-right: 0px;
        text-align: center;
    }

    .tabulator-row:last-child .tabulator-frozen.tabulator-frozen-left {
        border-bottom: 1px solid #999999;
    }

    .vpos {color: blue;}
    .vneg {color: red;}
    .colActivePasses {background-color:#2196f345!important}
    .colTotal {background-color:#ff57222e!important}
    .colTickets {background-color:#4caf5061!important}
    .colSeasonPasses {background-color:#ffc10757!important}
    .colMemberships {background-color:#d16ef53b!important}

    .tabulator-table .tabulator-row:nth-child(1) {
        background: #fff9bf!Important;
    }

    .tabulator-table .tabulator-row:nth-child(2) {
        border-bottom: 1px solid #000000;
        background: #fffce0!Important;
    }

    .tabulator-table .tabulator-row:nth-child(2) .tabulator-cell.tabulator-frozen.tabulator-frozen-left {
        border-bottom: 1px solid #888888;
    }

</style>
        
<script>

    var tabdef = {
        height: 425,
        layout: "fitColumns",
        headerSortElement: "",
        movableColumns: false,
        movableRows: false,
        columns: [
            { title: "Park", field: "parkgroupname", width: 100, frozen: true },
            {
                title: "Active Passes",
                cssClass: "colActivePasses",
                headerClick: function (e, column) {
                    var c = column.getTable();
                    c.toggleColumn("pavgpri18");
                    c.toggleColumn("pavgpri19");
                    c.toggleColumn("pavgpridiff");
                    c.toggleColumn("pavgpriper");
                    c.toggleColumn("prev18");
                    c.toggleColumn("prev19");
                    c.toggleColumn("prevdiff");
                    c.toggleColumn("prevper");
                    e.stopPropagation();
                },
                columns: [
                    {
                        title: "Units", columns: [
                            { title: "2019", field: "p18", hozAlign: "right", sorter: "number", width: 60, formatter: "html", sorterParams: { thousandSeparator: "," } },
                            { title: "2021", field: "p19", hozAlign: "right", sorter: "number", width: 60, formatter: "html", sorterParams: { thousandSeparator: "," } },
                            { title: "Δ", field: "pdiff", hozAlign: "right", sorter: "number", width: 60, formatter: "html" },
                            { title: "%Δ", field: "pper", hozAlign: "right", sorter: "number", width: 60, formatter: "html" },
                        ],
                    },
                    {
                        title: "Avg Price", columns: [
                            { title: "2019", field: "pavgpri18", visible: false, hozAlign: "right", sorter: "number", width: 60, sorterParams: { thousandSeparator: "," }, formatter: "money" },
                            { title: "2021", field: "pavgpri19", visible: false, hozAlign: "right", sorter: "number", width: 60, sorterParams: { thousandSeparator: "," }, formatter: "money" },
                            { title: "Δ", field: "pavgpridiff", visible: false, hozAlign: "right", sorter: "number", width: 60, formatter: "html" },
                            { title: "%Δ", field: "pavgpriper", visible: false, hozAlign: "right", sorter: "number", width: 60, formatter: "html" },
                        ],
                    },
                    {
                        title: "Revenue", columns: [
                            { title: "2019", field: "prev18", visible: false, hozAlign: "right", sorter: "number", width: 60, sorterParams: { thousandSeparator: "," } },
                            { title: "2021", field: "prev19", visible: false, hozAlign: "right", sorter: "number", width: 60, sorterParams: { thousandSeparator: "," } },
                            { title: "Δ", field: "prevdiff", visible: false, hozAlign: "right", sorter: "number", width: 60, formatter: "html" },
                            { title: "%Δ", field: "prevper", visible: false, hozAlign: "right", sorter: "number", width: 60, formatter: "html" },
                        ]
                    },
                ]
            },
            {
                title: "Total Admissions",
                cssClass: "colTotal",
                headerClick: function (e, column) {
                    var c = column.getTable();
                    c.toggleColumn("totavgpri18");
                    c.toggleColumn("totavgpri19");
                    c.toggleColumn("totavgpridiff");
                    c.toggleColumn("totavgpriper");
                    c.toggleColumn("totrev18");
                    c.toggleColumn("totrev19");
                    c.toggleColumn("totrevdiff");
                    c.toggleColumn("totrevper");
                    e.stopPropagation();
                },
                columns: [
                    {
                        title: "Units", columns: [
                            { title: "2019", field: "tot18", hozAlign: "right", sorter: "number", width: 60, sorterParams: { thousandSeparator: "," } },
                            { title: "2021", field: "tot19", hozAlign: "right", sorter: "number", width: 60, sorterParams: { thousandSeparator: "," } },
                            { title: "Δ", field: "totdiff", hozAlign: "right", sorter: "number", width: 60, formatter: "html" },
                            { title: "%Δ", field: "totper", hozAlign: "right", sorter: "number", width: 60, formatter: "html" },
                        ]
                    },
                    {
                        title: "Avg Price", columns: [
                            { title: "2019", field: "totavgpri18", visible: false, hozAlign: "right", sorter: "number", width: 60, sorterParams: { thousandSeparator: "," }, formatter: "money" },
                            { title: "2021", field: "totavgpri19", visible: false, hozAlign: "right", sorter: "number", width: 60, sorterParams: { thousandSeparator: "," }, formatter: "money" },
                            { title: "Δ", field: "totavgpridiff", visible: false, hozAlign: "right", sorter: "number", width: 60, formatter: "html" },
                            { title: "%Δ", field: "totavgpriper", visible: false, hozAlign: "right", sorter: "number", width: 60, formatter: "html" },
                        ]
                    },
                    {
                        title: "Revenue", columns: [
                            { title: "2019", field: "totrev18", visible: false, hozAlign: "right", sorter: "number", width: 60, sorterParams: { thousandSeparator: "," } },
                            { title: "2021", field: "totrev19", visible: false, hozAlign: "right", sorter: "number", width: 60, sorterParams: { thousandSeparator: "," } },
                            { title: "Δ", field: "totrevdiff", visible: false, hozAlign: "right", sorter: "number", width: 60, formatter: "html" },
                            { title: "%Δ", field: "totrevper", visible: false, hozAlign: "right", sorter: "number", width: 60, formatter: "html" },
                        ]
                    },
                ]
            },
            {
                title: "Tickets",
                cssClass: "colTickets",
                headerClick: function (e, column) {
                    var c = column.getTable();
                    c.toggleColumn("tixavgpri18");
                    c.toggleColumn("tixavgpri19");
                    c.toggleColumn("tixavgpridiff");
                    c.toggleColumn("tixavgpriper");
                    c.toggleColumn("tixrev18");
                    c.toggleColumn("tixrev19");
                    c.toggleColumn("tixrevdiff");
                    c.toggleColumn("tixrevper");
                    e.stopPropagation();
                },
                columns: [
                    {
                        title: "Units", columns: [
                            { title: "2019", field: "tix18", hozAlign: "right", sorter: "number", width: 60, sorterParams: { thousandSeparator: "," } },
                            { title: "2021", field: "tix19", hozAlign: "right", sorter: "number", width: 60, sorterParams: { thousandSeparator: "," } },
                            { title: "Δ", field: "tixdiff", hozAlign: "right", sorter: "number", width: 60, formatter: "html" },
                            { title: "%Δ", field: "tixper", hozAlign: "right", sorter: "number", width: 60, formatter: "html" },
                        ],
                    },
                    {
                        title: "Avg Price", columns: [
                            { title: "2019", field: "tixavgpri18", visible: false, hozAlign: "right", sorter: "number", width: 60, sorterParams: { thousandSeparator: "," }, formatter: "money" },
                            { title: "2021", field: "tixavgpri19", visible: false, hozAlign: "right", sorter: "number", width: 60, sorterParams: { thousandSeparator: "," }, formatter: "money" },
                            { title: "Δ", field: "tixavgpridiff", visible: false, hozAlign: "right", sorter: "number", width: 60, formatter: "html" },
                            { title: "%Δ", field: "tixavgpriper", visible: false, hozAlign: "right", sorter: "number", width: 60, formatter: "html" },
                        ],
                    },
                    {
                        title: "Revenue", columns: [
                            { title: "2019", field: "tixrev18", visible: false, hozAlign: "right", sorter: "number", width: 60, sorterParams: { thousandSeparator: "," } },
                            { title: "2021", field: "tixrev19", visible: false, hozAlign: "right", sorter: "number", width: 60, sorterParams: { thousandSeparator: "," } },
                            { title: "Δ", field: "tixrevdiff", visible: false, hozAlign: "right", sorter: "number", width: 60, formatter: "html" },
                            { title: "%Δ", field: "tixrevper", visible: false, hozAlign: "right", sorter: "number", width: 60, formatter: "html" },
                        ]
                    },
                ]
            },
            {
                title: "Season Passes",
                cssClass: "colSeasonPasses",
                headerClick: function (e, column) {
                    var c = column.getTable();
                    c.toggleColumn("spavgpri18");
                    c.toggleColumn("spavgpri19");
                    c.toggleColumn("spavgpridiff");
                    c.toggleColumn("spavgpriper");
                    c.toggleColumn("sprev18");
                    c.toggleColumn("sprev19");
                    c.toggleColumn("sprevdiff");
                    c.toggleColumn("sprevper");
                    e.stopPropagation();
                },
                columns: [
                    {
                        title: "Units", columns: [
                            { title: "2019", field: "sp18", hozAlign: "right", sorter: "number", width: 60, sorterParams: { thousandSeparator: "," } },
                            { title: "2021", field: "sp19", hozAlign: "right", sorter: "number", width: 60, sorterParams: { thousandSeparator: "," } },
                            { title: "Δ", field: "spdiff", hozAlign: "right", sorter: "number", width: 60, formatter: "html" },
                            { title: "%Δ", field: "spper", hozAlign: "right", sorter: "number", width: 60, formatter: "html" },
                        ],
                    },
                    {
                        title: "Avg Price", columns: [
                            { title: "2019", field: "spavgpri18", visible: false, hozAlign: "right", sorter: "number", width: 60, sorterParams: { thousandSeparator: "," }, formatter: "money" },
                            { title: "2021", field: "spavgpri19", visible: false, hozAlign: "right", sorter: "number", width: 60, sorterParams: { thousandSeparator: "," }, formatter: "money" },
                            { title: "Δ", field: "spavgpridiff", visible: false, hozAlign: "right", sorter: "number", width: 60, formatter: "html" },
                            { title: "%Δ", field: "spavgpriper", visible: false, hozAlign: "right", sorter: "number", width: 60, formatter: "html" },
                        ],
                    },
                    {
                        title: "Revenue", columns: [
                            { title: "2019", field: "sprev18", visible: false, hozAlign: "right", sorter: "number", width: 60, sorterParams: { thousandSeparator: "," } },
                            { title: "2021", field: "sprev19", visible: false, hozAlign: "right", sorter: "number", width: 60, sorterParams: { thousandSeparator: "," } },
                            { title: "Δ", field: "sprevdiff", visible: false, hozAlign: "right", sorter: "number", width: 60, formatter: "html" },
                            { title: "%Δ", field: "sprevper", visible: false, hozAlign: "right", sorter: "number", width: 60, formatter: "html" },
                        ]
                    },
                ]
            },
            {
                title: "Monthly Memberships",
                cssClass: "colMemberships",
                headerClick: function (e, column) {
                    var c = column.getTable();
                    c.toggleColumn("mavgpri18");
                    c.toggleColumn("mavgpri19");
                    c.toggleColumn("mavgpridiff");
                    c.toggleColumn("mavgpriper");
                    c.toggleColumn("mrev18");
                    c.toggleColumn("mrev19");
                    c.toggleColumn("mrevdiff");
                    c.toggleColumn("mrevper");
                    e.stopPropagation();
                },
                columns: [
                    {
                        title: "Units", columns: [
                            { title: "2019", field: "m18", hozAlign: "right", sorter: "number", width: 60, sorterParams: { thousandSeparator: "," } },
                            { title: "2021", field: "m19", hozAlign: "right", sorter: "number", width: 60, sorterParams: { thousandSeparator: "," } },
                            { title: "Δ", field: "mdiff", hozAlign: "right", sorter: "number", width: 60, formatter: "html" },
                            { title: "%Δ", field: "mper", hozAlign: "right", sorter: "number", width: 60, formatter: "html" },
                        ],
                    },
                    {
                        title: "Avg Price", columns: [
                            { title: "2019", field: "mavgpri18", visible: false, hozAlign: "right", sorter: "number", width: 60, sorterParams: { thousandSeparator: "," }, formatter: "money" },
                            { title: "2021", field: "mavgpri19", visible: false, hozAlign: "right", sorter: "number", width: 60, sorterParams: { thousandSeparator: "," }, formatter: "money" },
                            { title: "Δ", field: "mavgpridiff", visible: false, hozAlign: "right", sorter: "number", width: 60, formatter: "html" },
                            { title: "%Δ", field: "mavgpriper", visible: false, hozAlign: "right", sorter: "number", width: 60, formatter: "html" },
                        ],
                    },
                    {
                        title: "Revenue", columns: [
                            { title: "2019", field: "mrev18", visible: false, hozAlign: "right", sorter: "number", width: 60, sorterParams: { thousandSeparator: "," } },
                            { title: "2021", field: "mrev19", visible: false, hozAlign: "right", sorter: "number", width: 60, sorterParams: { thousandSeparator: "," } },
                            { title: "Δ", field: "mrevdiff", visible: false, hozAlign: "right", sorter: "number", width: 60, formatter: "html" },
                            { title: "%Δ", field: "mrevper", visible: false, hozAlign: "right", sorter: "number", width: 60, formatter: "html" },
                        ]
                    },
                ]
            },
        ]
    }

    var tabledata1 = <%= tabDat1 %>;
    var tabledata2 = <%= tabDat2 %>;
    var tabledata3 = <%= tabDat3 %>;

    var tablebox1 = new Tabulator("#table1", tabdef);
    var tablebox2 = new Tabulator("#table2", tabdef);
    var tablebox3 = new Tabulator("#table3", tabdef);
    tablebox1.on("tableBuilt", function () {
        tablebox1.setData(tabledata1);
    });
    tablebox2.on("tableBuilt", function () { tablebox2.setData(tabledata2); });
    tablebox3.on("tableBuilt", function () { tablebox3.setData(tabledata3); });



</script>

    </form>
</body>
</html>
