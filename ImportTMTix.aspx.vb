Imports Telerik.Web.UI
Imports System.IO
Imports GemBox.Spreadsheet

Public Class ImportTMTix
    Inherits System.Web.UI.Page

    Public UploadDir As String = "~/Uploads"
    Public TotalRows As Integer = 0
    Public ResultTxt As String = ""
    Public sfToken As AppAccessLibrary.AppToken_Type

    Public Sub Status(s As String, Optional SaveToLog As Boolean = True)
        If SaveToLog Then ResultTxt += "<p style='color:red;font-size:8pt;font-family:Arial;margin:0px;padding:0px'>" + s + "</p>"
        Response.Write("<p style='color:red;font-size:8pt;font-family:Arial;margin:0px;padding:0px'>" + s + "</p>")
        Response.Flush()
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        sfToken = AppToken_Type.GetCurrentToken()
        If sfToken.hasPermission(4) Then
            RadAsyncUpload2.TargetFolder = UploadDir
            pnlFinished.Visible = False
            pnlForm.Visible = True
            fldExpirationDate.SelectedDate = ReadAppParameter(ConnString("TMTix"), "DefaultExpirationDate", False).ConvertToDate
            fldSeason.Text = Today.Year.ToString
            LoadDropDown(fldEntitlementTypeID, ConnString("TMTix"), "EntitlementType", "EntitlementTypeID", "EntitlementTypeDesc")
        Else
            Status("Access Denied.")
            pnlForm.Visible = False
        End If

    End Sub

    Private Sub RadAsyncUpload2_FileUploaded(sender As Object, e As FileUploadedEventArgs) Handles RadAsyncUpload2.FileUploaded
        Try
            Dim LocalPath = Server.MapPath(UploadDir)
            e.File.SaveAs(LocalPath + "\" + e.File.FileName)
            If e.File.GetExtension = ".zip" Then
                Dim zip As New Chilkat.Zip()
                Dim unlocked As Boolean
                unlocked = zip.UnlockComponent(Utilities.ChilkatCodes.Zip)
                If (Not unlocked) Then
                    MsgBox(zip.LastErrorText)
                    Exit Sub
                End If
                Dim success As Boolean
                Dim fn = e.File.FileName
                success = zip.OpenZip(LocalPath + "\" + e.File.FileName)
                Dim count As Integer
                count = zip.Unzip(LocalPath)
                zip.CloseZip()
                IO.File.Delete(LocalPath + "\" + e.File.FileName)
            End If
            e.IsValid = True
        Catch ex As Exception
            ReportErr("RadAsyncUpload1_FileUploaded", ex)
            Status(ex.Message)
        End Try
    End Sub

    Public Sub ExtractNameFromCommaName(s As String, ByRef FirstName As String, ByRef LastName As String)
        Dim pts() As String = s.Split(",")
        If pts.Length = 2 Then
            Dim lk() As String = pts(0).Split(" ")
            If lk.Length = 1 Then
                LastName = lk(0).Trim.ToProperCase
            ElseIf lk.Length = 2 Then
                If lk(1).Length > 3 Then
                    LastName = (lk(0) + " " + lk(1)).Trim.ToProperCase
                Else
                    LastName = lk(0).Trim.ToProperCase
                End If
            ElseIf lk.Length = 3 Then
                LastName = lk(0).Trim.ToProperCase
            ElseIf lk.Length > 3 Then
                LastName = pts(0).Trim.ToProperCase
            End If
            If Not pts(1) Is Nothing Then
                pts(1) = pts(1).Trim
                Dim fn() As String = pts(1).Split(" ")
                FirstName = fn(0).Trim.ToProperCase
            End If
        End If
    End Sub

    Public Sub ProcessFile(fn As String)
        Try
            Dim csv As New Chilkat.Csv
            csv.HasColumnNames = True
            Dim Success As Boolean = csv.LoadFile(fn)
            If Success Then
                Dim employee(50000) As EmployeeTicketImport_Type
                Dim employee_count As Integer = -1
                Dim xo(csv.NumRows) As EmployeeTicketImport_Type
                For i As Integer = 0 To csv.NumRows - 1
                    Dim em As New EmployeeTicketImport_Type
                    Dim tmp As String = ""
                    ExtractNameFromCommaName(csv.GetCellByName(i, "Name"), em.FirstName, em.LastName)
                    tmp = csv.GetCellByName(i, "EID")
                    If IsNumeric(tmp) And tmp.Length > 0 Then em.EmployeeID = tmp.ConvertToInteger(0)
                    tmp = csv.GetCellByName(i, "ParkCode")
                    em.ParkID = ParkCodeToParkID(tmp)
                    tmp = csv.GetCellByName(i, "Tickets")
                    If IsNumeric(tmp) And tmp.Length > 0 Then em.EntitlementCount = tmp.ConvertToInteger(0)
                    em.EntitlementTypeID = fldEntitlementTypeID.SelectedValue.ConvertToInteger
                    em.Season = fldSeason.Text.ConvertToInteger
                    em.Email = csv.GetCellByName(i, "Email")
                    em.AltEmail = csv.GetCellByName(i, "AltEmail")
                    em.Phone = csv.GetCellByName(i, "Phone")
                    em.WorkPhone = csv.GetCellByName(i, "WorkPhone")
                    em.DateIssued = Today
                    em.ExpirationDate = fldExpirationDate.SelectedDate
                    em.DateAdded = Now()
                    If em.FullName <> "" And em.EmployeeID <> 0 Then
                        employee_count += 1
                        employee(employee_count) = em
                    Else
                        Dim s As String = ""
                        For x As Integer = 0 To csv.GetNumCols(i) - 1
                            s += csv.GetCell(i, x) + ";"
                        Next
                        Status("Row #" + i.ToString + " is nothing:" + s)
                    End If
                    If Int(i / 1000) = (i / 1000) Then
                        Status("Processed: " + Int(i / csv.NumRows * 100).ToString + "%<br>")
                    End If
                Next
                Status("Ticket Requests read: " + (employee_count - 1).ToString)
                If employee_count > -1 Then
                    Array.Resize(employee, employee_count + 1)
                    EmployeeTicketImport_Type.SaveArray(employee)
                    'Employee_Type.ImportAdjustedRecords(employee) ' Use this to import all records from scratch
                    Status("Finished processing.")
                End If
                IO.File.Delete(fn)
            End If
            SQL(ConnString("TMTix"), "EmployeeTicketImport_Import", True)
            Status("")
            Status("")
        Catch ex As Exception
            ReportErr("ProcessFile()", ex)
            Status(ex.Message)
        End Try
    End Sub

    Protected Sub cmdUpload_Click(sender As Object, e As EventArgs) Handles cmdUpload.Click
        Try
            fldSeason.Text = fldSeason.Text.Trim.FilterToJustNumbers
            Dim Season = fldSeason.Text.ConvertToInteger
            If Season < 2021 Or Season > Today.Year + 2 Then fldSeason.Text = Today.Year.ToString
            If fldSeason.Text.Length < 4 Then fldSeason.Text = Today.Year.ToString
            If fldExpirationDate.SelectedDate Is Nothing Then fldExpirationDate.SelectedDate = ReadAppParameter(ConnString("TMTix"), "DefaultExpirationDate", False).ConvertToDate
            Status("Employee Ticketing Import Session for " + Now.ToLongDateString)
            Dim LocalPath = Server.MapPath(UploadDir)
            Dim fInfo As FileInfo()
            Dim dInfo As New DirectoryInfo(LocalPath)
            fInfo = dInfo.GetFiles("*.csv")
            Dim files As String() = Directory.GetFiles(LocalPath, "*.csv")
            If Not files Is Nothing AndAlso files.Length > 0 Then
                For Each FName As String In files
                    Status("Processing: " + FName, False)
                    ProcessFile(FName)
                Next
                pnlFinished.Visible = True
                pnlForm.Visible = False
                Dim SendTo As String = "mark@kupferman.com"
                GreenArrow.SendMsg("TMV Employee Upload Lod for " + Now.ToShortDateString, SendTo, ResultTxt)
            Else
                Status("No qualified files found.")
            End If
        Catch ex As Exception
            ReportErr("cmdUpdate_Click", ex)
            Status(ex.Message)
        End Try
    End Sub

End Class