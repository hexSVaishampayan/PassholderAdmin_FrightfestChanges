Imports MembershipLibrary

Public Class giftcardlookup
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Page.Title = "Gift Card Lookup"
            Master.sfToken = AppToken_Type.GetCurrentToken()
            If Master.sfToken.hasPermission(505) Then
            Else
                Response.Write("<p>You do not have permission to access this resource #505. Please contact Interactive Marketing.</p>")
                Response.Flush()
            End If
        End If

    End Sub

    Public Function CalendarInfo(AccessoOrderID_ As Integer) As String
        Dim r As String = ""
        Dim c() As MemberCovidDays_Type = MemberCovidDays_Type.FindByAccessoOrderID(AccessoOrderID_)
        If Not c Is Nothing And c.Length > 0 Then
            Dim cc As Integer = 0
            Dim rr As String = "<tr><td class=moname>&nbsp;</td>"
            For i As Integer = 1 To 31
                rr += "<td class=modate>" + i.ToString + "</td>"
            Next
            rr += "</tr>"
            Dim rowNo As Integer = 0
            Dim CreditDays As Integer = 0
            Do
                Dim CellContent As String = ""
                If c(cc).caldate.Day = 1 Then
                    If rowNo > 1 Then rr += "</tr>"
                    rr += "<tr><td class=moname>" + c(cc).caldate.ToString("MMMM \'yy") + "</td>"
                End If
                Dim cls As String = ""
                If c(cc).ShouldBeOpen Then cls += "op "
                If c(cc).COVIDClosed Then cls += "closed "
                If c(cc).Paused Then cls += "pause "
                If c(cc).ApplicablePaused Then cls += "apppause "
                If c(cc).ShouldBeOpen And c(cc).COVIDClosed And Not c(cc).Paused Then
                    CreditDays += 1
                    CellContent = "<span class=creditcount>" + CreditDays.ToString + "</span>"
                End If
                If c(cc).Paused Then CellContent = "P"
                rr += "<td class='" + cls + "'>" + CellContent + "</td>"
                cc += 1
            Loop Until cc >= c.Length - 1
            rr += "</tr>"
            rr = "<h2>Calendar Visualization (Experimental)</h2><table class=rtable>" + rr + "</table>"
            rr += "<h3>Legand</h3>"
            rr += "<table class=legand>"
            rr += "<tr><td class='samp op'>&nbsp;</td><td class=legdesc>Park is open</td></tr>"
            rr += "<tr><td class='samp op closed'><span class=creditcount>3</span></td><td class=legdesc>Closed when it should have been open (# indicates credit received)</td></tr>"
            rr += "<tr><td class='samp op closed pause'>P</td><td class=legdesc>Paused during a time credit would have been earned (so no credit earned)</td></tr>"
            rr += "<tr><td class='samp op'>P</td><td class=legdesc>Paused during a time when the park was open (no credit earned, but no payments made either)</td></tr>"
            rr += "<tr><td class='samp'>P</td><td class=legdesc>Paused during a time when credit wouldn't be earned because park was supposed to be closed</td></tr>"
            rr += "</table>"
            r = rr
        End If
        Return r
    End Function

    Protected Sub cmdSearch_Click(sender As Object, e As EventArgs) Handles cmdSearch.Click
        Try
            Dim Result As String = ""
            Dim AccessoOrderID As Integer = fldAccessoOrderID.Text.Trim.FilterToJustNumbers
            If AccessoOrderID > 200000000 And AccessoOrderID < 300000000 Then
                Dim M As MemberCovidMonths_Type = MemberCovidMonths_Type.FindByAccessoOrderID(AccessoOrderID)
                If Not M Is Nothing Then
                    Dim r As String = ""
                    r += "<h2>Summary Details</h2>"
                    r += "<table class=infoblock>"
                    r += "<tr><td>Order ID</td><td><b>" + M.AccessoOrderID.ToString + "</b></td></tr>"
                    r += "<tr><td>LastName</td><td>" + M.LastName.ToString + "</td></tr>"
                    r += "<tr><td>Initial Order Date</td><td>" + M.OrderDate.ToString("MMMM d, yyyy") + "</td></tr>"
                    r += "<tr><td>Park</td><td>" + ParkNameLong(M.ParkID) + "</td></tr>"
                    r += "<tr><td>Days Lost (Park Closed)</td><td>" + M.DaysLostTotal.ToString + "</td></tr>"
                    r += "<tr><td>Days Paused</td><td>" + M.DaysPausedTotal.ToString + "</td></tr>"
                    r += "<tr><td>Net Days Owed</td><td>" + M.NetDaysOwed.ToString + "</td></tr>"
                    r += "<tr><td>Months Owed (before rounding)</td><td>" + M.FractionalMonthsOwed.ToString + "</td></tr>"
                    r += "<tr><td class=maininfo><b>Months Owed:</b></td><td class=maininfo><b>" + M.FinalMonthsOwed.ToString + " months</b></td></tr>"
                    r += "</table>"

                    r += "<h2>Calculation Details</h2>"
                    r += "<table class=infoblock>"

                    If M.DateClosed1 <> DefaultDate Then r += "<tr><td>Date Credit Started</td><td>" + M.DateClosed1.ToString("MMMM d, yyyy") + "</td></tr>"
                    If M.DateOpened1 <> DefaultDate Then r += "<tr><td>Date Credit Ended</td><td>" + M.DateOpened1.ToString("MMMM d, yyyy") + "</td></tr>"
                    If M.DateClosed2 <> DefaultDate Then r += "<tr><td>Date Credit Started (2)</td><td>" + M.DateClosed2.ToString("MMMM d, yyyy") + "</td></tr>"
                    If M.DateOpened2 <> DefaultDate Then r += "<tr><td>Date Credit Ended (2)</td><td>" + M.DateOpened2.ToString("MMMM d, yyyy") + "</td></tr>"

                    r += "<tr><td>Days Lost (park closed)</td><td>" + M.DaysLost1.ToString + "</td></tr>"
                    If M.DaysLost2 > 0 Then r += "<tr><td>Additional Days Lost</td><td>" + M.DaysLost2.ToString + "</td></tr>"
                    If M.DaysLost2 > 0 Then r += "<tr><td>Total Days Lost</td><td>" + M.DaysLostTotal.ToString + "</td></tr>"
                    If M.DaysPaused1 > 0 Then
                        r += "<tr><td>Pause Start Date</td><td>" + M.PausedStartDate1.ToString("MMMM d, yyyy") + "</td></tr>"
                        r += "<tr><td>Pause End Date</td><td>" + M.PausedEndDate1.ToString("MMMM d, yyyy") + "</td></tr>"
                        r += "<tr><td>Days Paused:</td><td>" + M.DaysPaused1.ToString + "</td></tr>"
                    End If
                    If M.DaysPaused2 > 0 Then
                        r += "<tr><td>Second Pause Start</td><td>" + M.PausedStartDate2.ToString("MMMM d, yyyy") + "</td></tr>"
                        r += "<tr><td>Second Pause End</td><td>" + M.PausedEndDate2.ToString("MMMM d, yyyy") + "</td></tr>"
                        r += "<tr><td>Days Paused (rnd 2)</td><td>" + M.DaysPaused2.ToString + "</td></tr>"
                        r += "<tr><td>Total Days Paused</td><td>" + M.DaysPausedTotal.ToString + "</td></tr>"
                    End If
                    If M.DaysPaused1 > 0 Then r += "<tr><td>Net Days Owed</td><td>" + M.NetDaysOwed.ToString + "</td></tr>"
                    r += "</table>"

                    If M.GiftCardOrderID > 0 Then
                        r += "<h2>Issued Gift Card Details</h2>"
                        r += "<table class=infoblock>"
                        r += "<tr><td>Card Order ID</td><td>" + M.GiftCardOrderID.ToString + "</td></tr>"
                        r += "<tr><td>Gift Card Value</td><td><b>" + (cDecimal(M.GiftCardValueInCents) / 100).ToString("$0.00") + "</b></td></tr>"
                        If M.GiftCardDisbursedDate <> DefaultDate Then r += "<tr><td>GiftCardDisbursedDate</td><td><b>" + M.GiftCardDisbursedDate.ToString + "</b></td></tr>"
                        r += "<tr><td>GiftCardDisbursedBy</td><td>" + M.GiftCardDisbursedBy.ToString + "</td></tr>"
                        r += "<tr><td>Gift Card Last 4</td><td>" + M.GiftCardLast4.ToString + "</td></tr>"
                        r += "<tr><td>Exchange ID</td><td>" + M.GiftCardExchangeID.ToString + "</td></tr>"
                        r += "<tr><td>Transaction ID</td><td>" + M.GiftCardTransactionID.ToString + "</td></tr>"
                        r += "<tr><td>Status Code</td><td>" + M.GiftCardStatusCode.ToString + "</td></tr>"
                        If M.ErrorCode.Length > 0 Then r += "<tr><td>Error Code</td><td>" + M.ErrorCode.ToString + "</td></tr>"

                    End If
                    r += "</table>"

                    r += CalendarInfo(AccessoOrderID)
                    Result = r
                Else
                    Result = "No Membership gift card record found for this account."
                End If
            Else
                Master.AddError("Invalid account ID. Please enter an Accesso Order/Confirmation number.")
            End If
            lblResults.Text = Result
            Master.DisplayErrors()
        Catch ex As Exception
            lblResults.Text = "<i>Error: " + ex.Message + "</i>"
        End Try
    End Sub

End Class

