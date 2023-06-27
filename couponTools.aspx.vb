Public Class couponTools
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.Title = "Member Reservations"
        Master.sfToken = AppToken_Type.GetCurrentToken()
        If Master.sfToken.hasPermission(506) Then

        Else
            Response.Redirect("https://www.sixflags.com")
        End If

    End Sub

    Protected Sub cmdSubmit_Click(sender As Object, e As EventArgs) Handles cmdSubmit.Click

    End Sub
End Class