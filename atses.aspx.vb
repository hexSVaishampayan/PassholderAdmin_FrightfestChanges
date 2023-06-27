Public Class atses
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Master.ResetErrors()

        If Not IsPostBack Then
            Page.Title = "ATS Coupon Checkout"
            Master.sfToken = AppToken_Type.GetCurrentToken()
            If Master.sfToken.hasPermission(703) Then
                pnlContent.Visible = True
            Else
                Response.Write("<p>No tienes permiso para acceder a este recurso # 703.</p>")
                Response.Flush()
                pnlContent.Visible = False
            End If
        End If
    End Sub

    Public Function isFormValid() As Boolean
        fldRangeStart.Text = fldRangeStart.Text.Trim.FilterToJustNumbers
        fldRangeEnd.Text = fldRangeEnd.Text.Trim.FilterToJustNumbers
        Dim RangeStart As Integer = cInteger(fldRangeStart.Text) + 500000
        Dim RangeEnd As Integer = cInteger(fldRangeEnd.Text) + 500000
        Dim TotalRange As Integer = RangeEnd - RangeStart + 1
        If TotalRange > 1000 Then
            Master.AddError("Intentaste activar {{actcount}} cupones. Solo puedes activar hasta 1000 cupones para un lote determinado.".Replace("{{actcount}}", TotalRange.ToString("#,##0")))
        End If
        If Not Master.HasErrors Then Return True
        Master.AddError("La operación NO se completó. Vuelve a intentarlo")
        Master.DisplayErrors()
        Return False
    End Function

    Protected Sub cmdActivate_Click(sender As Object, e As EventArgs) Handles cmdActivate.Click
        Master.ResetErrors()
        If isFormValid() Then
            Dim at As ATSInvite_Type = ATSInvite_Type.FindByATSInviteID(fldRangeStart.Text.ConvertToInteger)
            If Not at Is Nothing Then
                Try
                    Dim b As New ATSBatch_Type()
                    Try
                        With b
                            .AppUserID = Master.sfToken.AppUserID
                            .DateCreated = Now()
                            .Active = True
                            .ATSInviteIDStart = cInteger(fldRangeStart.Text) + 500000
                            .ATSInviteIDEnd = cInteger(fldRangeEnd.Text) + 500000
                            .ParkID = cInteger(at.ParkID)
                            .ATSPrizeTypeID = 4
                            .Save()
                        End With
                    Catch ex As Exception
                        Master.AddError("¡Hubo un error al activar el lote! Error: " + ex.Message)
                    End Try
                    If b.ATSBatchID > 0 Then
                        Dim RecordCount As Integer = b.ActivateInvites()
                        Dim UserName As String = Master.sfToken.UserName
                        If RecordCount = (b.ATSInviteIDEnd - b.ATSInviteIDStart + 1) Then
                            Master.AddError("{{username}} activaste correctamente <b> {{codecount}} </b> cupones en el lote # {{atsbatchid}}.".Replace("{{codecount}}", RecordCount.ToString("#,##0")).Replace("{{atsbatchid}}", b.ATSBatchID.ToString).Replace("{{username}}", UserName))
                            Master.ErrorColor = "#0000FF"
                            fldValidStart.Text = fldRangeStart.Text
                            fldValidEnd.Text = fldRangeEnd.Text
                        ElseIf RecordCount = 0 Then
                            Master.AddError("No pudimos activar ninguno de los registros del conjunto que seleccionó. ¿Ya estaban activados?")
                        ElseIf RecordCount < (b.ATSInviteIDEnd - b.ATSInviteIDStart + 1) Then
                            Master.AddError("Solo pudimos activar {{actual}} de los {{planificados}} cupones que seleccionó. ¿Algunos de ellos ya estaban activados?".Replace("{{actual}}", RecordCount.ToString("#,##0")).Replace("{{planned}}", (b.ATSInviteIDEnd - b.ATSInviteIDStart + 1).ToString("#,##0")))
                        End If
                    End If
                Catch ex As Exception
                    Master.AddError("¡Hubo un error al procesar el lote! Error: " + ex.Message)
                End Try
            Else
                Master.AddError(fldRangeStart.Text + " no está en un rango válido. Verifique su número inicial y vuelva a intentarlo.")
            End If
        End If
        Master.DisplayErrors()
    End Sub

    Public Function ShowRange(StartRange As Integer, EndRange As Integer) As String
        Dim r As String = ""
        Try
            If StartRange > EndRange Then
                Dim NewEndRange = StartRange
                StartRange = EndRange
                EndRange = NewEndRange
            End If
            Dim cnn As New SqlConnection(ConnString("Panel"))
            Dim cmd As New SqlCommand("ATSInvite_RangeStatus", cnn)
            Dim Result_Count As Integer = -1
            cmd.Parameters.AddWithValue("@StartRange", StartRange)
            cmd.Parameters.AddWithValue("@EndRange", EndRange)
            cmd.CommandType = CommandType.StoredProcedure
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
                            Dim li As String = "<tr><td>" + ReadDBValue(dat, "AtsInviteID", 0).ToString + "</td>"
                            li += "<td>" + If(ReadDBValue(dat, "ParkAbb", "") <> "", ReadDBValue(dat, "ParkAbb", ""), "<i>Unactivated</i>") + "</td>"
                            li += "<td>" + If(ReadDBValue(dat, "visitdate", DefaultDate) = DefaultDate, "&nbsp;", ReadDBValue(dat, "visitdate", DefaultDate).ToString("MMM d")) + "</td>"
                            li += "<td>" + If(ReadDBValue(dat, "datecreated", DefaultDate) = DefaultDate, "&nbsp;", ReadDBValue(dat, "datecreated", DefaultDate).ToString("MMM d @ HH:mm")) + "</td>"
                            li += "<td>" + ReadDBValue(dat, "AssignedBy", "&nbsp;") + "</td>"
                            li += "<td>" + If(ReadDBValue(dat, "DateFinished", DefaultDate) = DefaultDate, "&nbsp;", ReadDBValue(dat, "DateFinished", DefaultDate).ToString("MMM d @ HH:mm")) + "</td>"
                            li += "</tr>"
                            r += li
                        Loop Until Not dat.Read
                        dat.Close()
                    End If
                    Success = True
                    If Result_Count > 1 Then
                        r = MakeHTMLTable("dattab", "tx", r, "Coupon ID", "Park", "VisDate", "Activated", "Activated By", "Completed")
                    End If
                Catch ex As SqlException
                    FailCount += 1
                    If FailCount > 3 Then ReportErr(ex.TargetSite.Module.Name + " " + ex.TargetSite.Name, ex)
                Finally
                    cnn.Close()
                End Try
            Loop Until Success Or FailCount > 3
        Catch ex As Exception
            Master.AddError("Hubo un error al mostrar el rango. Error: " + ex.Message)
        End Try
        Return r
    End Function

    Protected Sub cmdValidateRange_Click(sender As Object, e As EventArgs) Handles cmdValidateRange.Click
        fldValidStart.Text = fldValidStart.Text.Trim.FilterToJustNumbers
        fldValidEnd.Text = fldValidEnd.Text.Trim.FilterToJustNumbers
        Dim StartRange As Integer = cInteger(fldValidStart.Text) + 500000
        Dim EndRange As Integer = cInteger(fldValidEnd.Text) + 500000
        If StartRange >= 1000000 And EndRange >= 1000000 Then
            lblResult.Text = ShowRange(StartRange, EndRange)
        Else
            Master.AddError("Ingresesa un rango válido.")
        End If
        Master.DisplayErrors()
    End Sub

    Protected Sub cmdDeactivate_Click(sender As Object, e As EventArgs) Handles cmdDeactivate.Click
        Try
            fldDeactivateCodeStart.Text = fldDeactivateCodeStart.Text.Trim.ToLower.FilterToAlphaNumericOnly
            fldDeactivateCodeEnd.Text = fldDeactivateCodeEnd.Text.Trim.ToLower.FilterToAlphaNumericOnly
            If fldDeactivateCodeStart.Text.Length = 16 And fldDeactivateCodeEnd.Text.Length = 16 Then
                Dim RecordsImpacted As Integer = SQL(ConnString("Panel"), "ATSInvite_Deactivate", True, New NameValuePair("@Start", fldDeactivateCodeStart.Text), New NameValuePair("@End", fldDeactivateCodeEnd.Text))
                If RecordsImpacted > 0 Then
                    Master.AddError("Cupones de {{items}} desactivados correctamente.".Replace("{{items}}", RecordsImpacted.ToString("#,##0")))
                Else
                    Master.AddError("Ningún registro se vio afectado. Verifica sus códigos y vuelva a intentarlo.")
                End If
            Else
                Master.AddError("Los códigos no son válidos. Comprueba los códigos y vuelva a intentarlo.")
            End If
        Catch ex As Exception
            Master.AddError("Hubo un error al desactivar el rango: " + ex.Message)
        End Try
        Master.DisplayErrors()
    End Sub
End Class