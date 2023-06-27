Public Class atsadmin
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadDropDown(fldParkID, ConnString("Common"), "Park", "ParkID", "ParkName", "Not ParkID in (50,51,52,53)", "ParkID")
            lblRemainingPrizes.Text = RemainingPrizes()
        End If
    End Sub

    Public Function RemainingPrizes() As String
        Dim r As String = ""
        Dim cnn As New SqlConnection(ConnString("Panel"))
        Dim cmd As New SqlCommand("AtsPrize_Remaining", cnn)
        Dim dat As SqlDataReader = Nothing
        Dim Success As Boolean = False
        Dim FailCount As Integer = 0
        cmd.CommandType = CommandType.StoredProcedure
        Do
            Try
                cnn.Open()
                dat = cmd.ExecuteReader
                If dat.HasRows Then
                    dat.Read()
                    r += "<table class=prizesremaining>"
                    Do
                        r += "<tr><td class=parkname>" + ReadDBValue(dat, "ParkName", "") + "</td>"
                        r += "<td class=remaining>" + ReadDBValue(dat, "Remaining", 0).ToString("#,##0") + "</td></tr>"
                    Loop Until Not dat.Read
                    dat.Close()
                    r += "</table>"
                End If
                Success = True
            Catch ex As SqlException
                FailCount += 1
                If FailCount > 3 Then ReportErr(ex.TargetSite.Module.Name + " " + ex.TargetSite.Name, ex)
            Finally
                cnn.Close()
            End Try
        Loop Until Success Or FailCount > 3
        Return r
    End Function

    Public Function isUploadValid() As Boolean
        fldStartNumber.Text = fldStartNumber.Text.Trim.FilterToJustNumbers
        fldRangeLength.Text = fldRangeLength.Text.Trim.FilterToJustNumbers
        If fldStartNumber.Text.Length = 0 Then
            Master.AddError("Please enter a media number!")
        ElseIf fldStartNumber.Text.Length < 19 Then
            Master.AddError("Your media number isn't long enough! Fix it.")
        End If
        If fldRangeLength.Text.Length = 0 Then
            Master.AddError("Please enter a valid range length! Not enoug codes.")
        End If
        If fldParkID.SelectedValue = "0" Then Master.AddError("Please select a park!")
        If Master.HasErrors Then
            Master.DisplayErrors()
            Return False
        End If
        Return True
    End Function

    Protected Sub cmdUpload_Click(sender As Object, e As EventArgs) Handles cmdUpload.Click
        If isUploadValid() Then
            Dim Result As Integer = SQL(ConnString("Panel"), "ATSPrize_AddCodes", True, New NameValuePair("@MediaNumber", fldStartNumber.Text), New NameValuePair("@RangeLength", fldRangeLength.Text), New NameValuePair("@ParkID", fldParkID.SelectedValue))
            lblRemainingPrizes.Text = RemainingPrizes()
            Master.AddError("Successfully added {{codes}} codes!")
            Master.DisplayErrors()
        End If
    End Sub
End Class

