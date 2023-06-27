Imports Telerik.Web.UI.Calendar

Public Class inparksales
    Inherits System.Web.UI.Page

    Public Function SummaryTable() As DataTable

        Dim conn As New SqlConnection(ConnString("MarketingAnalysis"))
        Dim adapter As New SqlDataAdapter()
        adapter.SelectCommand = New SqlCommand("Report_InParkSales", conn)
        adapter.SelectCommand.Parameters.AddWithValue("ReportDate", fldReportDate.SelectedDate)
        adapter.SelectCommand.CommandType = CommandType.StoredProcedure
        Dim myDataTable As New DataTable()
        conn.Open()
        Try
            adapter.Fill(myDataTable)
        Finally
            conn.Close()
        End Try
        Return myDataTable
    End Function

    Public Function TMData() As DataTable

        Dim conn As New SqlConnection(ConnString("MarketingAnalysis"))
        Dim adapter As New SqlDataAdapter()
        adapter.SelectCommand = New SqlCommand("Report_TMSales", conn)
        adapter.SelectCommand.Parameters.AddWithValue("ReportDate", fldReportDate.SelectedDate)
        adapter.SelectCommand.CommandType = CommandType.StoredProcedure
        Dim myDataTable As New DataTable()
        conn.Open()
        Try
            adapter.Fill(myDataTable)
        Finally
            conn.Close()
        End Try
        Return myDataTable
    End Function



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            pnlReport.Visible = False
            Page.Title = "In-Park Member Sales"
            Dim pass As String = ReadHTTPValue("p", "").Trim.ToUpper
            Dim dr As Date = ReadHTTPValue("dt", DefaultDate)
            Dim validated As Boolean = False
            If pass <> "p8jGEdA5r2Gs3Tc5GGA8Hgbv" Then
                validated = True
            Else
                Master.sfToken = AppToken_Type.GetCurrentToken()
                If Master.sfToken.hasPermission(504) Then validated = True
            End If
            If validated Then
                pnlReport.Visible = True
                fldReportDate.MinDate = #1/15/2018#
                fldReportDate.MaxDate = Today
                fldReportDate.SelectedDate = Today
                If dr <> DefaultDate Then fldReportDate.SelectedDate = dr
                RefreshReports()
            Else
                Response.Write("<p>Please contact Jordan DePuma or Mark Kupferman for access to this report.</p>")
            End If
        End If

    End Sub

    Public Sub RefreshReports()
        fldGrid.Rebind()
        Try
            Dim rows() As InParkSalesSummary_Type = InParkSalesSummary_Type.LoadReport(fldReportDate.SelectedDate)
            If rows.Length > 0 Then
                Dim MaxTime As DateTime = DefaultDate
                Dim MaxInPark As DateTime = DefaultDate
                For i As Integer = 0 To rows.Length - 1
                    If rows(i).MaxOrderDate > MaxTime Then MaxTime = rows(i).MaxOrderDate
                    If rows(i).InParkMaxDate > MaxInPark And rows(i).InParkMaxDate <= Now() Then MaxInPark = rows(i).InParkMaxDate
                Next
                lblHeader.Text = "In-Park Sales Summary"
                lblHeader2.Text = "For " + MaxTime.ToString("MMMM d, yyyy")
                lblLastUpdated.Text = "Update Status:<br>Membership Sales: " + MaxTime.ToString("h:mmtt") + "<br>Season Pass: " + MaxInPark.ToString("h:mmtt") + "<br>"
            End If
            fldGrid.Rebind()
            TMGrid.Rebind()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub fldGrid_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles fldGrid.NeedDataSource
        fldGrid.DataSource = SummaryTable()
    End Sub

    Private Sub TMGrid_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles TMGrid.NeedDataSource
        TMGrid.DataSource = TMData()
    End Sub

    Private Sub fldReportDate_SelectedDateChanged(sender As Object, e As SelectedDateChangedEventArgs) Handles fldReportDate.SelectedDateChanged
        Dim dr As Date = fldReportDate.SelectedDate
        Response.Redirect("inparksales.aspx?p=contest2&dt=" + dr.ToShortDateString)
    End Sub
End Class