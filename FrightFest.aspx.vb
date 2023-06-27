Imports Telerik.Web.UI.Calendar

Public Class FrightFest
    Inherits System.Web.UI.Page
    Public sfToken As AppAccessLibrary.AppToken_Type
    Const rowtmp As String = "<tr><td class='house'>{{house}}</td><td class='total'>{{total}}</td><td class='last60'>{{last60min}}</td><td class='hr'>{{t4pm}}</td><td class='hr'>{{t5pm}}</td><td class='hr'>{{t6pm}}</td><td class='hr'>{{t7pm}}</td><td class='hr'>{{t8pm}}</td><td class='hr'>{{t9pm}}</td><td class='hr'>{{t10pm}}</td><td class='hr'>{{t11pm}}</td><td class='hr'>{{t12am}}</td><td class='hr'>{{t1am}}</td><td class='hr'>{{t2am}}</td></tr>"
    Const rowstmp As String = "<tr class=subfoot><td class='house'>{{house}}</td><td class='total'>{{total}}</td><td class='last60'>{{last60min}}</td><td class='hr'>{{t4pm}}</td><td class='hr'>{{t5pm}}</td><td class='hr'>{{t6pm}}</td><td class='hr'>{{t7pm}}</td><td class='hr'>{{t8pm}}</td><td class='hr'>{{t9pm}}</td><td class='hr'>{{t10pm}}</td><td class='hr'>{{t11pm}}</td><td class='hr'>{{t12am}}</td><td class='hr'>{{t1am}}</td><td class='hr'>{{t2am}}</td></tr>"

    Public Function SummaryReport(sumrpt() As FFWristbandSummary_Type, ParkID As Integer) As String
        Dim r As String = ""
        If Not sumrpt Is Nothing AndAlso sumrpt.Length > 0 Then
            For i As Integer = 0 To sumrpt.Length - 1
                If sumrpt(i).ParkID = ParkID Then
                    r = ReadTextFile("~/frightfestsummarytemplate.html", True)
                    r = sumrpt(i).ApplyCodes(r)
                    Exit For
                End If
            Next
        End If
        Return r
    End Function

    Public Function Subfooter(rpt() As HouseReport_Type, ParkID As Integer) As String
        Dim r As String = ""
        For x As Integer = 0 To rpt.Length - 1
            If rpt(x).House = "" And rpt(x).ParkID = ParkID Then
                Dim rx As String = String.Copy(rowstmp)
                rx = rpt(x).ApplyCodes(rx)
                r += rx
                Exit For
            End If
        Next
        Return r
    End Function

    'https://feedback.sixflags.com/passadmin/frightfest.aspx?pw=ZUhKxKfN2onUEbg9CURGausxNGetP0aDq8dTSdatsml7

    Public Sub GenerateReport()
        SaveSessionValue("parkid", fldParkID.SelectedValue, True)
        Dim rpt() As HouseReport_Type = HouseReport_Type.ReadReport(fldReportDate.SelectedDate)
        Dim sumrpt() As FFWristbandSummary_Type = FFWristbandSummary_Type.ReadReport(fldReportDate.SelectedDate)
        Dim r As String = ""
        'Added By Sayali on 9/27/2022 for making report blank initially
        lblHouses.Text = ""
        lblHeadline.Text = ""
        'End of addition
        If Not rpt Is Nothing AndAlso rpt.Length > 0 Then
            Dim ParkID As Integer = 0
            For i As Integer = 0 To rpt.Length - 1
                If rpt(i).ParkID <> ParkID Then
                    If r <> "" Then
                        If ParkIDFromProductManagementID(ParkID) = fldParkID.SelectedValue.ConvertToInteger Or fldParkID.SelectedValue.ConvertToInteger = 0 Then
                            r += Subfooter(rpt, ParkID)
                            r += "</table>"
                            r += SummaryReport(sumrpt, ParkID)
                        End If
                    End If
                    ParkID = rpt(i).ParkID
                    If ParkIDFromProductManagementID(ParkID) = fldParkID.SelectedValue.ConvertToInteger Or fldParkID.SelectedValue.ConvertToInteger = 0 Then
                        r += "<h2>" + ParkNameLong(ParkIDFromProductManagementID(rpt(i).ParkID)) + "</h2>"
                        r += "<table class='rpt'>"
                        r += "<tr><th class=thhouse>House</th><th>Total</th><th>60m</th><th>4pm</th><th>5pm</th><th>6pm</th><th>7pm</th><th>8pm</th><th>9pm</th><th>10pm</th><th>11pm</th><th>12pm</th><th>1am</th><th>2am</th></tr>"
                    End If
                End If
                If ParkIDFromProductManagementID(ParkID) = fldParkID.SelectedValue.ConvertToInteger Or fldParkID.SelectedValue.ConvertToInteger = 0 Then
                    Dim row As String = String.Copy(rowtmp)
                    If rpt(i).House <> "" Then
                        row = rpt(i).ApplyCodes(row)
                        r += row
                    End If
                End If
            Next
            If ParkIDFromProductManagementID(ParkID) = fldParkID.SelectedValue.ConvertToInteger Or fldParkID.SelectedValue.ConvertToInteger = 0 Then
                r += Subfooter(rpt, ParkID)
                r += "</table>"
                r += SummaryReport(sumrpt, ParkID)
            End If
            lblHouses.Text = r
            lblUpdated.Text = "<div class=updated>Updated: " + Now.ToString("MMM d") + " at " + Now.ToString("h:mm tt") + "</div>"
            Dim mm As Date = fldReportDate.SelectedDate
            lblHeadline.Text = "Fright Pass Report for " + mm.ToString("MMMM d yyyy")
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Master.sfToken = AppToken_Type.GetCurrentToken()
            'Master.sfToken() = AppToken_Type.GetCurrentToken()
            'PassholderAdmin.

            'NewAdmin.Master.sfToken_Fright = AppToken_Type.GetCurrentToken()
            'Master.mnuMain.Visible = False

            If Master.sfToken.hasPermission(705) Then
                LoadDropDown(fldParkID, ConnString("Common"), "Park", "ParkID", "ParkName", "ParkID in (1,2,3,5,7,8,14,17,20,24,28,29,43,45)", "ParkID",,, True, "0", "All Parks")
                fldReportDate.SelectedDate = Today()
                SetDropDown(fldParkID, ReadSessionValue("parkid", 0))
                Dim pw As String = ReadHTTPValue("pw", "")
                If pw = "ZUhKxKfN2onUEbg9CURGausxNGetP0aDq8dTSdatsml7" Or isLocalUser() Then
                    fldReportDate.MaxDate = Today()
                    GenerateReport()
                Else
                    Page.Visible = False
                End If
            Else
                Dim s As String = "Access Denied"
                Response.Write("<p style='color:red;font-size:25pt;font-family:Arial;margin:15px;padding:0px;text-align:center;'>" + s + "</p>")
                Response.Flush()
                Page.Visible = False
                'MsgBox("Access Denied")
                'Status("Access denied.")
                'pnlForm.Visible = False
            End If
        End If
    End Sub

    Private Sub fldReportDate_SelectedDateChanged(sender As Object, e As SelectedDateChangedEventArgs) Handles fldReportDate.SelectedDateChanged
        GenerateReport()
    End Sub

    Protected Sub cmdGenerate_Click(sender As Object, e As EventArgs) Handles cmdGenerate.Click
        GenerateReport()
    End Sub

End Class