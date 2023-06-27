<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin.Master" CodeBehind="reservationreport.aspx.vb" Inherits="PassholderAdmin.reservationreport" %>
<%@ MasterType VirtualPath="~/Admin.Master" %> 

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

        <style>
        table.parkingclasssummary {
            width: 95%;
            max-width: 500px;
            border-collapse: collapse;
            margin-left: auto;
            margin-right: auto;
        }

        h1 {
            font-family: sans-serif;
            font-size: 18pt;
            background: #ddd;
            padding-top: 5px;
            padding-bottom: 5px;
            text-align: center;
            margin-left: -8px;
            margin-top: -8px;
        }

        div#fldDateToShow_wrapper {
            display: block !important;
            margin-left: auto;
            margin-right: auto;
            margin-bottom: 21px;
        }

        th.clhead {
            text-align: left;
            font-family: sans-serif;
            font-size: 11pt;
            text-align: center;
        }

        td {
            font-family: sans-serif;
            padding-top: 5px;
            padding-bottom: 5px;
        }

        .col1 {
            text-align: left;
            padding-left: 10px;
            border: 1px solid #777;
        }

        .col2, .col3, .col4 {
            text-align: center;
            border: 1px solid #777;
            border-collapse: collapse;
        }

        table.controlBlock {
            margin-left: auto;
            margin-right: auto;
            margin-bottom: 15px;
            display: block;
            width: 670px;
        }

        table.controlBlock td {
            vertical-align: top;
        padding: 5px;
        }

        #ContentPlaceHolder1_pnlReport h2 {
            margin-left: auto;
            margin-right: auto;
            text-align: center;
            margin-top: 12px;
        }

        #ContentPlaceHolder1_pnlReport h1 {
            margin: 0px;
            padding: 0px;
            width: 100%;
            padding-top: 4px;
            padding-bottom: 4px;
            box-shadow: 0px 0px 5px -1px #000;
            margin-bottom: 23px;
        }

    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet">
            <div>
                <asp:Panel ID="pnlReport" runat="server">

                    <h2>Report Settings</h2>


                    <table class="controlBlock">
                        <tr>
                            <td>
                                <telerik:RadDropDownList ID="fldParkID" AutoPostBack="true" Width="85px" runat="server"></telerik:RadDropDownList>
                            </td>
                            <td>
                                <telerik:RadDropDownList ID="fldReservationType" AutoPostBack="true" Width="250px" runat="server"></telerik:RadDropDownList>
                            </td>
                            <td>
                                <telerik:RadDropDownList ID="fldReservationLocationID" Width="250px" runat="server"></telerik:RadDropDownList>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="fldDateToShow" runat="server" AutoPostBack="True" Culture="en-US" ShowPopupOnFocus="True">
                                    <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;"></Calendar>
                                    <DateInput DisplayDateFormat="M/d/yyyy" DateFormat="M/d/yyyy" LabelWidth="40%" AutoPostBack="True">
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
                            </td>
                            <td>
                                <telerik:RadButton ID="cmdUpdate" runat="server" Text="Update"></telerik:RadButton>
                            </td>
                        </tr>
                    </table>

                    <h1>Upcoming Reservations</h1>

                    <telerik:RadGrid ID="fldSummary" runat="server" AutoGenerateColumns="False" Skin="Bootstrap" AllowSorting="True" AllowPaging="True" CssClass="miniTable" PageSize="7">
                        <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                        <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                            <Selecting AllowRowSelect="True" />
                            <Animation AllowColumnReorderAnimation="True" />
                        </ClientSettings>
                        <MasterTableView>
                            <CommandItemSettings ShowAddNewRecordButton="False" />
                            <Columns>
                                <telerik:GridBoundColumn DataField="CalDate" FilterControlAltText="Filter column column" UniqueName="VisitDate" DataFormatString="{0:MMM d}" HeaderAbbr="Date" HeaderText="Date" ItemStyle-HorizontalAlign="Left">
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Capacity" FilterControlAltText="Filter column column" UniqueName="Capacity" HeaderAbbr="Cap" HeaderText="Capacity" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Reserved" FilterControlAltText="Filter column column" UniqueName="Reserved" HeaderAbbr="Rsvd" HeaderText="Reserved" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="WaitList" FilterControlAltText="Filter column column" UniqueName="WaitList" HeaderAbbr="Wait" HeaderText="Wait List" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Cancelled" FilterControlAltText="Filter column column" UniqueName="Cancelled" HeaderAbbr="Canc" HeaderText="Cancelled" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                        <PagerStyle PageButtonCount="5" ShowPagerText="False" />
                    </telerik:RadGrid>


                    <asp:Label ID="lblListHeader" runat="server" Text="Reservations for Today"></asp:Label>

                    <telerik:RadGrid ID="fldGrid" runat="server" AutoGenerateColumns="False" Skin="Bootstrap" AllowSorting="True" CssClass="majorTable" Width="800px">
                        <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                        <ExportSettings FileName="Reservations" Pdf-Author="Six Flags Reservations" Pdf-PageBottomMargin=".5in" Pdf-PageTopMargin=".75in" Pdf-PageLeftMargin="0.5in" Pdf-PageRightMargin="0.5in" UseItemStyles="True" HideStructureColumns="True" Pdf-DefaultFontFamily="Arial" Pdf-BorderType="TopAndBottom" Pdf-BorderStyle="Thin" Pdf-BorderColor="Silver" ExportOnlyData="False">
                            <Pdf PageFooterMargin=".1in" PageHeaderMargin=".3in" PageLeftMargin=".4in" PageRightMargin=".4in" PageTitle="Hurricane Harbor Arlington Waterpark Reservations May 15, 2018">
                                <PageHeader>
                                </PageHeader>
                                <PageFooter>
                                    <LeftCell Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;100 Reservations" TextAlign="Left" />
                                    <MiddleCell Text="<?page-number?>" TextAlign="Center"  />
                                    <RightCell Text="Printed: 5/15/2018 10:00&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" TextAlign="Right" />
                                </PageFooter>
                            </Pdf>
                        </ExportSettings>
                        <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                            <Selecting AllowRowSelect="True" />
                            <Animation AllowColumnReorderAnimation="True" />
                        </ClientSettings>
                        <MasterTableView CommandItemDisplay="Top" CssClass="reservationTable" TableLayout="Fixed">
                            <CommandItemSettings ShowAddNewRecordButton="False" ShowExportToCsvButton="True" ShowRefreshButton="False" ShowExportToPdfButton="true" ShowPrintButton="true" />
                            <Columns>
                                <telerik:GridBoundColumn DataField="ReservationID" FilterControlAltText="Filter column column" UniqueName="ReservationID" HeaderAbbr="ID" HeaderText="ResID" ColumnGroupName="item" ItemStyle-HorizontalAlign="Left"  ItemStyle-Width="70px" FooterStyle-Width="70px" HeaderStyle-Width="70px" FooterStyle-BorderStyle="Dotted" HeaderStyle-HorizontalAlign="Left">
                                    <ItemStyle HorizontalAlign="Left" CssClass="majorCol1" Width="75px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="LastName" FilterControlAltText="Filter column column" UniqueName="LastName" HeaderAbbr="Last" HeaderText="Last Name"  ColumnGroupName="lname" ItemStyle-HorizontalAlign="Left"  ItemStyle-Width="100px" FooterStyle-Width="100px" HeaderStyle-Width="100px"  HeaderStyle-HorizontalAlign="Left" ItemStyle-BackColor="#e4e4e4">
                                    <ItemStyle HorizontalAlign="Left" CssClass="majorCol4" Width="70px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="FirstName" FilterControlAltText="Filter column column" UniqueName="FirstName" HeaderAbbr="First" HeaderText="First Name" ColumnGroupName="fname" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="100px" FooterStyle-Width="100px" HeaderStyle-Width="100px"  HeaderStyle-HorizontalAlign="Left">
                                    <ItemStyle HorizontalAlign="Left" CssClass="majorCol3" Width="70px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="MediaNumber" FilterControlAltText="Filter column column" UniqueName="MediaNumber" HeaderAbbr="Media" HeaderText="Media Number" ColumnGroupName="mid" ItemStyle-HorizontalAlign="Left" Groupable="False" Resizable="True" MaxLength="40" ItemStyle-Width="200px" FooterStyle-Width="200px" HeaderStyle-Width="175px" ShowFilterIcon="False"  HeaderStyle-HorizontalAlign="Left">
                                    <ItemStyle HorizontalAlign="Left" CssClass="majorCol2" Width="300px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="DateCreated" FilterControlAltText="Filter column column" UniqueName="DateCreated" DataFormatString="{0:MMM d}" HeaderAbbr="Rsvd" ColumnGroupName="rsvd" HeaderText="Reserved" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50px"  HeaderStyle-HorizontalAlign="Left">
                                    <ItemStyle HorizontalAlign="Left" CssClass="majorCol5" Width="70px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Used" FilterControlAltText="Filter column column" UniqueName="Park" HeaderAbbr="Used" HeaderText="Used" ItemStyle-HorizontalAlign="Left"  ColumnGroupName="used" ItemStyle-Width="50px" Exportable="False"  HeaderStyle-HorizontalAlign="Left">
                                    <ItemStyle HorizontalAlign="Left" CssClass="majorCol6" Width="50px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridCheckBoxColumn DataType="System.Boolean" FilterControlAltText="Filter column column" HeaderAbbr="Arrived" HeaderText="Arrived"  UniqueName="column" ItemStyle-Width="75px"  Exportable="False">
                                    <ItemStyle HorizontalAlign="Left" CssClass="majorCol7" Width="50px" />
                                </telerik:GridCheckBoxColumn>
                            </Columns>
                            <ItemStyle CssClass="reservationData" />
                            <AlternatingItemStyle CssClass="reservationData altres" />
                            <HeaderStyle CssClass="reservationHeader" />
                        </MasterTableView>
                    </telerik:RadGrid>
