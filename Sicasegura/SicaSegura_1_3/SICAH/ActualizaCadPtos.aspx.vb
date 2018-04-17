Imports System.Data.SqlClient
Imports GuarderiaFluvial
Partial Class SICAH_ActualizaCadPtos
    Inherits System.Web.UI.Page
    Dim conexion As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
    Dim da As System.Data.SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim dst As System.Data.DataSet = New System.Data.DataSet()
    Dim sentenciaUpdate As SqlCommand = New System.Data.SqlClient.SqlCommand("", conexion)

    Protected Sub UpdateFechaInicio(ByVal fechaIni As DateTime, ByVal codigoPVYCR As String, ByVal fechaNula As String)
        Dim objTrans As SqlTransaction
        If conexion.State = Data.ConnectionState.Closed Then conexion.Open()
        objTrans = conexion.BeginTransaction()
        sentenciaUpdate.Parameters.Clear()
        Try
            sentenciaUpdate.Transaction = objTrans
            sentenciaUpdate.CommandText = "UPDATE PVYCR_puntos SET fecha_inicion = @fecha_inicion " & _
                           "where codigoPVYCR='" & codigoPVYCR & "' "
            If fechaNula = "N" Then
                sentenciaUpdate.Parameters.AddWithValue("fecha_inicion", fechaIni)
            Else
                sentenciaUpdate.Parameters.AddWithValue("fecha_inicion", System.DBNull.Value)
            End If

            sentenciaUpdate.ExecuteNonQuery()
            objTrans.Commit()
        Catch Exc As System.Data.SqlClient.SqlException
            'Select Case Exc.Number
            '  'Case 547
            '  '    'Response.Write("Error: " & Exc.Message & " num: " & Exc.Number)
            '  '    Alert(Page, "No existe un elemento de medida para el punto con fecha: " & v_Fecha)

            '  Case 2627
            '    Alert(Page, "El Cauce ya existe ")
            '  Case Else
            '    Alert(Page, Exc.Message)
            'End Select
            objTrans.Rollback()
        End Try
    End Sub

    Protected Sub UpdateFechaFin(ByVal fechaFin As DateTime, ByVal codigoPVYCR As String, ByVal fechaNula As String)
        Dim objTrans As SqlTransaction
        If conexion.State = Data.ConnectionState.Closed Then conexion.Open()
        objTrans = conexion.BeginTransaction()
        sentenciaUpdate.Parameters.Clear()
        Try
            sentenciaUpdate.Transaction = objTrans

            sentenciaUpdate.CommandText = "UPDATE PVYCR_puntos SET fecha_FIN = @fecha_FIN " & _
                           "where codigoPVYCR='" & codigoPVYCR & "' "
            If fechaNula = "N" Then
                sentenciaUpdate.Parameters.AddWithValue("fecha_FIN", fechaFin)
            Else
                sentenciaUpdate.Parameters.AddWithValue("fecha_FIN", System.DBNull.Value)
            End If

            sentenciaUpdate.ExecuteNonQuery()
            objTrans.Commit()
        Catch Exc As System.Data.SqlClient.SqlException
            'Select Case Exc.Number
            '  'Case 547
            '  '    'Response.Write("Error: " & Exc.Message & " num: " & Exc.Number)
            '  '    Alert(Page, "No existe un elemento de medida para el punto con fecha: " & v_Fecha)

            '  Case 2627
            '    Alert(Page, "El Cauce ya existe ")
            '  Case Else
            '    Alert(Page, Exc.Message)
            'End Select
            objTrans.Rollback()
        End Try
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblCabecera.Text = genHTML.cabecera(Page)
        lblPestanyas.Text = genHTML.pestanyasMenu(6, "../", Page, Session("idperfil"), Session("usuarioReg"))
        If Request.QueryString("g") = "g" Then
            'pnlMostrar.Visible = True
            'pnlOcultar.Visible = False
            If Not IsPostBack Then
                If Session("usuarioReg") = Nothing Then
                    Response.Redirect("UsuarioNoRegistrado.aspx")
                    Exit Sub
                End If
                ActualizarPuntos()
                pnlMostrar.Visible = False
                pnlOcultar.Visible = True

            End If
        Else
            'ScriptManager.RegisterStartupScript(Page, Page.GetType(), "redireccion", "<script language='javascript'>window.location='ActualizaCadPtos.aspx?g=g';</script>", False)
            ClientScript.RegisterStartupScript(Page.GetType, "redireccion", "<script language='javascript'>window.location='ActualizaCadPtos.aspx?g=g';</script>")

        End If
    End Sub

    Protected Sub ActualizarPuntos()
        Dim i As Integer = 0
        Dim ERRORES As String = ""
        Dim tipo As String = ""
        Dim elemen As Integer = 0
        Dim fecha_inicion As DateTime
        Dim fecha_fin As DateTime

        dst.Tables.Clear()
        'obtenemos los código de los puntos
        Dim sentencia As String = "select distinct codigopvycr from pvycr_puntos order by codigopvycr "
        da.SelectCommand.CommandText = sentencia
        da.Fill(dst, "TablaPuntos")
        Dim filas As Integer = dst.Tables("TablaPuntos").Rows.Count
        For i = 0 To filas - 1
            If dst.Tables.Contains("TablaElementos") Then
                dst.Tables("TablaElementos").Rows.Clear()
            End If
            'para cada punto obtenemos sus elementos de medida
            sentencia = "SELECT idelementomedida,tipo FROM pvycr_elementosmedida where codigopvycr= '" & dst.Tables("TablaPuntos").Rows(i).Item("codigoPVYCR") & "'"
            da.SelectCommand.CommandText = sentencia
            'dst.Tables("tablaElementos").Clear()
            da.Fill(dst, "TablaElementos")
            elemen = dst.Tables("tablaElementos").Rows.Count
            For elemen = 0 To elemen - 1
                'para cada elemento de medida obtenemos la fecha de la primera lectura
                Select Case dst.Tables("tablaElementos").Rows(elemen).Item("tipo")
                    Case "E"
                        sentencia = "select top 1 fecha_medida from pvycr_datosalimentacion where codigopvycr = '" & dst.Tables("TablaPuntos").Rows(i).Item("codigoPVYCR") & "' and idelementomedida = '" & dst.Tables("TablaElementos").Rows(elemen).Item("idElementoMedida") & "' order by fecha_medida "
                        da.SelectCommand.CommandText = sentencia
                        da.Fill(dst, "TablaFechas")
                        sentencia = "select top 1 fecha_medida from pvycr_datosalimentacion where codigopvycr = '" & dst.Tables("TablaPuntos").Rows(i).Item("codigoPVYCR") & "' and idelementomedida = '" & dst.Tables("TablaElementos").Rows(elemen).Item("idElementoMedida") & "' order by fecha_medida desc "
                        da.SelectCommand.CommandText = sentencia
                        da.Fill(dst, "TablaFechasDesc")
                    Case "V"
                        sentencia = "select top 1 fecha_medida from pvycr_datosmotores where codigopvycr = '" & dst.Tables("TablaPuntos").Rows(i).Item("codigoPVYCR") & "' and idelementomedida = '" & dst.Tables("TablaElementos").Rows(elemen).Item("idElementoMedida") & "' order by fecha_medida "
                        da.SelectCommand.CommandText = sentencia
                        da.Fill(dst, "TablaFechas")
                        sentencia = "select top 1 fecha_medida from pvycr_datosmotores where codigopvycr = '" & dst.Tables("TablaPuntos").Rows(i).Item("codigoPVYCR") & "' and idelementomedida = '" & dst.Tables("TablaElementos").Rows(elemen).Item("idElementoMedida") & "' order by fecha_medida desc "
                        da.SelectCommand.CommandText = sentencia
                        da.Fill(dst, "TablaFechasDesc")
                    Case "Q"
                        sentencia = "select top 1 fecha_medida from pvycr_datosacequias where codigopvycr = '" & dst.Tables("TablaPuntos").Rows(i).Item("codigoPVYCR") & "' and idelementomedida = '" & dst.Tables("TablaElementos").Rows(elemen).Item("idElementoMedida") & "' order by fecha_medida "
                        da.SelectCommand.CommandText = sentencia
                        da.Fill(dst, "TablaFechas")
                        sentencia = "select top 1 fecha_medida from pvycr_datosacequias where codigopvycr = '" & dst.Tables("TablaPuntos").Rows(i).Item("codigoPVYCR") & "' and idelementomedida = '" & dst.Tables("TablaElementos").Rows(elemen).Item("idElementoMedida") & "' order by fecha_medida desc "
                        da.SelectCommand.CommandText = sentencia
                        da.Fill(dst, "TablaFechasDesc")
                    Case "H"
                        sentencia = "select top 1 fecha_medida from pvycr_datoshorometros where codigopvycr = '" & dst.Tables("TablaPuntos").Rows(i).Item("codigoPVYCR") & "' and idelementomedida = '" & dst.Tables("TablaElementos").Rows(elemen).Item("idElementoMedida") & "' order by fecha_medida "
                        da.SelectCommand.CommandText = sentencia
                        da.Fill(dst, "TablaFechas")
                        sentencia = "select top 1 fecha_medida from pvycr_datoshorometros where codigopvycr = '" & dst.Tables("TablaPuntos").Rows(i).Item("codigoPVYCR") & "' and idelementomedida = '" & dst.Tables("TablaElementos").Rows(elemen).Item("idElementoMedida") & "' order by fecha_medida desc "
                        da.SelectCommand.CommandText = sentencia
                        da.Fill(dst, "TablaFechasDesc")
                    Case "D"
                        sentencia = "select top 1 fecha_medida from pvycr_datosSuministros where codigopvycr = '" & dst.Tables("TablaPuntos").Rows(i).Item("codigoPVYCR") & "' and idelementomedida = '" & dst.Tables("TablaElementos").Rows(elemen).Item("idElementoMedida") & "' order by fecha_medida "
                        da.SelectCommand.CommandText = sentencia
                        da.Fill(dst, "TablaFechas")
                        sentencia = "select top 1 fecha_medida from pvycr_datosSuministros where codigopvycr = '" & dst.Tables("TablaPuntos").Rows(i).Item("codigoPVYCR") & "' and idelementomedida = '" & dst.Tables("TablaElementos").Rows(elemen).Item("idElementoMedida") & "' order by fecha_medida desc "
                        da.SelectCommand.CommandText = sentencia
                        da.Fill(dst, "TablaFechasDesc")
                End Select

            Next
            'ordenamos por fecha todas las lecturas de un punto (de todos sus elementos de medida) para obtener la menor
            If Not dst.Tables("TablaFechas") Is Nothing Then
                If dst.Tables("TablaFechas").Rows.Count > 0 Then
                    dst.Tables("TablaFechas").DefaultView.Sort = "fecha_medida"

                    'nos quedamos con la menos fecha de inicio para hacer el update
                    fecha_inicion = dst.Tables("TablaFechas").DefaultView(0).Item("fecha_medida")

                    'Dim comando As SqlClient.SqlCommand = New SqlClient.SqlCommand("", conexion)
                    UpdateFechaInicio(fecha_inicion, dst.Tables("TablaPuntos").Rows(i).Item("codigoPVYCR"), "N")
                Else
                    UpdateFechaInicio(fecha_inicion, dst.Tables("TablaPuntos").Rows(i).Item("codigoPVYCR"), "S")
                End If
                'LIMPIAMOS EL DATASET DE LAS FECHAS
                If dst.Tables.Contains("TablaFechas") Then
                    dst.Tables("TablaFechas").Rows.Clear()
                End If
            End If

            'ordenamos por fecha todas las lecturas de un punto (de todos sus elementos de medida) para obtener la mayor
            If Not dst.Tables("TablaFechasDesc") Is Nothing Then
                If dst.Tables("TablaFechasDesc").Rows.Count > 0 Then
                    dst.Tables("TablaFechasDesc").DefaultView.Sort = "fecha_medida"
                    'nos quedamos con la menos fecha de inicio para hacer el update
                    fecha_fin = dst.Tables("TablaFechasDesc").DefaultView(dst.Tables("TablaFechasDesc").Rows.Count - 1).Item("fecha_medida")
                    'If DateAdd(DateInterval.Month, 2, fecha_fin) < Date.Now Then
                    'Dim comando As SqlClient.SqlCommand = New SqlClient.SqlCommand("", conexion)
                    UpdateFechaFin(fecha_fin, dst.Tables("TablaPuntos").Rows(i).Item("codigoPVYCR"), "N")
                    'Else
                    ' UpdateFechaFin(fecha_fin, dst.Tables("TablaPuntos").Rows(i).Item("codigoPVYCR"), "S")
                    'End If
                Else
                    UpdateFechaFin(fecha_fin, dst.Tables("TablaPuntos").Rows(i).Item("codigoPVYCR"), "S")
                End If
                'LIMPIAMOS EL DATASET DE LAS FECHAS
                If dst.Tables.Contains("TablaFechasDesc") Then
                    dst.Tables("TablaFechasDesc").Rows.Clear()
                End If
            End If
        Next
    End Sub

End Class
