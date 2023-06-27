<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin.Master" CodeBehind="triage.aspx.vb" Inherits="PassholderAdmin.triage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <asp:Label ID="lblSubjectHeader" runat="server" Text="Message Subject" CssClass="fieldHeader hdrSubject"></asp:Label>
    <asp:Label ID="lblSubject" runat="server" Text="This is the subject" CssClass="fldSubject"></asp:Label>

    <asp:Label ID="lblDescHeader" runat="server" Text="Message" CssClass="fieldHeader hdrDesc"></asp:Label>
    <asp:Label ID="lblDesc" runat="server" Text="This is the subject" CssClass="fldDesc"></asp:Label>

    <asp:Button ID="Button1" runat="server" Text="Easy" />
    <asp:Button ID="Button2" runat="server" Text="Medium" />
    <asp:Button ID="Button3" runat="server" Text="Hard" />



    <asp:Button ID="cmdPrev" runat="server" Text="Previous" />
    <asp:Button ID="cmdNext" runat="server" Text="Next" />

</asp:Content>
