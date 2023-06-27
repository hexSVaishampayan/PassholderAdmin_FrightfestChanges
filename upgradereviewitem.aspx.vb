Imports MembershipLibrary
Public Class upgradereviewitem
    Inherits System.Web.UI.Page

    Public EmailList As PassholderLibrary.EmailTemplateList_Type
    Dim req As MemberUpgradeRequest_Type = Nothing
    Dim acOld As AccessoOrder_Type = Nothing
    Dim acOldPay As AccessoLastPayment_Type = Nothing
    Dim acNew As AccessoOrder_Type = Nothing

    Public Sub Status(s As String, Optional SaveToLog As Boolean = True)
        'If SaveToLog Then ResultTxt += "<p style='color:red;font-size:8pt;font-family:Arial;margin:0px;padding:0px'>" + s + "</p>"
        Response.Write("<p style='color:red;font-size:8pt;font-family:Arial;margin:0px;padding:0px'>" + s + "</p>")
        Response.Flush()
    End Sub


    Public Function MakeListItem(pd As AccessoOrderDetail_Type) As RadListBoxItem
        Dim id As New RadListBoxItem
        Dim productIcon As String = Lookup.ProductTypeIcon(pd.ProductTypeID)
        If pd.ProductTypeID = 53 Then
            If pd.ProductLevelID = 11 Then productIcon = "goldplus.png"
            If pd.ProductLevelID = 12 Then productIcon = "platinum.png"
            If pd.ProductLevelID = 13 Then productIcon = "diamond.png"
            If pd.ProductLevelID = 14 Then productIcon = "diamondelite.png"
        End If
        If productIcon <> "" Then productIcon = "https://static.sixflags.com/website/membership/producticons/" + productIcon
        id.Text = pd.FirstName + " " + pd.LastName
        id.Value = pd.LineItemID
        If pd.VoidReasonID = 999 Then
            pd.VoidDate = DefaultDate
            pd.VoidReasonID = 0
        End If
        With id.Attributes
            .Add("LineItemID", pd.LineItemID.ToString)
            .Add("ProductIcon", productIcon)
            .Add("StoreFrontProductID", pd.StoreFrontProductID.ToString)
            .Add("MediaNumber", pd.MediaNumber.ToString)
            .Add("FolioUniqueID", pd.FolioUniqueID.ToString)
            .Add("StoreFrontProductName", pd.StoreFrontProductName)
            .Add("FullName", pd.FirstName + " " + pd.LastName)
            .Add("ParkID", pd.ParkID.ToString)
            .Add("ProductTypeID", pd.ProductTypeID.ToString)
            .Add("ProductLevelID", pd.ProductLevelID.ToString)
            .Add("Age", If(pd.Age > 0, ", age " + pd.Age.ToString, ""))
            .Add("Status", If(pd.VoidDate <> DefaultDate, "<span class=passVoid>Void</span>", "<span class=passActive>Active</span>"))
            .Add("SmallStatus", If(pd.VoidDate <> DefaultDate, "<span class=passVoid2>Void: " + pd.VoidDate.ToShortDateString + ": " + Lookup.VoidCodeDescription(pd.VoidReasonID) + "</span>", "<span class=passActive2>Membership is Active</span>"))
            .Add("Price", pd.UnitPrice.ToString("$0.00"))
            If pd.VoidDate <> DefaultDate Then id.Enabled = False
        End With
        Return id
    End Function

    Public Function MakeListItemSP(pd As MemberUpgradeRequestPassInfo_Type) As RadListBoxItem
        Dim id As New RadListBoxItem
        Dim productIcon As String = Lookup.ProductTypeIcon(pd.ProductTypeID)
        If pd.ProductTypeID = 53 Then
            If pd.ProductLevelID = 11 Then productIcon = "goldplus.png"
            If pd.ProductLevelID = 12 Then productIcon = "platinum.png"
            If pd.ProductLevelID = 13 Then productIcon = "diamond.png"
            If pd.ProductLevelID = 14 Then productIcon = "diamondelite.png"
        End If
        If productIcon <> "" Then productIcon = "https://static.sixflags.com/website/membership/producticons/" + productIcon
        id.Text = pd.FirstName + " " + pd.LastName
        id.Value = pd.MemberUpgradeRequestPassID
        With id.Attributes
            .Add("MemberUpgradeRequestPassID", pd.MemberUpgradeRequestPassID.ToString)
            .Add("MemberUpgradeRequestID", pd.MemberUpgradeRequestID.ToString)
            .Add("RedeemableUniqueID", pd.RedeemableUniqueID.ToString)
            .Add("FolioUniqueID", pd.FolioUniqueID.ToString)
            .Add("MediaNumber", pd.MediaNumber)
            .Add("FullName", pd.FirstName + " " + pd.LastName)
            .Add("PurchaseDateEntered", pd.PurchaseDateEntered.ToString("MM/dd/yyyy"))
            .Add("OrderDate", "Purchased" + pd.OrderDate.ToString("MMMM d, yyyy"))
            .Add("PurchasePrice", pd.PurchasePrice.ToString("$0.00"))
            .Add("CreditAmount", cInteger(pd.PurchasePrice).ToString("0"))
            .Add("ParkID", pd.ParkID.ToString)
            .Add("parkName", ParkNameLong(pd.ParkID))
            .Add("ProductTypeID", pd.ProductTypeID.ToString)
            .Add("ProductLevelID", pd.ProductLevelID.ToString)
            .Add("Age", pd.Age.ToString)
            .Add("Email", pd.Email)
            .Add("VisitCount", pd.VisitCount)
            .Add("VoidReason", Lookup.VoidCodeDescription(pd.VoidReasonID))
            .Add("ProductIcon", productIcon)
            .Add("StoreFrontProductID", pd.StoreFrontProductID.ToString)
            .Add("StoreFrontProductName", pd.StoreFrontProductName)
            .Add("Age", If(pd.Age > 0, ", age " + pd.Age.ToString, ""))
            .Add("Status", If(pd.VoidDate <> DefaultDate, "<span class=passVoid>Void</span>", "<span class=passActive>Active</span>"))
            If pd.VoidDate <> DefaultDate Then id.Enabled = False
            If pd.SixFlagsBucksGranted = 0 Then
                .Add("SmallStatus", If(pd.VoidDate <> DefaultDate, "<span class=passVoid2>Void: " + pd.VoidDate.ToShortDateString + ": " + Lookup.VoidCodeDescription(pd.VoidReasonID) + "</span>", "<span class=passActive2>Pass is Active</span>"))
            Else
                .Add("SmallStatus", "<span class=passActive2>Granted " + pd.SixFlagsBucksGranted.ToString + " Six Flags Bucks</span>")
            End If
        End With
        If pd.SixFlagsBucksGranted > 0 Then
            id.Enabled = False
        End If
        Return id
    End Function

    Public Sub PreparePassList(myBox As RadListBox, MemberUpgradeRequestID As Integer)
        Dim pd() As MemberUpgradeRequestPassInfo_Type = MemberUpgradeRequestPassInfo_Type.FindByMemberUpgradeRequestID(MemberUpgradeRequestID)
        myBox.Items.Clear()
        If Not pd Is Nothing AndAlso pd.Length > 0 Then
            For i As Integer = 0 To pd.Length - 1
                If Not pd(i) Is Nothing Then
                    myBox.Items.Add(MakeListItemSP(pd(i)))
                    myBox.DataBind()
                End If
            Next
        Else
            Master.AddError("Unable to find any passes associated with this request. It is very odd.")
        End If
    End Sub

    Public Sub PrepareListBox(myBox As RadListBox, AccessoOrderID As Integer)
        Dim pd() As AccessoOrderDetail_Type = AccessoOrderDetail_Type.FindByAccessoOrderID(AccessoOrderID)
        myBox.Items.Clear()
        Dim Res As String = ""
        If Not pd Is Nothing Then
            AccessoOrderDetail_Type.FillInNames(pd)
            For i As Integer = 0 To pd.Length - 1
                If pd(i).isAdmissionPass Then
                    myBox.Items.Add(MakeListItem(pd(i)))
                    myBox.DataBind()
                End If
            Next
            For i As Integer = 0 To pd.Length - 1
                If pd(i).isDiningPass Then
                    myBox.Items.Add(MakeListItem(pd(i)))
                    myBox.DataBind()
                End If
            Next
            For i As Integer = 0 To pd.Length - 1
                If pd(i).isParkingPass Then
                    myBox.Items.Add(MakeListItem(pd(i)))
                    myBox.DataBind()
                End If
            Next
            For i As Integer = 0 To pd.Length - 1
                If pd(i).isDeposit Then
                    myBox.Items.Add(MakeListItem(pd(i)))
                    myBox.DataBind()
                End If
            Next
            For i As Integer = 0 To pd.Length - 1
                If Not pd(i).isAdmissionPass And Not pd(i).isParkingPass And Not pd(i).isDiningPass And Not pd(i).isDeposit Then
                    myBox.Items.Add(MakeListItem(pd(i)))
                    myBox.DataBind()
                End If
            Next
        End If
        If Res <> "" Then
            Res = MakeHTMLTable("passlineitems", "thd", Res, "LineItem", "PLU", "Product", "Name", "Age", "Processed", "VoidDate", "VoidReason", "Price")
        End If
    End Sub


    Public Function ReplaceRefundCodes(Template As String,
                                 NewOrderDate As Date,
                                 DaysSinceLastPayment As Integer,
                                 OldPaymentAmount As Decimal,
                                 NextPaymentDate As Date,
                                 PercentEnjoyed As Decimal,
                                 LastPaymentDate As Date,
                                 PercentRefundDate As Decimal,
                                 RefundDue As Decimal
                                 ) As String
        Dim rg As String = Template
        rg = rg.Replace("%%neworderdate%%", NewOrderDate.ToString("MMMM d"))
        rg = rg.Replace("%%dayssincelast%%", DaysSinceLastPayment.ToString)
        rg = rg.Replace("%%oldpayment%%", OldPaymentAmount.ToString("$0.00"))
        rg = rg.Replace("%%nextpaydate%%", NextPaymentDate.ToString("MMMM d"))
        rg = rg.Replace("%%percentenjoyed%%", PercentEnjoyed.ToString("0%"))
        rg = rg.Replace("%%oldpaymentdate%%", LastPaymentDate.ToString("MMMM d"))
        rg = rg.Replace("%%percentrefunddue%%", PercentRefundDate.ToString("0%"))
        rg = rg.Replace("%%refunddue%%", RefundDue.ToString("$0.00"))
        Return rg
    End Function


    Public Sub DisplayRecommendedRefund()
        pnlRefundInfo.Visible = False
        LoadOrderVariables()
        If Not req Is Nothing And Not acNew Is Nothing And Not acOld Is Nothing Then
            Dim SelectedItemMonthlyCost As Decimal = 0
            Dim ItemsRefunded As Integer = 0
            Dim PassesRefunded As Integer = 0
            If cmdCancelOldOrder.Text <> "Cancel Entire Order" Then
                If Not lstOldItems.SelectedItems Is Nothing Then
                    For i As Integer = 0 To lstOldItems.Items.Count - 1
                        If lstOldItems.Items.Item(i).Selected Then
                            Dim acl As AccessoOrderDetail_Type = AccessoOrderDetail_Type.FindByLineItemID(lstOldItems.Items.Item(i).Value)
                            SelectedItemMonthlyCost += acl.UnitPrice + acl.UnitTax
                            ItemsRefunded += 1
                            If acl.isAdmissionPass Then PassesRefunded += 1
                        End If
                    Next
                End If
            End If
            If Not acOldPay Is Nothing AndAlso acOldPay.PaymentNum > 1 Then
                Dim DaysSinceLastPayment As Integer = DateDiff(DateInterval.Day, acOldPay.PaymentDate, acNew.OrderDate)
                Dim NextPaymentDate As Date = MemberUtilities.NextPaymentDate(acOld.OrderDate, acOldPay.PaymentDate)
                Dim DaysBetweenPayments As Integer = DateDiff(DateInterval.Day, acOldPay.PaymentDate, NextPaymentDate)
                Dim PercentValueUsed As Decimal = (DaysSinceLastPayment / DaysBetweenPayments)
                Dim PercentRefundDue As Decimal = (1 - (DaysSinceLastPayment / DaysBetweenPayments))
                Dim MonthlyPaymentAmount As Decimal = AccessoOrder_Type.MonthlyMembershipPayment(acOld.AccessoOrderID)
                Dim RefundDue As Decimal = 0
                If cmdCancelOldOrder.Text = "Cancel Entire Order" Then
                    Try
                        RefundDue = (acOldPay.AmtReceived * (1 - (DaysSinceLastPayment / DaysBetweenPayments)))
                        litOriginalOrder.Text = acOldPay.ApplyCodes(litOriginalOrder.Text)
                        litNewOrder.Text = litNewOrder.Text.Replace("%%daysafterlastpayment%%", DaysSinceLastPayment.ToString + " days")
                        Dim template As String = "<p><b>Full Order Refund Guidance:</b> New order was %%neworderdate%%, <b>%%dayssincelast%% days</b> after their last payment of <b>%%oldpayment%%</b>. Their next payment would have taken place on %%nextpaydate%%  which means they enjoyed %%percentenjoyed%% of their most recent payment's value between %%oldpaymentdate%% and %%neworderdate%%, and are entitled to a <b>%%percentrefunddue%%</b> refund of their most recent payment.</p><p class='refundrow'>Refund Should likely be: <span class=refundamount>%%refunddue%%</span></p>"
                        Dim guesttemplate As String = "Your new order was placed on %%neworderdate%%, which was %%dayssincelast%% days after your last payment of %%oldpayment%%. Your next payment (for your old account) would have taken place on %%nextpaydate%% which means that we are able to credit your payment account with a refund of %%refunddue%%, equivalent to %%percentrefunddue%% of your most recent payment.</p>"
                        fldRefundDescription.Value = ReplaceRefundCodes(template, acNew.OrderDate, DaysSinceLastPayment, acOldPay.AmtReceived, NextPaymentDate, PercentValueUsed, acOldPay.PaymentDate, PercentRefundDue, RefundDue)
                        fldRefundDescriptionForGuest.Value = ReplaceRefundCodes(guesttemplate, acNew.OrderDate, DaysSinceLastPayment, acOldPay.AmtReceived, NextPaymentDate, PercentValueUsed, acOldPay.PaymentDate, PercentRefundDue, RefundDue)
                        lblRefundGuidance.Text = fldRefundDescription.Value
                        fldRefundRecommended.Value = RefundDue
                    Catch ex As Exception
                        Master.AddError("Error: " + ex.Message)
                    End Try
                Else ' Partial refund
                    Try
                        RefundDue = (SelectedItemMonthlyCost * (1 - (DaysSinceLastPayment / DaysBetweenPayments)))
                        If Not acOldPay Is Nothing Then litOriginalOrder.Text = acOldPay.ApplyCodes(litOriginalOrder.Text)
                        litNewOrder.Text = litNewOrder.Text.Replace("%%daysafterlastpayment%%", DaysSinceLastPayment.ToString + " days")
                        Dim template As String = "<p><b>Partial Order Refund Guidance:</b> New order was %%neworderdate%%, <b>%%dayssincelast%% days</b> after their last payment of <b>%%oldpayment%%</b> for the " + ItemsRefunded.SpellNumber + " selected items (" + PassesRefunded.ToString + " passes). Their next payment would have taken place on %%nextpaydate%%  which means they enjoyed %%percentenjoyed%% of their most recent payment's value between %%oldpaymentdate%% and %%neworderdate%%, and are entitled to a <b>%%percentrefunddue%%</b> refund of their most recent payment.</p><p class='refundrow'>Refund Should likely be: <span class=refundamount>%%refunddue%%</span></p>"
                        Dim guesttemplate As String = "Your new order was placed on %%neworderdate%%, which was %%dayssincelast%% days after your last payment of %%oldpayment%% for the " + PassesRefunded.ToString + " passes we cancelled for you. Your next payment (for your old account) would have taken place on %%nextpaydate%% which means that we are able to credit your payment account with a refund of %%refunddue%%, equivalent to %%percentrefunddue%% of your most recent payment for the items we cancelled.</p>"
                        fldRefundDescription.Value = ReplaceRefundCodes(template, acNew.OrderDate, DaysSinceLastPayment, SelectedItemMonthlyCost, NextPaymentDate, PercentValueUsed, acOldPay.PaymentDate, PercentRefundDue, RefundDue)
                        fldRefundDescriptionForGuest.Value = ReplaceRefundCodes(guesttemplate, acNew.OrderDate, DaysSinceLastPayment, acOldPay.AmtReceived, NextPaymentDate, PercentValueUsed, acOldPay.PaymentDate, PercentRefundDue, RefundDue)
                        lblRefundGuidance.Text = fldRefundDescription.Value
                        fldRefundRecommended.Value = RefundDue
                    Catch ex As Exception
                        Master.AddError("Error: " + ex.Message)
                    End Try
                End If
                If RefundDue > 0 And (RefundDue <= MonthlyPaymentAmount) Then pnlRefundInfo.Visible = True
            Else
                Dim DaysSinceLastPayment As Integer = DateDiff(DateInterval.Day, acOld.OrderDate, acNew.OrderDate)
                Dim NextPaymentDate As Date = MemberUtilities.NextPaymentDate(acOld.OrderDate, acOld.OrderDate)
                Dim DaysBetweenPayments As Integer = DateDiff(DateInterval.Day, acOld.OrderDate, NextPaymentDate)
                Dim PercentValueUsed As Decimal = (DaysSinceLastPayment / DaysBetweenPayments)
                Dim PercentRefundDue As Decimal = (1 - (DaysSinceLastPayment / DaysBetweenPayments))
                Dim MonthlyPaymentAmount As Decimal = AccessoOrder_Type.MonthlyMembershipPayment(acOld.AccessoOrderID)
                Dim RefundDue As Decimal = 0
                If DaysSinceLastPayment < 30 Then
                    If cmdCancelOldOrder.Text = "Cancel Entire Order" Then
                        Try
                            RefundDue = (MonthlyPaymentAmount * (1 - (DaysSinceLastPayment / DaysBetweenPayments)))
                            If Not acOldPay Is Nothing Then litOriginalOrder.Text = acOldPay.ApplyCodes(litOriginalOrder.Text)
                            litOriginalOrder.Text = litOriginalOrder.Text.Replace("%%paymentdate%%", acOld.OrderDate)
                            litOriginalOrder.Text = litOriginalOrder.Text.Replace("%%amtreceived%%", acOld.OrderAmount)
                            litNewOrder.Text = litNewOrder.Text.Replace("%%daysafterlastpayment%%", DaysSinceLastPayment.ToString + " days")
                            Dim template As String = "<p><b>Full NEW Order Refund Guidance:</b> New order was %%neworderdate%%, <b>%%dayssincelast%% days</b> after their purchase date. Their next payment would have taken place on %%nextpaydate%% for <b>%%oldpayment%%</b> which means they enjoyed %%percentenjoyed%% of their most initial payment's value between %%oldpaymentdate%% and %%neworderdate%%, and are entitled to a <b>%%percentrefunddue%%</b> refund of their initial payment (for those items).</p><p class='refundrow'>Refund Should likely be: <span class=refundamount>%%refunddue%%</span></p>"
                            Dim guesttemplate As String = "Your new order was placed on %%neworderdate%%, which was %%dayssincelast%% days after your initial order on %%oldpaymentdate%%. Your next payment (for your initial account) would have taken place on %%nextpaydate%% which means that we are able to credit your payment account with a refund of %%refunddue%%, equivalent to %%percentrefunddue%% of the Membership products on your initial order.</p>"
                            fldRefundDescription.Value = ReplaceRefundCodes(template, acNew.OrderDate, DaysSinceLastPayment, acOldPay.AmtReceived, NextPaymentDate, PercentValueUsed, acOldPay.PaymentDate, PercentRefundDue, RefundDue)
                            fldRefundDescriptionForGuest.Value = ReplaceRefundCodes(guesttemplate, acNew.OrderDate, DaysSinceLastPayment, acOldPay.AmtReceived, NextPaymentDate, PercentValueUsed, acOldPay.PaymentDate, PercentRefundDue, RefundDue)
                            lblRefundGuidance.Text = fldRefundDescription.Value
                            fldRefundRecommended.Value = RefundDue
                        Catch ex As Exception
                            Master.AddError("Error: " + ex.Message)
                        End Try
                    Else ' Partial refund
                        Try
                            RefundDue = (SelectedItemMonthlyCost * (1 - (DaysSinceLastPayment / DaysBetweenPayments)))
                            litOriginalOrder.Text = acOldPay.ApplyCodes(litOriginalOrder.Text)
                            litOriginalOrder.Text = litOriginalOrder.Text.Replace("%%paymentdate%%", acOld.OrderDate)
                            litOriginalOrder.Text = litOriginalOrder.Text.Replace("%%amtreceived%%", acOld.OrderAmount)
                            litNewOrder.Text = litNewOrder.Text.Replace("%%daysafterlastpayment%%", DaysSinceLastPayment.ToString + " days")
                            Dim template As String = "<p><b>Partial NEW Order Refund Guidance:</b> New order was %%neworderdate%%, <b>%%dayssincelast%% days</b> after their purchase date of the " + ItemsRefunded.SpellNumber + " selected items (" + PassesRefunded.ToString + " passes). Their next payment would have taken place on %%nextpaydate%%  which means they enjoyed %%percentenjoyed%% of their most recent payment's value between %%oldpaymentdate%% and %%neworderdate%%, and are entitled to a <b>%%percentrefunddue%%</b> refund of their most recent payment for those items.</p><p class='refundrow'>Refund Should likely be: <span class=refundamount>%%refunddue%%</span></p>"
                            Dim guesttemplate As String = "Your new order was placed on %%neworderdate%%, which was %%dayssincelast%% days after your last payment of %%oldpayment%% for the " + PassesRefunded.ToString + " passes we cancelled for you. Your next payment (for your old account) would have taken place on %%nextpaydate%% which means that we are able to credit your payment account with a refund of %%refunddue%%, equivalent to %%percentrefunddue%% of your most recent payment for the items we cancelled.</p>"
                            fldRefundDescription.Value = ReplaceRefundCodes(template, acNew.OrderDate, DaysSinceLastPayment, SelectedItemMonthlyCost, NextPaymentDate, PercentValueUsed, acOldPay.PaymentDate, PercentRefundDue, RefundDue)
                            fldRefundDescriptionForGuest.Value = ReplaceRefundCodes(guesttemplate, acNew.OrderDate, DaysSinceLastPayment, acOldPay.AmtReceived, NextPaymentDate, PercentValueUsed, acOldPay.PaymentDate, PercentRefundDue, RefundDue)
                            lblRefundGuidance.Text = fldRefundDescription.Value
                            fldRefundRecommended.Value = RefundDue
                        Catch ex As Exception
                            Master.AddError("Error: " + ex.Message)
                        End Try
                    End If
                End If
                If RefundDue > 0 And (RefundDue <= MonthlyPaymentAmount) Then pnlRefundInfo.Visible = True
            End If
        Else
            litNewOrder.Text = litNewOrder.Text.Replace("%%daysafterlastpayment%%", "n/a")
        End If

    End Sub

    Public Sub LoadOrderVariables(Optional Reload As Boolean = False)
        If req Is Nothing Or Reload Then req = MemberUpgradeRequest_Type.FindByMemberUpgradeRequestID(fldMemberUpgradeRequestID.Value)
        If Not req Is Nothing Then
            If req.OldAccessoOrderID > 0 Then
                If acOld Is Nothing Or Reload Then acOld = AccessoOrder_Type.FindByAccessoOrderID(req.OldAccessoOrderID)
                If acOldPay Is Nothing Or Reload Then acOldPay = AccessoLastPayment_Type.FindByAccessoOrderID(acOld.AccessoOrderID)
            End If
            If req.NewAccessoOrderID > 0 And (acNew Is Nothing Or Reload) Then acNew = AccessoOrder_Type.FindByAccessoOrderID(req.NewAccessoOrderID)
        End If
    End Sub

    Protected Sub lstOldItems_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstOldItems.SelectedIndexChanged
        LoadOrderVariables()
        Dim selectedCount As Integer = 0
        If Not lstOldItems.SelectedItems Is Nothing AndAlso lstOldItems.SelectedItems.Count > 0 Then
            cmdCancelOldOrder.Text = "Cancel Selected Items"
        Else
            cmdCancelOldOrder.Text = "Cancel Entire Order"
        End If
        DisplayRecommendedRefund()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim AjaxManager As RadAjaxManager = RadAjaxManager.GetCurrent(Page)
        AjaxManager.AjaxSettings.AddAjaxSetting(lstNewItems, fldConsiderationAmount)
        AjaxManager.AjaxSettings.AddAjaxSetting(lstNewItems, lstNewItems)
        AjaxManager.AjaxSettings.AddAjaxSetting(lstOldPasses, fldConsiderationAmount)
        AjaxManager.AjaxSettings.AddAjaxSetting(lstOldPasses, lstOldPasses)
        AjaxManager.AjaxSettings.AddAjaxSetting(lstOldPasses, cmdPromoSaveEdit)
        AjaxManager.AjaxSettings.AddAjaxSetting(lstNewItems, cmdPromoSaveEdit)
        AjaxManager.AjaxSettings.AddAjaxSetting(fldConsiderationAmount, cmdPromoSaveEdit)
        AjaxManager.AjaxSettings.AddAjaxSetting(cmdPromoSaveEdit, fldConsiderationAmount)
        AjaxManager.AjaxSettings.AddAjaxSetting(cmdPromoSaveEdit, lstNewItems)
        AjaxManager.AjaxSettings.AddAjaxSetting(cmdPromoSaveEdit, lstOldPasses)
        AjaxManager.AjaxSettings.AddAjaxSetting(cmdPromoSaveEdit, fldMemberUpgradeRequestStatusID)
        AjaxManager.AjaxSettings.AddAjaxSetting(pnlSendMessage, pnlSendMessage)
        AjaxManager.AjaxSettings.AddAjaxSetting(cmdSendMessage, pnlSendMessage)
        AjaxManager.AjaxSettings.AddAjaxSetting(cmdMessage, pnlSendMessage)
        AjaxManager.AjaxSettings.AddAjaxSetting(pnlSendMessage, fldMemberUpgradeRequestStatusID)
        EmailList = New PassholderLibrary.EmailTemplateList_Type(Master.ResetCache)
        If Not IsPostBack Then
            Master.sfToken = AppToken_Type.GetCurrentToken()
            If Master.sfToken.hasPermission(501) Then
                Dim ee() As PassholderLibrary.EmailTemplate_Type = EmailList.FindByTag("upgrades", False, Master.ResetCache)
                If Not ee Is Nothing AndAlso ee.Length > 0 Then
                    For i As Integer = 0 To ee.Length - 1
                        Dim li As New ListItem(ee(i).EmailTemplateDesc, ee(i).EmailTemplateCode)
                        fldSendMessage.Items.Add(li)
                    Next
                End If
                pnlSendMessage.Visible = False
                pnlForm.Visible = True
                cmdCancelOldOrder.Text = "Cancel Entire Order"
                Dim reqid As Integer = ReadHTTPValue("mur", 0)
                If reqid <> 0 Then
                    fldMemberUpgradeRequestID.Value = reqid
                    LoadOrderVariables()
                    If Not req Is Nothing Then
                        SetDropDown(fldMemberUpgradeRequestStatusID, req.MemberUpgradeRequestStatusID)
                        litRequestInfo.Text = req.ApplyCodes(litRequestInfo.Text).Replace("1/1/1980", "&nbsp;")
                        fldMemberUpgradeRequestTypeID.Value = req.MemberUpgradeRequestTypeID
                        If req.RefundApplied > 0 Then
                            fldRefundAmount.Text = req.RefundApplied.ToString("$0.00")
                            fldRefundAmount.Enabled = False
                        End If
                        pnlNewOrder.Visible = False
                        If req.NewAccessoOrderID > 0 Then
                            If Not acNew Is Nothing Then
                                litNewOrder.Text = acNew.ApplyCodes(litNewOrder.Text)
                                pnlNewOrder.Visible = True
                                PrepareListBox(lstNewItems, req.NewAccessoOrderID)
                                fldNewOrderID.Value = req.NewAccessoOrderID
                            Else
                                Master.AddError("New order was not found!")
                            End If
                        Else
                            Master.AddError("New Order Accesso Order ID was 0 (blank)")
                        End If
                        pnlOldOrder.Visible = False
                        If req.MemberUpgradeRequestTypeID = 1 Then
                            pnlPromoInfo.Visible = False
                            If req.OldAccessoOrderID > 0 Then
                                If Not acOld Is Nothing Then
                                    litOriginalOrder.Text = acOld.ApplyCodes(litOriginalOrder.Text)
                                    pnlOldOrder.Visible = True
                                    PrepareListBox(lstOldItems, req.OldAccessoOrderID)
                                    fldOldOrderID.Value = req.OldAccessoOrderID
                                    DisplayRecommendedRefund()
                                Else
                                    Master.AddError("Old order number entered by guest isn't valid")
                                End If
                            Else
                                Master.AddError("Old Accesso Order ID = 0. Can't display anything.")
                            End If
                        End If
                        pnlOldPasses.Visible = False
                        If req.MemberUpgradeRequestTypeID = 2 Then
                            pnlPromoInfo.Visible = True
                            PreparePassList(lstOldPasses, req.MemberUpgradeRequestID)
                            If lstOldPasses.Items.Count > 0 Then pnlOldPasses.Visible = True
                            litNewOrder.Text = litNewOrder.Text.Replace("%%daysafterlastpayment%%", "n/a")
                            pnlRefundInfo.Visible = False
                            SetGrantStatus()
                            cmdCancelOldOrder.Enabled = False
                            cmdCancelOldOrder.Text = "Use Grant Button Below"
                            cmdCancelOldOrder.CssClass = cmdCancelOldOrder.CssClass.addCSSClass("disbleGrantButton")
                            If req.RefundApplied > 0 Then
                                cmdPromoSaveEdit.Enabled = False
                                cmdPromoSaveEdit.Text = "Done!"
                                fldConsiderationAmount.Enabled = False
                                fldConsiderationAmount.Text = Math.Round(req.RefundApplied, 0).ToString
                            End If
                        End If
                    Else
                        Master.AddError("Member Upgrade Request is blank!")
                    End If
                Else
                    Master.AddError("Upgrade request identifier was invalid.")
                End If
            Else
                Status("Access denied.")
                pnlForm.Visible = False
                Master.SetTitle("Access Denied")
                Master.AddError("Access Denied")
            End If
        End If
        Master.DisplayErrors()
    End Sub


    Public Sub CancelEntireOrder()
        ' First verify that they aren't trying to cancel just a few items
        Dim AccessoOrderID As Integer = fldOldOrderID.Value.ConvertToInteger
        If AccessoOrderID > 200000000 Then
            SetDropDown(fldMemberUpgradeRequestStatusID, 2)
            Dim CancelDate As Date = #12/31/2050#
            Dim CRec As New Cancellation_Type
            Dim ac As AccessoOrder_Type = AccessoOrder_Type.FindByAccessoOrderID(AccessoOrderID)
            If Not ac Is Nothing Then
                With CRec
                    .AccessoOrderID = ac.AccessoOrderID
                    .ServiceRepID = 989
                    .Email = ac.Email
                    .GuestSignature = "GUEST SERVICES"
                    .DateCreated = Now()
                    .ParkID = ac.ParkID
                    .PurchaserName = ac.FullName
                    .Active = True
                    .Save()
                End With
                Dim cr As Accesso.CancellationResult_Type = CRec.CancelAccount(CancelDate, True, 77)
                If Not cr Is Nothing Then
                    If cr.Status = "OK" Or cr.ErrorMessage.Contains("memberships have already been cancelled") Then
                        Dim req As MemberUpgradeRequest_Type = MemberUpgradeRequest_Type.FindByMemberUpgradeRequestID(fldMemberUpgradeRequestID.Value.ConvertToInteger)
                        If Not req Is Nothing Then
                            PrepareListBox(lstOldItems, req.OldAccessoOrderID)
                            req.DateCancelled = Now()
                            req.DateReviewed = Now()
                            req.MemberUpgradeRequestStatusID = 2
                            req.Save()
                            SetDropDown(fldMemberUpgradeRequestStatusID, 2)
                            Dim em As PassholderLibrary.EmailTemplate_Type = EmailList.FindByEmailTemplateCode("OLDORDERCANCELLED")
                            If Not em Is Nothing Then
                                em.Subject = req.ApplyCodes(CRec.ApplyCodes(em.Subject))
                                em.EmailHTML = req.ApplyCodes(CRec.ApplyCodes(em.EmailHTML))
                                em.EmailFrom = "Six Flags Member Services"
                                em.SendMessage(CRec.Email, CRec.PurchaserName)
                            End If
                        End If
                        For i As Integer = 0 To lstOldItems.Items.Count - 1
                            AccessoOrderLineItem_Type.MarkVoid(lstOldItems.Items.Item(i).Value, 77)
                        Next
                        Master.AddError("The guest's account has been scheduled for immediate cancellation. It will not be reflected here for 24 hours.")
                        pnlNewOrder.Visible = False
                        If req.NewAccessoOrderID > 0 Then
                            Dim ac1 As AccessoOrder_Type = AccessoOrder_Type.FindByAccessoOrderID(req.NewAccessoOrderID)
                            If Not ac1 Is Nothing Then
                                litNewOrder.Text = ac1.ApplyCodes(litNewOrder.Text)
                                pnlNewOrder.Visible = True
                                PrepareListBox(lstNewItems, req.NewAccessoOrderID)
                                fldNewOrderID.Value = req.NewAccessoOrderID
                                lstNewItems.Enabled = False
                            End If
                        End If
                        Response.Redirect(Request.Url.PathAndQuery)
                    Else
                        Master.AddError("There was an error cancelling the guest's account when we submitted it to Accesso. The error was '" + cr.ErrorMessage + "'. You may need to go into Support Manager to finish the cancellation.")
                    End If
                Else
                    Master.AddError("There was an error cancelling the guest's account when we submitted it to Accesso. No error message was returned! You may need to go into Support Manager to finish the cancellation.")
                End If
            Else
                Master.AddError("There was an error cancelling the guest's account. We were unable to load the guest's record from the Marketing Database!! You may need to go into Support Manager to finish the cancellation.")
            End If
        End If
    End Sub

    Public Sub CancelIndividualItems()
        Try
            Dim CancelCount As Integer = 0
            Dim SuccessCount As Integer = 0
            Dim CancelDate As Date = Now()
            Dim xList As String = ""
            If Not lstOldItems.SelectedItems Is Nothing Then
                For i As Integer = 0 To lstOldItems.Items.Count - 1
                    If lstOldItems.Items.Item(i).Selected Then
                        CancelCount += 1
                        Dim cr As Accesso.CancellationResult_Type = Accesso.RemoveMembershipbyLineItemID(cBigInteger(lstOldItems.Items.Item(i).Value), CancelDate,,, 77,, False)
                        If cr.Status = "OK" Then
                            Master.AddError(cr.GuestFriendlyResultHTML)
                            lstOldItems.Items.Item(i).Enabled = False
                            AccessoOrderLineItem_Type.MarkVoid(lstOldItems.Items.Item(i).Value, 77)
                            xList += cr.GuestFriendlyResultHTML
                            SuccessCount += 1
                        Else
                            Master.AddError("Error Cancelling Ticket #" + cr.MediaNumber + " (" + cr.FirstName + " " + cr.LastName + "). Please cancel in Accesso Support Manager.")
                        End If
                    End If
                Next
            End If
            If SuccessCount = CancelCount Then
                Dim req As MemberUpgradeRequest_Type = MemberUpgradeRequest_Type.FindByMemberUpgradeRequestID(fldMemberUpgradeRequestID.Value.ConvertToInteger)
                If Not req Is Nothing Then
                    req.DateCancelled = Now()
                    req.DateReviewed = Now()
                    req.MemberUpgradeRequestStatusID = 3
                    SetDropDown(fldMemberUpgradeRequestStatusID, 3)
                    req.Save()
                    Dim em As PassholderLibrary.EmailTemplate_Type = EmailList.FindByEmailTemplateCode("OLDORDERCANCELLEDITEM")
                    If Not em Is Nothing Then
                        Dim ac As AccessoOrder_Type = AccessoOrder_Type.FindByAccessoOrderID(req.OldAccessoOrderID)
                        If Not ac Is Nothing Then
                            em.Subject = req.ApplyCodes(ac.ApplyCodes(em.Subject))
                            em.EmailHTML = req.ApplyCodes(ac.ApplyCodes(em.EmailHTML)).Replace("%%itemlist%%", xList)
                            em.EmailFrom = "Six Flags Member Services"
                            em.SendMessage(ac.Email, (ac.FirstName + " " + ac.LastName))
                        End If
                    End If
                    pnlOldOrder.Visible = False
                    If req.OldAccessoOrderID > 0 Then
                        Dim ac1 As AccessoOrder_Type = AccessoOrder_Type.FindByAccessoOrderID(req.OldAccessoOrderID)
                        If Not ac1 Is Nothing Then
                            litOriginalOrder.Text = ac1.ApplyCodes(litOriginalOrder.Text)
                            pnlOldOrder.Visible = True
                            PrepareListBox(lstOldItems, req.OldAccessoOrderID)
                            fldOldOrderID.Value = req.OldAccessoOrderID
                            cmdCancelOldOrder.Text = "Cancel Entire Order"
                        Else
                            Master.AddError("ac1 is nothing (part 2)")
                        End If
                    Else
                        Master.AddError("req.OldAccessoOrderID = 0")
                    End If
                End If
            End If
        Catch ex As Exception
            ReportErr("cmdCancelList_Click", ex, "")
        End Try
        Master.DisplayErrors()
    End Sub


    Protected Sub cmdCancelOldOrder_Click(sender As Object, e As EventArgs) Handles cmdCancelOldOrder.Click
        Dim selectedCount As Integer = 0
        If Not lstOldItems.SelectedItems Is Nothing AndAlso lstOldItems.SelectedItems.Count > 0 Then
            CancelIndividualItems()
        Else
            CancelEntireOrder()
        End If
        Master.DisplayErrors()
    End Sub

    Protected Sub cmdReturnToList_Click(sender As Object, e As EventArgs) Handles cmdReturnToList.Click
        Response.Redirect("upgradereview.aspx")
    End Sub

    Protected Sub fldMemberUpgradeRequestStatusID_SelectedIndexChanged(sender As Object, e As EventArgs) Handles fldMemberUpgradeRequestStatusID.SelectedIndexChanged
        Dim req As MemberUpgradeRequest_Type = MemberUpgradeRequest_Type.FindByMemberUpgradeRequestID(fldMemberUpgradeRequestID.Value.ConvertToInteger)
        If Not req Is Nothing Then
            req.MemberUpgradeRequestStatusID = fldMemberUpgradeRequestStatusID.SelectedValue.ConvertToInteger
            req.Save()
            litRequestInfo.Text = req.ApplyCodes(litRequestInfo.Text)
        End If

    End Sub

    Protected Sub fldSendMessage_SelectedIndexChanged(sender As Object, e As EventArgs) Handles fldSendMessage.SelectedIndexChanged
        If fldSendMessage.SelectedValue <> "0" Then
            pnlSendMessage.Visible = True
            Dim em As PassholderLibrary.EmailTemplate_Type = EmailList.FindByEmailTemplateCode(fldSendMessage.SelectedValue)
            If Not em Is Nothing Then fldMessageEditor.Content = em.EmailHTML
        Else
            pnlSendMessage.Visible = False
        End If
    End Sub

    Public Sub PrepEmailForEditing(req As MemberUpgradeRequest_Type, Optional TemplateName As String = "GENERICMSG")
        If Not req Is Nothing Then
            pnlSendMessage.Visible = True
            Dim ac1 As AccessoOrder_Type = AccessoOrder_Type.FindByAccessoOrderID(req.OldAccessoOrderID)
            Dim ac2 As AccessoOrder_Type = AccessoOrder_Type.FindByAccessoOrderID(req.NewAccessoOrderID)
            fldMessageEmailDrop.Items.Clear()
            fldMessageEmailDrop.Items.Add(New ListItem("Other Email", "Other"))
            SetDropDown(fldMessageEmailDrop, "Other")
            If Not ac1 Is Nothing Then
                fldMessageEmailDrop.Items.Add(New ListItem(ac1.Email + " (Original Order)", ac1.AccessoOrderID.ToString))
                SetDropDown(fldMessageEmailDrop, ac1.AccessoOrderID.ToString)
            End If
            If Not ac2 Is Nothing Then
                fldMessageEmailDrop.Items.Add(New ListItem(ac2.Email + " (New Order)", ac2.AccessoOrderID.ToString))
                SetDropDown(fldMessageEmailDrop, ac2.AccessoOrderID.ToString)
            End If
            Dim em As PassholderLibrary.EmailTemplate_Type = EmailList.FindByEmailTemplateCode(TemplateName)
            If Not em Is Nothing Then
                fldMessageEditor.Content = req.ApplyCodes(em.EmailHTML)
                fldMessageSubject.Text = req.ApplyCodes(em.Subject)
                If Not ac1 Is Nothing Then
                    fldMessageEditor.Content = ac1.ApplyCodes(fldMessageEditor.Content, "old")
                    fldMessageSubject.Text = ac1.ApplyCodes(fldMessageSubject.Text, "old")
                End If
                If Not ac2 Is Nothing Then
                    fldMessageEditor.Content = ac2.ApplyCodes(fldMessageEditor.Content, "new")
                    fldMessageSubject.Text = ac2.ApplyCodes(fldMessageSubject.Text, "new")
                End If
                If Not ac1 Is Nothing Then
                    fldMessageEditor.Content = ac1.ApplyCodes(fldMessageEditor.Content, "old")
                    fldMessageSubject.Text = ac1.ApplyCodes(fldMessageSubject.Text, "old")
                End If
                If Not ac2 Is Nothing Then
                    fldMessageEditor.Content = ac2.ApplyCodes(fldMessageEditor.Content, "new")
                    fldMessageSubject.Text = ac2.ApplyCodes(fldMessageSubject.Text, "new")
                End If
            End If
            SetDropDown(fldSendMessage, TemplateName)
        End If
    End Sub

    Protected Sub cmdMessage_Click(sender As Object, e As EventArgs) Handles cmdMessage.Click
        Dim req As MemberUpgradeRequest_Type = MemberUpgradeRequest_Type.FindByMemberUpgradeRequestID(fldMemberUpgradeRequestID.Value.ConvertToInteger)
        PrepEmailForEditing(req)
    End Sub

    Protected Sub cmdCancelMessage_Click(sender As Object, e As EventArgs) Handles cmdCancelMessage.Click
        pnlSendMessage.Visible = False
    End Sub

    Public Sub SendEditedMessage()
        Dim msgtext As String = fldMessageEditor.Content
        Dim msgsubject As String = fldMessageSubject.Text
        Dim em As PassholderLibrary.EmailTemplate_Type = EmailList.FindByEmailTemplateCode(fldSendMessage.SelectedValue)
        Dim Email As String = fldMessageEmail.Text.Trim.ToLower
        Dim GuestName As String = "Valued Guest"
        If fldMessageEmailDrop.SelectedValue.IsNumbersOnly Then
            Dim ac As AccessoOrder_Type = AccessoOrder_Type.FindByAccessoOrderID(fldMessageEmailDrop.SelectedValue)
            If Not ac Is Nothing Then
                Email = ac.Email
                GuestName = ac.FirstName + " " + ac.LastName
                msgsubject = ac.ApplyCodes(msgsubject)
                msgtext = ac.ApplyCodes(msgtext)
            End If
        End If
        em.EmailFrom = "Six Flags Member Services"
        em.Subject = msgsubject
        em.EmailHTML = msgtext
        em.SendMessage(Email, GuestName)
        Master.AddError("Message sent to " + GuestName + " (" + Email + ")")
        pnlSendMessage.Visible = False
        fldMessageEditor.Content = ""
        fldMessageSubject.Text = ""
        Master.DisplayErrors()
    End Sub

    Protected Sub cmdSendMessage_Click(sender As Object, e As EventArgs) Handles cmdSendMessage.Click
        SendEditedMessage()
    End Sub

    Protected Sub cmdPreviousRecord_Click(sender As Object, e As EventArgs) Handles cmdPreviousRecord.Click
        Dim req As MemberUpgradeRequest_Type = MemberUpgradeRequest_Type.FindPreviousRequest(fldMemberUpgradeRequestID.Value, If(IsReallyNumeric(Master.MemberUpgradeRequestStatusID), Master.MemberUpgradeRequestStatusID, 1))
        If Not req Is Nothing AndAlso req.MemberUpgradeRequestID.ToString <> "" Then
            Response.Redirect("upgradereviewitem.aspx?mur=" + req.MemberUpgradeRequestID.ToString)
        Else
            Master.AddError("Already reached the beginning of the queue.")
            Master.DisplayErrors()
        End If
    End Sub

    Protected Sub cmdNextRecord_Click(sender As Object, e As EventArgs) Handles cmdNextRecord.Click
        Dim req As MemberUpgradeRequest_Type = MemberUpgradeRequest_Type.FindNextRequest(fldMemberUpgradeRequestID.Value, If(IsReallyNumeric(Master.MemberUpgradeRequestStatusID), Master.MemberUpgradeRequestStatusID, 1))
        If Not req Is Nothing AndAlso req.MemberUpgradeRequestID.ToString <> "" Then
            Response.Redirect("upgradereviewitem.aspx?mur=" + req.MemberUpgradeRequestID.ToString)
        Else
            Master.AddError("Already reached the end of the queue.")
            Master.DisplayErrors()
        End If
    End Sub

    Public Sub RecordRefund(EditMessageFirst As Boolean)
        fldRefundAmount.Text = fldRefundAmount.Text.Trim.FilterToJustNumbersWithDecimal
        If fldRefundAmount.Text.Length > 0 Then
            Dim req As MemberUpgradeRequest_Type = MemberUpgradeRequest_Type.FindByMemberUpgradeRequestID(fldMemberUpgradeRequestID.Value)
            Dim RefundAmount As Decimal = CDec(fldRefundAmount.Text)
            If RefundAmount > 0 Then
                If Not req Is Nothing Then
                    Dim rr As MemberRefundRequest_Type = MemberRefundRequest_Type.SubmitRequest(req.OldAccessoOrderID, CDec(RefundAmount))
                    If Not rr Is Nothing Then
                        If rr.Success AndAlso rr.Status = "OK" Then
                            req.RefundSuggested = rr.Amount
                            req.RefundApplied = rr.Amount
                            req.RefundDescription = fldRefundDescription.Value
                            req.RefundDescriptionForGuest = fldRefundDescriptionForGuest.Value
                            req.Save()
                            PrepEmailForEditing(req, "REFUNDAPPLIED")
                            If Not EditMessageFirst Then
                                SendEditedMessage()
                                Master.AddError(("Refund of {{amount}} applied to the guest's account and email sent to guest. No further action in the Accesso system is required!").Replace("{{amount}}", rr.Amount.ToString("$0.00")))
                            Else
                                Master.AddError(("Refund of {{amount}} applied to the guest's account. No further action in the Accesso system is required! Now edit the email and send it when you are read.").Replace("{{amount}}", rr.Amount.ToString("$0.00")))
                            End If
                        Else
                            Master.AddError("Error automatically applying refund to guest's account in accesso system. You must apply it manually. " + If(rr.ErrorMessage.Length > 3, "Error: " + rr.ErrorMessage, ""))
                        End If
                    End If
                Else
                    Master.AddError("Error identifying request in RecordRefund()")
                End If
            End If
        Else
            Master.AddError("There isn't a valid refund amount listed. Nothing was recorded!")
        End If
        Master.DisplayErrors()
    End Sub

    Protected Sub cmdRefundSave_Click(sender As Object, e As EventArgs) Handles cmdRefundSave.Click
        RecordRefund(False)
    End Sub

    Protected Sub cmdRefundEdit_Click(sender As Object, e As EventArgs) Handles cmdRefundEdit.Click
        RecordRefund(True)
    End Sub

    Private Sub lstOldPasses_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstOldPasses.SelectedIndexChanged
        Dim val As Integer = 0
        For i As Integer = 0 To lstOldPasses.Items.Count - 1
            If lstOldPasses.Items.Item(i).Selected Then val += Math.Ceiling(cDecimal(lstOldPasses.Items.Item(i).Attributes.Item("PurchasePrice")))
        Next
        fldConsiderationAmount.Text = val.ToString
        SetGrantStatus()
    End Sub

    Public Function ReadyToGrant() As Boolean
        If IsNumbersOnly(fldConsiderationAmount.Text) AndAlso fldConsiderationAmount.Text.Trim.Length > 0 And cInteger(fldConsiderationAmount.Text) > 0 Then
            LoadOrderVariables()
            If req.RefundApplied = 0 Then
                Dim SPCount As Integer = 0
                If lstOldPasses.Items.Count > 0 Then
                    For i As Integer = 0 To lstOldPasses.Items.Count - 1
                        If lstOldPasses.Items.Item(i).Selected Then SPCount += 1
                    Next
                End If
                If SPCount > 0 Then
                    Dim MemCount As Integer = 0
                    For i As Integer = 0 To lstNewItems.Items.Count - 1
                        If lstNewItems.Items.Item(i).Attributes.Item("ProductTypeID") = 53 Then
                            If lstNewItems.Items.Item(i).Selected Then
                                MemCount += 1
                            End If
                        End If
                    Next
                    If MemCount = 1 Then
                        Return True
                    End If
                End If
            End If
        End If
        Return False
    End Function

    Public Sub SetGrantStatus()
        cmdPromoSaveEdit.Enabled = ReadyToGrant()
    End Sub

    Private Sub lstNewItems_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstNewItems.SelectedIndexChanged
        Dim MemberUpgradeRequestTypeID As Integer = cInteger(fldMemberUpgradeRequestTypeID.Value)
        If MemberUpgradeRequestTypeID = 1 Then
            For i As Integer = 0 To lstNewItems.Items.Count - 1
                lstNewItems.Items.Item(i).Selected = False
            Next
        ElseIf MemberUpgradeRequestTypeID = 2 Then
            Dim CheckedCount As Integer = 0
            For i As Integer = 0 To lstNewItems.Items.Count - 1
                If lstNewItems.Items.Item(i).Attributes.Item("ProductTypeID") = 53 Then
                    If lstNewItems.Items.Item(i).Selected Then
                        CheckedCount += 1
                        If CheckedCount > 1 Then lstNewItems.Items.Item(i).Selected = False
                    End If
                Else
                    lstNewItems.Items.Item(i).Selected = False
                End If
            Next
        End If
        SetGrantStatus()
    End Sub


    Public Sub PrepPromoEmailForEditing(req As MemberUpgradeRequest_Type, TemplateName As String, SixFlagsBucksAmount As Integer, ParkID As Integer, GuestName As String, MediaNumber As String)
        If Not req Is Nothing Then
            pnlSendMessage.Visible = True
            Dim ac2 As AccessoOrder_Type = AccessoOrder_Type.FindByAccessoOrderID(req.NewAccessoOrderID)
            fldMessageEmailDrop.Items.Clear()
            fldMessageEmailDrop.Items.Add(New ListItem("Other Email", "Other"))
            SetDropDown(fldMessageEmailDrop, "Other")
            If Not ac2 Is Nothing Then
                fldMessageEmailDrop.Items.Add(New ListItem(ac2.Email + " (New Order)", ac2.AccessoOrderID.ToString))
                SetDropDown(fldMessageEmailDrop, ac2.AccessoOrderID.ToString)
            End If
            Dim em As PassholderLibrary.EmailTemplate_Type = EmailList.FindByEmailTemplateCode(TemplateName)
            If Not em Is Nothing Then
                fldMessageEditor.Content = ac2.ApplyCodes(em.EmailHTML.Replace("%%buckscount%%", SixFlagsBucksAmount.ToString).Replace("%%membername%%", GuestName).Replace("%%medianumber%%", MediaNumber).Replace("%%validpark%%", ParkNameLong(ParkID)))
                fldMessageSubject.Text = ac2.ApplyCodes(em.Subject.Replace("%%buckscount%%", SixFlagsBucksAmount.ToString).Replace("%%membername%%", GuestName).Replace("%%medianumber%%", MediaNumber).Replace("%%validpark%%", ParkNameLong(ParkID)))
            End If
            SetDropDown(fldSendMessage, TemplateName)
        End If
    End Sub

    Protected Sub cmdPromoSaveEdit_Click(sender As Object, e As EventArgs) Handles cmdPromoSaveEdit.Click
        LoadOrderVariables()
        If ReadyToGrant() Then
            Dim ParkID As Integer = 0
            Dim FolioUniqueID As String = ""
            Dim GuestName As String = ""
            Dim MediaNumber As String = ""
            Dim MemCount As Integer = 0
            Dim ConsiderationAmount As Integer = cInteger(fldConsiderationAmount.Text)
            For i As Integer = 0 To lstNewItems.Items.Count - 1
                If lstNewItems.Items.Item(i).Attributes.Item("ProductTypeID") = 53 Then
                    If lstNewItems.Items.Item(i).Selected Then
                        ParkID = cInteger(lstNewItems.Items.Item(i).Attributes.Item("ParkID"), 0)
                        FolioUniqueID = lstNewItems.Items.Item(i).Attributes.Item("FolioUniqueID")
                        GuestName = lstNewItems.Items.Item(i).Attributes.Item("FullName")
                        MediaNumber = lstNewItems.Items.Item(i).Attributes.Item("MediaNumber")
                        Exit For
                    End If
                End If
            Next
            If ConsiderationAmount > 0 And FolioUniqueID.Length > 10 And ParkID > 0 And req.RefundApplied = 0 Then
                BenefitsLibrary.Coupon_Type.AssignSixFlagsBucks(ParkID, FolioUniqueID, ConsiderationAmount)
                With req
                    .DateCancelled = Now
                    .DateConfirmationEmail = Now
                    .DateReviewed = Now
                    .RefundApplied = ConsiderationAmount
                    .LastName = GuestName
                    .MemberUpgradeRequestStatusID = 9
                    .Save()
                End With
                Dim cUnits As Integer = 0
                For i As Integer = 0 To lstOldPasses.Items.Count - 1
                    If lstOldPasses.Items.Item(i).Selected Then cUnits = cUnits + 1
                Next
                For i As Integer = 0 To lstOldPasses.Items.Count - 1
                    If lstOldPasses.Items.Item(i).Selected Then
                        Dim li As MemberUpgradeRequestPass_Type = MemberUpgradeRequestPass_Type.FindByMemberUpgradeRequestPassID(lstOldPasses.Items.Item(i).Value)
                        If Not li Is Nothing Then
                            li.SixFlagsBucksGranted = ConsiderationAmount / cUnits
                            li.Save()
                        End If
                    End If
                Next
                cmdPromoSaveEdit.Enabled = False
                cmdPromoSaveEdit.Text = "Done!"
                fldConsiderationAmount.Enabled = False
                lstOldPasses.Enabled = False
                lstNewItems.Enabled = False
                fldConsiderationAmount.Text = Math.Round(req.RefundApplied, 0).ToString
                SetDropDown(fldMemberUpgradeRequestStatusID, 9)
                PrepPromoEmailForEditing(req, "PROMOCONSIDER", ConsiderationAmount, ParkID, GuestName, MediaNumber)
                Master.AddError(("We have assigned %%buckscount%% Six Flags Bucks to %%membername%% (Card #%%medianumber%%) valid at %%parkname%%. Please edit email below appropriately and send it to them so they will know!").Replace("%%buckscount%%", ConsiderationAmount.ToString).Replace("%%membername%%", GuestName).Replace("%%medianumber%%", MediaNumber).Replace("%%validpark%%", ParkNameLong(ParkID)))
            End If
        End If
    End Sub
End Class