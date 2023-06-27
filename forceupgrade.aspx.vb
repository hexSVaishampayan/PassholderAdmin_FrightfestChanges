Public Class forceupgrade
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Master.ResetErrors()
        If Not IsPostBack Then
            Master.sfToken = AppToken_Type.GetCurrentToken()
            pnlAssignConfirm.Visible = False
            pnlThankYou.Visible = False
            If Master.sfToken.hasPermission(501) Or isLocalUser() Then
            Else
                pnlForm.Visible = False
                Master.SetTitle("Access Denied")
                Master.AddError("Access Denied")
            End If
        End If
    End Sub

    Public Sub ReviewRecordPass()
        Try
            fldNewPassMediaNumber.Text = fldNewPassMediaNumber.Text.Trim.FilterToJustNumbers
            Dim MediaNumber As String = fldNewPassMediaNumber.Text
            Dim aMedia As String = Media_Type.ActiveMediaNumber(MediaNumber)
            If aMedia <> "" Then MediaNumber = aMedia
            Dim cg As CouponGuest_Type = CouponGuest_Type.FindByMediaNumber(MediaNumber)
            If Not cg Is Nothing Then
                Dim pa As Pass_Type = Pass_Type.FindFirstAdmissionByFolioUniqueID(cg.folioId)
                Dim lu As MemberUpgradeRequestPassLookup_Type = MemberUpgradeRequestPassLookup_Type.FindByMediaNumber(MediaNumber)
                Dim ac As AccessoOrderDetail_Type = AccessoOrderDetail_Type.FindPassByMediaNumber(MediaNumber)
                Dim UnitPrice As Decimal = 0.00
                Dim tax As Decimal = 0.00
                Dim OrderDate As Date = DefaultDate
                If lu Is Nothing Then
                    lu = New MemberUpgradeRequestPassLookup_Type
                    lu.FolioUniqueID = Guid.Parse(cg.folioId)
                    lu.LastName = cg.lastName
                    If Not pa Is Nothing Then
                        lu.RedeemableUniqueID = pa.RedeemableUniqueID
                        OrderDate = pa.OrderDate
                        UnitPrice = pa.Price
                        tax = pa.Tax
                    End If
                    If Not ac Is Nothing Then
                        OrderDate = ac.OrderDate
                        UnitPrice = ac.UnitTax
                        tax = ac.UnitTax
                    End If
                    lu.UnitPrice = UnitPrice
                    lu.TaxPaid = tax
                    lu.OrderDate = OrderDate
                    lu.MediaNumber = MediaNumber
                    lu.Save()
                End If
                Dim val As String = MediaNumber + "|" + lu.MemberUpgradeRequestPassLookupId.ToString + "|" + cg.lastName + "|" + OrderDate.ToShortDateString + "|" + UnitPrice.ToString("0.00")
                Dim li As New ListItem(MediaNumber + " (" + cg.firstName + " " + cg.lastName + ") - " + UnitPrice.ToString("$0"), val)
                Dim AlreadyExists As Boolean = False
                If fldSubmittedPassList.Items.Count > 0 Then
                    For i As Integer = 0 To fldSubmittedPassList.Items.Count - 1
                        If MediaNumber = fldSubmittedPassList.Items.Item(i).Value.Split("|")(0) Then
                            AlreadyExists = True
                            Master.AddError("You've already entered this pass.")
                        End If
                    Next
                End If
                If Not AlreadyExists Then
                    fldSubmittedPassList.Items.Add(li)
                    fldNewPassMediaNumber.Text = ""
                    pnlPassEntryReview.Visible = True
                    If fldAmountToAssign.Text.Length = 0 Then fldAmountToAssign.Text = "0"
                    If fldAmountToAssign.Text.IsNumbersOnly Then
                        Dim Amt As Integer = fldAmountToAssign.Text.ConvertToInteger(0)
                        Amt += lu.UnitPrice
                        fldAmountToAssign.Text = Amt.ToString
                    End If
                End If
            End If
        Catch ex As Exception
            ReportErr(ex)
            Master.AddError("We're sorry, but there was an error storing this pass entry. Please try again or report code #UPG384")
        End Try
        Master.DisplayErrors()
    End Sub

    Protected Sub cmdRemoveSelected_Click(sender As Object, e As EventArgs) Handles cmdRemoveSelected.Click
        If fldSubmittedPassList.Items.Count > 0 Then
            For i As Integer = 0 To fldSubmittedPassList.Items.Count - 1
                If fldSubmittedPassList.Items(i).Selected Then fldSubmittedPassList.Items.Remove(fldSubmittedPassList.Items(i))
            Next
            If fldSubmittedPassList.Items.Count = 0 Then
                pnlPassEntryReview.Visible = False
                pnlPassEntryAdd.Visible = True
            End If
        Else
            Master.AddError("You don't have any passes entered!")
        End If
        Master.DisplayErrors()
    End Sub

    Protected Sub cmdAcctSave_Click(sender As Object, e As EventArgs) Handles cmdAcctSave.Click
        ReviewRecordPass()
        Master.DisplayErrors()
        fldNewPassMediaNumber.Focus()
    End Sub

    Public Function FormIsValid() As Boolean
        fldAmountToAssign.Text = fldAmountToAssign.Text.Trim.FilterToJustNumbersWithDecimal
        fldNewAccessoOrderID.Text = fldNewAccessoOrderID.Text.Trim.FilterToJustNumbers
        fldComment.Text = fldComment.Text.Trim
        fldMediaNumberToAssign.Text = fldMediaNumberToAssign.Text.Trim.FilterToJustNumbers
        If fldAmountToAssign.Text.Length > 3 Then
            Master.AddError("Cannot assign more than 1000 Six Flags bucks")
        ElseIf fldAmountToAssign.Text.Length = 0 Then
            Master.AddError("Please indicate how many Six Flags bucks you want assigned.")
        ElseIf fldMediaNumberToAssign.Text.Length < 15 Then
            Master.AddError("Please enter a valid media number to assign the Six Flags bucks to.")
        ElseIf fldComment.Text.Length < 4 Then
            Master.AddError("Please indicate a reason for assigning the Six Flags bucks through this process.")
        ElseIf fldNewAccessoOrderID.Text.Length < 9 Then
            Master.AddError("Please indicate the new Accesso Order Id (confirmation number).")
        End If
        If Master.HasErrors Then Return False
        Return True
    End Function

    Protected Sub cmdAccountsSPNext_Click(sender As Object, e As EventArgs) Handles cmdAccountsSPNext.Click
        If FormIsValid() Then
            Dim NewMedia As String = fldMediaNumberToAssign.Text.Trim
            Dim cg As CouponGuest_Type = CouponGuest_Type.FindByMediaNumber(NewMedia)
            If Not cg Is Nothing Then
                Try
                    If Not Master.HasErrors Then
                        For i As Integer = 0 To fldSubmittedPassList.Items.Count - 1
                            Dim ee() As MemberUpgradeRequestPass_Type = MemberUpgradeRequestPass_Type.FindByMediaNumber_AllConnected(fldSubmittedPassList.Items(i).Value.Split("|")(0))
                            If Not ee Is Nothing Then Master.AddError("Pass #" + fldSubmittedPassList.Items(i).Value.Split("|")(0) + " has already been submitted for credit.")
                        Next
                    End If
                Catch ex As Exception
                    ReportErr(ex)
                    Master.AddError("We're sorry -- there was a problem processing this request. Please try again or report code #UPG450")
                End Try
                If Not Master.HasErrors Then
                    Dim Acc As AccessoOrder_Type = AccessoOrder_Type.FindByAccessoOrderID(fldNewAccessoOrderID.Text)
                    If Not Acc Is Nothing Then
                        Dim req As New MemberUpgradeRequest_Type
                        With req
                            .MemberUpgradeRequestStatusID = 1
                            .DateRequested = Today
                            .LastName = Acc.FirstName + " " + Acc.LastName
                            .GuestComment = "Requested by " + Master.sfToken.UserName + " to be added to " + NewMedia + "; " + fldComment.Text
                            .NewAccessoOrderID = cInteger(fldNewAccessoOrderID.Text)
                            .RequestedByFolioUniqueID = cg.FolioUniqueID
                            .MemberUpgradeRequestTypeID = MemberUpgradeRequestType_Enum.SeasonPassUpgrade
                            .Save()
                        End With
                        If req.MemberUpgradeRequestID > 0 Then
                            Try
                                For i As Integer = 0 To fldSubmittedPassList.Items.Count - 1
                                    Dim cols() As String = fldSubmittedPassList.Items.Item(i).Value.Split("|")
                                    If Not cols Is Nothing AndAlso cols.Length = 5 Then
                                        Try
                                            Dim MediaNumber As String = cols(0)
                                            Dim MemberUpgradeRequestPassLookupID As Integer = cInteger(cols(1))
                                            Dim LastName As String = cols(2)
                                            Dim OrderDate As Date = CDate(cols(3))
                                            Dim UnitPrice As Decimal = CDec(cols(4))
                                            Dim Mem As MemberUpgradeRequestPassLookup_Type = MemberUpgradeRequestPassLookup_Type.FindbyMemberUpgradeRequestPassLookupID(MemberUpgradeRequestPassLookupID)
                                            If Not Mem Is Nothing Then
                                                Dim ReqI As New MemberUpgradeRequestPass_Type
                                                With ReqI
                                                    .MemberUpgradeRequestID = req.MemberUpgradeRequestID
                                                    .DateAdded = Today
                                                    .FolioUniqueID = Mem.FolioUniqueID
                                                    .MediaNumberEntered = MediaNumber
                                                    .LastNameEntered = LastName
                                                    .PurchaseDateEntered = OrderDate
                                                    .PurchasePrice = UnitPrice
                                                    .RedeemableUniqueID = Mem.RedeemableUniqueID
                                                    .Save()
                                                End With
                                                lblConfirmText.Text = "Are you sure you want to assign <b>" + fldAmountToAssign.Text + "</b> " + cg.homePark.name + " Six Flags Bucks to " + cg.firstName + " " + cg.lastName + " (Card #" + cg.mediaNumber + ")? Please note that once you confirm this process cannot be undone."
                                                pnlAssignConfirm.Visible = True
                                                pnlAccountsSP.Visible = False
                                                hidBucksCount.Value = fldAmountToAssign.Text
                                                hidParkID.Value = cg.homePark.parkId
                                                hidMediaNumber.Value = cg.mediaNumber
                                                hidMemberUpgradeRequestID.Value = req.MemberUpgradeRequestID
                                                hidFolioUniqueID.Value = cg.FolioUniqueID.ToString
                                            Else
                                                ReportErr("cmdAccountsSPNExt_FailureToFindREQI", fldSubmittedPassList.Items.Item(i).Value)
                                                Master.AddError("There was an error processing your request. Hopefully it was a one-time thing. Please re-add your passes and try again.")
                                                fldSubmittedPassList.Items.Clear()
                                                Exit For
                                            End If
                                        Catch ex As Exception
                                            ReportErr(ex)
                                            Master.AddError("We're sorry -- there was a problem processing this request. Please try again or report code #UPG450")
                                        End Try
                                    Else
                                        Master.AddError("There was an error processing your request. Hopefully it was a one-time thing. Please re-add your passes and try again.")
                                        fldSubmittedPassList.Items.Clear()
                                        Exit For
                                    End If
                                Next
                            Catch ex As Exception
                                ReportErr(ex)
                                Master.AddError("We're sorry -- there was a problem processing this request. Please try again or report code #UPG510")
                            End Try
                        Else
                            Master.AddError("We're sorry -- there was a problem processing this request. Please try again or report code #UPG513")
                        End If
                    Else
                        Master.AddError("Unable to locate Accesso order #" + fldNewAccessoOrderID.Text + ". Please check the number, or wait 15 minutes until the order is recorded in the system.")
                    End If
                End If
            Else
                Master.AddError("The Pass ID #" + NewMedia + " isn't associated with an account that we can identify. Please check the number and try again.")
            End If
        End If
        Master.DisplayErrors()
    End Sub

    Protected Sub cmdComfirmExecute_Click(sender As Object, e As EventArgs) Handles cmdComfirmExecute.Click
        Dim ParkID As Integer = cInteger(hidParkID.Value)
        Dim FolioUniqueID As String = hidFolioUniqueID.Value
        Dim MemberUpgradeRequestID As Integer = hidMemberUpgradeRequestID.Value
        Dim MediaNumber As String = hidMediaNumber.Value
        Dim Amount As Integer = hidBucksCount.Value
        If Amount > 0 And FolioUniqueID <> "" And ParkID > 0 Then
            MemberUpgradeLog_Type.Log(1, MemberUpgradeRequestID, Master.sfToken.AppUserID, MediaNumber,,, "Forced assignment", Amount.ToString)
            BenefitsLibrary.Coupon_Type.AssignSixFlagsBucks(ParkID, FolioUniqueID, Amount)
            pnlAssignConfirm.Visible = False
            pnlThankYou.Visible = True
            lblThankYou.Text = "Process Complete. We've assigned <b>" + Amount.ToString + "</b> Six Flags Bucks to " + MediaNumber + ". It may take a couple of hours before the Bucks appear on the card."
            Dim mp() As MemberUpgradeRequestPass_Type = MemberUpgradeRequestPass_Type.FindByMemberUpgradeRequestID(MemberUpgradeRequestID)
            If Not mp Is Nothing Then
                For i As Integer = 0 To mp.Length - 1
                    mp(i).SixFlagsBucksGranted = Amount / mp.Length
                    mp(i).Save()
                Next
            End If
            Dim req As MemberUpgradeRequest_Type = MemberUpgradeRequest_Type.FindByMemberUpgradeRequestID(MemberUpgradeRequestID)
            If Not req Is Nothing Then
                req.MemberUpgradeRequestStatusID = 9
                req.RefundApplied = Amount
                req.DateReviewed = Now
                req.DateCancelled = Now
                req.Save()
            Else
                Master.AddError("Unable to update request record.")
            End If
        Else
            Master.AddError("Error adding Six Flags Bucks to folio. Action not completed. Please try again.")
            MemberUpgradeLog_Type.Log(1, MemberUpgradeRequestID, Master.sfToken.AppUserID, MediaNumber,,, "Error forcing Six Flags Bucks", Amount.ToString)
        End If
        Master.DisplayErrors()
    End Sub

    Protected Sub cmdConfirmCancel_Click(sender As Object, e As EventArgs) Handles cmdConfirmCancel.Click
        pnlAssignConfirm.Visible = False
        pnlAccountsSP.Visible = True
        Master.AddError("Action cancelled. Credit has NOT been assiged to guest.")
        Master.DisplayErrors()
    End Sub

    Protected Sub cmdThankYouContinue_Click(sender As Object, e As EventArgs) Handles cmdThankYouContinue.Click
        Response.Redirect("upgradereview.aspx")
    End Sub
End Class