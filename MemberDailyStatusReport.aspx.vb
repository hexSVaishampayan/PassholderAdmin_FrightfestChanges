Public Class MemberDailyStatusReport
    Inherits System.Web.UI.Page

    Public tabDat1 As String
    Public tabDat2 As String
    Public tabDat3 As String

    Public Function PrepareReport(ReportID As Integer, ReportTitle As String, ByRef tabref As String) As String
        Dim sb As New StringBuilder("")
        Try
            Dim rr() As DailySalesReportBuffer_Type = DailySalesReportBuffer_Type.LoadReport(ReportID)
            Dim FirstDate_TY As Date = DefaultDate
            Dim LastDate_TY As Date = DefaultDate
            Dim FirstDate_LY As Date = DefaultDate
            Dim LastDate_LY As Date = DefaultDate
            If Not rr Is Nothing And rr.Length > 0 Then

                For i As Integer = 0 To rr.Length - 1
                    Try
                        If rr(i).ParkGroupID = 0 Then LastDate_TY = rr(i).LastRecord19
                        If rr(i).ParkGroupID = 0 Then FirstDate_TY = rr(i).FirstRecord19
                        If rr(i).ParkGroupID = 0 Then LastDate_LY = rr(i).LastRecord18
                        If rr(i).ParkGroupID = 0 Then FirstDate_LY = rr(i).FirstRecord18
                    Catch ex As Exception

                    End Try
                Next
                tabref = DailySalesReportBuffer_Type.SerializeArray(rr)
            End If
            sb.Append("<div class='dateranges'><span class=dateRange>This Year: " + FirstDate_TY.ToShortDateString + " " + FirstDate_TY.ToShortTimeString + " to " + LastDate_TY.ToShortDateString + " " + LastDate_TY.ToShortTimeString + "</span>&nbsp;;")
            sb.Append("<span class=dateRange>Last Year: " + FirstDate_LY.ToShortDateString + " " + FirstDate_LY.ToShortTimeString + " to " + LastDate_LY.ToShortDateString + " " + LastDate_LY.ToShortTimeString + "</span><span class=instru>-- Click the section header to view average price and revenue.</span></div>")
        Catch ex As Exception
        End Try
        Return sb.ToString
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If ReadHTTPValue("p", "") = "9bcrY9R7VfE975%o%*Ea5W" Then
            lblReport1.Text = PrepareReport(1, "Daily (Since Midnight)", tabDat1)
            lblReport2.Text = PrepareReport(2, "Last Seven Days", tabDat2)
            lblReport3.Text = PrepareReport(3, "Cyber Sale", tabDat3)
        Else
            lblReport1.Text = "Error"
            lblReport2.Text = "Error"
            lblReport3.Text = "Error"
        End If
    End Sub

End Class