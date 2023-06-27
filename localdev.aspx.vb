Public Class localdev
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim url As String = ReadHTTPValue("url", "").UrlDecode
        Dim pw As String = ReadHTTPValue("pw", "").UrlDecode
        Dim TimeOut As Integer = ReadHTTPValue("to", 30000)
        Dim Auth As String = ReadHTTPValue("au", "").UrlDecode
        Dim Action As String = ReadHTTPValue("a", "POST")
        Dim payload As String = ReadHTTPPost()
        If Action <> "POST" Then payload = ""
        If pw = "pjGed88wNBsWXXhkPnC8MmF5UVqMfp8pNY9EM3zCupeXe9K3DTunMm9VUe5Pe73vpJnSkZEkhDp35Uet7WZk5aGr4aqeyyWP" Then
            Dim WebResults As WebResult = ReadWebPageWithWebResult(url, Action, payload, TimeOut, "application/json", If(Auth <> "", New NameValuePair("Authorization", Auth), Nothing))
            If WebResults.Response.StatusCode = Net.HttpStatusCode.OK Then
                Response.Write(WebResults.Output)
            End If
        Else
            Response.Write("<p>Password Error</p>")
        End If
    End Sub

End Class