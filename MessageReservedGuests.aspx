<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin.Master" CodeBehind="MessageReservedGuests.aspx.vb" Inherits="PassholderAdmin.MessageReservedGuests" %>
<%@ MasterType VirtualPath="~/Admin.Master" %> 

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .pagearea {
            width: 850px;
            max-width: 850px;
            margin-left: auto;
            margin-right: auto;
            margin-top: 67px;
            padding: 21px;
            box-shadow: 0px 0px 10px -3px #000000;
        }
    .fieldBlock {
    font-size: 10pt;
    font-weight: bold;
    color: #006bb6;
    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadSkinManager ID="RadSkinManager1" runat="server" Skin="Bootstrap"></telerik:RadSkinManager>
    <div class="pagearea">
<p>Use this tool to message all of the guests who have a particular kind of reservation on a specific date. If you need to you can also cancel out the reservations and close out the day (prevent additional reservations)</p>

    <asp:Panel ID="pnlParkID" runat="server">
    <p class="fieldBlock">Select Your Park<br />
        <asp:DropDownList ID="fldParkID" runat="server"></asp:DropDownList>
    </p>
    </asp:Panel>

    <asp:Panel ID="fldReservationTypeID" runat="server">
    <p class="fieldBlock">Select Reservation Type<br />
        <asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList>
    </p>
    </asp:Panel>

    <asp:Panel ID="fldVisitDate" runat="server">
    <p class="fieldBlock">Reservation Date<br />
        <telerik:RadDatePicker ID="RadDatePicker1" runat="server"></telerik:RadDatePicker>
    </p>
    </asp:Panel>

    <asp:Panel ID="lblVisitDetails" runat="server">
    <p class="fieldBlock">RESERVATION DETAILS (please verify!)</p>
        <asp:Label ID="lblReservationDetails" runat="server" Text=""></asp:Label>
    </asp:Panel>

    <asp:Panel ID="pnlEmailDetails" runat="server">

        <p class="fieldBlock">Email Subject:<br />
            <asp:TextBox ID="fldSubject" runat="server" Width="800" style="max-width:90%;"></asp:TextBox>
        </p>

        <p class="fieldBlock">Email Headline (appears above main text block):<br />
            <asp:TextBox ID="fldHeadline" runat="server" Width="800" style="max-width:90%;"></asp:TextBox>
        </p>

        <p class="fieldBlock">Body Copy (main text of the email):<br />
            <telerik:RadEditor ID="fldBodyCopy" runat="server" Width="750px">
                <Tools>
                    <telerik:EditorToolGroup Tag="MainToolbar">
                        <telerik:EditorTool Name="FindAndReplace" />
                        <telerik:EditorSeparator />
                        <telerik:EditorSplitButton Name="Undo">
                        </telerik:EditorSplitButton>
                        <telerik:EditorSplitButton Name="Redo">
                        </telerik:EditorSplitButton>
                        <telerik:EditorSeparator />
                        <telerik:EditorTool Name="Cut" />
                        <telerik:EditorTool Name="Copy" />
                        <telerik:EditorTool Name="Paste" ShortCut="CTRL+V / CMD+V" />
                    </telerik:EditorToolGroup>
                    <telerik:EditorToolGroup Tag="Formatting">
                        <telerik:EditorTool Name="Bold" />
                        <telerik:EditorTool Name="Italic" />
                        <telerik:EditorTool Name="Underline" />
                        <telerik:EditorSeparator />
                        <telerik:EditorSplitButton Name="ForeColor">
                        </telerik:EditorSplitButton>
                        <telerik:EditorSplitButton Name="BackColor">
                        </telerik:EditorSplitButton>
                        <telerik:EditorSeparator />
                        <telerik:EditorDropDown Name="FontName">
                        </telerik:EditorDropDown>
                        <telerik:EditorDropDown Name="RealFontSize">
                        </telerik:EditorDropDown>
                    </telerik:EditorToolGroup>
                </Tools>
                <Content>
</Content>
                <TrackChangesSettings CanAcceptTrackChanges="False" />
            </telerik:RadEditor>
            <p>
            </p>
        </p>

        </asp:Panel>
    </div>

</asp:Content>
