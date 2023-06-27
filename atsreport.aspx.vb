Public Class atsreport
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblReport1.Text = ATSStatusReport_Type.GenerateReport(14)
        lblReport2.Text += ATSMultiPartyReport_Type.GenerateReport(14)
    End Sub

End Class