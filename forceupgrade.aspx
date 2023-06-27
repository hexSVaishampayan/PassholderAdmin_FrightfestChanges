<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Admin.Master" CodeBehind="forceupgrade.aspx.vb" Inherits="PassholderAdmin.forceupgrade" %>
<%@ MasterType VirtualPath="~/Admin.Master" %> 

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .entrySection {
            padding-left: 11px;
            padding-bottom: 16px;
            border-radius: 10px;
            padding-top: 0px;
            box-shadow: 0px 0px 11px -4px #000;
            border: 1px solid #ddd;
            margin-left: -11px;
        }

        .entrySection h3 {
            margin-top: 13px;
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:panel id="pnlForm" runat="server">

        <asp:Panel ID="pnlAccountsSP" runat="server">

            <h2>Season Pass Upgrade to Membership</h2>
            <p>Use this form to "upgrade" a Season Pass for promotional credit. Please note that it may take a couple of hours before
                the coupons appear in the guest's account.</p>
            <asp:Panel ID="pnlPassEntry" runat="server">

                <div class="entrySection">
                    <asp:Panel ID="pnlPassEntryReview" runat="server" Visible="false">
                        <h3>Passes You've Entered So Far</h3>
                        <asp:ListBox ID="fldSubmittedPassList" runat="server" style="width:400px;"></asp:ListBox>
                        <p class="addRemoveRow"><asp:Button ID="cmdRemoveSelected" CssClass="cmdRemoveSelected" runat="server" Text="Remove Selected" />
                        </p>
                    </asp:Panel>

                    <asp:Panel ID="pnlPassEntryAdd" runat="server">

                    <h3>Add Passes You Want Credit For</h3>
    
                        <p class="fieldHeader">Pass ID</p>
                        <p class="field"><asp:TextBox ID="fldNewPassMediaNumber" Width="260px" Text="" runat="server"></asp:TextBox></p>
        
                        <div class="buttonentry">
                            <asp:Button ID="cmdAcctSave" CssClass="actionButton" runat="server" Text="Add" />
                        </div>
                    </asp:Panel> 

                </div>

                <h3>Where Do We Assign the Bucks?</h3>


                <p class="fieldHeader">New Accesso Order ID</p>
                <p><asp:TextBox ID="fldNewAccessoOrderID" style="width:300px;" runat="server"></asp:TextBox></p>

                <p class="fieldHeader">Reason for the Assignment (comment)</p>
                <p><asp:TextBox ID="fldComment" style="width:600px;" runat="server"></asp:TextBox></p>

                <p class="fieldHeader">Media Number to Assign Bucks To</p>
                <p><asp:TextBox ID="fldMediaNumberToAssign" style="width:300px;" runat="server"></asp:TextBox></p>

                <p class="fieldHeader">Amount of Six Flags Bucks:</p>
                <p><asp:TextBox ID="fldAmountToAssign" Width="100px" runat="server"></asp:TextBox></p>

            </asp:Panel>

            <div class="buttonRow">
                <asp:Button ID="cmdAccountsSPBack" CssClass="actionButton cancelButton" runat="server" Text="Back" />
                <asp:Button ID="cmdAccountsSPNext" CssClass="actionButton" runat="server" Text="Submit Request" />
            </div>

        </asp:Panel>

        <asp:Panel ID="pnlAssignConfirm" runat="server">
            <asp:Label ID="lblConfirmText" runat="server" Text="Label"></asp:Label>
            <div class="buttonRow">
                <asp:Button ID="cmdConfirmCancel" CssClass="actionButton cancelButton" runat="server" Text="Back" />
                <asp:Button ID="cmdComfirmExecute" CssClass="actionButton" runat="server" Text="Submit Request" />
            </div>
        </asp:panel>

        <asp:Panel ID="pnlThankYou" runat="server">
            <asp:Label ID="lblThankYou" runat="server" Text="Label"></asp:Label>
            <div class="buttonRow">
                <asp:Button ID="cmdThankYouContinue" CssClass="actionButton" runat="server" Text="Continue" />
            </div>
        </asp:panel>

        <asp:HiddenField ID="hidMediaNumber" runat="server" />
        <asp:HiddenField ID="hidParkID" runat="server" />
        <asp:HiddenField ID="hidBucksCount" runat="server" />
        <asp:HiddenField ID="hidFolioUniqueID" runat="server" />
        <asp:HiddenField ID="hidMemberUpgradeRequestID" runat="server" />


    </asp:panel>

</asp:Content>
