Public Class validatepassno
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim pw As String = ReadHTTPValue("pw", "")
        Dim mn As String = ReadHTTPValue("mn", "")
        If pw = "catdog52" Then
            Dim x As ValidatePassStatus_Type = MediaNumberValidation_Type.RetrievePassStatusFromServer(mn, True)
            Response.Write(x.webResultData.Output)
        End If
    End Sub

End Class