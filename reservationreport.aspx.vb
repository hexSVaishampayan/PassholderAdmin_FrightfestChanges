Public Class reservationreport
    Inherits System.Web.UI.Page

    Public Function PrepareReservationList(ParkID_ As Integer, VisitDate_ As Date, ReservationTypeID_ As Integer, ReservationLocationID_ As Integer) As DataTable
        Dim cnn As New SqlConnection(ConnString("PassholderSupport"))
        Dim adapter As New SqlDataAdapter()
        adapter.SelectCommand = New SqlCommand("Reservation_List", cnn)
        With adapter.SelectCommand
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@ParkID", ParkID_)
            .Parameters.AddWithValue("@VisitDate", VisitDate_)
            .Parameters.AddWithValue("@ReservationTypeID", ReservationTypeID_)
            .Parameters.AddWithValue("@ReservationLocationID", ReservationLocationID_)
        End With
        Dim myDataTable As New DataTable()
        cnn.Open()
        Try
            adapter.Fill(myDataTable)
        Finally
            cnn.Close()
        End Try
        Return myDataTable
    End Function

    Public Function PrepareReservationStatus(ParkID_ As Integer, VisitDate_ As Date, ReservationTypeID_ As Integer, ReservationLocationID_ As Integer) As DataTable
        Dim cnn As New SqlConnection(ConnString("PassholderSupport"))
        Dim adapter As New SqlDataAdapter()
        adapter.SelectCommand = New SqlCommand("Reservation_List_Status", cnn)
        With adapter.SelectCommand
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@ParkID", ParkID_)
            .Parameters.AddWithValue("@VisitDate", VisitDate_)
            .Parameters.AddWithValue("@ReservationTypeID", ReservationTypeID_)
            .Parameters.AddWithValue("@ReservationLocationID", ReservationLocationID_)
        End With
        Dim myDataTable As New DataTable()
        cnn.Open()
        Try
            adapter.Fill(myDataTable)
        Finally
            cnn.Close()
        End Try
        Return myDataTable
    End Function

    Public Function PrepareSummary(ParkID_ As Integer, ReservationTypeID_ As Integer, ReservationLocationID_ As Integer) As DataTable
        Dim cnn As New SqlConnection(ConnString("PassholderSupport"))
        Dim adapter As New SqlDataAdapter()
        adapter.SelectCommand = New SqlCommand("Reservation_Summary", cnn)
        With adapter.SelectCommand
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@ParkID", ParkID_)
            .Parameters.AddWithValue("@ReservationTypeID", ReservationTypeID_)
            .Parameters.AddWithValue("@ReservationLocationID", ReservationLocationID_)
        End With
        Dim myDataTable As New DataTable()
        cnn.Open()
        Try
            adapter.Fill(myDataTable)
        Finally
            cnn.Close()
        End Try
        Return myDataTable
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            pnlReport.Visible = False
            Page.Title = "Member Reservations"
            Master.sfToken = AppToken_Type.GetCurrentToken()
            If Master.sfToken.hasPermission(505) Then
                pnlReport.Visible = True
                Dim ParkID As Integer = LoadCookie("ReserveParkID", 1)
                Dim ReservationTypeID As Integer = LoadCookie("ReserveTypeID", 1)
                Dim ReservationLocationID As Integer = LoadCookie("ReserveLocationID", 1)
                radHelpers.LoadRadDropDown(fldParkID, ConnString("PassholderSupport"), "ReservationPark_DropDownList_Parks", "ParkID", "ParkAbb")
                radHelpers.SetRadDropDown(fldParkID, ParkID)
                radHelpers.LoadRadDropDown(fldReservationType, ConnString("PassholderSupport"), "ReservationPark_DropDownList", "ReservationTypeID", "ReservationTypeDesc", "ParkID=" + fldParkID.SelectedValue)
                radHelpers.SetRadDropDown(fldReservationType, ReservationTypeID)
                Dim query As String = "ParkID=" + ParkID.ToString + " and ReservationTypeID=" + ReservationTypeID.ToString
                radHelpers.LoadRadDropDown(fldReservationLocationID, ConnString("PassholderSupport"), "ReservationLocation_DropDownList", "ReservationLocationID", "ReservationLocationName", query)
                radHelpers.SetRadDropDown(fldReservationLocationID, ReservationLocationID)
                fldDateToShow.SelectedDate = Today
            Else
                Response.Write("<p>You do not have permission to access this resource #505. Please contact Interactive Marketing.</p>")
                Response.Flush()
            End If
        End If
    End Sub

    Private Sub fldGrid_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles fldGrid.NeedDataSource
        Dim tRows As DataTable = PrepareReservationList(fldParkID.SelectedValue.ConvertToInt64, fldDateToShow.SelectedDate, fldReservationType.SelectedValue.ConvertToInt64, fldReservationLocationID.SelectedValue.ConvertToInteger)
        TryCast(sender, RadGrid).DataSource = tRows
        lblListHeader.Text = "<h1 class=majorListHeader>Reservations for " + CDate(fldDateToShow.SelectedDate).ToString("MMMM d") + "</h1>"
        fldGrid.ExportSettings.Pdf.PageTitle = ParkNameLong(cInteger(fldParkID.SelectedValue)) + " " + fldReservationType.SelectedText + " for " + CDate(fldDateToShow.SelectedDate).ToString("MMMM d, yyyy")
        fldGrid.ExportSettings.Pdf.PageFooter.RightCell.Text = "Version: " + Now().ToShortDateString + " " + Now().ToShortTimeString + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
        fldGrid.ExportSettings.Pdf.PageFooter.LeftCell.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + tRows.Rows.Count.ToString + " Reservations"
    End Sub

    Private Sub fldReservationStatus_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles fldReservationStatus.NeedDataSource
        Dim tRows As DataTable = PrepareReservationStatus(fldParkID.SelectedValue.ConvertToInt64, fldDateToShow.SelectedDate, fldReservationType.SelectedValue.ConvertToInt64, fldReservationLocationID.SelectedValue.ConvertToInteger)
        TryCast(sender, RadGrid).DataSource = tRows
        lblAllRecords.Text = "<h1 class=majorListHeader>All Records (Including Cancelled and Waitlist)</h1>"
        fldReservationStatus.ExportSettings.Pdf.PageTitle = ParkNameLong(cInteger(fldParkID.SelectedValue)) + " " + fldReservationType.SelectedText + " for " + CDate(fldDateToShow.SelectedDate).ToString("MMMM d, yyyy")
        fldReservationStatus.ExportSettings.Pdf.PageFooter.RightCell.Text = "Version: " + Now().ToShortDateString + " " + Now().ToShortTimeString + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
        fldReservationStatus.ExportSettings.Pdf.PageFooter.LeftCell.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + tRows.Rows.Count.ToString + " Reservations"
    End Sub


    Protected Sub cmdUpdate_Click(sender As Object, e As EventArgs) Handles cmdUpdate.Click
        SaveCookie("ReserveParkID", fldParkID.SelectedValue.ConvertToInteger, Now.AddYears(1))
        SaveCookie("ReserveTypeID", fldReservationType.SelectedValue.ConvertToInteger, Now.AddYears(1))
        SaveCookie("ReserveLocationID", fldReservationLocationID.SelectedValue.ConvertToInteger, Now.AddYears(1))
        fldGrid.Rebind()
        fldSummary.Rebind()
    End Sub

    Private Sub fldSummary_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles fldSummary.NeedDataSource
        TryCast(sender, RadGrid).DataSource = PrepareSummary(fldParkID.SelectedValue.ConvertToInt64, fldReservationType.SelectedValue.ConvertToInt64, fldReservationLocationID.SelectedValue.ConvertToInteger)
    End Sub

    Private Sub fldParkID_SelectedIndexChanged(sender As Object, e As DropDownListEventArgs) Handles fldParkID.SelectedIndexChanged
        radHelpers.LoadRadDropDown(fldReservationType, ConnString("PassholderSupport"), "ReservationPark_DropDownList", "ReservationTypeID", "ReservationTypeDesc", "ParkID=" + fldParkID.SelectedValue)
        Dim ParkID As Integer = fldParkID.SelectedValue.ConvertToInteger
        Dim ReservationTypeID As Integer = fldReservationType.SelectedValue.ConvertToInteger
        Dim query As String = "ParkID=" + ParkID.ToString + " and ReservationTypeID=" + ReservationTypeID.ToString
        radHelpers.LoadRadDropDown(fldReservationLocationID, ConnString("PassholderSupport"), "ReservationLocation_DropDownList", "ReservationLocationID", "ReservationLocationName", query)
    End Sub

    Private Sub fldReservationType_SelectedIndexChanged(sender As Object, e As DropDownListEventArgs) Handles fldReservationType.SelectedIndexChanged
        Dim ParkID As Integer = fldParkID.SelectedValue.ConvertToInteger
        Dim ReservationTypeID As Integer = fldReservationType.SelectedValue.ConvertToInteger
        Dim query As String = "ParkID=" + ParkID.ToString + " and ReservationTypeID=" + ReservationTypeID.ToString
        radHelpers.LoadRadDropDown(fldReservationLocationID, ConnString("PassholderSupport"), "ReservationLocation_DropDownList", "ReservationLocationID", "ReservationLocationName", query)
    End Sub

End Class