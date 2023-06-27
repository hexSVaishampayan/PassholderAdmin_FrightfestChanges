Public Class refund
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Page.Title = "Season Pass Refunds"
            Master.sfToken = AppToken_Type.GetCurrentToken()
            If Master.sfToken.hasPermission(505) Then
            Else
                Response.Write("<p>You do not have permission to access this resource #505. Please contact Interactive Marketing.</p>")
                Response.Flush()
            End If
        End If

    End Sub

End Class