Imports Microsoft.VisualBasic
Imports System.Data
Imports GuarderiaFluvial
Imports System
Imports System.Math

Public Class SICA_FuncionesCaudalMasivo



    Shared Sub CalcularCaudalMasivo(ByVal codigoPVYCR As String, ByVal idElemento As String, _
    ByVal FechaIni As DateTime, ByVal FechaFin As DateTime)
        '*************************** declaración de variables ****************************************************************************
        Dim conexion As SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
        Dim v_calado As Decimal = 0
        Dim v_escala As Decimal = 0
        Dim v_caudal, caudalA, caudalF, v_caudal_ant, v_caudal_C, diametro, v_caudal_curva, v_caudal_curva_ant As Decimal
        Dim v_RegimenCurva, v_RegimenCurva_ant As String
        Dim j As Integer
        'para saber los regstros que tenemos que evaluar, obtenemos los datos de la tabla acequias para las fechas pasadas por el usuario
        Dim dstacequias As DataSet = CrearDataSet(codigoPVYCR, idElemento, FechaIni, FechaFin, conexion)
        Dim v_items As Integer = dstacequias.Tables("TablaAcequias2").Rows.Count
        Dim i As Integer
        Dim pcaudalC As Decimal = Nothing
        Dim mensaje As String = ""
        '****************************** CALCULO DEL CAUDAL NCM 21/10/2009 **********************************************************************
        '***** TENEMOS TRES TIPOS DE CÁCULO, CAUDAL (C), MOLINETE (A) Y FLOTADOR (F). Los registros se van a evaluar siempre en ese orden
        '***** 1) C --> con el nivel y la curva de gasto que hay en la base de datos recalculamos su caudal
        '***** 2) A y F --> recalculamos la curva de gasto, el caudal se queda igual
        '**************************************************************************************************************************************

        For i = 0 To v_items - 1
            'asigno el valor del calado, tiempo, velocidades a variables por comidad para programar
            Dim pcalado As String = dstacequias.Tables("tablaacequias2").Rows(i).Item("calado").ToString
            Dim pescala As String = dstacequias.Tables("tablaacequias2").Rows(i).Item("escala").ToString
            'obtenemso las curvas que tenemos en este punto
            Dim dstCurvas As DataSet = CrearDatasetCurvas(codigoPVYCR, idElemento, conexion)
            Dim v_curva As String = ""

            'inicializamos variables
            v_caudal = 0
            v_caudal_ant = 0
            v_caudal_curva = 0
            v_caudal_C = Nothing
            caudalA = Nothing
            caudalF = Nothing
            ' primero comprobaremos si faltan el calado no podremos hacer ningún cálculo
            If pcalado <> "" And IsDBNull(pcalado) = False Then
                v_calado = Convert.ToDecimal(pcalado)
                'no existe la escala y sí el calado, copiamos el valor del calado en la escala
                If pescala = "" Then
                    'cOPIAMOS EL VALOR DEL CALADO EN LA ESCALA
                    v_escala = Convert.ToDecimal(pcalado)
                Else
                    v_escala = Convert.ToDecimal(pescala)
                End If

                If dstacequias.Tables("tablaAcequias2").Rows(i).Item("tipoobtencioncaudal").ToString = "C" Then
                    v_RegimenCurva = ObtenerCodCurva(codigoPVYCR, idElemento, dstacequias.Tables("tablaacequias2").Rows(i).Item("RegimenCurva").ToString, conexion)
                    ' si sólo tiene un valor el ddl, entonces nos quedaremos con v_regimenCurva
                    v_caudal_C = calcular_Caudal(v_calado, v_RegimenCurva, 1, conexion)
                    pcaudalC = v_caudal_C
                    Try
                        actualizacionCaudal(codigoPVYCR, idElemento, dstacequias.Tables("tablaacequias2").Rows(i).Item("Fecha_Medida").ToString, "C", pcaudalC, conexion)
                    Catch ex As System.Data.SqlClient.SqlException
                        mensaje = mensaje & "error para la fecha: " & dstacequias.Tables("tablaacequias2").Rows(i).Item("Fecha_Medida").ToString & " y tipoObtención : C "
                    End Try
                ElseIf dstacequias.Tables("tablaAcequias2").Rows(i).Item("tipoobtencioncaudal").ToString = "F" Then
                    'calculamos el caudal F si existe el tiempo
                    'en las variables acabadas en _ant tenemos los varoles por si marcan estimada
                    caudalF = utiles.nullACero(dstacequias.Tables("TablaAcequias2").Rows(i).Item("Caudal_M3S"))

                    'para cada regimen curva calculamos el caudal y nos quedamos con el menor
                    Dim v_numcurvas As Integer = dstCurvas.Tables("TablaCurvas").Rows.Count
                    For j = 0 To v_numcurvas - 1
                        v_RegimenCurva = dstCurvas.Tables("TablaCurvas").Rows(j).Item("Cod_curva")
                        'calcularemos el caudal para cada curva de gasto del combo, excepto para la CT (por petición del usurio)
                        If v_RegimenCurva <> 991 Then
                            ' llamaremos a calcular caudal cuando el tipode caudal sea F ya que previamente habremos calculado
                            ' el tipocaudal A por el orden en que se ha creado las filas en el dataset (C, A, F)
                            v_caudal = calcular_Caudal(v_calado, v_RegimenCurva, 2, conexion)
                            'restremos al caudal obtenido el de molinete, y la menor diferencia será la curva con la que nos quedemos
                            v_caudal_curva = Math.Abs(v_caudal - caudalF)
                            If j = 0 Then
                                v_curva = v_RegimenCurva
                                v_caudal_ant = v_caudal
                                v_caudal_curva_ant = v_caudal_curva
                                v_RegimenCurva_ant = v_RegimenCurva
                            Else
                                If v_caudal_curva < v_caudal_curva_ant Then
                                    v_curva = v_RegimenCurva
                                    v_caudal_ant = v_caudal
                                    v_caudal_curva_ant = v_caudal_curva
                                    v_RegimenCurva_ant = v_RegimenCurva
                                    'Else
                                    '    v_curva = v_RegimenCurva
                                End If
                            End If
                        End If
                    Next
                    Try

                        actualizacionCurvas(codigoPVYCR, idElemento, dstacequias.Tables("tablaacequias2").Rows(i).Item("Fecha_Medida").ToString, "F", v_curva, conexion)
                    Catch ex As System.Data.SqlClient.SqlException
                        mensaje = mensaje & "error para la fecha: " & dstacequias.Tables("tablaacequias2").Rows(i).Item("Fecha_Medida").ToString & " y tipoObtención : F "
                    End Try

                ElseIf dstacequias.Tables("tablaAcequias2").Rows(i).Item("tipoobtencioncaudal").ToString = "A" Then
                    'calculamos el caudal por molinete A si existe alguna velocidad
                    caudalA = utiles.nullACero(dstacequias.Tables("TablaAcequias2").Rows(i).Item("Caudal_M3S"))

                    'para cada regimen curva calculamos el caudal y nos quedamos con el menor
                    Dim v_numcurvas As Integer = dstCurvas.Tables("TablaCurvas").Rows.Count
                    For j = 0 To v_numcurvas - 1
                        v_RegimenCurva = dstCurvas.Tables("TablaCurvas").Rows(j).Item("Cod_curva")
                        'calcularemos el caudal para cada curva de gasto del combo, excepto para la CT (por petición del usurio)
                        If v_RegimenCurva <> 991 Then
                            ' llamaremos a calcular caudal cuando el tipode caudal sea F ya que previamente habremos calculado
                            ' el tipocaudal A por el orden en que se ha creado las filas en el dataset (C, A, F)
                            v_caudal = calcular_Caudal(v_calado, v_RegimenCurva, 2, conexion)
                            'restremos al caudal obtenido el de molinete, y la menor diferencia será la curva con la que nos quedemos
                            v_caudal_curva = Math.Abs(v_caudal - caudalA)
                            If j = 0 Then
                                v_curva = v_RegimenCurva
                                v_caudal_ant = v_caudal
                                v_caudal_curva_ant = v_caudal_curva
                                v_RegimenCurva_ant = v_RegimenCurva
                            Else
                                If v_caudal_curva < v_caudal_curva_ant Then
                                    v_curva = v_RegimenCurva
                                    v_caudal_ant = v_caudal
                                    v_caudal_curva_ant = v_caudal_curva
                                    v_RegimenCurva_ant = v_RegimenCurva
                                    'Else
                                    '    v_curva = v_RegimenCurva
                                End If
                            End If
                        End If
                    Next
                    Try
                        actualizacionCurvas(codigoPVYCR, idElemento, dstacequias.Tables("tablaacequias2").Rows(i).Item("Fecha_Medida").ToString, "A", v_curva, conexion)
                    Catch ex As System.Data.SqlClient.SqlException
                        mensaje = mensaje & "error para la fecha: " & dstacequias.Tables("tablaacequias2").Rows(i).Item("Fecha_Medida").ToString & " y tipoObtención : A "
                    End Try
                End If
            Else
                pcaudalC = Nothing
            End If

        Next
    End Sub
    Shared Function calcular_Caudal(ByVal calado As Double, ByVal RegimenCurva As String, ByVal NumCurvasGasto As Integer, ByVal conexion As System.Data.SqlClient.SqlConnection) As Decimal
        'Dim conexion As SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
        Dim N0, N1, N2, C0, C2, v_caudal As Decimal
        Dim daCaudal As SqlClient.SqlDataAdapter = New SqlClient.SqlDataAdapter("", conexion)
        Dim dstCaudal As DataSet = New DataSet()
        Dim i As Integer

        'utiles.Comprobar_Conexion_BD(, conexion)

        'si el punto de control tiene una sola curva de gasto la plaicaremos directamente
        If NumCurvasGasto = 1 Then
            daCaudal.SelectCommand.CommandText = "SELECT top 1 Cod_Curva,Nivel,Caudal " & _
                                  "FROM PVYCR_CurvasAcequias_Valores " & _
                                  "WHERE Cod_Curva = " & RegimenCurva & " And Nivel = " & Replace(calado, ",", ".")
            daCaudal.Fill(dstCaudal, "tablaCurvas")


            'Si No encontramos el caudal por nivel (igualado con el valor del calado), haremos interpolación
            If (dstCaudal.Tables("tablaCurvas").Rows.Count = 0) Then

                'CALCULO POR INTERPOLACION
                daCaudal.SelectCommand.CommandText = "SELECT top 1 Nivel " & _
                                                       "FROM PVYCR_CurvasAcequias_Valores " & _
                                                       "WHERE Cod_Curva = " & RegimenCurva & " And Nivel < " & Replace(calado, ",", ".") & _
                                                       " order by nivel desc"
                daCaudal.Fill(dstCaudal, "tablaCaudalN0")

                daCaudal.SelectCommand.CommandText = "SELECT top 1 Nivel " & _
                                                       "FROM PVYCR_CurvasAcequias_Valores " & _
                                                       "WHERE Cod_Curva = " & RegimenCurva & " And Nivel > " & Replace(calado, ",", ".") & _
                                                       " order by nivel"

                daCaudal.Fill(dstCaudal, "tablaCaudalN2")

                daCaudal.SelectCommand.CommandText = "SELECT top 1 Caudal " & _
                                                       "FROM PVYCR_CurvasAcequias_Valores " & _
                                                       "WHERE Cod_Curva = " & RegimenCurva & " And Nivel < " & Replace(calado, ",", ".") & _
                                                       " order by nivel desc"
                daCaudal.Fill(dstCaudal, "tablaCaudalC0")

                daCaudal.SelectCommand.CommandText = "SELECT top 1 Caudal " & _
                                                       "FROM PVYCR_CurvasAcequias_Valores " & _
                                                       "WHERE Cod_Curva = " & RegimenCurva & " And Nivel > " & Replace(calado, ",", ".") & _
                                                       " order by nivel"
                daCaudal.Fill(dstCaudal, "tablaCaudalC2")
                'Si no tienen valores ni por arriba ni por bajo, nos quedamos con el que nos pasan
                If (dstCaudal.Tables("tablaCaudalN0").Rows.Count = 0) Then
                    N0 = Replace(calado, ",", ".")
                Else
                    N0 = dstCaudal.Tables("tablaCaudalN0").Rows(0).Item("Nivel")
                End If
                If (dstCaudal.Tables("tablaCaudalN2").Rows.Count = 0) Then
                    N2 = Replace(calado, ",", ".")
                Else
                    N2 = dstCaudal.Tables("tablaCaudalN2").Rows(0).Item("Nivel")
                End If
                If (dstCaudal.Tables("tablaCaudalC0").Rows.Count = 0) Then
                    C0 = Replace(calado, ",", ".")
                Else
                    C0 = dstCaudal.Tables("tablaCaudalC0").Rows(0).Item("Caudal")
                End If
                If (dstCaudal.Tables("tablaCaudalC2").Rows.Count = 0) Then
                    C2 = Replace(calado, ",", ".")
                Else
                    C2 = dstCaudal.Tables("tablaCaudalC2").Rows(0).Item("caudal")
                End If
                v_caudal = ((calado - N0) / (N2 - N0)) * (C2 - C0) + C0
                Return Format(v_caudal, "#,##0.###")
            Else
                Return dstCaudal.Tables("tablaCurvas").Rows(0).Item("Caudal")
            End If

        ElseIf NumCurvasGasto = 2 Then
            'tendremos que calcular el caudal de flotador y/o el de molinete y en función del caudal
            'obtenido y del nivel k ya lo tenemos haremos la interpolación
            'CALCULO POR INTERPOLACION
            'For i = 0 To RegimenCurva.Count - 1
            daCaudal.SelectCommand.CommandText = "SELECT top 1 Nivel " & _
                                                   "FROM PVYCR_CurvasAcequias_Valores " & _
                                                   "WHERE Cod_Curva = " & RegimenCurva & " And Nivel < " & Replace(calado, ",", ".") & _
                                                   " order by nivel desc"
            daCaudal.Fill(dstCaudal, "tablaCaudalN0")

            daCaudal.SelectCommand.CommandText = "SELECT top 1 Nivel " & _
                                                   "FROM PVYCR_CurvasAcequias_Valores " & _
                                                   "WHERE Cod_Curva = " & RegimenCurva & " And Nivel > " & Replace(calado, ",", ".") & _
                                                   " order by nivel"

            daCaudal.Fill(dstCaudal, "tablaCaudalN2")

            daCaudal.SelectCommand.CommandText = "SELECT top 1 Caudal " & _
                                                   "FROM PVYCR_CurvasAcequias_Valores " & _
                                                   "WHERE Cod_Curva = " & RegimenCurva & " And Nivel < " & Replace(calado, ",", ".") & _
                                                   " order by nivel desc"
            daCaudal.Fill(dstCaudal, "tablaCaudalC0")

            daCaudal.SelectCommand.CommandText = "SELECT top 1 Caudal " & _
                                                   "FROM PVYCR_CurvasAcequias_Valores " & _
                                                   "WHERE Cod_Curva = " & RegimenCurva & " And Nivel > " & Replace(calado, ",", ".") & _
                                                   " order by nivel"
            daCaudal.Fill(dstCaudal, "tablaCaudalC2")
            'Si no tienen valores ni por arriba ni por bajo, nos quedamos con el que nos pasan
            If (dstCaudal.Tables("tablaCaudalN0").Rows.Count = 0) Then
                N0 = Replace(calado, ",", ".")
            Else
                N0 = utiles.nullACero(dstCaudal.Tables("tablaCaudalN0").Rows(0).Item("Nivel"))
            End If
            If (dstCaudal.Tables("tablaCaudalN2").Rows.Count = 0) Then
                N2 = Replace(calado, ",", ".")
            Else
                N2 = utiles.nullACero(dstCaudal.Tables("tablaCaudalN2").Rows(0).Item("Nivel"))
            End If
            If (dstCaudal.Tables("tablaCaudalC0").Rows.Count = 0) Then
                C0 = Replace(calado, ",", ".")
            Else
                C0 = utiles.nullACero(dstCaudal.Tables("tablaCaudalC0").Rows(0).Item("Caudal"))
            End If
            If (dstCaudal.Tables("tablaCaudalC2").Rows.Count = 0) Then
                C2 = Replace(calado, ",", ".")
            Else
                C2 = utiles.nullACero(dstCaudal.Tables("tablaCaudalC2").Rows(0).Item("caudal"))
            End If
            v_caudal = ((calado - N0) / (N2 - N0)) * (C2 - C0) + C0

            Return Format(v_caudal, "#,##0.###")


        End If
    End Function
    
    Shared Function CrearDataSet(ByVal codigoPVYCR As String, ByVal idElemento As String, ByVal fechaini As DateTime, ByVal fechafin As DateTime, ByVal conexion As System.Data.SqlClient.SqlConnection) As DataSet
        'Dim conexion As SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
        Dim daAcequias As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
        Dim dstacequias As DataSet = New DataSet
        If conexion.State = ConnectionState.Closed Then conexion.Open()
        daAcequias.SelectCommand.CommandText = "SELECT E.ESCALA_M escala, E.CALADO_M calado,E.fecha_medida,E.Caudal_m3s, e.TipoObtencionCaudal, " & _
                                            "E.Observaciones, A1_M,A2_M,A3_M,B1_M,B2_M,B3_M,B4_M,H12_M,H23_M,H34_M,Offset_M, " & _
                                            "E.RegimenCurva, E.Caudal_M3S " & _
                                           "FROM PVYCR_DatosAcequias E, PVYCR_Puntos P " & _
                                           "where E.idElementoMedida = '" & idElemento & "' and E.CodigoPVYCR = '" & codigoPVYCR & "' And " & _
                                           "Fecha_medida >= '" & fechaini & " 00:00:00" & "' and Fecha_medida <= '" & fechafin & " 23:59:59" & "' " & _
                                           "and E.CodigoPVYCR = P.CodigoPVYCR and regimenCurva is not null " & _
                                           "order by E.Fecha_medida, e.tipoObtencionCaudal "

        daAcequias.Fill(dstacequias, "TablaAcequias2")
        Return dstacequias
    End Function
    Shared Function CrearDatasetCurvas(ByVal codigoPVYCR As String, ByVal idElemento As String, ByVal conexion As System.Data.SqlClient.SqlConnection) As DataSet
        'Dim conexion As SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
        Dim daCurvas As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
        Dim dstCurvas As DataSet = New DataSet
        If conexion.State = ConnectionState.Closed Then conexion.Open()
        daCurvas.SelectCommand.CommandText = "SELECT CodigoPVYCR,REGIMEN,FECHA_INICIO_USO,Cod_Curva,En_Activo, " & _
                                  "FECHA_FIN_USO,Comentarios,Probabilidad " & _
                                  "FROM PVYCR_CurvasAcequias " & _
                                  "WHERE codigoPVYCR = '" & codigoPVYCR & "' and idElementoMedida = '" & idElemento & "' and FECHA_FIN_USO >= getdate() " & _
                                  "order by Cod_Curva "

        daCurvas.Fill(dstCurvas, "TablaCurvas")

        Return dstCurvas
    End Function
    Shared Function ObtenerCodCurva(ByVal codigoPVYCR As String, ByVal idElemento As String, ByVal regimen As String, ByVal conexion As System.Data.SqlClient.SqlConnection) As String
        '    Dim conexion As SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
        Dim daCurvas As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
        Dim dstCurvas As DataSet = New DataSet
        Dim cod_curva As String = ""
        If conexion.State = ConnectionState.Closed Then conexion.Open()
        daCurvas.SelectCommand.CommandText = "SELECT Cod_Curva " & _
                                        "FROM PVYCR_CurvasAcequias " & _
                                        "WHERE codigoPVYCR = '" & codigoPVYCR & "' and idElementoMedida = '" & idElemento & "' and FECHA_FIN_USO >= getdate() " & _
                                        "and regimen = '" & regimen & "' order by Cod_Curva "

        daCurvas.Fill(dstCurvas, "TablaCurvas")
        cod_curva = dstCurvas.Tables("TablaCurvas").Rows(0).Item("cod_curva").ToString
        Return cod_curva
    End Function
    Shared Sub actualizacionCaudal(ByVal codigoPVYCR As String, ByVal idElemento As String, ByVal fechaMedida As String, ByVal tipoObt As String, ByVal Caudal As Decimal, ByVal conexion As System.Data.SqlClient.SqlConnection)
        'Dim conexion As SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
        Dim comando As SqlClient.SqlCommand = New SqlClient.SqlCommand("", conexion)
        If conexion.State = ConnectionState.Closed Then conexion.Open()
        comando.Parameters.Clear()

        comando.CommandText = "UPDATE PVYCR_datosacequias SET caudal_m3s = @caudal " & _
                       "where idElementomedida='" & idElemento & "' AND codigoPVYCR='" & codigoPVYCR & "' and fecha_Medida = '" & fechaMedida & "' " & _
                       "and tipoObtencionCaudal = '" & tipoObt & "' "

        comando.Parameters.AddWithValue("caudal", Caudal)

        comando.ExecuteNonQuery()
    End Sub
    Shared Sub actualizacionCurvas(ByVal codigoPVYCR As String, ByVal idElemento As String, ByVal fechaMedida As String, ByVal tipoObt As String, ByVal Curva As String, ByVal conexion As System.Data.SqlClient.SqlConnection)
        'Dim conexion As SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
        Dim comando As SqlClient.SqlCommand = New SqlClient.SqlCommand("", conexion)
        If conexion.State = ConnectionState.Closed Then conexion.Open()
        Dim regimen As String = ObtenerRegimenCurva(codigoPVYCR, idElemento, Curva, conexion)
        comando.Parameters.Clear()

        comando.CommandText = "UPDATE PVYCR_datosacequias SET RegimenCurva = @Curva " & _
                       "where idElementomedida='" & idElemento & "' AND codigoPVYCR='" & codigoPVYCR & "' and fecha_Medida = '" & fechaMedida & "' " & _
                       "and tipoObtencionCaudal = '" & tipoObt & "' "

        comando.Parameters.AddWithValue("Curva", regimen)

        comando.ExecuteNonQuery()
    End Sub
    Shared Function ObtenerRegimenCurva(ByVal codigoPVYCR As String, ByVal idElemento As String, ByVal cod_curva As String, ByVal conexion As System.Data.SqlClient.SqlConnection) As String
        'Dim conexion As SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
        Dim daCurvas As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
        Dim dstCurvas As DataSet = New DataSet
        If conexion.State = ConnectionState.Closed Then conexion.Open()
        daCurvas.SelectCommand.CommandText = "SELECT Regimen " & _
                                        "FROM PVYCR_CurvasAcequias " & _
                                        "WHERE codigoPVYCR = '" & codigoPVYCR & "' and idElementoMedida = '" & idElemento & "' and FECHA_FIN_USO >= getdate() " & _
                                        "and cod_curva = '" & cod_curva & "' order by Cod_Curva "

        daCurvas.Fill(dstCurvas, "TablaRegimenCurvas")
        cod_curva = dstCurvas.Tables("TablaRegimenCurvas").Rows(0).Item("Regimen").ToString
        Return cod_curva
    End Function
End Class

