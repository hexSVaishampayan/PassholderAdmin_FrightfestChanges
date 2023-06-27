<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin.Master" CodeBehind="couponTools.aspx.vb" Inherits="PassholderAdmin.couponTools" %>
<%@ MasterType VirtualPath="~/Admin.Master" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="padding:20px;padding-top:10px;">
    <h2>Add A Test Coupon</h2>

    <p class="fieldHeader">Coupon Code</p>
    <p class="fieldRow"><asp:TextBox ID="fldCouponCode" runat="server" Width="220px"></asp:TextBox></p>

    <p class="fieldHeader">FolioID</p>
    <p class="fieldRow"><asp:TextBox ID="fldFolioUniqueID" runat="server" Width="430px"></asp:TextBox></p>

    <p class="fieldHeader">Start Date <i>(optional)</i></p>
    <p class="fieldRow"><telerik:RadDatePicker ID="fldStartDate" runat="server"></telerik:RadDatePicker> </p>

    <p class="fieldHeader">End Date <i>(optional)</i></p>
    <p class="fieldRow"><telerik:RadDatePicker ID="fldEndDate" runat="server"></telerik:RadDatePicker> </p>

    <p style="margin-top:25px;"><asp:Button ID="cmdSubmit" runat="server" Text="Submit" CssClass="actionButton" /></p>

    </div>
</asp:Content>
