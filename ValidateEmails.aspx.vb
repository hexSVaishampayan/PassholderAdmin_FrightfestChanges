Imports System.IO
Imports Telerik.Web.UI.Upload

Public Class ValidateEmails
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
        If Not IsPostBack Then
            sfToken = AppToken_Type.GetCurrentToken()
            If sfToken.hasPermission(4) Then
                Try
                    RadAsyncUpload1.TargetFolder = UploadDir
                    pnlFinished.Visible = False
                    pnlForm.Visible = True
                Catch ex As Exception
                    Status(ex.Message)
                End Try
            Else
                Status("Access Denied.")
                pnlForm.Visible = False
            End If
        End If
    End Sub

    Public Sub ProcessFile(fname As String)
        If fname.Length > 0 Then
            Using MyReader As New FileIO.TextFieldParser(fname)
                MyReader.TextFieldType = FileIO.FieldType.Delimited
                MyReader.SetDelimiters(",")
                Dim CurrentRow As String()
                While Not MyReader.EndOfData
                    Try
                        CurrentRow = MyReader.ReadFields()

                    Catch ex As Exception

                    End Try
                End While
            End Using
        End If
    End Sub

    Protected Sub cmdUpload_Click(sender As Object, e As EventArgs) Handles cmdUpload.Click
        Status("Initiatied process.", False)
        Dim LocalPath = Server.MapPath(UploadDir)
        Dim fInfo As FileInfo()
        Dim dInfo As New DirectoryInfo(LocalPath)
        fInfo = dInfo.GetFiles("*.xls")
        Dim files As String() = Directory.GetFiles(LocalPath, "*.xls")
        If Not files Is Nothing AndAlso files.Length > 0 Then
            For Each FName As String In files
                Status("Processing: " + FName, False)
                ProcessFile(FName)
                '                File.Move(FName, ("C:\Archive\TDP\" + FName.Replace(LocalPath, "")).Replace(".xls", Now.ToString("-yyyyMMddhhmmss") + ".xls"))
                If File.Exists(FName) Then File.Delete(FName)
            Next
        Else
            Status("No qualified files found.")
        End If

    End Sub
End Class