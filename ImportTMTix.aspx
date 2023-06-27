<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin.Master" CodeBehind="ImportTMTix.aspx.vb" Inherits="PassholderAdmin.ImportTMTix" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <style>
        select#ContentPlaceHolder1_fldEntitlementTypeID {
            font-size: 15pt;
        }
    </style>

    <div>
        
        <h1>Employee Ticket Import Tool</h1>

        <asp:panel id="pnlForm" runat="server">
            <p class="fieldHeader">What season are these items for?</p>
            <asp:TextBox ID="fldSeason" runat="server"></asp:TextBox>

            <p class="fieldHeader">When will these entitlements expire?</p>
            <telerik:raddatepicker id="fldExpirationDate" runat="server"></telerik:raddatepicker>

            <p class="fieldHeader">What is the entitlement?</p>
            <asp:DropDownList ID="fldEntitlementTypeID" runat="server"></asp:DropDownList>

            <p class="fieldHeader">Select the data file below and then press "Process Files."</p>
            <telerik:RadAsyncUpload ID="RadAsyncUpload2" runat="server" chunksize="0" multiplefileselection="Automatic" targetfolder="~/Uploads">
                <FileFilters>
                    <telerik:FileFilter Description="Excel Document" Extensions="csv,xlsx,xls" />
                </FileFilters>
            </telerik:RadAsyncUpload>

            <asp:button id="cmdUpload" runat="server" text="Process Files" CssClass="cmdAction" />


        </asp:panel>
        <asp:panel id="pnlFinished" runat="server">
            <asp:label id="lblFinished" runat="server" text=""></asp:label>
        </asp:panel>





    </div>


</asp:Content>
