Public Class foliolookup
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim pw As String = ReadHTTPValue("pw", "")
        Dim mn As String = ReadHTTPValue("mn", "")
        If pw = "catdog52" Then
            Dim folio As Identity_Type.FolioLookup_Type = Identity_Type.FolioIDLookup(mn)
            If Not folio Is Nothing Then
                If Not folio.WebResults Is Nothing Then
                    Response.Write(folio.WebResults.Output)
                Else
                    Response.Write("<p>Folio.Webresults is nothing<br>")
                    Response.Write("Token: " + folio.Token + "<br>")
                    Response.Write("URL: " + folio.url)
                    Response.Write("</p>")
                End If
            Else
                Response.Write("Folio is nothing")
            End If
        Else
            Response.Write("Password Error")
        End If
    End Sub

End Class