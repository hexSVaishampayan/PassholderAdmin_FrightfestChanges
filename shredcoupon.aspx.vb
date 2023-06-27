Public Class shredcoupon
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            pnlResult.Visible = False
            If Not IsPostBack Then
                Master.sfToken = AppToken_Type.GetCurrentToken()
                If Master.sfToken.hasPermission(515) Then
                    Master.sfToken.LogEntry(33)
                    pnlForm.Visible = True
                    pnlError.Visible = False
                Else
                    pnlForm.Visible = False
                    pnlError.Visible = True
                End If
            End If
        Catch ex As Exception
            Master.AddError("Error: " + ex.Message + ex.StackTrace.Replace(vbCr, "<br>"))
        End Try
    End Sub


    Public Sub LoadCoupons()
        Try
            Dim c As CouponGuest_Type = CouponGuest_Type.FindByMediaNumber(fldMediaNumber.Text, True)
            If Not c Is Nothing Then
                Master.sfToken.LogEntry(34, "Viewed coupons for " + c.FullName + " (ID #" + c.mediaNumber + ")")
                pnlCoupons.Visible = True
                lblGuestInfo.Text = "<table class=guestInfo>"
                lblGuestInfo.Text += "<tr><td>Guest Name:</td><td class=fieldData>" + c.FullName + "</td></tr>"
                lblGuestInfo.Text += "<tr><td>Pass Type:</td><td class=fieldData>" + c.PassTypeDisplayName + "</td></tr>"
                lblGuestInfo.Text += "<tr><td>Park:</td><td class=fieldData>" + c.ParkName + "</td></tr>"
                lblGuestInfo.Text += "</table>"
                fldFolioUniqueID.Value = c.FolioUniqueID.ToString
                fldParkID.Value = c.ParkID
                If Not c.coupons Is Nothing AndAlso c.coupons.Length > 0 Then
                    For i As Integer = 0 To c.coupons.Length - 1
                        If Not c.coupons(i) Is Nothing Then
                            If c.coupons(i).usedQuantity < c.coupons(i).quantity And c.coupons(i).quantity > 0 And Not c.coupons(i).isUsed Then
                                Dim QtyAvail As Integer = c.coupons(i).quantity
                                Dim QtyText As String = If(QtyAvail > 1, "(" + QtyAvail.ToString + ") ", "")
                                ckCoupons.Items.Add(New ListItem(QtyText + c.coupons(i).couponName + " (" + c.coupons(i).couponCode + ")", c.coupons(i).couponCode))
                            End If
                        End If
                    Next
                End If
            Else
                Master.AddError("Error retrieving coupons from system. No response returned. Please check the number and try again.")
            End If
        Catch ex As Exception
            Master.AddError("Error: " + ex.Message + ex.StackTrace.Replace(vbCr, "<br>"))
        End Try
        Master.DisplayErrors()
    End Sub

    Protected Sub cmdFindMediaNumber_Click(sender As Object, e As EventArgs) Handles cmdFindMediaNumber.Click
        Try
            fldMediaNumber.Text = fldMediaNumber.Text.Trim.FilterToJustNumbers
            If fldMediaNumber.Text.Length >= 15 Then
                LoadCoupons()
            Else
                Master.AddError("Please enter a valid media number.")
            End If
        Catch ex As Exception
            Master.AddError("Error: " + ex.Message + ex.StackTrace.Replace(vbCr, "<br>"))
        End Try
        Master.DisplayErrors()
    End Sub

    Protected Sub cmdSubmit_Click(sender As Object, e As EventArgs) Handles cmdSubmit.Click
        Try
            Dim isItemsSelected As Boolean = False
            Dim CList As New CouponList_Type(True)
            If Not ckCoupons.Items Is Nothing AndAlso ckCoupons.Items.Count > 0 Then
                For i As Integer = 0 To ckCoupons.Items.Count - 1
                    If ckCoupons.Items(i).Selected Then
                        Dim FolioUniqueID As String = fldFolioUniqueID.Value
                        Dim ParkID As Integer = fldParkID.Value
                        Dim mc As BenefitsLibrary.Coupon_Type = CList.FindbyCouponCode(ckCoupons.Items(i).Value)
                        If Not mc Is Nothing Then
                            Dim c As ShredCoupon_Type = ShredCoupon_Type.ShredCoupon(FolioUniqueID, mc.CouponID, ParkID, 1, Now())
                            If c.Result = ShredCoupon_Type.Result_Type.Success Then
                                Master.AddError("Successfully shredded coupon " + ckCoupons.Items(i).Text)
                                Master.sfToken.LogEntry(31, "Shredded coupon " + mc.CouponCode + " for guest " + FolioUniqueID)
                            Else
                                Master.AddError("Error shredding coupon " + ckCoupons.Items(i).Text)
                                Master.sfToken.LogEntry(32, "Error shredding coupon " + mc.CouponCode + " for guest " + FolioUniqueID)
                            End If
                            ckCoupons.Items(i).Selected = False
                            isItemsSelected = True
                            Master.AddError("Coupon is no longer active or valid.")
                        End If
                    End If
                    ckCoupons.Items(i).Selected = False
                Next
                If Not isItemsSelected Then
                    Master.AddError("Please Select a coupon to shred.")
                Else
                    LoadCoupons()
                End If
            End If
        Catch ex As Exception
            Master.AddError("Error: " + ex.Message + ex.StackTrace.Replace(vbCr, "<br>"))
        End Try
        Master.DisplayErrors()
    End Sub
End Class