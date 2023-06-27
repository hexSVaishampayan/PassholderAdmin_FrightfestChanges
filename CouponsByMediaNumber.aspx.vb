Public Class CouponsByMediaNumber
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim MediaNumber As String = ReadHTTPValue("medianumber", "")
        If MediaNumber <> "" Then
            Dim domain As String = "services01.sftp.com"
            Dim q As String = "https://" + domain + "/benefits/api/coupon/getcouponsbymedianumber?medianumber=" + MediaNumber
            Dim wr As WebResult = ReadWebPageWithWebResult(q, "GET", "", 30000, "application/json", New NameValuePair("accept", "application/json"))
            If Not wr Is Nothing Then
                If Not wr.Output Is Nothing Then
                    Dim s As String = wr.Output
                    Response.Write(s)
                    Response.Flush()
                End If
            End If
        End If
    End Sub

End Class