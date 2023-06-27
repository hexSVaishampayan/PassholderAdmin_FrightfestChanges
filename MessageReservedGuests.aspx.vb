Public Class MessageReservedGuests
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Page.Title = "Member Reservations"
            Master.sfToken = AppToken_Type.GetCurrentToken()
            If 1 = 1 Then 'Master.sfToken.hasPermission(513) Then
                LoadDropDown(fldParkID, ParkArrayForMenu())
            Else
                Response.Write("<p>You do not have permission to access this resource #505. Please contact Interactive Marketing.</p>")
                Response.Flush()
            End If

        End If
    End Sub

End Class