<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin.Master" CodeBehind="atsreport.aspx.vb" Inherits="PassholderAdmin.atsreport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<style>
.pg {
    display: block;
    padding-top: 21px;
    margin-left: 25px;
}

    h1 {
        font-size: 22pt;
        display: block;
    }
    table.atstab {
        margin-top:0px;
    }
    .info {margin-top:3px;}
</style>
<div class="pg">
    <h1>ATS Completion Report</h1>
    <asp:Label ID="lblReport1" runat="server" Text="Label"></asp:Label>
    <p class="info">This shows the total number of responses collected each day. The goal is for this to be at least 100 every day. If you are behind
        on a day you can make it up the next day.
    </p>
    <br />
    <h1>Party Duplicate Report</h1>
    <asp:Label ID="lblReport2" runat="server" Text=""></asp:Label>
    <p class="info">This shows the percentage of guests who say that multiple people in their party received an invite. Each party should
        receive no more than ONE card, so this number should ALWAYS be zero or close to zero.
        <b>Anything above 15% means that retraining may be required.</b>
    </p>

</div>
</asp:Content>
