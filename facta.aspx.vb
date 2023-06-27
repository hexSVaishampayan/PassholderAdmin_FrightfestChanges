Imports MembershipLibrary

Public Class facta
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Page.Title = "FACTA Settlement Voucher Replacement"
            Master.sfToken = AppToken_Type.GetCurrentToken()
            If Master.sfToken.hasPermission(505) Then

            Else
                Response.Write("<p>You do not have permission to access this resource #505. Please contact Interactive Marketing.</p>")
                Response.Flush()
            End If
        End If
    End Sub

    Protected Sub cmdGenerateVoucher_Click(sender As Object, e As EventArgs) Handles cmdGenerateVoucher.Click
        fldMediaNumber.Text = fldMediaNumber.Text.Trim
        fldPhone.Text = fldPhone.Text.Trim
        If fldMediaNumber.Text.Length = 20 Then
            Dim fv As FactaVoucher_Type = FactaVoucher_Type.FindByMediaNumber(fldMediaNumber.Text)
            If Not fv Is Nothing Then
                If fv.ClassMemberID > 0 Then
                    Dim cm As ClassMember_Type = ClassMember_Type.FindByClassMemberID(fv.ClassMemberID)
                    If Not cm Is Nothing Then
                        cm.FactaVoucherID = 0
                        cm.ParkID = 0
                        fv.ClassMemberID = -1
                        FactaLog_Type.Log(200, cm.ClassMemberID, fv.FactaVoucherID, If(fldPhone.Text <> "", "Phone: " + fldPhone.Text, ""))
                        cm.Save()
                        fv.Save()
                        Master.AddError("Guest account has been reset. They can select another park now by refreshing the page or clicking the link in their email.")
                        If fldPhone.Text.Length > 6 Then
                            Dim phoneNo As String = fldPhone.Text
                            Dim txt As String = "Access your Six Flags Voucher: https://mypass.sixflags.com/facta/d.aspx?cm=" + cm.ClassMemberCode
                            Dim r As TwilioSMSStatus = SendSMS(phoneNo, txt)
                            Master.AddError("Guest has been texted a link to their voucher.")
                        End If
                        fldMediaNumber.Text = ""
                    Else
                        Master.AddError("Error loading Class Member record from voucher record. Please report this error. Cannot continue.")
                    End If
                Else
                    Master.AddError("This media number is not associated with a class member yet. It is likely you entered the incorrect number. Please try again.")
                End If
            Else
                Master.AddError("A voucher with this media number was not found. Please try again.")
            End If
        Else
            Master.AddError("Media number must be exactly 20 characters. Please check the number.")
        End If
        Master.DisplayErrors()
    End Sub
End Class

