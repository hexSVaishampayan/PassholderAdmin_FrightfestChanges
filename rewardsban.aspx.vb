Public Class rewardsban
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        pnlResult.Visible = False
        If Not IsPostBack Then
            Master.sfToken = AppToken_Type.GetCurrentToken()
            If Master.sfToken.hasPermission(515) Then
                Master.sfToken.LogEntry(33)
                pnlForm.Visible = True
                pnlError.Visible = False
            Else
                pnlForm.Visible = False
                pnlError.Visible = True
            End If
        End If
    End Sub


    Protected Sub cmdFindMediaNumber_Click(sender As Object, e As EventArgs) Handles cmdFindMediaNumber.Click
        fldMediaNumber.Text = fldMediaNumber.Text.Trim.FilterToJustNumbers
        If fldMediaNumber.Text.Length >= 15 Then
            Dim AlreadyBanned As Boolean = False
            Dim b() As LoyaltyBan_Type = LoyaltyBan_Type.FindByMediaNumber(fldMediaNumber.Text)
            If Not b Is Nothing AndAlso b.Length > 0 Then
                For i As Integer = 0 To b.Length - 1
                    If Now() >= b(i).BanStartDate And Now() <= b(i).BanEndDate Then
                        Master.AddError(b(i).ApplyCodes("Member is already banned from {{banstartdate}} to {{banenddate}}. Reason is: ""{{banreason}}"". Request aborted."))
                        AlreadyBanned = True
                        Exit For
                    End If
                Next
            End If
            If Not AlreadyBanned Then
                Dim bn As New LoyaltyBan_Type()
                Dim m As Media_Type = Media_Type.FindByMediaNumber(fldMediaNumber.Text)
                If Not m Is Nothing Then
                    With bn
                        .BanStartDate = Now()
                        .BanEndDate = Now().AddYears(10)
                        .BanReason = fldReason.Text.Trim
                        .FolioUniqueID = m.FolioUniqueID
                        .Save()
                    End With
                    If bn.LoyaltyBanID > 0 Then
                        Master.AddError("Successfully banned {{medianumber}} from Member Rewards until {{banenddate}}.")
                    Else
                        Master.AddError("Error saving ban request to database.")
                    End If
                Else
                    Master.AddError("Error reading FolioUniqueID for Media Number.")
                End If
            End If
        Else
            Master.AddError("Please enter a valid media number.")
        End If
        Master.DisplayErrors()
    End Sub

End Class