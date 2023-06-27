<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin.Master" CodeBehind="pause.aspx.vb" Inherits="PassholderAdmin.pause" %>
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
    </style>

    <div class="holdbox">

        <h1>Lookup Membership Pause Status</h1>

        <p><asp:DropDownList ID="fldSearchType" runat="server">
            <asp:ListItem Value="1">Order ID</asp:ListItem>
            <asp:ListItem Value="2">Hold ID</asp:ListItem>
            </asp:DropDownList><br />
        <asp:TextBox ID="fldSearchText" runat="server"></asp:TextBox></p>
        <p><asp:Button ID="cmdSearch" runat="server" Text="Search" /></p>

        <p><asp:Label ID="lblResults" runat="server" Text=""></asp:Label></p>


        <h1 style="margin-top:45px; margin-bottom:7px; font-size:18pt;">Pause Member</h1>
        <p>Please only use this only when the guest just <i>can't</i> pause themselves and you're sure that the guest qualifies.</p>

        <p><asp:TextBox ID="fldAccessoOrderID" runat="server"></asp:TextBox><br />
        <p><asp:Button ID="cmdPause" runat="server" Text="Pause" style="background:red;" /></p>

        <h1 style="margin-top:45px; margin-bottom:7px; font-size:18pt;">UnPause Member</h1>
        <p>This will instantly unpause the Member and make tomorrow their new billing day of the month.</p>

        <p><asp:TextBox ID="fldAccessoOrderID_UnPause" runat="server"></asp:TextBox><br />
        <p><asp:Button ID="cmdUnpause" runat="server" Text="Unpause" style="background:red;" /></p>



    </div>
</asp:Content>
