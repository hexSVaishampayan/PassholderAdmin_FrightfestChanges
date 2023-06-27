Imports Telerik.Web.UI

Public Class radHelpers
    Overloads Shared Sub LoadRadDropDown(ByRef lst As Telerik.Web.UI.RadDropDownList, ByVal cString As String, ByVal ProcedureName As String, ValueFieldName As String, TextFieldName As String, ParamArray Pairs() As NameValuePair)
        If cString <> "" And ProcedureName <> "" Then
            Dim cnn As New SqlConnection(cString)
            Dim adp As SqlDataAdapter = New SqlDataAdapter
            adp.SelectCommand = New SqlCommand(ProcedureName, cnn)
            If Not Pairs Is Nothing Then
                For i As Integer = 0 To Pairs.Length - 1
                    Dim Name As String = Pairs(i).Name
                    Dim Value As Object = Pairs(i).Value
                    If Name <> "" Then
                        If Left(Name, 1) <> "@" Then Name = "@" + Name
                        adp.SelectCommand.Parameters.AddWithValue(Name, Value)
                    End If
                Next
            End If
            adp.SelectCommand.CommandType = CommandType.StoredProcedure
            Dim tab As New DataTable
            cnn.Open()
            Try
                adp.Fill(tab)
                With lst
                    .DataSource = tab
                    .DataValueField = ValueFieldName
                    .DataTextField = TextFieldName
                    .DataBind()
                End With
            Catch ex As SqlException
                ReportErr("Utilities:LoadDropDown", ex)
            Finally
                cnn.Close()
            End Try
        End If
    End Sub

    Overloads Shared Sub LoadRadDropDown(ByVal lst As Telerik.Web.UI.RadDropDownList, Values() As NameValuePair, Optional ByVal AddDefault As Boolean = False, Optional ByVal DefaultValue As String = "0", Optional ByVal DefaultText As String = "")
        Dim s As String = ""
        lst.Items.Clear()
        If AddDefault Then
            Dim l As New DropDownListItem(DefaultText, DefaultValue)
            lst.Items.Add(l)
        End If
        If Not Values Is Nothing AndAlso Values.Length > 0 Then
            For i As Integer = 0 To Values.Length - 1
                If Not Values(i) Is Nothing Then
                    Dim l As New DropDownListItem(Values(i).Name, Values(i).Value)
                    If l.Text.Length > 0 Then lst.Items.Add(l)
                    l = Nothing
                End If
            Next
        End If
    End Sub

    Overloads Shared Sub LoadRadDropDown(ByVal lst As Telerik.Web.UI.RadDropDownList, ByVal CString As String, ByVal TableName As String, ByVal ValueFieldName As String, ByVal TextFieldName As String, Optional ByVal WhereString As String = "", Optional ByVal OrderBy As String = "", Optional ByVal IsStoredProcedure As Boolean = False, Optional ByVal ProcedureName As String = "", Optional ByVal AddDefault As Boolean = False, Optional ByVal DefaultValue As String = "0", Optional ByVal DefaultText As String = "")
        Dim s As String = ""
        s = GenerateQuery(TableName, ValueFieldName, TextFieldName, WhereString, OrderBy, IsStoredProcedure, ProcedureName, AddDefault, DefaultValue, DefaultText)
        lst.Items.Clear()
        If s <> "" And CString.Length > 3 Then
            Dim cnn As New SqlConnection(CString)
            Dim cmd As New SqlCommand(s, cnn)
            Dim dat As SqlDataReader = Nothing
            Dim Success As Boolean = False
            Dim FailCount As Integer = 0
            Do
                Try
                    cnn.Open()
                    dat = cmd.ExecuteReader
                    If dat.HasRows Then
                        dat.Read()
                        Do
                            Dim l As New DropDownListItem(ReadDBValue(dat, TextFieldName, ""), ReadDBValue(dat, ValueFieldName, ""))
                            If l.Text.Length > 0 Then lst.Items.Add(l)
                            l = Nothing
                        Loop Until Not dat.Read
                        dat.Close()
                    End If
                    Success = True
                Catch ex As SqlException
                    FailCount += 1
                    If FailCount > 3 Then ReportErr(ex.TargetSite.Module.Name + " " + ex.TargetSite.Name, ex)
                Finally
                    cnn.Close()
                End Try
            Loop Until Success Or FailCount > 3
        End If
    End Sub

    Overloads Shared Sub LoadRadDropDown(ByVal lst As Telerik.Web.UI.RadComboBox, Values() As NameValuePair, Optional ByVal AddDefault As Boolean = False, Optional ByVal DefaultValue As String = "0", Optional ByVal DefaultText As String = "")
        Dim s As String = ""
        lst.Items.Clear()
        If AddDefault Then
            Dim l As New RadComboBoxItem(DefaultText, DefaultValue)
            lst.Items.Add(l)
        End If
        If Not Values Is Nothing AndAlso Values.Length > 0 Then
            For i As Integer = 0 To Values.Length - 1
                Dim l As New RadComboBoxItem(Values(i).Name, Values(i).Value)
                If l.Text.Length > 0 Then lst.Items.Add(l)
                l = Nothing
            Next
        End If
    End Sub


    Overloads Shared Sub LoadRadDropDown(ByVal lst As Telerik.Web.UI.RadComboBox, ByVal CString As String, ByVal TableName As String, ByVal ValueFieldName As String, ByVal TextFieldName As String, Optional ByVal WhereString As String = "", Optional ByVal OrderBy As String = "", Optional ByVal IsStoredProcedure As Boolean = False, Optional ByVal ProcedureName As String = "", Optional ByVal AddDefault As Boolean = False, Optional ByVal DefaultValue As String = "0", Optional ByVal DefaultText As String = "")
        Dim s As String = ""
        s = GenerateQuery(TableName, ValueFieldName, TextFieldName, WhereString, OrderBy, IsStoredProcedure, ProcedureName, AddDefault, DefaultValue, DefaultText)
        lst.Items.Clear()
        If s <> "" And CString.Length > 3 Then
            Dim cnn As New SqlConnection(CString)
            Dim cmd As New SqlCommand(s, cnn)
            Dim dat As SqlDataReader = Nothing
            Dim Success As Boolean = False
            Dim FailCount As Integer = 0
            Do
                Try
                    cnn.Open()
                    dat = cmd.ExecuteReader
                    If dat.HasRows Then
                        dat.Read()
                        Do
                            Dim l As New RadComboBoxItem(ReadDBValue(dat, TextFieldName, ""), ReadDBValue(dat, ValueFieldName, ""))
                            If l.Text.Length > 0 Then lst.Items.Add(l)
                            l = Nothing
                        Loop Until Not dat.Read
                        dat.Close()
                    End If
                    Success = True
                Catch ex As SqlException
                    FailCount += 1
                    If FailCount > 3 Then ReportErr(ex.TargetSite.Module.Name + " " + ex.TargetSite.Name, ex)
                Finally
                    cnn.Close()
                End Try
            Loop Until Success Or FailCount > 3
        End If
    End Sub

    Overloads Shared Sub LoadRadListBox(ByVal lst As Telerik.Web.UI.RadListBox, Values() As NameValuePair, Optional ByVal AddDefault As Boolean = False, Optional ByVal DefaultValue As String = "0", Optional ByVal DefaultText As String = "")
        Dim s As String = ""
        lst.Items.Clear()
        If AddDefault Then
            Dim l As New RadListBoxItem(DefaultText, DefaultValue)
            lst.Items.Add(l)
        End If
        If Not Values Is Nothing AndAlso Values.Length > 0 Then
            For i As Integer = 0 To Values.Length - 1
                Dim l As New RadListBoxItem(Values(i).Name, Values(i).Value)
                If l.Text.Length > 0 Then lst.Items.Add(l)
                l = Nothing
            Next
        End If
    End Sub


    Overloads Shared Sub LoadRadListBox(ByVal lst As Telerik.Web.UI.RadListBox, ByVal CString As String, ByVal TableName As String, ByVal ValueFieldName As String, ByVal TextFieldName As String, Optional ByVal WhereString As String = "", Optional ByVal OrderBy As String = "", Optional ByVal IsStoredProcedure As Boolean = False, Optional ByVal ProcedureName As String = "", Optional ByVal AddDefault As Boolean = False, Optional ByVal DefaultValue As String = "0", Optional ByVal DefaultText As String = "")
        Dim s As String = ""
        s = GenerateQuery(TableName, ValueFieldName, TextFieldName, WhereString, OrderBy, IsStoredProcedure, ProcedureName, AddDefault, DefaultValue, DefaultText)
        lst.Items.Clear()
        If s <> "" And CString.Length > 3 Then
            Dim cnn As New SqlConnection(CString)
            Dim cmd As New SqlCommand(s, cnn)
            Dim dat As SqlDataReader = Nothing
            Dim Success As Boolean = False
            Dim FailCount As Integer = 0
            Do
                Try
                    cnn.Open()
                    dat = cmd.ExecuteReader
                    If dat.HasRows Then
                        dat.Read()
                        Do
                            Dim l As New RadListBoxItem(ReadDBValue(dat, TextFieldName, ""), ReadDBValue(dat, ValueFieldName, ""))
                            If l.Text.Length > 0 Then lst.Items.Add(l)
                            l = Nothing
                        Loop Until Not dat.Read
                        dat.Close()
                    End If
                    Success = True
                Catch ex As SqlException
                    FailCount += 1
                    If FailCount > 3 Then ReportErr(ex.TargetSite.Module.Name + " " + ex.TargetSite.Name, ex)
                Finally
                    cnn.Close()
                End Try
            Loop Until Success Or FailCount > 3
        End If
    End Sub

    Overloads Shared Sub SetRadDropDown(ByVal lst As RadDropDownList, ByVal val As Integer)
        SetRadDropDown(lst, val.ToString)
    End Sub

    Overloads Shared Sub SetRadDropDown(ByVal lst As RadComboBox, ByVal val As Integer)
        SetRadDropDown(lst, val.ToString)
    End Sub

    Overloads Shared Sub SetRadDropDown(ByVal lst As RadDropDownList, ByVal val As String)
        On Error Resume Next
        If lst.Items.Count >= 1 Then
            Dim x As Integer = 0
            For i As Integer = 0 To lst.Items.Count - 1
                If lst.Items(i).Value = val Then x = i
                lst.Items(i).Selected = False
            Next
            lst.SelectedIndex = x
        End If
    End Sub

    Overloads Shared Sub SetRadDropDown(ByVal lst As RadComboBox, ByVal val As String)
        On Error Resume Next
        If lst.Items.Count >= 1 Then
            Dim x As Integer = 0
            For i As Integer = 0 To lst.Items.Count - 1
                If lst.Items(i).Value = val Then x = i
                lst.Items(i).Selected = False
            Next
            lst.SelectedIndex = x
        End If
    End Sub

End Class
