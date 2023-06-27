Public Class weatherforecast
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If ReadHTTPValue("pc", "") = "ERYTpnsyFv9h%rT4d3kT" Then
                lblResult.Text = Weatherlibrary.Forecast_Park7DayForecast_Type.PrepareReport()
            Else
                Page.Visible = False
            End If

        End If
    End Sub

End Class