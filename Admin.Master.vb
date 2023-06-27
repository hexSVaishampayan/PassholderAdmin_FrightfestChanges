Public Class Admin
    Inherits System.Web.UI.MasterPage

    Public Enum ErrMsgType_Enum As Integer
        Green = 1
        Yellow = 2
        Red = 3
    End Enum

    Public _ResetCache_ As Boolean = False

    Public Property MemberUpgradeRequestStatusID As Integer
        Get
            Return ReadSessionValue("MemberUpgradeRequestStatusID", 0, EncryptPass, True)
        End Get
        Set(value As Integer)
            SaveSessionValue("MemberUpgradeRequestStatusID", value, True, 6, EncryptPass)
        End Set
    End Property

    Public Property UpgradeParkID As Integer
        Get
            Return ReadSessionValue("UpgradeParkID", 0, EncryptPass, True)
        End Get
        Set(value As Integer)
            SaveSessionValue("UpgradeParkID", value, True, 6, EncryptPass)
        End Set
    End Property

    Public Property ErrorColor As String
        Get
            Return IIf(Not Session("ErrorColor") Is Nothing, Session("ErrorColor"), "")
        End Get
        Set(value As String)
            Session("ErrorColor") = Session("ErrorColor")
        End Set
    End Property

    Public Sub AddError(s As String, Optional LogActivityID As Integer = 0, Optional LogComment As String = "")
        LogComment = LogComment.Replace("%%", s)
        If LogActivityID > 0 Then Log(LogActivityID, LogComment)
        If s = "" Then Session("ErrorText") = ""
        If Session("ErrorText") Is Nothing Then Session("ErrorText") = ""
        If s <> "" Then Session("ErrorText") = Session("ErrorText") + "<li class='ErrorTextLI'>" + s.AntiXSS + "</li>"
    End Sub

    Public Property ResetCache() As Boolean
        Get
            Return _ResetCache_
        End Get
        Set(value As Boolean)
            _ResetCache_ = value
        End Set
    End Property

    Public Function HasErrors() As Boolean
        If Not Session("ErrorText") Is Nothing AndAlso Session("ErrorText") <> "" Then Return True
        Return False
    End Function

    Public Function HasValidationErrors() As Boolean
        Return HasErrors()
    End Function

    Public Function ListErrors() As String
        Dim r As String = ""
        If Not Session("ErrorText") Is Nothing AndAlso Session("ErrorText") <> "" Then
            Return Session("ErrorText").ToString.StripHTML
        End If
        Return r
    End Function

    Public Overloads Sub DisplayErrors(Optional Color As String = "#FFC300", Optional SaveErrorsToLog As Boolean = False)
        If Not Session("ErrorText") Is Nothing AndAlso Session("ErrorText") <> "" Then
            'If FolioIdIsLoaded And SaveErrorsToLog Then Log(40, Session("ErrorText"), UserFolioUniqueID)
            pnlErrors.Visible = True
            If Color = "green" Then Color = "#7FFF7F"
            lblErrors.Text = "<div class='ErrorTextDIV' " + IIf(Color <> "", "style='background-color:" + Color + ";' ", "") + "><ul class='ErrorTextUL'>" + Session("ErrorText").ToString.DecodeFormatting + "</ul></div>"
            Session("ErrorText") = ""
            ErrorColor = ""
            pnlErrors.Focus()
        ElseIf lblErrors.Text = "" Then
            pnlErrors.Visible = False
        End If
    End Sub

    Public Overloads Sub DisplayErrors(ErrMsgType As ErrMsgType_Enum, Optional SaveErrorsToLog As Boolean = False)
        Select Case ErrMsgType
            Case ErrMsgType_Enum.Green
                DisplayErrors("green", SaveErrorsToLog)
            Case ErrMsgType_Enum.Red
                DisplayErrors("red", SaveErrorsToLog)
            Case ErrMsgType_Enum.Yellow
                DisplayErrors(, SaveErrorsToLog)
        End Select
    End Sub

    Public Sub ResetErrors()
        pnlErrors.Visible = False
        lblErrors.Text = ""
    End Sub

    Public Property EncryptPass As String = "5&hvMF#Kgcd8k!a2&t$^aBh^a&Cd$2W*D6g!fA!ZTxLQUjZw9L"
    Private _sfToken As AppAccessLibrary.AppToken_Type
    Public Property sfToken() As AppAccessLibrary.AppToken_Type

        Get
            Dim AnonymousIsOK As Boolean = False
            If HttpContext.Current.Request.Url.AbsoluteUri.Contains("atsreport") Then AnonymousIsOK = True
            If HttpContext.Current.Request.Url.AbsoluteUri.Contains("frightfest") Then AnonymousIsOK = False
            If _sfToken Is Nothing Then _sfToken = AppToken_Type.GetCurrentToken(,, AnonymousIsOK)
            Return _sfToken
        End Get
        Set(value As AppToken_Type)
            _sfToken = value
        End Set
    End Property

    Public Sub SetTitle(s As String)
        lblTitle.Text = "<h1>" + s + "</h1>"
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        DisplayErrors()
    End Sub

    Private Sub Admin1_Init(sender As Object, e As EventArgs) Handles Me.Init
        _sfToken = sfToken()
        lblUserStatus.Text = sfToken.UserStatus
        _ResetCache_ = ReadHTTPValue("rb", False)
    End Sub

    Public Sub DisplayMessage(msg As String, title As String, Optional width As Integer = 480, Optional height As Integer = 250)
        RadWindowManager1.RadAlert(msg, width, height, title, "alertCallBackFn")
    End Sub

    Private Sub Admin_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
        DisplayErrors()
    End Sub

    Private Sub Admin_Error(sender As Object, e As EventArgs) Handles Me.[Error]
        Dim ex As Exception = Server.GetLastError()
        AddError("Page Error: " + ex.Message + ";" + ex.StackTrace)
        DisplayErrors()
        Server.ClearError()
    End Sub

End Class