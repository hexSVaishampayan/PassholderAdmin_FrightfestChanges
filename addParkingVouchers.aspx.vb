Public Class addParkingVouchers
    Inherits System.Web.UI.Page

    Public ResultTxt As String = ""

    Public Sub Status(s As String, Optional SaveToLog As Boolean = True)
        If SaveToLog Then ResultTxt += "<p style='color:red;font-size:8pt;font-family:Arial;margin:0px;padding:0px'>" + s + "</p>"
        Response.Write("<p style='color:red;font-size:8pt;font-family:Arial;margin:0px;padding:0px'>" + s + "</p>")
        Response.Flush()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Master.sfToken = AppToken_Type.GetCurrentToken()
            If Master.sfToken.hasPermission(500) Then
                pnlForm.Visible = True
                '                Status("Logged in")
            Else
                Status("Access denied.")
                pnlForm.Visible = False
            End If
        End If
    End Sub

    Protected Sub cmdImport_Click(sender As Object, e As EventArgs) Handles cmdImport.Click
        ' 51010000180470000001 to  51010000180470010000 
        fldBeginning.Text = fldBeginning.Text.Trim.FilterToJustNumbers
        fldEnding.Text = fldEnding.Text.Trim.FilterToJustNumbers
        Dim errs As String = ""
        If fldBeginning.Text.Length < 10 Then errs += "<li>Beginning value is invalid.</li>"
        If fldEnding.Text.Length < 10 Then errs += "<li>Ending value is invalid.</li>"
        If errs.Length = 0 Then
            Dim Start As Integer = cInteger(Right(fldBeginning.Text, 7))
            Dim Finish As Integer = cInteger(Right(fldEnding.Text, 7))
            Dim Prefix As String = Left(fldBeginning.Text, fldBeginning.Text.Length - 7)
            If Prefix + Start.ToString("0000000") = fldBeginning.Text And Prefix + Finish.ToString("0000000") = fldEnding.Text Then
                If Finish > Start Then
                    Dim VoucherCount As Integer = Finish - Start
                    Dim V(VoucherCount) As ReservationVoucher_Type
                    Dim V_Count As Integer = -1
                    For i As Integer = Start To Finish
                        Dim Voucher As New ReservationVoucher_Type
                        With Voucher
                            .ReservationID = 0
                            .ReservationTypeID = 1
                            .ReservationVoucherTypeID = 1
                            .MediaNumber = Prefix + i.ToString("0000000")
                            .VoucherCode = "PV" + Prefix + i.ToString("0000000") + MakeRandomString(, 6)
                            .DateCreated = Now()
                            .Active = True
                        End With
                        V_Count += 1
                        V(V_Count) = Voucher
                    Next
                    ReservationVoucher_Type.SaveArray(V)
                    errs = "Success! Finished saving " + (V_Count + 1).ToString + " vouchers."
                Else
                    errs = "Ending number is smaller than starting number!"
                End If
            Else
                errs = "Mismatch in start or end values.<br>Start: " + Prefix + Start.ToString("0000000") + " <> " + fldBeginning.Text + "<br>Finish: " + Prefix + Finish.ToString("0000000") + " <> " + fldEnding.Text
            End If
        Else
            errs = "Please check the following:<ul>" + errs + "</ul>"
        End If
        If errs.Length > 0 Then
            Master.DisplayMessage(errs, "Please check the following:")
        End If
    End Sub
End Class