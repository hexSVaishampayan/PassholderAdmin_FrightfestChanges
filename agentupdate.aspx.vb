Public Class agentupdate
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Dim y As String = Agent_Type.CreateAgents()
        Dim x = Agent_Type.UpdateAgents()
        x += FreshdeskGroup_Type.UpdateGroups()
        Response.Write(x)

    End Sub

End Class