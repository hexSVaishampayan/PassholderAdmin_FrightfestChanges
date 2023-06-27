<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin.Master" CodeBehind="giftcardlookup.aspx.vb" Inherits="PassholderAdmin.giftcardlookup" %>
<%@ MasterType VirtualPath="~/Admin.Master" %> 

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <style>
        .holdbox {
            margin-left: 20px;
            margin-top: 73px;
            display: block;
        }
        table.infoblock {
            font-size: 16pt;
            width: 800px;
        }


        h1 {
            font-size: 20pt;
        }
h2 {
    margin-top: 50px;
    font-size: 17pt;
    margin-bottom: 13px;
}

h3 {
    color: black;
}
        input#ContentPlaceHolder1_cmdUnpause,
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
        input#ContentPlaceHolder1_fldAccessoOrderID_UnPause,
        input#ContentPlaceHolder1_fldAccessoOrderID,
        input#ContentPlaceHolder1_fldSearchText {
            font-size: 22pt;
            width: 251px;
        }
        .infoblock td {
            padding: 3px;
        }

        .infoblock tr {
            border-bottom: 1px solid #dddddd;
            border-top: 1px solid #dddddd;
        }

        td.maininfo {
            background: #fffdb4;
        }

        table.infoblock td:first-child {
            width: 387px;
        }

        table.rtable {
            margin-top: 19px;
        }
        .rtable td {
            padding: 5px;
            border: 1px solid #f4f4f4;
            background: white;
            text-align: center;
        }

        td.moname {
            text-align: left;
            padding-right: 16px;
            background: none;
            font-weight: bold;
        }

        td.modate {
            background: none;
            font-weight: bold;
            border-top: none;
            width: 15px;
            font-size: 9pt;
        }

td.op {
    background: #b3e6b5;
}

td.closed {
    background: #27c2f1;
}

td.closed.pause,
td.op.closed.pause {
    font-weight: bold;
    color: white;
    background: #ff7c7c;
}

        td.op.closed.pause.apppause {
        }

span.creditcount {
    font-size: 8pt;
}

.samp {
    width: 21px;
    height: 21px;
    text-align: center;
    vertical-align: middle;
}

td.legdesc {
    padding-left: 7px;
}

.legand td {
    border-bottom: 7px solid #f9f9f9;
}

    </style>

    <div class="holdbox">

        <h1>Lookup Member Gift Card Status</h1>

        <asp:TextBox ID="fldAccessoOrderID" runat="server"></asp:TextBox></p>
        <p><asp:Button ID="cmdSearch" runat="server" Text="Search" /></p>

        <p><asp:Label ID="lblResults" runat="server" Text=""></asp:Label></p>

    </div>
</asp:Content>
