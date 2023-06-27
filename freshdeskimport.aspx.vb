Imports System.IO


Public Class freshdeskimport
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

    Private Sub RadAsyncUpload1_FileUploaded(sender As Object, e As Telerik.Web.UI.FileUploadedEventArgs) Handles RadAsyncUpload1.FileUploaded
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
            e.IsValid = False
        Catch ex As Exception
            ReportErr("RadAsyncUpload1_FileUploaded", ex)
            Status(ex.Message)
        End Try
    End Sub

    Public Sub ClearDirectory()
        Dim LocalPath = Server.MapPath(UploadDir)
        Dim fInfo As FileInfo()
        Dim dInfo As New DirectoryInfo(LocalPath)
        fInfo = dInfo.GetFiles("*.csv")
        Dim files As String() = Directory.GetFiles(LocalPath, "*.csv")
        If Not files Is Nothing AndAlso files.Length > 0 Then
            For Each FName As String In files
                IO.File.Delete(FName)
            Next
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Page.Title = "Import Freshdesk"
            Master.sfToken = AppToken_Type.GetCurrentToken()
            If Master.sfToken.hasPermission(26) Then
                Try
                    ClearDirectory()
                    RadAsyncUpload1.TargetFolder = UploadDir
                Catch ex As Exception
                    Response.Write("<p>Error initializing page. Help!</p>")
                    Response.Flush()
                End Try
            Else
                Response.Write("<p>You do not have permission to access this resource #505. Please contact Interactive Marketing.</p>")
                Response.Flush()
            End If
        End If
    End Sub

    Public Sub ProcessFile(fn As String)
        Try
            Dim csv As New Chilkat.Csv()
            csv.HasColumnNames = True
            Dim Success As Boolean = csv.LoadFile(fn)
            Dim rowCount As Integer = 0
            Dim secCount As Integer = 0
            If Success Then
                Status("Spreadsheet contains: " + csv.NumRows.ToString + " rows")
                Dim fdt(csv.NumRows) As GuestActivityLibrary.FreshDeskTicket_Type
                For i As Integer = 0 To csv.NumRows - 1
                    Dim em As New GuestActivityLibrary.FreshDeskTicket_Type
                    em.TicketID = csv.GetCellByName(i, "Ticket ID").ConvertToInteger()
                    em.Subject = csv.GetCellByName(i, "Subject").Truncate(100)
                    em.Status = csv.GetCellByName(i, "Status")
                    em.Priority = csv.GetCellByName(i, "Priority")
                    em.Source = csv.GetCellByName(i, "Source")
                    em.Type = csv.GetCellByName(i, "Type").Truncate(50)
                    em.Agent = csv.GetCellByName(i, "Agent").Truncate(50)
                    em.Park = csv.GetCellByName(i, "Group").Truncate(50)
                    Dim t As String = csv.GetCellByName(i, "Created time")
                    If IsDate(t) Then em.DateCreated = CDate(t)
                    t = csv.GetCellByName(i, "Resolved time")
                    If IsDate(t) Then em.DateResolved = CDate(t)
                    t = csv.GetCellByName(i, "Closed time")
                    If IsDate(t) Then em.DateClosed = CDate(t)
                    t = csv.GetCellByName(i, "Last update time")
                    If IsDate(t) Then em.DateUpdatedLast = CDate(t)
                    t = csv.GetCellByName(i, "Initial response time")
                    If IsDate(t) Then em.DateInitialResponse = CDate(t)
                    em.AgentInteractions = csv.GetCellByName(i, "Agent interactions")
                    em.CustomerInteractions = csv.GetCellByName(i, "Customer interactions")
                    em.ResolutionStatus = csv.GetCellByName(i, "Resolution status")
                    em.FirstResponseStatus = csv.GetCellByName(i, "First response status")
                    em.MembershipType = csv.GetCellByName(i, "Membership Type")
                    em.Category = csv.GetCellByName(i, "Category").Truncate(50)
                    em.IssueCategory = csv.GetCellByName(i, "Issue Category").Truncate(50)
                    em.IssueSubCategory = csv.GetCellByName(i, "Issue Sub Category").Truncate(50)
                    em.ConfirmationNumber = csv.GetCellByName(i, "Confirmation Number").Truncate(30)
                    em.MediaNumber = csv.GetCellByName(i, "Media Number").Truncate(50)
                    em.FullName = csv.GetCellByName(i, "Full name").Truncate(50)
                    em.Email = csv.GetCellByName(i, "Email").Truncate(100)
                    em.Tags = csv.GetCellByName(i, "Tags").Truncate(200)
                    em.SurveyResults = csv.GetCellByName(i, "Survey results").Truncate(200)
                    em.TriageCategory = csv.GetCellByName(i, "Triage Category").Truncate(200)
                    em.TriageComplexity = csv.GetCellByName(i, "Triage Complexity").Truncate(200)
                    em.TicketType = csv.GetCellByName(i, "Ticket Type").Truncate(200)
                    em.ContactID = csv.GetCellByName(i, "Contact ID").Truncate(200)
                    fdt(i) = em
                    rowCount += 1
                    secCount += 1
                    If secCount >= csv.NumRows / 20 Then
                        Status("Imported " + (rowCount / csv.NumRows).ToString("0%"))
                        secCount = 0
                    End If
                Next
                Status("Saving...")
                GuestActivityLibrary.FreshDeskTicket_Type.SaveArray(fdt)
                Status("")
            End If
        Catch ex As Exception
            ReportErr("ProcessFile()", ex)
            Status(ex.Message)
        End Try
    End Sub

    Protected Sub cmdUpload_Click(sender As Object, e As EventArgs) Handles cmdUpload.Click
        Try
            Status("Freshdesk Processing Session for " + Now.ToLongDateString)
            Dim LocalPath = Server.MapPath(UploadDir)
            Dim fInfo As FileInfo()
            Dim dInfo As New DirectoryInfo(LocalPath)
            fInfo = dInfo.GetFiles("*.csv")
            Dim files As String() = Directory.GetFiles(LocalPath, "*.csv")
            If Not files Is Nothing AndAlso files.Length > 0 Then
                For Each FName As String In files
                    Status("Processing: " + FName, False)
                    ProcessFile(FName)
                    IO.File.Delete(FName)
                Next
                pnlFinished.Visible = True
                pnlForm.Visible = False
                'Dim SendTo As String = "mark@kupferman.com, jschaefer@sftp.com, mkupferman@sftp.com, jDePuma@sftp.com"
                'GreenArrow.SendMsg("TMV Employee Upload Lod for " + Now.ToShortDateString, SendTo, ResultTxt)
            Else
                Status("No qualified files found.")
            End If
        Catch ex As Exception
            ReportErr("cmdUpdate_Click", ex)
            Status(ex.Message)
        End Try
    End Sub
End Class