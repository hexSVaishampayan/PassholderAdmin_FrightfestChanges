Public Class MemberTest
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        pnlResult.Visible = False
        If Not IsPostBack Then
            Master.sfToken = AppToken_Type.GetCurrentToken()
            If Master.sfToken.hasPermission(515) Then
                pnlForm.Visible = True
                pnlError.Visible = False
            Else
                pnlForm.Visible = False
                pnlError.Visible = True
            End If
        End If
    End Sub

    Protected Sub cmdSubmit_Click(sender As Object, e As EventArgs) Handles cmdSubmit.Click
        fldIPAddress.Text = fldIPAddress.Text.Trim.FilterToJustNumbersWithDecimal
        Dim ip As System.Net.IPAddress
        Dim isValid As Boolean = System.Net.IPAddress.TryParse(fldIPAddress.Text, ip)
        Dim r As Integer = 0
        If isValid Then
            If Not fldBenefit.SelectedItem Is Nothing Then
                Dim cnn As New SqlConnection(ConnString("Staging"))
                Dim cmd As New SqlCommand("MemberTest_Update", cnn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@IPAddress", fldIPAddress.Text)
                cmd.Parameters.AddWithValue("@TestGroupID", fldBenefit.SelectedValue)
                Dim Success As Boolean = False
                Dim FailCount As Integer = 0
                Do
                    Try
                        cnn.Open()
                        r = cmd.ExecuteScalar
                        Success = True
                        If r = 1 Then
                            lblResult.Text = "IP address " + fldIPAddress.Text + " has been updated to " + fldBenefit.SelectedItem.Text.ToProperCase + ". Please have the guest clear their cookies and visit the purchase page again."
                        Else
                            lblResult.Text = "ALERT: There was an error updating IP address " + fldIPAddress.Text + " to " + fldBenefit.SelectedItem.Text.ToProperCase + ". Please consult your system administrator."
                        End If
                        pnlResult.Visible = True
                    Catch ex As SqlException
                        FailCount += 1
                        If FailCount > 3 Then
                            pnlResult.Visible = True
                            lblResult.Text = "There was an error saving this update: " + ex.Message
                        End If
                    Finally
                        cnn.Close()
                    End Try
                Loop Until Success Or FailCount > 3
            End If
        Else
            pnlResult.Visible = True
            lblResult.Text = "The IP address you have entered below is not valid. Please check the IP address and try again."
        End If

    End Sub

    Protected Sub cmdCheckOrder_Click(sender As Object, e As EventArgs) Handles cmdCheckOrder.Click
        Dim r As String = ""
        fldAccessoOrderID.Text = fldAccessoOrderID.Text.Trim.FilterToJustNumbers
        If fldAccessoOrderID.Text.Length = 9 AndAlso fldAccessoOrderID.Text.IsNumbersOnly Then
            Dim cnn As New SqlConnection(ConnString("Staging"))
            Dim cmd As New SqlCommand("MemberTest_LookupOrder", cnn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@AccessoOrderID", fldAccessoOrderID.Text)
            Dim Success As Boolean = False
            Dim FailCount As Integer = 0
            Do
                Try
                    cnn.Open()
                    r = cmd.ExecuteScalar
                    Success = True
                    If r <> "" Then
                        lblResult.Text = "Order #" + fldAccessoOrderID.Text + " " + r + "."
                    Else
                        lblResult.Text = "There is not a special offer on file for this order ID"
                    End If
                    pnlResult.Visible = True
                Catch ex As SqlException
                    FailCount += 1
                    If FailCount > 3 Then
                        pnlResult.Visible = True
                        lblResult.Text = "There was an error saving this update: " + ex.Message
                    End If
                Finally
                    cnn.Close()
                End Try
            Loop Until Success Or FailCount > 3
        Else
            pnlResult.Visible = True
            lblResult.Text = "Please enter a valid order ID."
        End If
    End Sub

    Protected Sub cmdLookupIP_Click(sender As Object, e As EventArgs) Handles cmdLookupIP.Click
        Dim r As String = ""
        fldIPLookup.Text = fldIPLookup.Text.Trim.FilterToJustNumbersWithDecimal
        If fldIPLookup.Text.Length >= 7 Then
            Dim cnn As New SqlConnection(ConnString("Staging"))
            Dim cmd As New SqlCommand("MemberTest_LookupIP", cnn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@IPAddress", fldIPLookup.Text)
            Dim Success As Boolean = False
            Dim FailCount As Integer = 0
            Do
                Try
                    cnn.Open()
                    r = cmd.ExecuteScalar
                    Success = True
                    If r <> "" Then
                        lblResult.Text = "IP address " + fldIPLookup.Text + " received the offer '" + r + "'."
                    Else
                        lblResult.Text = fldIPLookup.Text + " has not been assigned an offer yet."
                    End If
                    pnlResult.Visible = True
                Catch ex As SqlException
                    FailCount += 1
                    If FailCount > 3 Then
                        pnlResult.Visible = True
                        lblResult.Text = "There was an error looking up this IP address."
                    End If
                Finally
                    cnn.Close()
                End Try
            Loop Until Success Or FailCount > 3
        Else
            pnlResult.Visible = True
            lblResult.Text = "Please enter a valid IP address."
        End If
    End Sub
End Class