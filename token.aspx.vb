Public Class token
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim pw As String = ReadHTTPValue("pw", "")
        If pw = "catdog52" Then
            Response.Write(CheckinToken.GetNewToken().webResponse)
        End If
    End Sub

End Class