<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin.Master" CodeBehind="addParkingVouchers.aspx.vb" Inherits="PassholderSupport.addParkingVouchers" %>
<%@ MasterType VirtualPath="~/Admin.Master" %> 

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
            <h2>Add Parking Vouchers</h2>
        <asp:panel id="pnlForm" runat="server">

            <p>Enter the beginning and the end of the range:</p>    

            <table>
                <tr><td>Beginning of Range</td><td><asp:TextBox ID="fldBeginning" runat="server" Width="225px"></asp:TextBox> </td></tr>
                <tr><td>End of Range</td><td><asp:TextBox ID="fldEnding" runat="server" Width="225px"></asp:TextBox> </td></tr>
            </table>
            <p class="buttonRow"><asp:Button ID="cmdImport" runat="server" Text="Import" CssClass="actionButton" /></p>


        </asp:panel>
</asp:Content>
