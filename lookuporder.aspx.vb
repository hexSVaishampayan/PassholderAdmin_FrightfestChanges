Public Class lookuporder
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Page.Title = "Order Lookup"
            Dim pw As String = ReadHTTPValue("pw", "")
            If pw = "58nF96Qw9VrMG38s3Wc2vLcRmYVBtzO88ifQuZTC12EVQoDk1t5l" Then
                pnlContent.Visible = True
            Else
                pnlContent.Visible = False
                Response.Write("<p>You do not have permission to access this resource #505. Please contact Interactive Marketing.</p>")
                Response.Flush()
            End If
        End If
    End Sub

    Protected Sub cmdSearch_Click(sender As Object, e As EventArgs) Handles cmdSearch.Click
        fldSearchText.Text = fldSearchText.Text.Trim.FilterToJustNumbers
        If fldSearchText.Text.Length >= 9 Then
            Dim ao As AccessoOrder_Type = AccessoOrder_Type.FindByAccessoOrderID_Extended(fldSearchText.Text.ConvertToInteger, False, True)
            If Not ao Is Nothing Then
                Dim r As String = ""
                r += "<h2>Order Overview</h2>"
                r += "<table class=buyerinfo>"
                r += "<tr><td class=itemdesc>Order ID</td><td class=itemdetail>" + ao.AccessoOrderID.ToString + "</td>"
                r += "<tr><td class=itemdesc>Park</td><td class=itemdetail>" + ParkNameLong(ao.ParkID) + "</td>"
                r += "<tr><td class=itemdesc>Order Date</td><td class=itemdetail>" + ao.OrderDate.ToString + "</td>"
                r += "<tr><td class=itemdesc>Purchaser Name</td><td class=itemdetail>" + ao.FirstName + " " + ao.LastName + "</td>"
                r += "<tr><td class=itemdesc>Address</td><td class=itemdetail>" + ao.Address1 + "<br>" + ao.City + ", " + ao.PostalCode + "</td>"
                r += "<tr><td class= itemdesc > Phone Number</td><td Class=itemdetail>" + ao.PhoneNumber + "</td>"
                r += "<tr><td Class=itemdesc>Email</td><td Class=itemdetail><a href='mailto:" + ao.Email + "'>" + ao.Email + "</td>"
                r += "<tr><td class=itemdesc>IP Address</td><td class=itemdetail>" + ao.IPAddress + "</td>"
                r += "<tr><td class=itemdesc>Order Total</td><td class=itemdetail>" + ao.OrderAmount.ToString("$0.00") + "</td>"
                r += "<tr><td class=itemdesc>Processing Fee</td><td class=itemdetail>" + ao.ProcessingFee.ToString("$0.00") + "</td>"
                r += "</table>"

                If ao.MonthlyPayment > 0 Then
                    r += "<table class=buyerinfo>"
                    r += "<tr><td class=itemdesc>Monhtly Payment</td><td class=itemdetail>" + ao.MonthlyPayment.ToString("$0.00") + "</td>"
                    r += "<tr><td class=itemdesc>Members in Order</td><td class=itemdetail>" + ao.MemberCount.ToString + "</td>"
                    r += "</table>"
                End If

                Dim aod() As AccessoOrderDetail_Type = AccessoOrderDetail_Type.FindByAccessoOrderID(fldSearchText.Text.ConvertToInteger)
                If Not aod Is Nothing And aod.Length > 0 Then
                    r += "<h2>Order Items</h2>"
                    r += "<table class=itemlist>"
                    r += "<tr><th>LineItem</th><th>SKU</th><th>Store Front Product</th><th>Product Type</th><th>Level</th><th>Price</th><th>Tax</th><th>Name</th></tr>"
                    For i As Integer = 0 To aod.Length - 1
                        If Not aod(i) Is Nothing Then
                            r += "<tr>"
                            r += "<td class=it>" + aod(i).LineItemID.ToString + "</td>"
                            r += "<td class=it>" + aod(i).StoreFrontProductID.ToString + "</td>"
                            r += "<td class=it>" + aod(i).StoreFrontProductName + "</td>"
                            r += "<td class=it>" + Lookup.ProductTypeDesc(aod(i).ProductTypeID) + "</td>"
                            r += "<td class=it>" + Lookup.ProductLevelDesc(aod(i).ProductLevelID) + "</td>"
                            r += "<td class=it>" + aod(i).UnitPrice.ToString("$0.00") + "</td>"
                            r += "<td class=it>" + aod(i).UnitTax.ToString("$0.00") + "</td>"
                            r += "<td class=it>" + aod(i).FirstName + " " + aod(i).LastName + "</td>"
                            r += "</tr>"
                        End If
                    Next
                    r += "</table>"
                End If
                Dim st As String = AccessoPayment_Type.PaymentHistory(fldSearchText.Text.ConvertToInteger)
                If st <> "" Then
                    r += "<h2>Payment History</h2>"
                    r += st
                End If

                lblResults.Text = r
            Else
                    lblResults.Text = "<div class=noresults>No Results Found.</div>"
            End If
        End If
    End Sub
End Class