Public Class upgradereview
    Inherits System.Web.UI.Page

    Public Sub Status(s As String, Optional SaveToLog As Boolean = True)
        'If SaveToLog Then ResultTxt += "<p style='color:red;font-size:8pt;font-family:Arial;margin:0px;padding:0px'>" + s + "</p>"
        Response.Write("<p style='color:red;font-size:8pt;font-family:Arial;margin:0px;padding:0px'>" + s + "</p>")
        Response.Flush()
    End Sub

    Public Function GenerateRequestList(QueryID As Integer) As String
        Dim req As String = ""
        Dim Queries(9) As String
        Dim ParkID As Integer = cInteger(fldParkID.SelectedValue)
        Dim ParkQ As String = ""
        If ParkID > 0 Then ParkQ = " AND ParkID=" + ParkID.ToString
        Queries(0) = "Select * from MemberUpgradeRequest_Summary with (nolock) where MemberUpgradeRequestStatusID in (1,5)" + ParkQ + " order by DateRequested desc"
        Queries(1) = "Select * from MemberUpgradeRequest_Summary with (nolock) where MemberUpgradeRequestStatusID=1" + ParkQ + " order by DateRequested desc"
        Queries(2) = "Select * from MemberUpgradeRequest_Summary with (nolock) where MemberUpgradeRequestStatusID=2" + ParkQ + " order by DateRequested desc"
        Queries(3) = "Select * from MemberUpgradeRequest_Summary with (nolock) where MemberUpgradeRequestStatusID=3" + ParkQ + " order by DateRequested desc"
        Queries(4) = "Select * from MemberUpgradeRequest_Summary with (nolock) where MemberUpgradeRequestStatusID=4" + ParkQ + " order by DateRequested desc"
        Queries(5) = "Select * from MemberUpgradeRequest_Summary with (nolock) where MemberUpgradeRequestStatusID=5" + ParkQ + " order by DateRequested desc"
        Queries(6) = "Select * from MemberUpgradeRequest_Summary with (nolock) where MemberUpgradeRequestStatusID=6" + ParkQ + " order by DateRequested desc"
        Queries(7) = "Select * from MemberUpgradeRequest_Summary with (nolock) where MemberUpgradeRequestStatusID=7" + ParkQ + " order by DateRequested desc"
        Queries(8) = "Select * from MemberUpgradeRequest_Summary with (nolock) where MemberUpgradeRequestStatusID=8" + ParkQ + " order by DateRequested desc"
        Queries(9) = "Select * from MemberUpgradeRequest_Summary with (nolock) where MemberUpgradeRequestStatusID=9" + ParkQ + " order by DateRequested desc"
        Dim Query As String = Queries(QueryID)
        Dim mur() As MemberUpgradeRequestSummary_Type = MemberUpgradeRequestSummary_Type.LoadArray(Query)
        If Not mur Is Nothing AndAlso mur.Length > 0 Then
            For i As Integer = 0 To mur.Length - 1
                Dim li As String = "<tr>"
                li += "<td class=upgcol0><a href='upgradereviewitem.aspx?mur=" + mur(i).MemberUpgradeRequestID.ToString + "'>Review</a></td>"
                li += "<td class=upgcol1>" + mur(i).DateRequested.ToString("yyyy-MM-dd hh:mm") + "</a></td>"
                li += "<td class=upgcol2>" + mur(i).FirstName + " " + mur(i).LastName + "</td>"
                li += "<td class=upgcol3>" + mur(i).MemberUpgradeRequestStatusDesc + "</td>"
                li += "<td class=upgcol4>" + mur(i).OrigOrderID.ToString + "</td>"
                li += "<td class=upgcol5>" + mur(i).OrigOrderDate.ToString("yyyy-MM-dd") + "</td>"
                li += "<td class=upgcol6>" + mur(i).NewOrderID.ToString + "</td>"
                li += "<td class=upgcol7>" + mur(i).NewOrderDate.ToString("yyyy-MM-dd") + "</td>"
                li += "<td class=upgcol8>" + mur(i).CurrentAccountStatus + "</td>"
                li += "<td class=upgcol9>" + mur(i).RefundApplied.ToString("$0.00") + "</td>"
                req += li
            Next
            req = MakeHTMLTable("reqtab", "reqh", req, "Review", "Requested", "Name", "Status", "OrigID", "OrigDate", "NewID", "NewDate", "OrigStat")
        End If
        Return req
    End Function


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Master.sfToken = AppToken_Type.GetCurrentToken()
            If Master.sfToken.hasPermission(501) Then
                LoadDropDown(fldParkID, ConnString("Common"), "Park", "ParkID", "ParkName", "Parkid in (1,2,3,5,6,7,8,14,17,20,24)", ,,, True, "0", "All Parks")
                SetDropDown(fldParkID, 0)
                If Master.MemberUpgradeRequestStatusID <> 0 Then
                    SetDropDown(fldMemberUpgradeRequestStatusID, Master.MemberUpgradeRequestStatusID)
                End If
                If Master.UpgradeParkID <> 0 Then
                    SetDropDown(fldParkID, Master.UpgradeParkID)
                End If
                pnlForm.Visible = True
                QueryTypeID.Value = fldMemberUpgradeRequestStatusID.SelectedValue
                Master.MemberUpgradeRequestStatusID = cInteger(fldMemberUpgradeRequestStatusID.SelectedValue)
                lblReport.Text = GenerateRequestList(fldMemberUpgradeRequestStatusID.SelectedValue)
            Else
                Status("Access denied.")
                pnlForm.Visible = False
            End If
        End If

    End Sub

    Protected Sub fldMemberUpgradeRequestStatusID_SelectedIndexChanged(sender As Object, e As EventArgs) Handles fldMemberUpgradeRequestStatusID.SelectedIndexChanged
        QueryTypeID.Value = fldMemberUpgradeRequestStatusID.SelectedValue
        Master.MemberUpgradeRequestStatusID = cInteger(fldMemberUpgradeRequestStatusID.SelectedValue)
        lblReport.Text = GenerateRequestList(fldMemberUpgradeRequestStatusID.SelectedValue)
    End Sub

    Protected Sub fldParkID_SelectedIndexChanged(sender As Object, e As EventArgs) Handles fldParkID.SelectedIndexChanged
        Master.UpgradeParkID = fldParkID.SelectedValue
        lblReport.Text = GenerateRequestList(fldMemberUpgradeRequestStatusID.SelectedValue)
    End Sub

    Protected Sub cmdPromoCredit_Click(sender As Object, e As EventArgs) Handles cmdPromoCredit.Click
        Response.Redirect("forceupgrade.aspx")
    End Sub
End Class