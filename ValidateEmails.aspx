<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin.Master" CodeBehind="ValidateEmails.aspx.vb" Inherits="PassholderAdmin.ValidateEmails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


<h2>Upload Processed Emails</h2>

    <asp:panel id="pnlForm" runat="server">
        <p>Use the "Download All" option to download all of the records from 250ok. Then use this tool to upload.</p>
        <p>Select the data file below and then press "Process Files."</p>
	    <telerik:radasyncupload id="RadAsyncUpload1" runat="server" chunksize="0" multiplefileselection="Automatic" targetfolder="~/Uploads">
            <FileFilters>
                <telerik:FileFilter Description="Excel Document" Extensions="csv" />
            </FileFilters>
	    </telerik:radasyncupload>

        <asp:button id="cmdUpload" runat="server" text="Process Files" />
    </asp:panel>
    <asp:panel id="pnlFinished" runat="server">
        <asp:label id="lblFinished" runat="server" text=""></asp:label>
    </asp:panel>


</asp:Content>
