<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin.Master" CodeBehind="managereservations.aspx.vb" Inherits="PassholderAdmin.managereservations" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Manage Reservations</h2>
    <p>This tool allows you to manage your reservation capacity.</p>
    <p>Park: <asp:DropDownList ID="fldParkID" runat="server" AutoPostBack="True"></asp:DropDownList></p>

</asp:Content>
