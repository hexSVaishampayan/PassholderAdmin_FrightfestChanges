<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/NewAdmin.Master" CodeBehind="FrightFest.aspx.vb" Inherits="PassholderAdmin.FrightFest" %>
<%@ MasterType VirtualPath="~/NewAdmin.Master" %> 

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .pg {
            padding-top: 24px;
            margin-left: 21px;
            width: 925px;
            min-width: 925px;
        }

        .rpt {

        }
        .rpt td {
            border-bottom: 1px dotted #888888;
            padding-top: 2px;
            padding-bottom: 2px;
            text-align: right;
            width: 47px;
            padding-right:5px;
        }
        td.house {
            width: 250px;
            text-align:left;
        }
        th {
            font-size: 9pt;
            text-align: right;
            border-bottom: 2px solid;
            padding-right:5px;
        }
        th.thhouse {
            text-align: left;
        }
        h1 {
            font-size: 21pt;
            margin-bottom: 0px;
        }
        .updated {
            font-style: italic;
        }

        .subfoot td {
            font-weight: bold;
            border-top: 1px solid;
            padding-top: 4px;
            vertical-align: middle;
            border-bottom: none;
            font-size: 11pt;
        }

        td.total {
            background: #f1f1f1;
        }

        td.last60 {
            border-right: 1px solid #888888;
        }

        .datepicker {
            margin-left: 20px;
            margin-top: 26px;
            font-size: 9pt;
            margin-bottom:50px;
        }

        .reportdateheader {
            margin-bottom: 3px;
            display: block;
            font-style: italic;
            margin-left: 0px;
        }
        input#ContentPlaceHolder1_cmdGenerate {
            display: block;
            padding: 9px;
            margin-top: 8px;
            padding-left: 20px;
            padding-right: 20px;
            border: none;
            box-shadow: 0px 2px 5px -3px #000000;
            background:#000000;
            color:#ffffff;
        }

        select#ContentPlaceHolder1_fldParkID {
            font-size: 12pt;
            margin-bottom: 5px;
        }

    </style>

    <div class="pg">
        <h1><asp:Label ID="lblHeadline" runat="server"></asp:Label></h1>
        <asp:Label ID="lblUpdated" runat="server" Text=""></asp:Label>
        <asp:Label ID="lblHouses" runat="server" Text=""></asp:Label>
    </div>

    <div class="datepicker">
        <div class="reportdateheader">Change Report Date:</div>
        <telerik:RadDatePicker ID="fldReportDate" CssClass="dtpicker" runat="server" AutoPostBack="True" MinDate="2021-09-10" MaxDate="2022-11-30" FocusedDate="2022-09-15">
            <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;"></Calendar>
            <DateInput DisplayDateFormat="M/d/yyyy" DateFormat="M/d/yyyy" LabelWidth="40%" AutoPostBack="True" value="">
<EmptyMessageStyle Resize="None"></EmptyMessageStyle>

<ReadOnlyStyle Resize="None"></ReadOnlyStyle>

<FocusedStyle Resize="None"></FocusedStyle>

<DisabledStyle Resize="None"></DisabledStyle>

<InvalidStyle Resize="None"></InvalidStyle>

<HoveredStyle Resize="None"></HoveredStyle>

<EnabledStyle Resize="None"></EnabledStyle>
            </DateInput>
            <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
        </telerik:RadDatePicker>

        <div class="reportdateheader" style="margin-top:20px;">Limit Results to One Park:</div>
        <asp:DropDownList ID="fldParkID" runat="server"></asp:DropDownList>

        <asp:Button ID="cmdGenerate" runat="server" Text="Refresh" />
    </div>
</asp:Content>
