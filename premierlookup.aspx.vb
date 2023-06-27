Public Class premierlookup
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim parkid As Integer = ReadHTTPValue("parkID", 0)
        Dim passNumber As String = ReadHTTPValue("passNumber", "")
        If parkid > 0 And passNumber <> "" Then
            Dim url As String = "http://CoreTechs.PremierParksllc.com:50301/WebSalesService/Member/Validate?parkID=" + parkid.ToString + "&passNumber=" + passNumber
            Response.Write(ReadWebPage(url))
        End If
    End Sub

End Class