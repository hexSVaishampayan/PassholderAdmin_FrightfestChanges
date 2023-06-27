<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin.Master" CodeBehind="inparksales.aspx.vb" Inherits="PassholderAdmin.inparksales" %>
<%@ MasterType VirtualPath="~/Admin.Master" %> 

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta http-equiv="refresh" content="360" />
    <style>

        div#ContentPlaceHolder1_pnlReport {
            max-width: 1000px;
            margin-left: auto;
            margin-right: auto;
        }
        h3.reportheader {
            text-align: center;
            font-size: 16pt;
        }
        h4.reportheader {
            text-align: center;
            font-size: 12pt;
        }
        p.lastUpdated {
            text-align: left;
            margin-top: 12px;
            font-style: italic;
            margin-left: 9px;
        }
        td.hlight {
            background: #ffff4266!important;
            font-weight:bold;
        }
        td.hlight2 {
            font-weight:bold;
        }
        .reportDate {
            margin-top: 20px;
            margin-left: 15px;
            font-size: 9pt;
            margin-bottom: 30px;
        }

        .reportDateHeader {
            margin-bottom: 4px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:Panel ID="pnlReport" runat="server">
        <h3 class="reportheader"><asp:Label ID="lblHeader" runat="server" Text="In-Park Membership Sales"></asp:Label></h3>
        <h4 class="reportheader"><asp:Label ID="lblHeader2" runat="server" Text="for April 28, 2018"></asp:Label></h4>

        <telerik:RadGrid ID="fldGrid" runat="server" AutoGenerateColumns="False" Skin="Bootstrap" AllowSorting="True">
        <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
            <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                <Selecting AllowRowSelect="True" />
                <Animation AllowColumnReorderAnimation="True" />
            </ClientSettings>
            <MasterTableView>
                <CommandItemSettings ShowAddNewRecordButton="False" />
                <Columns>
                    <telerik:GridBoundColumn DataField="ParkGroupName" FilterControlAltText="Filter column column" UniqueName="Park" HeaderAbbr="Park" HeaderText="Park" ItemStyle-HorizontalAlign="Left">
                        <ItemStyle HorizontalAlign="Left" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="MemPer" FilterControlAltText="Filter column column" UniqueName="MemPer" HeaderAbbr="Mem%" HeaderText="Mem%" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:0%}">
                        <ItemStyle HorizontalAlign="Right" CssClass="hlight" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="SeasonPass" FilterControlAltText="Filter column column" UniqueName="SP" HeaderAbbr="SP" HeaderText="SP" ItemStyle-HorizontalAlign="Right">
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Membership" FilterControlAltText="Filter column column" UniqueName="Memberships" HeaderAbbr="Mem" HeaderText="Mem" ItemStyle-HorizontalAlign="Right">
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="DiamondPer" FilterControlAltText="Filter column column" UniqueName="DiamondPer" HeaderAbbr="Dia%" HeaderText="Dia%" ItemStyle-HorizontalAlign="Right">
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="DiningPass" FilterControlAltText="Filter column column" UniqueName="Dine" HeaderAbbr="Di" HeaderText="Dine" ItemStyle-HorizontalAlign="Right">
                        <ItemStyle HorizontalAlign="Right"/>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="DiningPassPer" FilterControlAltText="Filter column column" UniqueName="DinePer" HeaderAbbr="Di%" HeaderText="Dine%" ItemStyle-HorizontalAlign="Right">
                        <ItemStyle HorizontalAlign="Right" CssClass="hlight2"  />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="RidingPass" FilterControlAltText="Filter column column" UniqueName="Ride" HeaderAbbr="Di" HeaderText="Ride" ItemStyle-HorizontalAlign="Right">
                        <ItemStyle HorizontalAlign="Right"/>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="RidingPassPer" FilterControlAltText="Filter column column" UniqueName="RidePer" HeaderAbbr="Di%" HeaderText="Ride%" ItemStyle-HorizontalAlign="Right">
                        <ItemStyle HorizontalAlign="Right" CssClass="hlight2"  />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ParkRev" FilterControlAltText="Filter column column" UniqueName="ParkRev" HeaderAbbr="Rev$" HeaderText="Rev$" ItemStyle-HorizontalAlign="Right">
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>
                </Columns>
            </MasterTableView>
            </telerik:RadGrid>
        <p class="lastUpdated"><asp:Label ID="lblLastUpdated" runat="server" Text="Data not Updated"></asp:Label></p>


        <telerik:RadGrid ID="TMGrid" runat="server" AutoGenerateColumns="False" Skin="Bootstrap" AllowSorting="True" ShowGroupPanel="True">
        <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
            <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True" AllowDragToGroup="True">
                <Selecting AllowRowSelect="True" />
                <Animation AllowColumnReorderAnimation="True" />
            </ClientSettings>
            <MasterTableView>
                <CommandItemSettings ShowAddNewRecordButton="False" />
                <Columns>
                    <telerik:GridBoundColumn DataField="ParkGroupName" FilterControlAltText="Filter column column" UniqueName="Park" HeaderAbbr="Park" HeaderText="Park" ItemStyle-HorizontalAlign="Left">
                        <ItemStyle HorizontalAlign="Left" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="TeamMemberName" FilterControlAltText="Filter column column" UniqueName="Seller" HeaderAbbr="Seller" HeaderText="Seller" ItemStyle-HorizontalAlign="Left">
                        <ItemStyle HorizontalAlign="Left" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="SPUnits" FilterControlAltText="Filter column column" UniqueName="SP" HeaderAbbr="SP" HeaderText="SP" ItemStyle-HorizontalAlign="Right">
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="MemberUnits" FilterControlAltText="Filter column column" UniqueName="Tot" HeaderAbbr="Tot" HeaderText="Tot" ItemStyle-HorizontalAlign="Right">
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="GoldPlusUnits" FilterControlAltText="Filter column column" UniqueName="GoldP" HeaderAbbr="GP" HeaderText="GP" ItemStyle-HorizontalAlign="Right">
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="PlatinumUnits" FilterControlAltText="Filter column column" UniqueName="Plat" HeaderAbbr="PL" HeaderText="PL" ItemStyle-HorizontalAlign="Right">
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="DiamondUnits" FilterControlAltText="Filter column column" UniqueName="Dia" HeaderAbbr="DI" HeaderText="DI" ItemStyle-HorizontalAlign="Right">
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="DiamondEliteUnits" FilterControlAltText="Filter column column" UniqueName="DiElit" HeaderAbbr="DE" HeaderText="DE" ItemStyle-HorizontalAlign="Right">
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="DiningUnits" FilterControlAltText="Filter column column" UniqueName="Dine" HeaderAbbr="DIN" HeaderText="DN" ItemStyle-HorizontalAlign="Right">
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="RidingUnits" FilterControlAltText="Filter column column" UniqueName="Ride" HeaderAbbr="RID" HeaderText="RD" ItemStyle-HorizontalAlign="Right">
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Revenue" FilterControlAltText="Filter column column" UniqueName="Revenue" HeaderAbbr="Rev$" HeaderText="Rev$" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right">
                        <ItemStyle HorizontalAlign="Right" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="LastSale" FilterControlAltText="Filter column column" UniqueName="LastSale" HeaderAbbr="Last" HeaderText="Last" ItemStyle-HorizontalAlign="Left">
                        <ItemStyle HorizontalAlign="Left" />
                    </telerik:GridBoundColumn>

                </Columns>
                <GroupByExpressions>
                    <telerik:GridGroupByExpression>
                        <GroupByFields>
                            <telerik:GridGroupByField FieldAlias="ParkGroupName" FieldName="ParkGroupName" FormatString="" HeaderText="" />
                        </GroupByFields>
                    </telerik:GridGroupByExpression>
                </GroupByExpressions>
            </MasterTableView>
            </telerik:RadGrid>


    <div class="reportDate">
        <div class="reportDateHeader">Change Report Date:</div>
        <div><telerik:RadDatePicker ID="fldReportDate" runat="server" AutoPostBack="True" Culture="en-US">
            <Calendar EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False">
            </Calendar>
            <DateInput AutoPostBack="True" DateFormat="M/d/yyyy" DisplayDateFormat="M/d/yyyy" LabelWidth="40%">
                <EmptyMessageStyle Resize="None" />
                <ReadOnlyStyle Resize="None" />
                <FocusedStyle Resize="None" />
                <DisabledStyle Resize="None" />
                <InvalidStyle Resize="None" />
                <HoveredStyle Resize="None" />
                <EnabledStyle Resize="None" />
            </DateInput>
            <DatePopupButton HoverImageUrl="" ImageUrl="" />
            </telerik:RadDatePicker></div>
    </div>




    </asp:Panel>
</asp:Content>
