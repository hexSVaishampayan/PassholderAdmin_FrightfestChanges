Imports MembershipLibrary

Public Class pause
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Page.Title = "Member Reservations"
            Master.sfToken = AppToken_Type.GetCurrentToken()
            If Master.sfToken.hasPermission(505) Then
            Else
                Response.Write("<p>You do not have permission to access this resource #505. Please contact Interactive Marketing.</p>")
                Response.Flush()
            End If
        End If

    End Sub

    Public Function GenerateDisaply(d As DeferHold_Type) As String
        Dim r As String = "<table class=infoblock>"
        r += "<tr><td>Pause ID</td><td>" + d.DeferHoldID.ToString + "</td></tr>"
        r += "<tr><td>Order ID</td><td>" + d.AccessoOrderID.ToString + "</td></tr>"
        r += "<tr><td>End Date</td><td>" + d.EndDate.ToString("MM/dd/yyyy") + " (or until park opens, whichever is later)</td></tr>"
        r += "<tr><td>Date Requested</td><td>" + d.DateRequested.ToString("MMM d") + " at " + d.DateRequested.ToShortTimeString + "</td></tr>"
        If Not d.DateVerified = DefaultDate Then
            r += "<tr><td>Date Enabled</td><td>" + d.DateVerified.ToString("MMM d") + " at " + d.DateVerified.ToShortTimeString + "</td></tr>"
        Else
            r += "<tr><td>Date Enabled</td><td style='color:red;'><i>Not Active</i></td></tr>"
        End If
        If d.RequestedByUserID > 0 Then
            Dim u As AppAccessLibrary.AppUser_Type = AppAccessLibrary.AppUser_Type.FindByAppUserID(d.RequestedByUserID)
            If Not u Is Nothing Then
                r += "<tr><td>Paused by User</td><td>" + u.FullName + "</td></tr>"
            End If
        End If
        r += "</table>"
        Return r
    End Function

    Protected Sub cmdSearch_Click(sender As Object, e As EventArgs) Handles cmdSearch.Click
        Try
            Dim d As DeferHold_Type = Nothing
            lblResults.Text = "<i>Record not found. Usually this means a hold request was not submitted for this account.</i>"
            Dim t As String = fldSearchText.Text.Trim.FilterToJustNumbers
            Dim n As Integer = cBigInteger(t, 0)
            If n > Integer.MaxValue Then n = 0
            If n > 0 Then
                If fldSearchType.SelectedValue = "1" Then
                    d = DeferHold_Type.FindByAccessoOrderID(n)
                ElseIf fldSearchType.SelectedValue = "2" Then
                    d = DeferHold_Type.FindByDeferHoldID(n)
                End If
                If Not d Is Nothing Then
                    lblResults.Text = GenerateDisaply(d)
                End If
            End If
        Catch ex As Exception
            lblResults.Text = "<i>Error: " + ex.Message + "</i>"
        End Try
    End Sub

    Protected Sub cmdPause_Click(sender As Object, e As EventArgs) Handles cmdPause.Click
        fldAccessoOrderID.Text = fldAccessoOrderID.Text.Trim.FilterToJustNumbers
        If fldAccessoOrderID.Text.Length = 9 Then
            Dim AccessoOrderID As Integer = fldAccessoOrderID.Text.ConvertToInteger
            Dim ao As AccessoOrder_Type = AccessoOrder_Type.FindByAccessoOrderID(AccessoOrderID)
            If Not ao Is Nothing Then
                If DeferHold_Type.AllowedToPause(ao.ParkID) Or Master.sfToken.hasPermission(516) Then
                    Dim dh As DeferHold_Type = DeferHold_Type.FindByAccessoOrderID(AccessoOrderID)
                    If dh Is Nothing Then dh = New DeferHold_Type
                    Dim Status As String = MembershipLibrary.AccessoLookup.PauseAccount(AccessoOrderID)
                    If Status = "OK" Then
                        dh.AccessoOrderID = fldAccessoOrderID.Text
                        dh.DateRequested = Now()
                        dh.RequestedByUserID = Master.sfToken.AppUserID
                        dh.DateVerified = Now()
                        dh.EndDate = #9/1/2021#
                        dh.Save()
                        Dim d As DeferHold_Type = DeferHold_Type.FindByAccessoOrderID(fldAccessoOrderID.Text.ConvertToInteger)
                        If Not d Is Nothing Then
                            lblResults.Text = GenerateDisaply(d)
                            Master.AddError("Account {{accessoorderid}} has been paused.".Replace("{{accessoorderid}}", dh.AccessoOrderID))
                            fldAccessoOrderID.Text = ""
                        Else
                            Master.AddError("Error placing account on pause!")
                        End If
                    Else
                        Master.AddError("Error pausing Membership. System returned: {{error}}".Replace("{{error}}", Status))
                        If Status.Contains("billable membership") Then Master.AddError("This usually means that the Membership is already paused in Accesso's system.")
                    End If
                Else
                    Master.AddError("Action Failed: Guests at {{parkname}} are no longer allowed to pause their accounts because the park is open.".Replace("{{parkname}}", ParkNameLong(ao.ParkID)))
                End If
            Else
                Master.AddError("Account {{accessoorderid}} does not seem to exist. Please check the number and try again.".Replace("{{accessoorderid}}", AccessoOrderID))
            End If
        Else
            Master.AddError("Error in account number. Please try again.")
        End If
        Master.DisplayErrors()
    End Sub

    Protected Sub cmdUnpause_Click(sender As Object, e As EventArgs) Handles cmdUnpause.Click
        fldAccessoOrderID.Text = fldAccessoOrderID.Text.Trim.FilterToJustNumbers
        If fldAccessoOrderID_UnPause.Text.Length = 9 Then
            Dim AccessoOrderID As Integer = fldAccessoOrderID_UnPause.Text.ConvertToInteger
            Dim tx As DeferHold_Type = DeferHold_Type.FindByAccessoOrderID(AccessoOrderID, Today())
            Dim ao As AccessoOrder_Type = AccessoOrder_Type.FindByAccessoOrderID(AccessoOrderID)
            If Not ao Is Nothing Then
                Dim Status As String = MembershipLibrary.AccessoLookup.UnPauseAccount(AccessoOrderID, Now().AddDays(1).Day)
                If Status = "OK" Then
                    Dim dh As DeferHold_Type = DeferHold_Type.FindByAccessoOrderID(AccessoOrderID, Today)
                    If Not dh Is Nothing Then
                        dh.EndDate = Today
                        dh.Save()
                    End If
                    Master.AddError("Membership account {{accessoorderid}} has been successfully unpaused. The new monthly billing day is the {{daymo}} of each month.".Replace("{{accessoorderid}}", AccessoOrderID.ToString).Replace("{{daymo}}", Now().AddDays(1).Day.NumberRank))
                Else
                    Master.AddError("Error unpausing Membership account. System returned: {{error}}".Replace("{{error}}", Status))
                End If
            Else
                Master.AddError("Account {{accessoorderid}} does not seem to exist. Please check the number and try again.".Replace("{{accessoorderid}}", AccessoOrderID))
            End If
        Else
            Master.AddError("Error in account number. Please try again.")
        End If
        Master.DisplayErrors()
    End Sub
End Class

