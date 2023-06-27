<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin.Master" validateRequest="false" CodeBehind="upgradereviewitem.aspx.vb" Inherits="PassholderAdmin.upgradereviewitem" %>
<%@ MasterType VirtualPath="~/Admin.Master" %> 

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>

        div#ContentPlaceHolder1_pnlPageInfo {
            width: 1000px;
            margin-left: auto;
            margin-right: auto;
            margin-top: 60px;
        }
        .SectionRows {
            display:table;
        }
        .SectionRowInner {
            display:table-cell;
            vertical-align:top;
        }
        h2 {
            margin-top:30px;
        }
        .SectionRowInner {
            display: table-cell;
            vertical-align: top;
            width: 30%;
        }

        td.fdesc {
            font-weight: bold;
            padding-right: 10px;
            color: #4773ab;
        }

        .commentguest {
            border: 1px solid #ddd;
            padding: 10px;
            max-width: 950px;
            margin-top: 0px;
            background: white;
            box-shadow: 0px 0px 10px -3px #000;
            color: blue;
            font-size: 16pt;
        }

        p.commentheader {
            font-weight: bold;
            margin-bottom: 3px;
        }

        table.passlineitems {
            font-size: 9pt;
            width: 100%;
            margin-top: 16px;
            border-collapse:collapse;
        }

        .passlineitems td {
            border-bottom: 1px solid #ccc;
            padding-bottom: 2px;
            padding-top: 2px;
            border-top: 1px solid #ccc;
        }

        .admissionitem {color:red;}
        .deposititem {color:purple;}
        .dineitem {
            color: green;
        }
        .parkitem {color:blue;}
        .otheritem {color:dimgrey;        }

        li {
            margin-bottom: 0%!important;
            max-width: 100%;
        }

        .fldOrderDate {

        }

        span.past11 {
            color:forestgreen;
            font-weight: bold;
            font-size: 11pt;
        }

        span.before11 {
            color: red;
            font-weight: bold;
            font-size: 11pt;
        }

        h2 {
            margin-top: 30px;
            font-size: 20pt;
            margin-bottom: 4px;
            color: #656565;
        }

        .auto-style1 {
            width: 150px;
        }

        .actionButton.cancelOrder {
            background:red;
        }

    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:panel id="pnlForm" runat="server">

        <asp:Panel ID="pnlPageInfo" runat="server">

        <div class="buttonRow">
            <asp:Button ID="cmdCancelOldOrder" CssClass="actionButton cancelOrder" runat="server" Text="Cancel Old Order" />
            <asp:Button ID="cmdMessage" CssClass="actionButton navigation" runat="server" Text="Email Guest" />
            <asp:DropDownList ID="fldMemberUpgradeRequestStatusID" CssClass="changestatusdropdown" runat="server" AutoPostBack="True">
                <asp:ListItem Value="1">Request</asp:ListItem>
                <asp:ListItem Value="2">Order Cancelled</asp:ListItem>
                <asp:ListItem Value="3">Partial Cancellation</asp:ListItem>
                <asp:ListItem Value="4">Upgrade Disallowed</asp:ListItem>
                <asp:ListItem Value="5">Contact Guest</asp:ListItem>
                <asp:ListItem Value="6">Resolved</asp:ListItem>
                <asp:ListItem Value="7">Duplicate</asp:ListItem>
                <asp:ListItem Value="8">Needs Refund</asp:ListItem>
                <asp:ListItem Value="9">Consideration Granted</asp:ListItem>
            </asp:DropDownList>
            <div class="navControls">
                <asp:Button ID="cmdPreviousRecord" CssClass="actionButton navigation" runat="server" Text="&larr;" />
                <asp:Button ID="cmdReturnToList" CssClass="actionButton navigation" runat="server" Text="Return To List" />
                <asp:Button ID="cmdNextRecord" CssClass="actionButton navigation" runat="server" Text="&#10148;" />
            </div>
        </div>
        <div class="buttonRow buttonRow2">
        </div>

        <asp:Panel ID="pnlSendMessage" runat="server">
            <table class="msgtemplaterow"><tr><td style="vertical-align: middle;padding-right: 10px;}" class="auto-style1">Template:</td><td>
                <asp:DropDownList ID="fldSendMessage" runat="server" CssClass="sendmsgbutton" AutoPostBack="True">
                    <asp:ListItem Value="0">Select...</asp:ListItem>
                </asp:DropDownList></td></tr>
            <tr><td class="auto-style1">Subject:</td><td>
                <asp:TextBox ID="fldMessageSubject" runat="server" Width="741px"></asp:TextBox></td></tr>
            <tr><td class="auto-style1">Email:</td><td>
                <asp:DropDownList ID="fldMessageEmailDrop" CssClass="sendmsgbutton" runat="server">
                    <asp:ListItem Value="0">Select...</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="fldMessageEmail" runat="server" Width="373px"></asp:TextBox></td></tr>
            </table>
            


            <telerik:RadEditor ID="fldMessageEditor" runat="server" NewLineBr="False" NewLineMode="Br" Height="600px" Width="1050px">
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
                    </telerik:EditorToolGroup>
                </Tools>
                <Content>
                </Content>
                <TrackChangesSettings CanAcceptTrackChanges="False" />
            </telerik:RadEditor>
        <div class="buttonRow">
            <br />
            <asp:Button ID="cmdSendMessage" CssClass="actionButton" runat="server" Text="Send Email" />
            <asp:Button ID="cmdCancelMessage" CssClass="actionButton cancelButton" runat="server" Text="Cancel Email" />
        </div>

        </asp:Panel>


            <asp:Literal ID="litRequestInfo" runat="server">
                <h2>About The Request</h2>
                <div class="SectionRows">
                    <div class="SectionRowInner">
                    <table class="requestInfoTable">
                        <tr><td class="fdesc">RequestID:</td><td>%%memberupgraderequestid%%</td></tr>
                        <tr><td class="fdesc">Status:</td><td>%%memberupgraderequeststatusdesc%%</td></tr>
                        <tr><td class="fdesc">Guest Last Name:</td><td>%%lastname%%</td></tr>
                    </table>
                    </div>

                    <div class="SectionRowInner">
                    <table class="requestInfoTable">
                        <tr><td class="fdesc">Date Requested:</td><td>%%daterequested%%</td></tr>
                        <tr><td class="fdesc">Date Reviewed:</td><td>%%datereviewed%%</td></tr>
                        <tr><td class="fdesc">Date Cancelled:</td><td>%%datecancelled%%</td></tr>
                        <tr><td class="fdesc">Date Comfirmation Email:</td><td>%%dateconfirmationemail%%</td></tr>
                    </table>
                    </div>

                    <div class="SectionRowInner">
                    <table class="requestInfoTable">
                        <tr><td class="fdesc">Old Account:</td><td>%%oldaccessoorderid%%</td></tr>
                        <tr><td class="fdesc">New Account:</td><td>%%newaccessoorderid%%</td></tr>
                        <tr><td class="fdesc">Dining Account:</td><td>%%oldmemberdiningorderid%%</td></tr>
                    </table>
                    </div>
                </div>

                <div class="commentblock">
                    <p class="commentheader">Guest Comment:</p>
                    <div class="commentguest">%%guestcomment%%</div>
                    </div>

            </asp:Literal>


            <table class="orderSection"><tr><td class="orderBlock">




            <asp:panel id="pnlOldOrder" runat="server">
            <asp:Literal ID="litOriginalOrder" runat="server">
                <h2>Old Order Details</h2>

                <div class="SectionRows">
                    <div class="SectionRowInner">
                        <table class="requestInfoTable xCompare">
                            <tr><td class="fdesc">OrderID:</td><td><span class="orderidno">%%accessoorderid%%</span></td></tr>
                            <tr><td class="fdesc">Park:</td><td>%%parkname%%</td></tr>
                            <tr><td class="fdesc">OrderDate:</td><td class="fldOrderDate">%%orderdatediff%%</td></tr>
                            <tr><td class="fdesc">&nbsp;</td><td class="monthlypayment">&nbsp;</td></tr>
                            <tr><td class="fdesc">Last Payment:</td><td>%%paymentdate%%</td></tr>
                            <tr><td class="fdesc">Last Pay Amount:</td><td>%%amtreceived%%</td></tr>
                            <tr><td class="fdesc">Monthly Pmt:</td><td class="monthlypayment">%%monthlypaymentwithtax%%</td></tr>
                            <tr><td class="fdesc">Guest Name:</td><td>%%firstname%% %%lastname%%</td></tr>
                            <tr><td class="fdesc">Address 1:</td><td>%%address1%%</td></tr>
                            <tr><td class="fdesc">Address 2:</td><td>%%city%%, %%postalcode%%</td></tr>
                            <tr><td class="fdesc">Phone:</td><td>%%phonenumber%%</td></tr>
                            <tr><td class="fdesc">Email:</td><td>%%email%%</td></tr>
                            <tr><td class="fdesc">IP Address:</td><td>%%ipaddress%%</td></tr>
                        </table>
                    </div>
                </div>

            </asp:Literal>
                
                <telerik:RadListBox ID="lstOldItems" runat="server" SelectionMode="Multiple" RenderMode="Lightweight" AutoPostBack="True">
                    <HeaderTemplate>Old (Original) Order Items</HeaderTemplate>
                    <itemTemplate>
                        <div class="revItem">
                            <div class="revImg"><img src='<%# DataBinder.Eval(Container, "Attributes['ProductIcon']") %>' class="imgIcon" /></div>
                            <div class="revLeft">
                                <div class="revName"><span class="revGuest"><%# DataBinder.Eval(Container, "Text")%></span><%# DataBinder.Eval(Container, "Attributes['age']") %></div>
                                <div class="revSub1"><%# DataBinder.Eval(Container, "Attributes['MediaNumber']") %></div>
                                <div class="revSub2"><%# DataBinder.Eval(Container, "Attributes['StoreFrontProductName']") %></div>
                                <div class="revSub2"><%# DataBinder.Eval(Container, "Attributes['SmallStatus']") %></div>
                            </div>                        
                            <div class="revRight">
                                <div class="revStatus"><%# DataBinder.Eval(Container, "Attributes['Status']") %></div>
                                <div class="revPrice"><%# DataBinder.Eval(Container, "Attributes['Price']") %></div>
                            </div>
                        </div>
                    </itemTemplate>
                </telerik:RadListBox>
            </asp:Panel>


            <asp:panel id="pnlOldPasses" runat="server">
            <asp:Literal ID="litOldPasses" runat="server">
                <h2>2018 Season Passes</h2>

                <div class="SectionRows">
                    <div class="SectionRowInner">
                        <div class="requestClearBlock">
                            <div class="requestBlockMessage">
                                (doesn't apply to Season Pass conversions)
                            </div>
                        </div>
                    </div>
                </div>

            </asp:Literal>
                
                <telerik:RadListBox ID="lstOldPasses" runat="server" SelectionMode="Multiple" RenderMode="Lightweight" AutoPostBack="true">
                    <HeaderTemplate>Old (Original) Order Items</HeaderTemplate>
                    <itemTemplate>
                        <div class="revItem">
                            <div class="revImg"><img src='<%# DataBinder.Eval(Container, "Attributes['ProductIcon']") %>' class="imgIcon" /></div>
                            <div class="revLeft">
                                <div class="revName"><span class="revGuest"><%# DataBinder.Eval(Container, "Text")%></span><%# DataBinder.Eval(Container, "Attributes['age']") %></div>
                                <div class="revSub1"><%# DataBinder.Eval(Container, "Attributes['MediaNumber']") %></div>
                                <div class="revSub2"><%# DataBinder.Eval(Container, "Attributes['StoreFrontProductName']") %></div>
                                <div class="revSub2"><%# DataBinder.Eval(Container, "Attributes['ParkName']") %></div>
                                <div class="revSub2"><%# DataBinder.Eval(Container, "Attributes['SmallStatus']") %></div>
                            </div>                        
                            <div class="revRight">
                                <div class="revStatus"><%# DataBinder.Eval(Container, "Attributes['Status']") %></div>
                                <div class="revPrice"><%# DataBinder.Eval(Container, "Attributes['purchaseprice']") %></div>
                            </div>
                        </div>
                    </itemTemplate>
                </telerik:RadListBox>
            </asp:Panel>

            <asp:panel id="pnlRefundInfo" runat="server">
                <h2>Refund Guidance</h2>
                <asp:Label ID="lblRefundGuidance" runat="server" Text="" CssClass="refundGuidance"></asp:Label>
                <table><tr>
                    <td class="refundDesc">Refund Applied:</td>
                    <td><asp:TextBox ID="fldRefundAmount" runat="server" CssClass="refundBox" Text="0"></asp:TextBox></td>
                    <td><asp:Button ID="cmdRefundSave" runat="server" CssClass="refundButton" Text="Submit" /></td>
                    <td><asp:Button ID="cmdRefundEdit" runat="server" CssClass="refundButton" Text="Save/Edit Msg" /></td>
                       </tr></table>

                <p class="refundguidance">
                </p>
            </asp:Panel>

            </td><td class="orderBlock">
            
            <asp:panel id="pnlNewOrder" runat="server">
            <asp:Literal ID="litNewOrder" runat="server">
                <h2>New Order Details</h2>

                <div class="SectionRows">
                    <div class="SectionRowInner">
                        <table class="requestInfoTable xCompare">
                            <tr><td class="fdesc">OrderID:</td><td><span class="orderidno">%%accessoorderid%%</span></td></tr>
                            <tr><td class="fdesc">Park:</td><td>%%parkname%%</td></tr>
                            <tr><td class="fdesc">OrderDate:</td><td>%%orderdate%%</td></tr>
                            <tr><td class="fdesc">Days Since Last Pay:</td><td class="dayssincelast">%%daysafterlastpayment%%</td></tr>
                            <tr><td class="fdesc">Order Total:</td><td>%%orderamount%%</td></tr>
                            <tr><td class="fdesc">Fee:</td><td class="newprocessingfee">%%processingfee%%</td></tr>
                            <tr><td class="fdesc">Monthly Pmt:</td><td class="monthlypayment">%%monthlypaymentwithtax%%</td></tr>
                            <tr><td class="fdesc">Guest Name:</td><td>%%firstname%% %%lastname%%</td></tr>
                            <tr><td class="fdesc">Address 1:</td><td>%%address1%%</td></tr>
                            <tr><td class="fdesc">Address 2:</td><td>%%city%%, %%postalcode%%</td></tr>
                            <tr><td class="fdesc">Phone:</td><td>%%phonenumber%%</td></tr>
                            <tr><td class="fdesc">Email:</td><td>%%email%%</td></tr>
                            <tr><td class="fdesc">IP Address:</td><td>%%ipaddress%%</td></tr>
                        </table>
                    </div>
                </div>
            </asp:Literal>

                <telerik:RadListBox ID="lstNewItems" runat="server" RenderMode="Lightweight" AutoPostBack="true">
                    <HeaderTemplate>New (Original) Order Items</HeaderTemplate>
                    <itemTemplate>
                        <div class="revItem">
                            <div class="revImg"><img src='<%# DataBinder.Eval(Container, "Attributes['ProductIcon']") %>' class="imgIcon" /></div>
                            <div class="revLeft">
                                <div class="revName"><span class="revGuest"><%# DataBinder.Eval(Container, "Text")%></span><%# DataBinder.Eval(Container, "Attributes['age']") %></div>
                                <div class="revSub1"><%# DataBinder.Eval(Container, "Attributes['MediaNumber']") %></div>
                                <div class="revSub2"><%# DataBinder.Eval(Container, "Attributes['StoreFrontProductName']") %></div>
                                <div class="revSub2"><%# DataBinder.Eval(Container, "Attributes['SmallStatus']") %></div>
                            </div>                        
                            <div class="revRight">
                                <div class="revStatus"><%# DataBinder.Eval(Container, "Attributes['Status']") %></div>
                                <div class="revPrice"><%# DataBinder.Eval(Container, "Attributes['Price']") %></div>
                            </div>
                        </div>
                    </itemTemplate>
                </telerik:RadListBox>

            </asp:Panel>
            <asp:panel id="pnlPromoInfo" runat="server">
                <h2>Promotional Consideration Guidance</h2>
                <p class="promoinst">
                    1) On the LEFT, select which Passes are receiving consideration.<br />
                    2) On the RIGHT, select ONE Membership which will receive the coupons.<br />
                    3) Below, enter the number of Six Flags Bucks the guest should get.<br />
                    <B>Coupons will be added as soon as you press the "Save" button!</B>
                </p>
                <asp:Label ID="promoConsiderationGuidance" runat="server" Text="" CssClass="refundGuidance"></asp:Label>
                <table><tr>
                    <td class="refundDesc">Consideration Applied:</td>
                    <td><asp:TextBox ID="fldConsiderationAmount" runat="server" CssClass="refundBox" Text="0"></asp:TextBox></td>
                    <td><asp:Button ID="cmdPromoSaveEdit" runat="server" CssClass="promoButton" Text="Grant/Edit Msg" /></td>
                       </tr></table>

            </asp:Panel>


            </td></tr></table>

        <div class="instructionblock">
        </div>

        </asp:Panel>

    <script>
        function listBoxSelecting(sender, args) {//args.set_cancel(true);
        }
    </script>
    </asp:panel>
    <asp:HiddenField ID="fldNewOrderID" runat="server" />
    <asp:HiddenField ID="fldOldOrderID" runat="server" />
    <asp:HiddenField ID="fldMemberUpgradeRequestID" runat="server" />
    <asp:HiddenField ID="fldMemberUpgradeRequestTypeID" runat="server" />
    <asp:HiddenField ID="fldRefundRecommended" runat="server" />
    <asp:HiddenField ID="fldRefundDescription" runat="server" />
    <asp:HiddenField ID="fldRefundDescriptionForGuest" runat="server" />
    <br />
    <br />
    <br />

</asp:Content>
