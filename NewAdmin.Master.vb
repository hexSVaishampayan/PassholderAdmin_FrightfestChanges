Public Class NewAdmin
    Inherits System.Web.UI.MasterPage



    Public _ResetCache_ As Boolean = False

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


    Private Sub Admin1_Init(sender As Object, e As EventArgs) Handles Me.Init
        _sfToken = sfToken()
        lblUserStatus.Text = sfToken.UserStatus
        _ResetCache_ = ReadHTTPValue("rb", False)
    End Sub



End Class