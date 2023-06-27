<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin.Master" CodeBehind="atses.aspx.vb" Inherits="PassholderAdmin.atses" %>
<%@ MasterType VirtualPath="~/Admin.Master" %> 


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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

    </style>
    <asp:Panel ID="pnlContent" runat="server">
    <div class="holdbox">

        <div class="pgfunction">
        <h1>Activar cupones ATS</h1>
        <p class="instructions">Utilisa esta función para activar una variedad de cupones ATS.</p>

        <p class="fieldHeader">Rango de los cupones</p>
        <p class="field"><asp:TextBox ID="fldRangeStart" Width="100px" Text="" runat="server"></asp:TextBox> to <asp:TextBox ID="fldRangeEnd" Width="100px" Text="" runat="server"></asp:TextBox></p>

        <p><asp:Button ID="cmdActivate" runat="server" Text="Activar" /></p>
        </div>

        <div class="pgfunction">
        <h1>Validar rango de cupones</h1>
        <p class="instructions">Esto te permite validar el estado de una variedad de cupones.</p>
        <p class="fieldHeader">Rango de los cupones</p>
        <p class="field"><asp:TextBox ID="fldValidStart" Width="100px" Text="" runat="server"></asp:TextBox> to <asp:TextBox ID="fldValidEnd" Width="100px" Text="" runat="server"></asp:TextBox></p>

        <p><asp:Button ID="cmdValidateRange" runat="server" Text="Validar" /></p>

        <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>
        </div>


        <div class="pgfunction">
        <h1>Desactivar rango de cupones</h1>
        <p class="instructions">Esto te permite desactivar una variedad de cupones. Asegúrate de tener el cupón del principio y del final del rango que quieres desactivar.</p>

        <p class="fieldHeader">Código de la primera tarjeta</p>
        <p class="field"><asp:TextBox ID="fldDeactivateCodeStart" Width="250px" Text="" runat="server"></asp:TextBox></p>

        <p class="fieldHeader">Código de la última tarjeta</p>
        <p class="field"><asp:TextBox ID="fldDeactivateCodeEnd" Width="250px" Text="" runat="server"></asp:TextBox></p>

        <p><asp:Button ID="cmdDeactivate" runat="server" Text="Deactivar" /></p>
        </div>




    </div>
</asp:Panel>
</asp:Content>
