<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin.Master" CodeBehind="upgradereview.aspx.vb" Inherits="PassholderAdmin.upgradereview" %>
<%@ MasterType VirtualPath="~/Admin.Master" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        table.reqtab {
            font-size: 11pt;
            width: 100%;
            border-collapse:collapse;
        }

        .reqtab tr {
            height: 29px;
            vertical-align: middle;
        }

        table.reqtab {
            font-size: 10pt;
            width: 90%;
            border-collapse: collapse;
            margin-left: auto;
            margin-right: auto;
            margin-top: 61px;
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:panel id="pnlForm" runat="server">

        <div class="groupSettingsRow">
            <table class="groupSettings"><tr><td>Report Group:</td><td>
                <asp:DropDownList ID="fldMemberUpgradeRequestStatusID" AutoPostBack="true" runat="server">
                <asp:ListItem Value="0">All Active Requests</asp:ListItem>
                <asp:ListItem Value="1">Upgrade Requests</asp:ListItem>
                <asp:ListItem Value="2">Total Cancellations</asp:ListItem>
                <asp:ListItem Value="3">Partial Cancellations</asp:ListItem>
                <asp:ListItem Value="4">No Cancellable Passes</asp:ListItem>
                <asp:ListItem Value="5">Problem: Contact Guest</asp:ListItem>
                <asp:ListItem Value="6">Resolved Issues</asp:ListItem>
                <asp:ListItem Value="7">Duplicates</asp:ListItem>
                <asp:ListItem Value="8">Cancelled, Needs Refund</asp:ListItem>
                <asp:ListItem Value="9">Promotional Credit Granted</asp:ListItem>
            </asp:DropDownList>

            </td>
                <td>Park:</td><td>
                    <asp:DropDownList ID="fldParkID" runat="server" AutoPostBack="True"></asp:DropDownList>
                              </td>
            <td>
               &nbsp;&nbsp;&nbsp;&nbsp; <asp:Button ID="cmdPromoCredit" runat="server" Text="Promo Credit" />
            </td>

                                         </tr></table>
        </div>        

        <asp:Label ID="lblReport" runat="server" Text=""></asp:Label>

        <asp:HiddenField ID="QueryTypeID" runat="server" />
    </asp:panel>
</asp:Content>
