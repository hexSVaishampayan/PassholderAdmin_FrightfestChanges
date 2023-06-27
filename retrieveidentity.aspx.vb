Public Class retrieveidentity
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim pw As String = ReadHTTPValue("pw", "")
        Dim mn As String = ReadHTTPValue("mn", "")
        If pw = "catdog52" Then
            Dim x As Identity_Type = Identity_Type.RetrieveIdentityRecord(mn)
            Response.Write(x.webOutput)
        End If
    End Sub

End Class