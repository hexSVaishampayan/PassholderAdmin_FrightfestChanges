Public Class onlinesales
    Inherits System.Web.UI.Page

    Public Function PrepareReport(ReportID As Integer, ReportTitle As String) As String
        Dim sb As New StringBuilder("<h1>" + ReportTitle + "</h1>")
        sb.Append("<Table Class='dailymemreport'>")
        Try
            Dim rr() As DailySalesReportBuffer_Type = DailySalesReportBuffer_Type.LoadReport(ReportID)
            sb.Append("<th class='descCol blank'>&nbsp;</th>")
            sb.Append("<th class='topHeader col1' colspan=4>ALL Passes</th>")
            sb.Append("<th class='topHeader col2' colspan=4>Membership</th>")
            sb.Append("<th Class='topHeader col3' colspan=4>Season Pass</th>")
            sb.Append("<th Class='topHeader col4' colspan=4>Member as %</th>")
            sb.Append("<th Class='topHeader col5' colspan=4>Single Tix</th>")
            sb.Append("<th Class='topHeader col6' colspan=4>All Admission</th>")
            sb.Append("<th Class='topHeader col6' colspan=4>Member Dining</th>")
            sb.Append("</tr>")
            sb.Append("<tr><th Class='descName descCol'>Park</th>")
            sb.Append("<th>19</th><th>20</th><th>Δ</th><th>%Δ</th>")
            sb.Append("<th>19</th><th>20</th><th>Δ</th><th>%Δ</th>")
            sb.Append("<th>19</th><th>20</th><th>Δ</th><th>%Δ</th>")
            sb.Append("<th>19</th><th>20</th><th>Δ</th><th>%Δ</th>")
            sb.Append("<th>19</th><th>20</th><th>Δ</th><th>%Δ</th>")
            sb.Append("<th>19</th><th>20</th><th>Δ</th><th>%Δ</th>")
            sb.Append("<th>19</th><th>20</th><th>Δ</th><th>%Δ</th>")
            sb.Append("</tr>")
            Dim FirstDate As Date = DefaultDate
            Dim LastUpdated As Date = DefaultDate
            If Not rr Is Nothing And rr.Length > 0 Then

                For i As Integer = 0 To rr.Length - 1
                    Dim ln As String = "<tr>"
                    ln += "<td Class=descCol>" + rr(i).ParkGroupName + "</td>"

                    Dim pCol As String = If((rr(i).P19 - rr(i).P18) > 0, " isPos", If((rr(i).P19 - rr(i).P18) < 0, " isNeg", ""))
                    ln += "<td Class=itemNo>" + rr(i).P18.ToString("#,##0") + "</td>"
                    ln += "<td Class=itemNo>" + rr(i).P19.ToString("#,##0") + "</td>"
                    ln += "<td Class='itemdif" + pCol + "'>" + (rr(i).P19 - rr(i).P18).ToString("+#,##0;-#,##0;0") + "</td>"
                    ln += "<td class='itemdifp" + pCol + " leftcol'>" + If(rr(i).P18 > 0, ((rr(i).P19 - rr(i).P18) / rr(i).P18), 0).ToString("+0%;-0%;0%") + "</td>"

                    Dim mCol As String = If((rr(i).M19 - rr(i).M18) > 0, " isPos", If((rr(i).M19 - rr(i).M18) < 0, " isNeg", ""))
                    ln += "<td class=itemNo>" + rr(i).M18.ToString("#,##0") + "</td>"
                    ln += "<td class=itemNo>" + rr(i).M19.ToString("#,##0") + "</td>"
                    ln += "<td class='itemdif" + mCol + "'>" + (rr(i).M19 - rr(i).M18).ToString("+#,##0;-#,##0;0") + "</td>"
                    ln += "<td class='itemdifp" + mCol + "'>" + If(rr(i).M18 > 0, ((rr(i).M19 - rr(i).M18) / rr(i).M18), 0).ToString("+0%;-0%;0%") + "</td>"

                    Dim sCol As String = If((rr(i).Sp19 - rr(i).Sp18) > 0, " isPos", If((rr(i).Sp19 - rr(i).Sp18) < 0, " isNeg", ""))
                    ln += "<td class=itemNo>" + rr(i).Sp18.ToString("#,##0") + "</td>"
                    ln += "<td class=itemNo>" + rr(i).Sp19.ToString("#,##0") + "</td>"
                    ln += "<td class='itemdif" + sCol + "'>" + (rr(i).Sp19 - rr(i).Sp18).ToString("+#,##0;-#,##0;0") + "</td>"
                    ln += "<td class='itemdifp" + sCol + " leftcol'>" + If(rr(i).Sp18 > 0, ((rr(i).Sp19 - rr(i).Sp18) / rr(i).Sp18), 0).ToString("+0%;-0%;0%") + "</td>"

                    Dim xCol As String = If((rr(i).MemPer19 - rr(i).MemPer18) > 0, " isPos", If((rr(i).MemPer19 - rr(i).MemPer18) < 0, " isNeg", ""))
                    ln += "<td class=itemNo>" + rr(i).MemPer18.ToString("0.0%") + "</td>"
                    ln += "<td class=itemNo>" + rr(i).MemPer19.ToString("0.0%") + "</td>"
                    ln += "<td class='itemdif" + xCol + "'>" + (rr(i).MemPer19 - rr(i).MemPer18).ToString("+0.0%;-0.0%;0.0%") + "</td>"
                    ln += "<td class='itemdifp" + xCol + " leftcol'>" + If(rr(i).MemPer18 > 0, ((rr(i).MemPer19 - rr(i).MemPer18) / rr(i).MemPer18), 0).ToString("+0%;-0%;0%") + "</td>"

                    Dim tCol As String = If((rr(i).Tix19 - rr(i).Tix18) > 0, " isPos", If((rr(i).Tix19 - rr(i).Tix18) < 0, " isNeg", ""))
                    ln += "<td class=itemNo>" + rr(i).Tix18.ToString("#,##0") + "</td>"
                    ln += "<td class=itemNo>" + rr(i).Tix19.ToString("#,##0") + "</td>"
                    ln += "<td class='itemdif" + tCol + "'>" + (rr(i).Tix19 - rr(i).Tix18).ToString("+#,##0;-#,##0;0") + "</td>"
                    ln += "<td class='itemdifp" + tCol + " leftcol'>" + If(rr(i).Tix18 > 0, ((rr(i).Tix19 - rr(i).Tix18) / rr(i).Tix18), 0).ToString("+0%;-0%;0%") + "</td>"

                    Dim totCol As String = If((rr(i).Tot19 - rr(i).Tot18) > 0, " isPos", If((rr(i).Tot19 - rr(i).Tot18) < 0, " isNeg", ""))
                    ln += "<td class=itemNo>" + rr(i).Tot18.ToString("#,##0") + "</td>"
                    ln += "<td class=itemNo>" + rr(i).Tot19.ToString("#,##0") + "</td>"
                    ln += "<td class='itemdif" + totCol + "'>" + (rr(i).Tot19 - rr(i).Tot18).ToString("+#,##0;-#,##0;0") + "</td>"
                    ln += "<td class='itemdifp" + totCol + " leftcol'>" + If(rr(i).Tot18 > 0, ((rr(i).Tot19 - rr(i).Tot18) / rr(i).Tot18), 0).ToString("+0%;-0%;0%") + "</td>"

                    Dim dCol As String = If((rr(i).D19 - rr(i).D18) > 0, " isPos", If((rr(i).D19 - rr(i).D18) < 0, " isNeg", ""))
                    ln += "<td class=itemNo>" + rr(i).D18.ToString("#,##0") + "</td>"
                    ln += "<td class=itemNo>" + rr(i).D19.ToString("#,##0") + "</td>"
                    ln += "<td class='itemdif" + dCol + "'>" + (rr(i).D19 - rr(i).D18).ToString("+#,##0;-#,##0;0") + "</td>"
                    ln += "<td class='itemdifp" + dCol + "'>" + If(rr(i).D18 > 0, ((rr(i).D19 - rr(i).D18) / rr(i).D18), 0).ToString("+0%;-0%;0%") + "</td>"

                    ln += "</tr>"
                    sb.Append(ln)
                    If rr(i).ParkGroupID = 0 Then LastUpdated = rr(i).LastRecord19
                    If rr(i).ParkGroupID = 0 Then FirstDate = rr(i).FirstRecord19
                Next
            End If
            sb.Append("</table>")
            sb.Append("<p class=dateRange>Range: " + FirstDate.ToString("MMM d' at 'HH:mm") + " to " + LastUpdated.ToString("MMM d' at 'HH:mm") + "</p>")
            sb.Append("<p class='updaterow'>Updated: " + LastUpdated.ToShortTimeString + "<span style='font-size:8pt;'><br>Refreshed every 10 minutes or so</p></span>")
        Catch ex As Exception
        End Try
        Return sb.ToString
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim p As String = ReadHTTPValue("p", "")
        If p = "P4CKugpm8mC05th9V7gp" Then
            lblReport1.Text = PrepareReport(1, "Daily Online Sales Status")
            'lblReport2.Text = PrepareReport(3, "Flash Sale")
            lblReport3.Text = PrepareReport(2, "Last Seven Days")
        Else
            lblReport1.Text = "Error"
            'lblReport2.Text = "Error"
            lblReport3.Text = "Error"
        End If
    End Sub

    Protected Sub cmdReRun_Click(sender As Object, e As EventArgs) Handles cmdReRun.Click
        SQL(ConnString("MarketingAnalysis"), "Report_DailySalesStatus")
    End Sub

End Class