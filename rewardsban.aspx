<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin.Master" CodeBehind="rewardsban.aspx.vb" Inherits="PassholderAdmin.rewardsban" %>
<%@ MasterType VirtualPath="~/Admin.Master" %> 

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        #form1 {
            background: #959595;
        }

        #ContentPlaceHolder1_pnlForm {
            padding: 20px!important;
            padding-top: 0px!important;
            margin-top: 0px!important;
        }
.pagecontent {
    width: 90%;
    margin-left: auto;
    margin-right: auto;
    background: white;
    max-width: 1050px;
    margin-left: auto;
    margin-right: auto;
    padding: 30px;
    box-shadow: 0px 0px 10px -4px #000;
    padding-left: 37px;
    padding-top: 1px;
}
        h1 {
            font-size: 20pt!important;
            margin-top: 30px!important;
        }

.displayPage table td label {
    font-size: 13pt!important;
    margin-bottom: 0px;
    display: inline-block;
    margin-left: 7px!important;
    padding: 2px!important;
    padding-bottom: 2px!important;
    padding-right: 10px!important;
}
        .actionButton {
            border: none;
            padding: 13px;
            padding-left: 20px;
            padding-right: 20px;
            background: #0067b8;
            color: white;
            box-shadow: 0px 2px 8px -3px #000;
        }

        input#ContentPlaceHolder1_cmdSubmit:hover {
            box-shadow: 0px 2px 10px -3px #000;
            background: #005191;
        }

        input#ContentPlaceHolder1_cmdSubmit:active {
            box-shadow: none;
            background: #000000;
        }

        div#ContentPlaceHolder1_pnlResult {
            background: #fff400;
            padding: 20px;
            box-shadow: 0px 0px 10px -3px #000;
        }
        table.guestInfo {
            margin-top: 31px;
            font-size: 14pt;
        }

        .guestInfo td {
            padding: 6px;
            background: #ffffb2;
        }

        .guestInfo td.fieldData {
            font-weight: bold;
        }

        div#pnlErrors {
    margin-top: 37px;
    display: block;
    padding-top: 4px;
    padding-bottom: 47px;
}
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="pnlForm" runat="server">
        <div class="pagecontent">
        <asp:Panel ID="pnlResult" runat="server" Visible="false">
            <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>
        </asp:Panel>

        <h1>Ban Guest from Member Rewards</h1>
        <p>You can use this tool to ban a Member from participating in Membership Rewards. When you do this, the Member will no longer be able to register and if they are registered, they will no longer be able to view or redeem points.</p>
        <h3>What is the guest's media number?</h3>
        <p><asp:TextBox ID="fldMediaNumber" Width="350px" runat="server"></asp:TextBox></p>

        <h3>Reason for the Ban?</h3>
        <p><asp:TextBox ID="fldReason" Rows="5" TextMode="MultiLine" Width="350px" runat="server"></asp:TextBox></p>




        <p><asp:Button ID="cmdFindMediaNumber" CssClass="actionButton" runat="server" Text="Ban From Rewards" /></p>

    </asp:Panel>
    <asp:Panel ID="pnlError" runat="server">
        <br />
        <br />
        <br />
        <p>You do not have access to this tool. Permission 515 required.</p>
    </asp:Panel>
    <asp:HiddenField ID="fldFolioUniqueID" runat="server" />
    <asp:HiddenField ID="fldParkID" runat="server" />
</asp:Content>
