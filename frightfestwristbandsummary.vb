Public Class FFWristbandSummary_Type

    Public ParkID As Integer = 0
    Public Guests As Integer = 0
    Public DEGuests As Integer = 0
    Public DIGuests As Integer = 0
    Public SeasWGuests As Integer = 0
    Public DayWGuests As Integer = 0
    Public HouseVisits As Integer = 0
    Public UniqueVisits As Integer = 0
    Public DupeVisits As Integer = 0
    Public AvgVisits As Decimal = 0
    Public AvgUniqueVisits As Decimal = 0
    Public Vis1Only As Integer = 0
    Public Vis2Plus As Integer = 0
    Public DEVisits As Integer = 0
    Public DIVisits As Integer = 0
    Public SeasWVisits As Integer = 0
    Public DayWVisits As Integer = 0
    Public DEAvg As Decimal = 0
    Public DIAvg As Decimal = 0
    Public SeasWAvg As Decimal = 0
    Public DayWAvg As Decimal = 0
    Public HVisits01 As Integer = 0
    Public HVisits02 As Integer = 0
    Public HVisits03 As Integer = 0
    Public HVisits04 As Integer = 0
    Public HVisits05 As Integer = 0
    Public HVisits06 As Integer = 0
    Public HVisits07 As Integer = 0
    Public HVisits08 As Integer = 0
    Public HVisits09 As Integer = 0
    Public HVisits10 As Integer = 0
    Public HVisits11 As Integer = 0
    Public HVisits12 As Integer = 0
    Public HVisits13 As Integer = 0
    Public HVisits14 As Integer = 0
    Public HVisits15 As Integer = 0
    Public HVisits16 As Integer = 0
    Public HVisits17 As Integer = 0
    Public HVisits18 As Integer = 0
    Public HVisits19 As Integer = 0
    Public HVisits20 As Integer = 0

    Public Shared Sub ReadDat(dat As SqlDataReader, s As FFWristbandSummary_Type)
        With s
            .ParkID = ReadDBValue(dat, "ParkID", 0)
            .Guests = ReadDBValue(dat, "Guests", 0)
            .DEGuests = ReadDBValue(dat, "DEGuests", 0)
            .DIGuests = ReadDBValue(dat, "DIGuests", 0)
            .SeasWGuests = ReadDBValue(dat, "SeasWGuests", 0)
            .DayWGuests = ReadDBValue(dat, "DayWGuests", 0)
            .HouseVisits = ReadDBValue(dat, "HouseVisits", 0)
            .UniqueVisits = ReadDBValue(dat, "UniqueVisits", 0)
            .DupeVisits = ReadDBValue(dat, "DupeVisits", 0)
            .AvgVisits = ReadDBValueDecimal(dat, "AvgVisits", 0)
            .AvgUniqueVisits = ReadDBValueDecimal(dat, "AvgUniqueVisits", 0)
            .Vis1Only = ReadDBValue(dat, "Vis1Only", 0)
            .Vis2Plus = ReadDBValue(dat, "Vis2Plus", 0)
            .DEVisits = ReadDBValue(dat, "DEVisits", 0)
            .DIVisits = ReadDBValue(dat, "DIVisits", 0)
            .SeasWVisits = ReadDBValue(dat, "SeasWVisits", 0)
            .DayWVisits = ReadDBValue(dat, "DayWVisits", 0)
            .DEAvg = ReadDBValueDecimal(dat, "DEAvg", 0)
            .DIAvg = ReadDBValueDecimal(dat, "DIAvg", 0)
            .SeasWAvg = ReadDBValueDecimal(dat, "SeasWAvg", 0)
            .DayWAvg = ReadDBValueDecimal(dat, "DayWAvg", 0)
            .HVisits01 = ReadDBValue(dat, "HVisits01", 0)
            .HVisits02 = ReadDBValue(dat, "HVisits02", 0)
            .HVisits03 = ReadDBValue(dat, "HVisits03", 0)
            .HVisits04 = ReadDBValue(dat, "HVisits04", 0)
            .HVisits05 = ReadDBValue(dat, "HVisits05", 0)
            .HVisits06 = ReadDBValue(dat, "HVisits06", 0)
            .HVisits07 = ReadDBValue(dat, "HVisits07", 0)
            .HVisits08 = ReadDBValue(dat, "HVisits08", 0)
            .HVisits09 = ReadDBValue(dat, "HVisits09", 0)
            .HVisits10 = ReadDBValue(dat, "HVisits10", 0)
            .HVisits11 = ReadDBValue(dat, "HVisits11", 0)
            .HVisits12 = ReadDBValue(dat, "HVisits12", 0)
            .HVisits13 = ReadDBValue(dat, "HVisits13", 0)
            .HVisits14 = ReadDBValue(dat, "HVisits14", 0)
            .HVisits15 = ReadDBValue(dat, "HVisits15", 0)
            .HVisits16 = ReadDBValue(dat, "HVisits16", 0)
            .HVisits17 = ReadDBValue(dat, "HVisits17", 0)
            .HVisits18 = ReadDBValue(dat, "HVisits18", 0)
            .HVisits19 = ReadDBValue(dat, "HVisits19", 0)
            .HVisits20 = ReadDBValue(dat, "HVisits20", 0)
        End With
    End Sub

    Public Function NameValuePairs() As NameValuePair()
        Dim nv(0) As NameValuePair
        Try
            NameValuePair.AddToArray(nv, "ParkID", Me.ParkID.ToString)
            NameValuePair.AddToArray(nv, "Guests", Me.Guests.ToString)
            NameValuePair.AddToArray(nv, "DEGuests", Me.DEGuests.ToString)
            NameValuePair.AddToArray(nv, "DIGuests", Me.DIGuests.ToString)
            NameValuePair.AddToArray(nv, "SeasWGuests", Me.SeasWGuests.ToString)
            NameValuePair.AddToArray(nv, "DayWGuests", Me.DayWGuests.ToString)
            NameValuePair.AddToArray(nv, "HouseVisits", Me.HouseVisits.ToString)
            NameValuePair.AddToArray(nv, "UniqueVisits", Me.UniqueVisits.ToString)
            NameValuePair.AddToArray(nv, "DupeVisits", Me.DupeVisits.ToString)
            NameValuePair.AddToArray(nv, "AvgVisits", Me.AvgVisits.ToString)
            NameValuePair.AddToArray(nv, "AvgUniqueVisits", Me.AvgUniqueVisits.ToString)
            NameValuePair.AddToArray(nv, "Vis1Only", Me.Vis1Only.ToString)
            NameValuePair.AddToArray(nv, "Vis2Plus", Me.Vis2Plus.ToString)
            NameValuePair.AddToArray(nv, "DEVisits", Me.DEVisits.ToString)
            NameValuePair.AddToArray(nv, "DIVisits", Me.DIVisits.ToString)
            NameValuePair.AddToArray(nv, "SeasWVisits", Me.SeasWVisits.ToString)
            NameValuePair.AddToArray(nv, "DayWVisits", Me.DayWVisits.ToString)
            NameValuePair.AddToArray(nv, "DEAvg", Me.DEAvg.ToString)
            NameValuePair.AddToArray(nv, "DIAvg", Me.DIAvg.ToString)
            NameValuePair.AddToArray(nv, "SeasWAvg", Me.SeasWAvg.ToString)
            NameValuePair.AddToArray(nv, "DayWAvg", Me.DayWAvg.ToString)
            NameValuePair.AddToArray(nv, "HVisits01", Me.HVisits01.ToString)
            NameValuePair.AddToArray(nv, "HVisits02", Me.HVisits02.ToString)
            NameValuePair.AddToArray(nv, "HVisits03", Me.HVisits03.ToString)
            NameValuePair.AddToArray(nv, "HVisits04", Me.HVisits04.ToString)
            NameValuePair.AddToArray(nv, "HVisits05", Me.HVisits05.ToString)
            NameValuePair.AddToArray(nv, "HVisits06", Me.HVisits06.ToString)
            NameValuePair.AddToArray(nv, "HVisits07", Me.HVisits07.ToString)
            NameValuePair.AddToArray(nv, "HVisits08", Me.HVisits08.ToString)
            NameValuePair.AddToArray(nv, "HVisits09", Me.HVisits09.ToString)
            NameValuePair.AddToArray(nv, "HVisits10", Me.HVisits10.ToString)
            NameValuePair.AddToArray(nv, "HVisits11", Me.HVisits11.ToString)
            NameValuePair.AddToArray(nv, "HVisits12", Me.HVisits12.ToString)
            NameValuePair.AddToArray(nv, "HVisits13", Me.HVisits13.ToString)
            NameValuePair.AddToArray(nv, "HVisits14", Me.HVisits14.ToString)
            NameValuePair.AddToArray(nv, "HVisits15", Me.HVisits15.ToString)
            NameValuePair.AddToArray(nv, "HVisits16", Me.HVisits16.ToString)
            NameValuePair.AddToArray(nv, "HVisits17", Me.HVisits17.ToString)
            NameValuePair.AddToArray(nv, "HVisits18", Me.HVisits18.ToString)
            NameValuePair.AddToArray(nv, "HVisits19", Me.HVisits19.ToString)
            NameValuePair.AddToArray(nv, "HVisits20", Me.HVisits20.ToString)
        Catch ex As Exception
        End Try
        Return nv
    End Function

    Public Function ApplyCodes(s As String, Optional Prefix As String = "") As String
        Dim r As New System.Text.StringBuilder(s)
        r.Replace("{{" + Prefix + "parkid}}", ParkID.ToString)
        r.Replace("{{" + Prefix + "guests}}", Guests.ToString)
        r.Replace("{{" + Prefix + "deguests}}", DEGuests.ToString)
        r.Replace("{{" + Prefix + "diguests}}", DIGuests.ToString)
        r.Replace("{{" + Prefix + "seaswguests}}", SeasWGuests.ToString)
        r.Replace("{{" + Prefix + "daywguests}}", DayWGuests.ToString)
        r.Replace("{{" + Prefix + "housevisits}}", HouseVisits.ToString)
        r.Replace("{{" + Prefix + "uniquevisits}}", UniqueVisits.ToString)
        r.Replace("{{" + Prefix + "dupevisits}}", DupeVisits.ToString)
        r.Replace("{{" + Prefix + "avgvisits}}", AvgVisits.ToString)
        r.Replace("{{" + Prefix + "avguniquevisits}}", AvgUniqueVisits.ToString)
        r.Replace("{{" + Prefix + "vis1only}}", Vis1Only.ToString)
        r.Replace("{{" + Prefix + "vis2plus}}", Vis2Plus.ToString)
        r.Replace("{{" + Prefix + "devisits}}", DEVisits.ToString)
        r.Replace("{{" + Prefix + "divisits}}", DIVisits.ToString)
        r.Replace("{{" + Prefix + "seaswvisits}}", SeasWVisits.ToString)
        r.Replace("{{" + Prefix + "daywvisits}}", DayWVisits.ToString)
        r.Replace("{{" + Prefix + "deavg}}", DEAvg.ToString)
        r.Replace("{{" + Prefix + "diavg}}", DIAvg.ToString)
        r.Replace("{{" + Prefix + "seaswavg}}", SeasWAvg.ToString)
        r.Replace("{{" + Prefix + "daywavg}}", DayWAvg.ToString)
        r.Replace("{{" + Prefix + "hvisits01}}", HVisits01.ToString)
        r.Replace("{{" + Prefix + "hvisits02}}", HVisits02.ToString)
        r.Replace("{{" + Prefix + "hvisits03}}", HVisits03.ToString)
        r.Replace("{{" + Prefix + "hvisits04}}", HVisits04.ToString)
        r.Replace("{{" + Prefix + "hvisits05}}", HVisits05.ToString)
        r.Replace("{{" + Prefix + "hvisits06}}", HVisits06.ToString)
        r.Replace("{{" + Prefix + "hvisits07}}", HVisits07.ToString)
        r.Replace("{{" + Prefix + "hvisits08}}", HVisits08.ToString)
        r.Replace("{{" + Prefix + "hvisits09}}", HVisits09.ToString)
        r.Replace("{{" + Prefix + "hvisits10}}", HVisits10.ToString)
        r.Replace("{{" + Prefix + "hvisits11}}", HVisits11.ToString)
        r.Replace("{{" + Prefix + "hvisits12}}", HVisits12.ToString)
        r.Replace("{{" + Prefix + "hvisits13}}", HVisits13.ToString)
        r.Replace("{{" + Prefix + "hvisits14}}", HVisits14.ToString)
        r.Replace("{{" + Prefix + "hvisits15}}", HVisits15.ToString)
        r.Replace("{{" + Prefix + "hvisits16}}", HVisits16.ToString)
        r.Replace("{{" + Prefix + "hvisits17}}", HVisits17.ToString)
        r.Replace("{{" + Prefix + "hvisits18}}", HVisits18.ToString)
        r.Replace("{{" + Prefix + "hvisits19}}", HVisits19.ToString)
        r.Replace("{{" + Prefix + "hvisits20}}", HVisits20.ToString)
        Return r.ToString
    End Function

    Public Shared Function LoadArray(Query As String, ParamArray Pairs As NameValuePair()) As FFWristbandSummary_Type()
        Dim Result(1000) As FFWristbandSummary_Type
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
                        Dim x As New FFWristbandSummary_Type
                        FFWristbandSummary_Type.ReadDat(dat, x)
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

    Public Shared Function ReadReport(Optional ReportDate As DateTime = DefaultDate) As FFWristbandSummary_Type()
        Dim Query As String = ReadTextFile("~/frightfestdailysummary.sql")
        Query = Query.Replace("{{date}}", ReportDate.ToString("yyyy-MM-dd"))
        Return FFWristbandSummary_Type.LoadArray(Query)
    End Function

End Class
