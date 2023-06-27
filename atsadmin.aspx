<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin.Master" CodeBehind="atsadmin.aspx.vb" Inherits="PassholderAdmin.atsadmin" %>
<%@ MasterType VirtualPath="~/Admin.Master" %> 

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

<style>
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

        input#ContentPlaceHolder1_cmdValidateRange,
        input#ContentPlaceHolder1_cmdDeactivate,
        input#ContentPlaceHolder1_cmdActivate
        {
            font-size: 21px;
            padding: 8px;
            background: green;
            color: white;
            box-shadow: 0px 2px 5px -2px #888;
            border: none;
            padding-left: 17px;
            padding-right: 17px;
            margin-top:10px;
        }

        input#ContentPlaceHolder1_cmdDeactivate {
            background: red;
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

        select#ContentPlaceHolder1_fldParkID {
            font-size: 16pt;
            padding: 2px;
            border: 1px solid #cccccc;
            box-shadow: 0px 0px 4px -2px #000000;
        }

        .infoblock td {
            padding: 3px;
        }

        table.dattab {
            width: 100%;
            text-align: left;
            max-width: 900px;
        }

        .dattab td {
            border-bottom: 1pt solid #aaaaaa;
            padding: 3px;
        }

        .pgfunction {
            background: white;
            padding: 11px;
            max-width: 891px;
            border: 1px solid #dddddd;
            margin-bottom: 14px;
            box-shadow: 0px 0px 8px -5px #000000;
            border-top-left-radius: 0px;
        }

        table.prizesremaining {
            width: 350px;
            margin-bottom: 20px;
        }

        td.remaining {
            text-align: right;
            padding-bottom: 3px;
        }

        h1 {
            font-size: 27pt!important;
        }

        h2 {
            font-size: 16pt!important;
            margin-top: 9px!important;
        }

        .pgfunction {
            margin-left: auto;
            margin-right: auto;
            padding: 33px;
            margin-top: 60px;
        }

        .toolContents {
        }


</style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="pnlContent" runat="server">
    <div class="holdbox">

        <div class="pgfunction">

            <h1>ATS Prize Tools</h1>

            <div class="toolContents">
                <h2>Prizes Remaining</h2>
                <asp:Label ID="lblRemainingPrizes" runat="server" Text=""></asp:Label>
            </div>

            <div class="toolContents">
                <h2>Add Media Numbers</h2>
                <p class="instructions">Add media numbers from a park</p>

                <p class="fieldHeader">Range of Coupons</p>
                <p class="field"><asp:TextBox ID="fldStartNumber" Width="250px" Text="" runat="server"></asp:TextBox></p>

                <p class="fieldHeader">Number Of Codes</p>
                <p class="field"><asp:TextBox ID="fldRangeLength" Width="100px" Text="" runat="server"></asp:TextBox></p>

                <p class="fieldHeader">Park</p>
                <p class="field"><asp:DropDownList ID="fldParkID" runat="server"></asp:DropDownList></p>

                <p><asp:Button ID="cmdUpload" runat="server" Text="Upload" /></p>
            </div>
        </div>


    </div>
</asp:Panel>
</asp:Content>
