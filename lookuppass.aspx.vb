Public Class lookuppass
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Page.Title = "Season Pass Lookup"
            Dim pw As String = ReadHTTPValue("pw", "")
            If pw = "58nF96Qw9VrMG38s3Wc2vLcRmYVBtzO88ifQuZTC12EVQoDk1t5l" Then
                pnlContent.Visible = True
            Else
                pnlContent.Visible = False
                Response.Write("<p>You do not have permission to access this resource #505. Please contact Interactive Marketing.</p>")
                Response.Flush()
            End If
        End If
    End Sub

    Public Function VisitTable(url As String)

    End Function

    Protected Sub cmdSearch_Click(sender As Object, e As EventArgs) Handles cmdSearch.Click
        fldSearchText.Text = fldSearchText.Text.Trim
        If fldSearchText.Text.Length > 8 Then
            Dim sl() As Scanlog_Type = Nothing
            If fldSearchType.SelectedValue = 1 Then
                sl = Scanlog_Type.FindByMediaNumber(fldSearchText.Text)
            End If
            If Not sl Is Nothing Then
                Dim r As String = ""
                For i As Integer = 0 To sl.Length - 1
                    Dim li As String = "<tr>"
                    li += "<td class='col1'>" + sl(i).VisitDate.ToShortDateString + "</td>"
                    li += "<td class='col2'>" + sl(i).MediaNumber + "</td>"
                    li += "<td class='col3'>" + ParkNameLong(sl(i).VisitParkID) + "</td>"
                    r += "</tr>"
                    r += li
                Next
                If Not r Is Nothing Then r = "<table class=visitlist><tr><th>VisitDate</th><th>Media Number</th><th>Park</th>" + r + "</table>"
                lblResults.Text = r
            Else
                lblResults.Text = "<div class=noresults>No Visits Found</div>"
            End If
        Else
            lblResults.Text = "Please enter a valid Media number or order number"
        End If
    End Sub
End Class