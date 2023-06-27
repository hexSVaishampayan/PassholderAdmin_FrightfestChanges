Public Class ats
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Page.Title = "ATS Coupon Checkout"
            Master.sfToken = AppToken_Type.GetCurrentToken()
            If Master.sfToken.hasPermission(703) Then
                pnlContent.Visible = True
            Else
                Response.Write("<p>You do not have permission to access this resource #703.</p>")
                Response.Flush()
                pnlContent.Visible = False
            End If
        End If
    End Sub

    Public Function isFormValid() As Boolean
        fldRangeStart.Text = fldRangeStart.Text.Trim.FilterToJustNumbers
        fldRangeEnd.Text = fldRangeEnd.Text.Trim.FilterToJustNumbers
        Dim RangeStart As Integer = cInteger(fldRangeStart.Text)
        Dim RangeEnd As Integer = cInteger(fldRangeEnd.Text)
        Dim TotalRange As Integer = RangeEnd - RangeStart + 1
        If TotalRange > 1000 Then
            Master.AddError("You've attempted to activate {{actcount}} coupons. You can only activate up to 1000 coupons for a given batch.".Replace("{{actcount}}", TotalRange.ToString("#,##0")))
        End If
        If Not Master.HasErrors Then Return True
        Master.AddError("The operation was NOT completed. Please try again.")
        Master.DisplayErrors()
        Return False
    End Function

    Protected Sub cmdActivate_Click(sender As Object, e As EventArgs) Handles cmdActivate.Click
        Master.ResetErrors()
        If isFormValid() Then
            Dim at As ATSInvite_Type = ATSInvite_Type.FindByATSInviteID(fldRangeStart.Text.ConvertToInteger)
            If Not at Is Nothing Then
                Try
                    Dim b As New ATSBatch_Type()
                    Try
                        With b
                            .AppUserID = Master.sfToken.AppUserID
                            .DateCreated = Now()
                            .Active = True
                            .ATSInviteIDStart = cInteger(fldRangeStart.Text)
                            .ATSInviteIDEnd = cInteger(fldRangeEnd.Text)
                            .ParkID = cInteger(at.ParkID)
                            .ATSPrizeTypeID = 3
                            .Save()
                        End With
                    Catch ex As Exception
                        Master.AddError("There was an error activating the batch! Error: " + ex.Message)
                    End Try
                    If b.ATSBatchID > 0 Then
                        Dim RecordCount As Integer = b.ActivateInvites()
                        Dim UserName As String = Master.sfToken.UserName
                        If RecordCount = (b.ATSInviteIDEnd - b.ATSInviteIDStart + 1) Then
                            Master.AddError("{{username}} successfully activated <b>{{codecount}}</b> coupons in batch #{{atsbatchid}}.".Replace("{{codecount}}", RecordCount.ToString("#,##0")).Replace("{{atsbatchid}}", b.ATSBatchID.ToString).Replace("{{username}}", UserName))
                            Master.ErrorColor = "#0000FF"
                            fldValidStart.Text = fldRangeStart.Text
                            fldValidEnd.Text = fldRangeEnd.Text
                        ElseIf RecordCount = 0 Then
                            Master.AddError("We were unable to activate any of the records in the set you selected. Were they already activated?")
                        ElseIf RecordCount < (b.ATSInviteIDEnd - b.ATSInviteIDStart + 1) Then
                            Master.AddError("We were only able to activate {{actual}} of the {{planned}} coupons you selected. Were some of them already activated?".Replace("{{actual}}", RecordCount.ToString("#,##0")).Replace("{{planned}}", (b.ATSInviteIDEnd - b.ATSInviteIDStart + 1).ToString("#,##0")))
                        End If
                    End If
                Catch ex As Exception
                    Master.AddError("There was an error processing the batch! Error: " + ex.Message)
                End Try
            Else
                Master.AddError(fldRangeStart.Text + " is not in a valid range. Please check your starting number and try again.")
            End If
        End If
        Master.DisplayErrors()
    End Sub

    Public Function ShowRange(StartRange As Integer, EndRange As Integer) As String
        Dim r As String = ""
        Try
            If StartRange > EndRange Then
                Dim NewEndRange = StartRange
                StartRange = EndRange
                EndRange = NewEndRange
            End If
            Dim cnn As New SqlConnection(ConnString("Panel"))
            Dim cmd As New SqlCommand("ATSInvite_RangeStatus", cnn)
            Dim Result_Count As Integer = -1
            cmd.Parameters.AddWithValue("@StartRange", StartRange)
            cmd.Parameters.AddWithValue("@EndRange", EndRange)
            cmd.CommandType = CommandType.StoredProcedure
            Dim dat As SqlDataReader = Nothing
            Dim Success As Boolean = False
            Dim FailCount As Integer = 0
            Do
                Try
                    cnn.Open()
                    dat = cmd.ExecuteReader
                    If dat.HasRows Then
                        dat.Read()
                        Do
                            Result_Count += 1
                            Dim li As String = "<tr><td>" + ReadDBValue(dat, "AtsInviteID", 0).ToString + "</td>"
                            li += "<td>" + If(ReadDBValue(dat, "ParkAbb", "") <> "", ReadDBValue(dat, "ParkAbb", ""), "<i>Unactivated</i>") + "</td>"
                            li += "<td>" + If(ReadDBValue(dat, "visitdate", DefaultDate) = DefaultDate, "&nbsp;", ReadDBValue(dat, "visitdate", DefaultDate).ToString("MMM d")) + "</td>"
                            li += "<td>" + If(ReadDBValue(dat, "datecreated", DefaultDate) = DefaultDate, "&nbsp;", ReadDBValue(dat, "datecreated", DefaultDate).ToString("MMM d @ HH:mm")) + "</td>"
                            li += "<td>" + ReadDBValue(dat, "AssignedBy", "&nbsp;") + "</td>"
                            li += "<td>" + If(ReadDBValue(dat, "DateFinished", DefaultDate) = DefaultDate, "&nbsp;", ReadDBValue(dat, "DateFinished", DefaultDate).ToString("MMM d @ HH:mm")) + "</td>"
                            li += "</tr>"
                            r += li
                        Loop Until Not dat.Read
                        dat.Close()
                    End If
                    Success = True
                    If Result_Count > 1 Then
                        r = MakeHTMLTable("dattab", "tx", r, "Coupon ID", "Park", "VisDate", "Activated", "Activated By", "Completed")
                    End If
                Catch ex As SqlException
                    FailCount += 1
                    If FailCount > 3 Then ReportErr(ex.TargetSite.Module.Name + " " + ex.TargetSite.Name, ex)
                Finally
                    cnn.Close()
                End Try
            Loop Until Success Or FailCount > 3
        Catch ex As Exception
            Master.AddError("There was an error displaying the range. Error: " + ex.Message)
        End Try
        Return r
    End Function

    Protected Sub cmdValidateRange_Click(sender As Object, e As EventArgs) Handles cmdValidateRange.Click
        fldValidStart.Text = fldValidStart.Text.Trim.FilterToJustNumbers
        fldValidEnd.Text = fldValidEnd.Text.Trim.FilterToJustNumbers
        Dim StartRange As Integer = cInteger(fldValidStart.Text)
        Dim EndRange As Integer = cInteger(fldValidEnd.Text)
        If StartRange >= 1000000 And EndRange >= 1000000 Then
            lblResult.Text = ShowRange(StartRange, EndRange)
        Else
            Master.AddError("Please enter a valid range.")
        End If
        Master.DisplayErrors()
    End Sub

    Protected Sub cmdDeactivate_Click(sender As Object, e As EventArgs) Handles cmdDeactivate.Click
        Try
            fldDeactivateCodeStart.Text = fldDeactivateCodeStart.Text.Trim.ToLower.FilterToAlphaNumericOnly
            fldDeactivateCodeEnd.Text = fldDeactivateCodeEnd.Text.Trim.ToLower.FilterToAlphaNumericOnly
            If fldDeactivateCodeStart.Text.Length = 16 And fldDeactivateCodeEnd.Text.Length = 16 Then
                Dim RecordsImpacted As Integer = SQL(ConnString("Panel"), "ATSInvite_Deactivate", True, New NameValuePair("@Start", fldDeactivateCodeStart.Text), New NameValuePair("@End", fldDeactivateCodeEnd.Text))
                If RecordsImpacted > 0 Then
                    Master.AddError("Successfully deactivated {{items}} coupons.".Replace("{{items}}", RecordsImpacted.ToString("#,##0")))
                Else
                    Master.AddError("No records were impacted. Please check your codes and try again.")
                End If
            Else
                Master.AddError("Codes are not valid. Please check the codes and try again.")
            End If
        Catch ex As Exception
            Master.AddError("There was an error deactivating the range: " + ex.Message)
        End Try
        Master.DisplayErrors()
    End Sub
End Class