<br /><br /><br />
                    <asp:Label ID="lblAllRecords" runat="server" Text="All Records"></asp:Label>

                    <telerik:RadGrid ID="fldReservationStatus" runat="server" AutoGenerateColumns="False" Skin="Bootstrap" AllowSorting="True" CssClass="majorTable" Width="800px">
                        <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                        <ExportSettings FileName="Reservations" Pdf-Author="Six Flags Reservations" Pdf-PageBottomMargin=".5in" Pdf-PageTopMargin=".75in" Pdf-PageLeftMargin="0.5in" Pdf-PageRightMargin="0.5in" UseItemStyles="True" HideStructureColumns="True" Pdf-DefaultFontFamily="Arial" Pdf-BorderType="TopAndBottom" Pdf-BorderStyle="Thin" Pdf-BorderColor="Silver" ExportOnlyData="False">
                            <Pdf PageFooterMargin=".1in" PageHeaderMargin=".3in" PageLeftMargin=".4in" PageRightMargin=".4in" PageTitle="Hurricane Harbor Arlington Waterpark Reservations May 15, 2018">
                                <PageHeader>
                                </PageHeader>
                                <PageFooter>
                                    <LeftCell Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;100 Reservations" TextAlign="Left" />
                                    <MiddleCell Text="<?page-number?>" TextAlign="Center"  />
                                    <RightCell Text="Printed: 5/15/2018 10:00&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" TextAlign="Right" />
                                </PageFooter>
                            </Pdf>
                        </ExportSettings>
                        <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                            <Selecting AllowRowSelect="True" />
                            <Animation AllowColumnReorderAnimation="True" />
                        </ClientSettings>
                        <MasterTableView CommandItemDisplay="Top" CssClass="reservationTable" TableLayout="Fixed">
                            <CommandItemSettings ShowAddNewRecordButton="False" ShowExportToCsvButton="True" ShowRefreshButton="False" ShowExportToPdfButton="true" ShowPrintButton="true" />
                            <Columns>
                                <telerik:GridBoundColumn DataField="ReservationID" FilterControlAltText="Filter column column" UniqueName="ReservationID" HeaderAbbr="ID" HeaderText="ResID" ColumnGroupName="item" ItemStyle-HorizontalAlign="Left"  ItemStyle-Width="70px" FooterStyle-Width="70px" HeaderStyle-Width="70px" FooterStyle-BorderStyle="Dotted" HeaderStyle-HorizontalAlign="Left">
                                    <ItemStyle HorizontalAlign="Left" CssClass="majorCol1" Width="75px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="LastName" FilterControlAltText="Filter column column" UniqueName="LastName" HeaderAbbr="Last" HeaderText="Last Name"  ColumnGroupName="lname" ItemStyle-HorizontalAlign="Left"  ItemStyle-Width="100px" FooterStyle-Width="100px" HeaderStyle-Width="100px"  HeaderStyle-HorizontalAlign="Left" ItemStyle-BackColor="#e4e4e4">
                                    <ItemStyle HorizontalAlign="Left" CssClass="majorCol4" Width="70px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="FirstName" FilterControlAltText="Filter column column" UniqueName="FirstName" HeaderAbbr="First" HeaderText="First Name" ColumnGroupName="fname" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="100px" FooterStyle-Width="100px" HeaderStyle-Width="100px"  HeaderStyle-HorizontalAlign="Left">
                                    <ItemStyle HorizontalAlign="Left" CssClass="majorCol3" Width="70px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="MediaNumber" FilterControlAltText="Filter column column" UniqueName="MediaNumber" HeaderAbbr="Media" HeaderText="Media Number" ColumnGroupName="mid" ItemStyle-HorizontalAlign="Left" Groupable="False" Resizable="True" MaxLength="40" ItemStyle-Width="200px" FooterStyle-Width="200px" HeaderStyle-Width="175px" ShowFilterIcon="False"  HeaderStyle-HorizontalAlign="Left">
                                    <ItemStyle HorizontalAlign="Left" CssClass="majorCol2" Width="300px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="DateCreated" FilterControlAltText="Filter column column" UniqueName="DateCreated" DataFormatString="{0:MMM d}" HeaderAbbr="Rsvd" ColumnGroupName="rsvd" HeaderText="Reserved" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50px"  HeaderStyle-HorizontalAlign="Left">
                                    <ItemStyle HorizontalAlign="Left" CssClass="majorCol5" Width="70px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Used" FilterControlAltText="Filter column column" UniqueName="Park" HeaderAbbr="Used" HeaderText="Used" ItemStyle-HorizontalAlign="Left"  ColumnGroupName="used" ItemStyle-Width="50px" Exportable="False"  HeaderStyle-HorizontalAlign="Left">
                                    <ItemStyle HorizontalAlign="Left" CssClass="majorCol6" Width="50px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ReservationStatus" FilterControlAltText="Filter column column" UniqueName="ReservationStatus" HeaderAbbr="Status" HeaderText="Status" ColumnGroupName="stat" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="100px" FooterStyle-Width="100px" HeaderStyle-Width="100px"  HeaderStyle-HorizontalAlign="Left">
                                    <ItemStyle HorizontalAlign="Left" CssClass="majorCol3" Width="70px" />
                                </telerik:GridBoundColumn>
                            </Columns>
                            <ItemStyle CssClass="reservationData" />
                            <AlternatingItemStyle CssClass="reservationData altres" />
                            <HeaderStyle CssClass="reservationHeader" />
                        </MasterTableView>
                    </telerik:RadGrid>


                </asp:Panel>
            </div>
        </telerik:RadAjaxPanel>

</asp:Content>
