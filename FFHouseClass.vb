Public Class HouseReport_Type

    Public ParkID As Integer = 0
    Public House As String = ""
    Public Total As Integer = 0
    Public Last60Min As Integer = 0
    Public T4pm As Integer = 0
    Public T5pm As Integer = 0
    Public T6pm As Integer = 0
    Public T7pm As Integer = 0
    Public T8pm As Integer = 0
    Public T9pm As Integer = 0
    Public T10pm As Integer = 0
    Public T11pm As Integer = 0
    Public T12am As Integer = 0
    Public T1am As Integer = 0
    Public T2am As Integer = 0

    Public Shared Sub ReadDat(dat As SqlDataReader, s As HouseReport_Type)
        With s
            .ParkID = ReadDBValue(dat, "ParkID", 0)
            .House = ReadDBValue(dat, "House", "")
            .Total = ReadDBValue(dat, "Total", 0)
            .Last60Min = ReadDBValue(dat, "Last60Min", 0)
            .T4pm = ReadDBValue(dat, "T4pm", 0)
            .T5pm = ReadDBValue(dat, "T5pm", 0)
            .T6pm = ReadDBValue(dat, "T6pm", 0)
            .T7pm = ReadDBValue(dat, "T7pm", 0)
            .T8pm = ReadDBValue(dat, "T8pm", 0)
            .T9pm = ReadDBValue(dat, "T9pm", 0)
            .T10pm = ReadDBValue(dat, "T10pm", 0)
            .T11pm = ReadDBValue(dat, "T11pm", 0)
            .T12am = ReadDBValue(dat, "T12am", 0)
            .T1am = ReadDBValue(dat, "T1am", 0)
            .T2am = ReadDBValue(dat, "T2am", 0)
        End With
    End Sub

    Public Function NameValuePairs() As NameValuePair()
        Dim nv(0) As NameValuePair
        Try
            NameValuePair.AddToArray(nv, "ParkID", Me.ParkID.ToString)
            NameValuePair.AddToArray(nv, "House", Me.House.ToString)
            NameValuePair.AddToArray(nv, "Total", Me.Total.ToString)
            NameValuePair.AddToArray(nv, "Last60Min", Me.Last60Min.ToString)
            NameValuePair.AddToArray(nv, "T6pm", Me.T6pm.ToString)
            NameValuePair.AddToArray(nv, "T7pm", Me.T7pm.ToString)
            NameValuePair.AddToArray(nv, "T8pm", Me.T8pm.ToString)
            NameValuePair.AddToArray(nv, "T9pm", Me.T9pm.ToString)
            NameValuePair.AddToArray(nv, "T10pm", Me.T10pm.ToString)
            NameValuePair.AddToArray(nv, "T11pm", Me.T11pm.ToString)
            NameValuePair.AddToArray(nv, "T12am", Me.T12am.ToString)
            NameValuePair.AddToArray(nv, "T1am", Me.T1am.ToString)
            NameValuePair.AddToArray(nv, "T2am", Me.T2am.ToString)
        Catch ex As Exception
        End Try
        Return nv
    End Function

    Public Function ApplyCodes(s As String, Optional Prefix As String = "") As String
        Dim r As New System.Text.StringBuilder(s)
        r.Replace("{{" + Prefix + "parkid}}", ParkID.ToString)
        r.Replace("{{" + Prefix + "house}}", House)
        r.Replace("{{" + Prefix + "total}}", Total.ToString)
        r.Replace("{{" + Prefix + "last60min}}", Last60Min.ToString)
        r.Replace("{{" + Prefix + "t4pm}}", T4pm.ToString)
        r.Replace("{{" + Prefix + "t5pm}}", T5pm.ToString)
        r.Replace("{{" + Prefix + "t6pm}}", T6pm.ToString)
        r.Replace("{{" + Prefix + "t7pm}}", T7pm.ToString)
        r.Replace("{{" + Prefix + "t8pm}}", T8pm.ToString)
        r.Replace("{{" + Prefix + "t9pm}}", T9pm.ToString)
        r.Replace("{{" + Prefix + "t10pm}}", T10pm.ToString)
        r.Replace("{{" + Prefix + "t11pm}}", T11pm.ToString)
        r.Replace("{{" + Prefix + "t12am}}", T12am.ToString)
        r.Replace("{{" + Prefix + "t1am}}", T1am.ToString)
        r.Replace("{{" + Prefix + "t2am}}", T2am.ToString)
        Return r.ToString
    End Function

    Public Shared Function LoadArray(Query As String, ParamArray Pairs As NameValuePair()) As HouseReport_Type()
        Dim Result(1000) As HouseReport_Type
        Dim Result_Count As Integer = -1
        Dim ConnSt As String = "Server=10.129.18.40;Database=benefits;User Id=marketing;Password=mCvFeuBiNU9QjUfsLpZe;"
        Dim cnn As New SqlConnection(ConnSt)
        Dim cmd As New SqlCommand(Query, cnn)
        If Not Pairs Is Nothing Then
            For i As Integer = 0 To Pairs.Length - 1
                Dim Name As String = Pairs(i).Name
                Dim Value As Object = Pairs(i).Value
                If Name = "sp" Or Name = "storedprocedure" Then
                    cmd.CommandType = CommandType.StoredProcedure
                Else
                    If Left(Name, 1) <> "@" Then Name = "@" + Name
                    cmd.Parameters.AddWithValue(Name, Value)
                End If
            Next
        End If
        If Left(Query, 6).ToUpper <> "SELECT" Then cmd.CommandType = CommandType.StoredProcedure
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
                        Result_Count += 1
                        If Result_Count + 3 >= Result.Length Then Array.Resize(Result, Result_Count + 500)
                        Dim x As New HouseReport_Type
                        HouseReport_Type.ReadDat(dat, x)
                        Result(Result_Count) = x
                    Loop Until Not dat.Read
                    dat.Close()
                End If
                Success = True
                Array.Resize(Result, Result_Count + 1)
            Catch ex As SqlException
                FailCount += 1
                If FailCount > 3 Then ReportErr(ex.TargetSite.Module.Name + " " + ex.TargetSite.Name, ex)
            Finally
                cnn.Close()
            End Try
        Loop Until Success Or FailCount > 3
        If Result_Count > -1 Then Return Result
        Return Nothing
    End Function

    Public Shared Function ReadReport(Optional ReportDate As DateTime = DefaultDate) As HouseReport_Type()
        Dim Query As String = ReadTextFile("~/frightfestlookup.sql", True)
        Query = Query.Replace("{{date}}", ReportDate.ToString("yyyy-MM-dd"))
        Return HouseReport_Type.LoadArray(Query)
    End Function


End Class
