Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic.CompilerServices
Imports System.Runtime.CompilerServices

Namespace AccesoADatos
    Partial Public Class AccesoADatos2
        Public Function SacarLecturasPreYPro(ByVal EM As String, ByVal strWhere As String, ByVal strOrden As String, ByVal conexion As SqlConnection, Optional ByVal anyadir_diferencial As Boolean = False) As Object()

            Dim strSQL As String = ""
            Select Case Left(EM, 1)
                Case "Q"
                    strSQL = "SELECT * FROM (" & _
                             "SELECT CodigoPVYCR, idElementoMedida, Cod_Fuente_Dato, Fecha_Medida, Escala_M, Calado_M, NULL as RegimenCurva, NULL as NumeroParada, Caudal_M3S, TipoObtencionCaudal, NULL as Duda_Calidad,'Pendiente' as Observaciones, 1 as Estadillo " & _
                             "FROM PVYCR_DatosAcequias_Estadillo " & strWhere & _
                             "UNION " & _
                             "SELECT CodigoPVYCR, idElementoMedida, Cod_Fuente_Dato, Fecha_Medida, Escala_M, Calado_M, RegimenCurva, NumeroParada, Caudal_M3S, TipoObtencionCaudal, Duda_Calidad, Observaciones, 0 as Estadillo " & _
                             "FROM PVYCR_DatosAcequias " & strWhere & _
                             ") AS DTABLE " & strOrden
                Case "V"
                    strSQL = "SELECT * FROM (" & _
                             "SELECT CodigoPVYCR, idElementoMedida, Cod_Fuente_Dato, Fecha_Medida, LecturaContador_M3,CaudalMedido, Funciona as En_marcha, 'Pendiente' as Observaciones, idIncidenciaVolumetrica, ConsumoVolumetricoAdicional, ReinicioLecturaVolumetrica, Justificacion,  1 as Estadillo " & _
                             "FROM PVYCR_DatosMotores_Estadillo " & strWhere & _
                             "UNION " & _
                             "SELECT CodigoPVYCR, idElementoMedida, Cod_Fuente_Dato, Fecha_Medida, LecturaContador_M3,CaudalMedido, Funciona as En_marcha, 'Consolidado' as Observaciones, idIncidenciaVolumetrica, ConsumoVolumetricoAdicional, ReinicioLecturaVolumetrica, Justificacion, 0 as Estadillo " & _
                             "FROM PVYCR_DatosMotores " & strWhere & _
                             ") AS DTABLE " & strOrden
                Case "E"
                    strSQL = "SELECT * FROM (" & _
                             "SELECT CodigoPVYCR, idElementoMedida, Cod_Fuente_Dato, Fecha_Medida, LecturaI, LecturaII, LecturaIII, Total_KWH, Total_Kvar, Funciona as En_marcha, cast(Observaciones as nvarchar(255)) as Observaciones, Justificacion, idIncidenciaElectrica, ConsumoElectricoAdicional, ReinicioLecturaElectrica, 1 as Estadillo " & _
                             "FROM PVYCR_DatosAlimentacion_Estadillo " & strWhere & _
                             "UNION " & _
                             "SELECT CodigoPVYCR, idElementoMedida, Cod_Fuente_Dato, Fecha_Medida, LecturaI, LecturaII, LecturaIII, Total_KWH, Total_Kvar, Funciona as En_marcha, cast(Observaciones as nvarchar(255)) as Observaciones, Justificacion, idIncidenciaElectrica, ConsumoElectricoAdicional, ReinicioLecturaElectrica, 0 as Estadillo " & _
                             "FROM PVYCR_DatosAlimentacion " & strWhere & _
                             ") AS DTABLE " & strOrden
                Case "H"
                    strSQL = "SELECT * FROM (" & _
                             "SELECT CodigoPVYCR, idElementoMedida, Cod_Fuente_Dato, Fecha_Medida, HorasIntervalo, Funciona as En_marcha, cast(Observaciones as nvarchar(255)) as Observaciones, idIncidenciaVolumetrica, ConsumoVolumetricoAdicional, ReinicioLecturaVolumetrica, 1 as Estadillo " & _
                             "FROM PVYCR_DatosHorometros_Estadillo " & strWhere & _
                             "UNION " & _
                             "SELECT CodigoPVYCR, idElementoMedida, Cod_Fuente_Dato, Fecha_Medida, HorasIntervalo, Funciona as En_marcha, cast(Observaciones as nvarchar(255)) as Observaciones, idIncidenciaVolumetrica, ConsumoVolumetricoAdicional, ReinicioLecturaVolumetrica, 0 as Estadillo " & _
                             "FROM PVYCR_DatosHorometros " & strWhere & _
                             ") AS DTABLE " & strOrden
            End Select

            Dim da As New SqlDataAdapter("", conexion)
            Dim dst As New DataSet
            Dim i As Integer
            da.SelectCommand.CommandText = strSQL
            da.Fill(dst, "Datos")
            If (anyadir_diferencial = True) Then
                dst.Tables(0).Columns.Add("Volumen parcial")

                For i = 0 To dst.Tables(0).Rows.Count - 2
                    dst.Tables(0).Rows(i)("Volumen parcial") = dst.Tables(0).Rows(i)("LecturaContador_M3") - dst.Tables(0).Rows(i + 1)("LecturaContador_M3")
                Next
                dst.Tables(0).Rows(dst.Tables(0).Rows.Count - 1)("Volumen parcial") = 0
            End If
            Dim vector(2)
            vector(0) = dst
            vector(1) = strSQL
            Return vector
        End Function

        Public Function obtenerAcumulado(ByVal codigoPVYCR As String, ByVal EM As String, ByVal conexion As SqlConnection, Optional ByVal fechaInicio As String = "", Optional ByVal fechaFin As String = "") As Object()
            Dim resultado(4) As Object

            Dim da As New SqlDataAdapter("", conexion)
            Dim dst As New DataSet

            If dst.Tables.Contains("Datos") Then
                dst.Tables.Remove("Datos")
            End If

            CrearDatasetDiferencial(Strings.Left(EM, 1), codigoPVYCR, EM, fechaFin, fechaInicio, conexion, dst)
            dst.Tables.Item(0).TableName = "Datos"

            FinalizarDatasetElementos(Left(EM, 1), dst)

            obtenerVolumenDiferencial(Left(EM, 1), dst.Tables.Item("Datos"))

            Dim codigoCauce As String = Left(codigoPVYCR, InStr(codigoPVYCR, "P") - 1)

            Select Case Strings.Left(EM, 1)
                Case "Q"
                    resultado(0) = Nothing
                    resultado(1) = obtenerCaudalAcumulado(dst)                     '(m3)
                    Exit Select
                Case "V"
                    resultado(0) = Nothing
                    resultado(1) = obtenerVolumenAcumulado(dst)                    '(m3)
                    Exit Select
                Case "E"
                    resultado(0) = obtenerVolumenElectricoAcumulado(dst)
                    resultado(1) = obtenerVolumenElectricoAcumuladoM3(dst)         '(m3)
                    Exit Select
                Case "H"
                    resultado(0) = obtenerVolumenAcumuladoHoras(dst)
                    resultado(1) = obtenerVolumenHorometroAcumulado(dst)           '(m3)
                    Exit Select
            End Select

            resultado(2) = Me.ObtenerConcesion(codigoCauce, conexion)
            resultado(3) = Me.ObtenerPorConcesion(CInt(resultado(2)), CDbl(resultado(1)))
            If dst.Tables("Datos").Rows.Count > 0 Then
                resultado(4) = dst.Tables("Datos").Rows(0).Item("fecha_medida")
            End If

            Return resultado
        End Function

        Private Function CalculaAnyoHidrologico() As Object()
            Dim vector(2)
            Dim AnyoHidroIni As DateTime
            Dim AnyoHidroFin As DateTime

            If Today.Month >= 10 Then
                AnyoHidroIni = CDate("01/10/" & Today.Year())
                AnyoHidroFin = CDate("30/09/" & Today.Year() + 1)
            Else
                AnyoHidroIni = CDate("01/10/" & Today.Year() - 1)
                AnyoHidroFin = CDate("30/09/" & Today.Year())
            End If

            vector(0) = AnyoHidroIni
            vector(1) = AnyoHidroFin
            Return vector
        End Function

        Private Sub CrearDatasetDiferencial(ByVal tipoElemen As String, ByVal codigoPVYCR As String, ByVal idelemento As String, ByVal FiltroFechaFin As String, ByVal FiltroFechaIni As String, ByVal conexion As SqlConnection, ByRef dst As DataSet)
            'No filtramos por las nulas, las sacamos todas (En sicasegura Mostrar Nulos = True)
            Dim sentenciaSel As String = ""
            Dim sFiltro As String = ""
            Dim fechainicio, fechaFin As DateTime
            Dim sentenciaOrder As String = ""
            Dim vFiltroFechaIni As String = ""
            Dim vFiltroFechaFin As String = ""

            Dim da = New SqlDataAdapter("", conexion)
            If conexion.State = ConnectionState.Closed Then conexion.Open()
            Dim comando As New SqlCommand("", conexion)
            comando.CommandText = "SELECT isnull(TipoSensor,'') FROM PVYCR_Puntos WHERE codigoPVYCR='" & codigoPVYCR & "'"
            Dim Telemedida As String = comando.ExecuteScalar

            If Telemedida = "" Then
                If DateTime.Now.Month < 10 Then
                    fechainicio = "01/10/" & DateTime.Now.Year - 2
                    fechaFin = DateTime.Today
                Else
                    fechainicio = "01/10/" & DateTime.Now.Year - 1
                    fechaFin = DateTime.Today
                End If

            Else
                If DateTime.Now.Month < 10 Then
                    fechainicio = "01/10/" & DateTime.Now.Year - 1
                    fechaFin = DateTime.Today
                Else
                    fechainicio = "01/10/" & DateTime.Now.Year
                    fechaFin = DateTime.Today
                End If
            End If
            dst.Clear()

            If FiltroFechaFin <> "" Then
                vFiltroFechaFin = FiltroFechaFin & " 23:59:59"
            End If
            If FiltroFechaIni <> "" Then
                vFiltroFechaIni = FiltroFechaIni & " 00:00:00"
            End If
            If FiltroFechaFin <> "" And FiltroFechaIni <> "" Then
                sFiltro = sFiltro & " and Fecha_medida between '" & vFiltroFechaIni & "' and '" & vFiltroFechaFin & "' "
            ElseIf FiltroFechaFin = "" And FiltroFechaIni = "" Then
                sFiltro = sFiltro & " and Fecha_Medida between '" & fechainicio & " 00:00:00' and '" & fechaFin & " 23:59:59' "
            Else
                sFiltro = ""
            End If

            If tipoElemen = "Q" Then
                sentenciaSel = "SELECT d.CodigoPVYCR,d.idElementoMedida, d.Cod_Fuente_Dato,d.Fecha_Medida,Escala_M,d.Calado_M,d.Observaciones " & _
                   ",d.TipoObtencionCaudal,d.RegimenCurva,d.NumeroParada,d.Caudal_M3S,d.DUDA_CALIDAD, F.FUENTE_DATOS DescFuenteDato " & _
                   ",d.modificado FROM PVYCR_DatosAcequias D " & _
                   "INNER JOIN  FUENTES_DE_DATOS F ON F.Cod_Fuente_dato=D.Cod_Fuente_dato " & _
                    "where codigoPVYCR = '" & codigoPVYCR & "' and " & _
                    "idElementoMedida =  '" & idelemento & "' "

                sentenciaOrder = " order by codigoPVYCR, Fecha_Medida  desc, d.Cod_Fuente_dato, d.TipoObtencionCaudal "

                If sFiltro <> "" Then
                    sentenciaSel = sentenciaSel & sFiltro & sentenciaOrder
                Else
                    sentenciaSel = sentenciaSel & " and Fecha_Medida between '" & fechainicio & " 00:00:00' and '" & fechaFin & " 23:59:59' " & sentenciaOrder
                End If

                da.SelectCommand.CommandText = sentenciaSel
                da.Fill(dst, "TablaAcequias")

                Dim Tabla As DataTable
                Tabla = dst.Tables("TablaAcequias").Clone()
                For i = dst.Tables("TablaAcequias").Rows.Count - 1 To 0 Step -1
                    Tabla.Rows.Add(dst.Tables("TablaAcequias").Rows(i).ItemArray)
                Next
                dst.Tables.Remove(dst.Tables("TablaAcequias"))
                dst.Tables.Add(Tabla)

            ElseIf tipoElemen = "E" Then
                sentenciaSel = "SELECT  D.CodigoPVYCR, D.Cod_Fuente_Dato, D.Fecha_Medida, D.idElementoMedida, " & _
                                "D.LecturaI,D.LecturaII, D.LecturaIII,D.total_Kwh, D.total_kvar, D.Funciona, D.Observaciones, D.idIncidenciaelectrica, " & _
                                "D.ConsumoElectricoAdicional, D.ReinicioLecturaElectrica, D.Justificacion, " & _
                                "IV.descripcion descIncElec, F.FUENTE_DATOS DescFuenteDato,isnull(C.E_FCorrectorContActiva,0) * isnull(C.E_RelacionM3_KWH,0) relacionM3_Kwh, C.IdContador " & _
                                ",D.modificado FROM PVYCR_DatosAlimentacion D " & _
                                "LEFT OUTER JOIN  PVYCR_incidencias IV ON Iv.idIncidencia=D.idIncidenciaElectrica " & _
                                "INNER JOIN  FUENTES_DE_DATOS F ON F.Cod_Fuente_dato=D.Cod_Fuente_dato " & _
                                        "LEFT OUTER JOIN  PVYCR_Contadores_ElementosMedida E ON E.codigoPVYCR = d.codigoPVYCR and " & _
                               "E.idelementoMedida = D.idelementomedida and D.Fecha_Medida BETWEEN E.FechaInicio AND ISNULL(E.FECHAFINAL, GETDATE()) " & _
                               "LEFT OUTER JOIN  PVYCR_Contadores C ON C.idContador = E.idContador and " & _
                               "C.FechaRevision = E.fechaRevision and c.tipocontador = 'E' " & _
                               "where D.idElementoMedida = '" & idelemento & "' and D.CodigoPVYCR = '" & codigoPVYCR & "' "

                sentenciaOrder = " order by D.codigoPVYCR, D.Fecha_Medida desc"

                If sFiltro <> "" Then
                    sentenciaSel = sentenciaSel & sFiltro & sentenciaOrder
                Else
                    sentenciaSel = sentenciaSel & " and Fecha_Medida between '" & fechainicio & " 00:00:00' and '" & fechaFin & " 23:59:59' " & sentenciaOrder
                End If

                da.SelectCommand.CommandText = sentenciaSel
                da.Fill(dst, "TablaAlimentacion")

                Dim Tabla As DataTable
                Tabla = dst.Tables("TablaAlimentacion").Clone()
                For i = dst.Tables("TablaAlimentacion").Rows.Count - 1 To 0 Step -1
                    Tabla.Rows.Add(dst.Tables("TablaAlimentacion").Rows(i).ItemArray)
                Next
                dst.Tables.Remove(dst.Tables("TablaAlimentacion"))
                dst.Tables.Add(Tabla)

                QuitarRegistrosSegunFuenteDato("E", dst)

            ElseIf tipoElemen = "V" Then
                sentenciaSel = "SELECT D.CodigoPVYCR, D.Cod_Fuente_Dato, D.Fecha_Medida, D.idElementoMedida, " & _
                               "D.LecturaContador_M3, D.Funciona, D.Justificacion, D.Observaciones, D.idIncidenciaVolumetrica, " & _
                               "D.ConsumoVolumetricoAdicional, D.ReinicioLecturaVolumetrica,D.CaudalMedido, " & _
                               "IV.descripcion DescIncVol, F.FUENTE_DATOS DescFuenteDato " & _
                               ",d.modificado FROM PVYCR_DatosMotores D " & _
                               "LEFT OUTER JOIN  PVYCR_incidencias IV ON Iv.idIncidencia=D.idIncidenciaVolumetrica " & _
                                "INNER JOIN  FUENTES_DE_DATOS F ON F.Cod_Fuente_dato=D.Cod_Fuente_dato " & _
                               "where D.idElementoMedida = '" & idelemento & "' and D.CodigoPVYCR = '" & codigoPVYCR & "' "

                sentenciaOrder = " order by D.codigoPVYCR, D.Fecha_Medida desc"

                If sFiltro <> "" Then
                    sentenciaSel = sentenciaSel & sFiltro & sentenciaOrder
                Else
                    sentenciaSel = sentenciaSel & " and Fecha_Medida between '" & fechainicio & " 00:00:00' and '" & fechaFin & " 23:59:59' " & sentenciaOrder
                End If

                da.SelectCommand.CommandText = sentenciaSel
                da.Fill(dst, "TablaMotores")

                Dim Tabla As DataTable
                Tabla = dst.Tables("TablaMotores").Clone()
                For i = dst.Tables("TablaMotores").Rows.Count - 1 To 0 Step -1
                    Tabla.Rows.Add(dst.Tables("TablaMotores").Rows(i).ItemArray)
                Next
                dst.Tables.Remove(dst.Tables("TablaMotores"))
                dst.Tables.Add(Tabla)

                QuitarRegistrosSegunFuenteDato("V", dst)

            ElseIf tipoElemen = "H" Then
                sentenciaSel = "SELECT  D2.CodigoPVYCR, D2.Cod_Fuente_Dato, D2.Fecha_Medida, D2.HorasIntervalo, D2.idElementoMedida, D2.Funciona, D2.Observaciones, " & _
                               "  D2.idIncidenciaVolumetrica, D2.ConsumoVolumetricoAdicional, D2.ReinicioLecturaVolumetrica, D2.descIncVol,  " & _
                               " D2.DescFuenteDato, D2.Caudal_LSeg, D2.codigoMotobomba, d2.modificado " & _
                               "  FROM " & _
                                " (SELECT  D.CodigoPVYCR, D.Cod_Fuente_Dato,D.Fecha_Medida," & _
                                "D.HorasIntervalo, D.idElementoMedida, " & _
                                "D.Funciona, substring(D.Observaciones,0,512) as Observaciones, D.idIncidenciaVolumetrica, " & _
                                "D.ConsumoVolumetricoAdicional, D.ReinicioLecturaVolumetrica, " & _
                                "IV.descripcion descIncVol, F.FUENTE_DATOS DescFuenteDato,  C.Caudal_LSeg,C.codigoMotobomba " & _
                                ",d.modificado FROM PVYCR_DatosHorometros D " & _
                                "LEFT OUTER JOIN  PVYCR_incidencias IV ON Iv.idIncidencia=D.idIncidenciaVolumetrica " & _
                                "INNER JOIN  FUENTES_DE_DATOS F ON F.Cod_Fuente_dato=D.Cod_Fuente_dato " & _
                                "INNER JOIN  PVYCR_ElementosMedida_MotoBombas E ON E.codigoPVYCR = d.codigoPVYCR and " & _
                               "E.idelementoMedida = D.idelementomedida " & _
                               "INNER JOIN  PVYCR_Motobombas C ON C.codigoMotobomba = E.codigoMotobomba and " & _
                               "C.FechaRevision = E.fechaRevision  " & _
                               "where D.idElementoMedida = '" & idelemento & "' and D.CodigoPVYCR = '" & codigoPVYCR & "' AND D.Fecha_Medida BETWEEN C.FechaInicial AND ISNULL(C.FECHAFIN, GETDATE()) " & _
                                " UNION " & _
                                "SELECT  D.CodigoPVYCR, D.Cod_Fuente_Dato,D.Fecha_Medida," & _
                                "D.HorasIntervalo, D.idElementoMedida, " & _
                                "D.Funciona,substring(D.Observaciones,0,512) as Observaciones, D.idIncidenciaVolumetrica, " & _
                                "D.ConsumoVolumetricoAdicional, D.ReinicioLecturaVolumetrica, " & _
                                "IV.descripcion descIncVol, F.FUENTE_DATOS DescFuenteDato,  NULL AS Caudal_LSeg,NULL AS codigoMotobomba " & _
                                ",d.modificado FROM PVYCR_DatosHorometros D " & _
                                "LEFT OUTER JOIN  PVYCR_incidencias IV ON Iv.idIncidencia=D.idIncidenciaVolumetrica " & _
                                "INNER JOIN  FUENTES_DE_DATOS F ON F.Cod_Fuente_dato=D.Cod_Fuente_dato " & _
                                "where D.idElementoMedida = '" & idelemento & "' and D.CodigoPVYCR = '" & codigoPVYCR & "'  " & _
                                " AND NOT EXISTS(SELECT * FROM dbo.PVYCR_ElementosMedida_MotoBombas AS E WHERE E.codigoPVYCR = D.CodigoPVYCR AND E.idElementoMedida = D.idElementoMedida )) as D2 " & _
                                " WHERE (1=1) " & _
                                " AND NOT EXISTS (SELECT D1.CODIGOPVYCR FROM PVYCR_DATOSHOROMETROS D1 where d2.codigopvycr=d1.codigopvycr and " & _
                                "d2.idelementomedida = d1.idelementomedida and d2.cod_fuente_dato = d1.cod_fuente_dato and d2.fecha_medida = d1.fecha_medida and (d1.cod_fuente_dato <> '05' and d1.idincidenciavolumetrica in (10,11)) ) "

                sentenciaOrder = " order by D2.codigoPVYCR, D2.Fecha_medida desc"

                If sFiltro <> "" Then
                    sentenciaSel = sentenciaSel & sFiltro & sentenciaOrder
                Else
                    sentenciaSel = sentenciaSel & " and D2.Fecha_Medida between '" & fechainicio & " 00:00:00' and '" & fechaFin & " 23:59:59' " & sentenciaOrder
                End If

                da.SelectCommand.CommandText = sentenciaSel
                da.Fill(dst, "TablaHorometros")

                Dim Tabla As DataTable
                Tabla = dst.Tables("TablaHorometros").Clone()
                For i = dst.Tables("TablaHorometros").Rows.Count - 1 To 0 Step -1
                    Tabla.Rows.Add(dst.Tables("TablaHorometros").Rows(i).ItemArray)
                Next
                dst.Tables.Remove(dst.Tables("TablaHorometros"))
                dst.Tables.Add(Tabla)

            ElseIf tipoElemen = "D" Then
                sentenciaSel = "SELECT d.CodigoPVYCR,d.idElementoMedida, d.Cod_Fuente_Dato,d.Fecha_Medida,suministroMensualM3,d.Observaciones, " & _
                                   " F.FUENTE_DATOS DescFuenteDato " & _
                                   "FROM PVYCR_DatosSuministros D " & _
                                   "INNER JOIN  FUENTES_DE_DATOS F ON F.Cod_Fuente_dato=D.Cod_Fuente_dato " & _
                                   "where codigoPVYCR = '" & codigoPVYCR & "' and " & _
                                  "idElementoMedida =  '" & idelemento & "' "

                sentenciaOrder = " order by codigoPVYCR, Fecha_Medida  desc, d.Cod_Fuente_dato "

                If sFiltro <> "" Then
                    sentenciaSel = sentenciaSel & sFiltro & sentenciaOrder
                Else
                    sentenciaSel = sentenciaSel & " and Fecha_Medida between '" & fechainicio & " 00:00:00' and '" & fechaFin & " 23:59:59' " & sentenciaOrder
                End If

                da.SelectCommand.CommandText = sentenciaSel
                da.Fill(dst, "TablaDiferencial")

                Dim Tabla As DataTable
                Tabla = dst.Tables("TablaDiferencial").Clone()
                For i = dst.Tables("TablaDiferencial").Rows.Count - 1 To 0 Step -1
                    Tabla.Rows.Add(dst.Tables("TablaDiferencial").Rows(i).ItemArray)
                Next
                dst.Tables.Remove(dst.Tables("TablaDiferencial"))
                dst.Tables.Add(Tabla)
            End If

        End Sub

        Protected Sub FinalizarDatasetElementos(ByVal tipo As String, ByVal dst As DataSet)
            If (tipo = "Q") Then
                obtenerVolumenDiferencial("Q", dst.Tables.Item("Datos"))
                obtenerCaudalAcumulado(dst)
            ElseIf (tipo = "E") Then
                obtenerVolumenDiferencial("E", dst.Tables.Item("Datos"))
                obtenerVolumenElectricoAcumulado(dst)
            ElseIf (tipo = "V") Then
                obtenerVolumenDiferencial("V", dst.Tables.Item("Datos"))
                obtenerVolumenAcumulado(dst)
            ElseIf (tipo = "H") Then
                obtenerVolumenDiferencial("H", dst.Tables.Item("Datos"))
                obtenerVolumenHorometroAcumulado(dst)
            ElseIf (tipo = "D") Then
                obtenerVolumenDiferencial("D", dst.Tables.Item("Datos"))
            End If
        End Sub

        Protected Function obtenerCaudalAcumulado(ByVal dst As DataSet) As Double
            Dim volumen As Double = 0
            'Como en la select ordeno por fecha descendientemente el primer registro del dataset será el volumen final o mas reciente
            If dst.Tables("Datos").Rows.Count > 0 Then
                If dst.Tables("Datos").Rows(dst.Tables("Datos").Rows.Count - 1).Item("Diferencial_Acum") = 0 Then
                    For j As Integer = dst.Tables("Datos").Rows.Count - 1 To 0 Step -1
                        If dst.Tables("Datos").Rows(j).Item("Diferencial_Acum") <> 0 Then
                            volumen = dst.Tables("Datos").Rows(j).Item("Diferencial_Acum")
                            Exit For
                        End If
                    Next
                Else
                    volumen = dst.Tables("Datos").Rows(dst.Tables("Datos").Rows.Count - 1).Item("Diferencial_Acum")
                End If
            End If

            Return volumen
        End Function

        Protected Function ObtenerConcesion(ByVal CodigoCauce As String, ByVal conexion As SqlConnection) As Integer
            Dim Concesion As Integer
            If conexion.State = ConnectionState.Closed Then conexion.Open()

            Dim daConcesion As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
            daConcesion.SelectCommand.CommandText = " SELECT volumenMaximoAnuallegal_m3 FROM PVYCR_Cauces WHERE codigocauce='" & CodigoCauce & "'"
            Concesion = nullACero(daConcesion.SelectCommand.ExecuteScalar())

            Return Concesion
        End Function

        Protected Function ObtenerPorConcesion(ByVal Concesion As Integer, ByVal Acumulado As Double) As Double
            Dim Porcentaje As Double = 0
            If Acumulado <> 0 And Concesion <> 0 Then
                Porcentaje = (100 * Acumulado / Concesion)
            End If
            Return Porcentaje
        End Function

        Protected Function obtenerVolumenAcumulado(ByVal dst As DataSet) As Double
            Dim volumen As Double = 0
            'Como en la select ordeno por fecha descendientemente el primer registro del dataset será el volumen final o mas reciente
            If dst.Tables("Datos").Rows.Count > 0 Then
                If nullACero(dst.Tables("Datos").Rows(dst.Tables("Datos").Rows.Count - 1).Item("Diferencial_Acum")) <> 0 Then
                    volumen = dst.Tables("Datos").Rows(dst.Tables("Datos").Rows.Count - 1).Item("Diferencial_Acum")
                End If
            End If
            Return volumen
        End Function

        Protected Function obtenerVolumenAcumuladoHoras(ByVal dst As DataSet) As Double
            Dim volumen As Double = 0
            'Como en la select ordeno por fecha descendientemente el primer registro del dataset será el volumen final o mas reciente
            If dst.Tables("Datos").Rows.Count > 0 Then
                If nullACero(dst.Tables("Datos").Rows(dst.Tables("Datos").Rows.Count - 1).Item("Diferencial_Acum_horas")) <> 0 Then
                    volumen = dst.Tables("Datos").Rows(dst.Tables("Datos").Rows.Count - 1).Item("Diferencial_Acum_horas")
                End If
            End If
            Return volumen
        End Function

        Protected Sub obtenerVolumenDiferencial(ByVal tipo As String, ByVal Tabla As DataTable)
            Dim filas As Integer = Tabla.Rows.Count
            If filas = 0 Then Exit Sub

            Dim i As Integer
            Dim v_lectura_ant_horas, v_diferencial_horas, v_vol_horas, v_vol_ant_horas, v_diferencial_acum_horas, v_vol, v_vol_ant, v_diferencial, v_diferencial_kwh, v_kwh, v_caudal, v_caudal_ant, v_caudal_medio, v_diferencial_acum, v_acum_kwh, v_lectura_ant, v_diferencial_m3, v_diferencial_acumm3 As Double
            Dim v_tiempo, v_tiempo_ant As Date
            Dim v_tiempo_medio As Integer
            Dim primeroNulo As String = "N"

            If Not Tabla.Columns.Contains("Diferencial") Then
                Tabla.Columns.Add(New DataColumn("Diferencial", Type.GetType("System.Double")))
                Tabla.Columns.Add(New DataColumn("Diferencial_Acum", Type.GetType("System.Double")))
                Tabla.Columns.Add(New DataColumn("Diferencial_AcumM3", Type.GetType("System.Double")))
                Tabla.Columns.Add(New DataColumn("Diferencial_Acum_horas", Type.GetType("System.Double")))
            End If

            If tipo = "V" Then
                For i = 0 To filas - 1
                    If i = 0 Then
                        Tabla.Rows(i).Item("Diferencial") = 0
                        Tabla.Rows(i).Item("Diferencial_Acum") = 0
                        v_lectura_ant = nullACero(Tabla.Rows(i).Item("LecturaContador_M3"))
                        v_vol_ant = v_lectura_ant

                        If Tabla.Rows(i).Item("LecturaContador_M3") Is System.DBNull.Value Then
                            primeroNulo = "S"
                        End If
                    Else
                        If (Tabla.Rows(i).Item("LecturaContador_M3") Is System.DBNull.Value) Then
                            v_diferencial = 0
                            Tabla.Rows(i).Item("Diferencial") = 0
                            Tabla.Rows(i).Item("Diferencial_Acum") = v_diferencial_acum
                        Else
                            v_vol = nullACero(Tabla.Rows(i).Item("LecturaContador_M3"))
                            If primeroNulo = "S" Then
                                primeroNulo = "N"
                                v_diferencial = 0
                                v_lectura_ant = nullACero(Tabla.Rows(i).Item("LecturaContador_M3"))
                            Else
                                If nullACero(Tabla.Rows(i).Item("idincidenciaVolumetrica")) = 6 Or nullACero(Tabla.Rows(i).Item("idincidenciaVolumetrica")) = 7 Then
                                    v_diferencial = nullACero(Tabla.Rows(i).Item("LecturaContador_M3")) - nullACero(Tabla.Rows(i).Item("ReiniciolecturaVolumetrica")) + _
                                                    nullACero(Tabla.Rows(i).Item("ConsumoVolumetricoAdicional"))
                                ElseIf nullACero(Tabla.Rows(i).Item("idincidenciaVolumetrica")) = 5 Or nullACero(Tabla.Rows(i).Item("idincidenciaVolumetrica")) = 8 Then
                                    If Tabla.Rows(i).Item("ConsumoVolumetricoAdicional") Is System.DBNull.Value Then
                                        v_diferencial = nullACero(Tabla.Rows(i).Item("LecturaContador_M3")) + nullACero(Tabla.Rows(i).Item("ConsumoVolumetricoAdicional")) - v_lectura_ant
                                    Else
                                        v_diferencial = nullACero(Tabla.Rows(i).Item("LecturaContador_M3")) + nullACero(Tabla.Rows(i).Item("ConsumoVolumetricoAdicional")) - v_lectura_ant
                                    End If
                                Else
                                    v_diferencial = v_vol - v_vol_ant
                                End If
                            End If

                            v_vol_ant = v_vol
                            v_lectura_ant = Tabla.Rows(i).Item("LecturaContador_M3")
                            v_diferencial_acum = v_diferencial_acum + v_diferencial

                            Tabla.Rows(i).Item("Diferencial") = v_diferencial
                            Tabla.Rows(i).Item("Diferencial_Acum") = v_diferencial_acum
                        End If
                    End If
                Next
            ElseIf tipo = "E" Then
                If Tabla.Rows.Count > 0 Then
                    For i = 0 To Tabla.Rows.Count - 1
                        If i = 0 Then
                            Tabla.Rows(i).Item("Diferencial") = 0
                            Tabla.Rows(i).Item("Diferencial_acum") = 0
                            Tabla.Rows(i).Item("Diferencial_acumM3") = 0
                            v_vol_ant = nullACero(Tabla.Rows(i).Item("Total_Kwh"))
                            v_lectura_ant = v_vol_ant

                            If Tabla.Rows(i).Item("Total_Kwh") Is System.DBNull.Value Then
                                primeroNulo = "S"
                            End If
                        Else
                            If Tabla.Rows(i).Item("Total_Kwh") Is System.DBNull.Value Then
                                v_diferencial = 0
                                v_diferencial_acum = v_diferencial_acum
                                v_diferencial_acumm3 = v_diferencial_acum
                                v_lectura_ant = nullACero(Tabla.Rows(i).Item("Total_Kwh"))

                                Tabla.Rows(i).Item("Diferencial") = 0
                                Tabla.Rows(i).Item("Diferencial_acum") = nullACero(v_acum_kwh)
                                Tabla.Rows(i).Item("Diferencial_acumM3") = nullACero(v_diferencial_acumm3)

                            Else
                                v_vol = nullACero(Tabla.Rows(i).Item("Total_Kwh"))
                                If (primeroNulo = "S") Then
                                    primeroNulo = "N"
                                    v_diferencial = 0
                                    v_diferencial_m3 = 0
                                    v_kwh = 0
                                Else
                                    v_lectura_ant = v_vol

                                    If nullACero(Tabla.Rows(i).Item("idIncidenciaElectrica")) = 2 Or nullACero(Tabla.Rows(i).Item("idIncidenciaElectrica")) = 3 Then
                                        If Tabla.Rows(i).Item("COD_FUENTE_DATO") = "05" Then
                                            v_diferencial = (nullACero(Tabla.Rows(i).Item("Total_Kwh")) - nullACero(Tabla.Rows(i).Item("ReiniciolecturaElectrica")) + _
                                            nullACero(Tabla.Rows(i).Item("ConsumoElectricoAdicional"))) * nullACero(Tabla.Rows(i).Item("relacionM3_kwh"))
                                            v_kwh = v_diferencial

                                        End If
                                    ElseIf nullACero(Tabla.Rows(i).Item("idIncidenciaElectrica")) = 1 Or nullACero(Tabla.Rows(i).Item("idIncidenciaElectrica")) = 4 Then
                                        If Not Tabla.Rows(i).Item("ConsumoElectricoAdicional") Is System.DBNull.Value Then
                                            v_diferencial = (nullACero(Tabla.Rows(i).Item("Total_Kwh")) + nullACero(Tabla.Rows(i).Item("ConsumoElectricoAdicional")) - _
                                            v_lectura_ant) * nullACero(Tabla.Rows(i).Item("relacionM3_kwh"))
                                            v_kwh = v_diferencial
                                        Else
                                            v_diferencial = (nullACero(Tabla.Rows(i).Item("Total_Kwh")) + nullACero(Tabla.Rows(i).Item("ConsumoElectricoAdicional")) - _
                                            v_lectura_ant) * nullACero(Tabla.Rows(i).Item("relacionM3_kwh"))
                                            v_kwh = v_diferencial
                                        End If
                                    Else
                                        v_diferencial = (v_vol - v_vol_ant) * nullACero(Tabla.Rows(i).Item("relacionM3_kwh"))
                                        v_kwh = (v_vol - v_vol_ant)
                                    End If
                                End If

                                v_vol_ant = v_vol
                                v_acum_kwh = v_acum_kwh + v_kwh
                                v_diferencial_acum = v_diferencial_acum + v_diferencial
                                v_diferencial_m3 = v_diferencial
                                v_diferencial_acumm3 = v_diferencial_acum

                                Tabla.Rows(i).Item("Diferencial") = v_diferencial_m3
                                Tabla.Rows(i).Item("Diferencial_acum") = v_acum_kwh
                                Tabla.Rows(i).Item("Diferencial_acumm3") = v_diferencial_acumm3
                            End If
                        End If
                    Next
                End If
            ElseIf tipo = "Q" Then
                If Tabla.Rows.Count > 0 Then
                    For i = 0 To Tabla.Rows.Count - 1
                        If i = 0 Then
                            Tabla.Rows(i).Item("Diferencial") = 0
                            Tabla.Rows(i).Item("Diferencial_acum") = 0
                            If Tabla.Rows(i).Item("Caudal_M3S") Is System.DBNull.Value Then
                                primeroNulo = "S"
                            End If
                        Else
                            v_caudal = nullACero(Tabla.Rows(i).Item("Caudal_M3S"))
                            v_caudal_ant = nullACero(Tabla.Rows(i - 1).Item("Caudal_M3S"))
                            v_caudal_medio = (v_caudal + v_caudal_ant) / 2

                            v_tiempo = Tabla.Rows(i).Item("Fecha_medida")
                            v_tiempo_ant = Tabla.Rows(i - 1).Item("Fecha_medida")
                            v_tiempo_medio = Math.Abs(Convert.ToInt32(DateDiff(DateInterval.Minute, v_tiempo_ant, v_tiempo) * 60))
                            v_diferencial = (v_caudal_medio * v_tiempo_medio)

                            'Si el caudal es nulo pasaremos al registro siguiente y ése no lo tendremos en cuenta
                            If Tabla.Rows(i).Item("Caudal_M3S") Is System.DBNull.Value Then
                                v_diferencial_acum = v_diferencial_acum
                                Tabla.Rows(i).Item("Diferencial") = 0
                                Tabla.Rows(i).Item("Diferencial_acum") = 0
                            Else
                                If primeroNulo = "S" Then
                                    primeroNulo = "N"
                                Else
                                    v_diferencial_acum = v_diferencial_acum + v_diferencial
                                    Tabla.Rows(i).Item("Diferencial") = v_diferencial
                                    Tabla.Rows(i).Item("Diferencial_acum") = v_diferencial_acum
                                End If
                            End If
                        End If
                    Next
                End If
            ElseIf tipo = "H" Then
                If Tabla.Rows.Count > 0 Then
                    For i = 0 To Tabla.Rows.Count - 1
                        If i = 0 Then
                            Tabla.Rows(i).Item("Diferencial") = 0
                            Tabla.Rows(i).Item("Diferencial_Acum") = 0
                            Tabla.Rows(i).Item("Diferencial_Acum_horas") = 0
                            v_vol_ant = nullACero(Tabla.Rows(i).Item("HorasIntervalo")) * 3600 * nullACero(Tabla.Rows(i).Item("Caudal_LSeg")) / 1000
                            v_vol_ant_horas = nullACero(Tabla.Rows(i).Item("HorasIntervalo"))
                            v_lectura_ant = v_vol_ant
                            If Tabla.Rows(i).Item("HorasIntervalo") Is System.DBNull.Value Then
                                primeroNulo = "S"
                            End If
                        Else
                            If (primeroNulo = "S") Then
                                If Not Tabla.Rows(i).Item("HorasIntervalo") Is System.DBNull.Value Then
                                    primeroNulo = "N"
                                    v_diferencial = 0
                                    v_vol_ant = nullACero(Tabla.Rows(i).Item("HorasIntervalo")) * 3600 * nullACero(Tabla.Rows(i).Item("Caudal_LSeg")) / 1000
                                    v_vol_ant_horas = nullACero(Tabla.Rows(i).Item("HorasIntervalo"))
                                Else
                                    v_vol_ant = 0
                                    v_lectura_ant = 0
                                End If
                            Else
                                If Tabla.Rows(i).Item("HorasIntervalo").ToString Is System.DBNull.Value Then
                                    v_diferencial = 0
                                    v_diferencial_horas = 0
                                    v_diferencial_acum = v_diferencial_acum
                                    v_diferencial_acum_horas = v_diferencial_acum_horas

                                    Tabla.Rows(i).Item("Diferencial") = 0
                                    Tabla.Rows(i).Item("Diferencial_Acum") = v_diferencial_acum
                                    Tabla.Rows(i).Item("Diferencial_Acum_horas") = v_diferencial_acum_horas
                                Else
                                    v_vol = nullACero(Tabla.Rows(i).Item("HorasIntervalo")) * 3600 * nullACero(Tabla.Rows(i).Item("Caudal_LSeg")) / 1000
                                    v_vol_horas = nullACero(Tabla.Rows(i).Item("HorasIntervalo"))
                                    If nullACero(Tabla.Rows(i).Item("idincidenciaVolumetrica")) = 10 Or nullACero(Tabla.Rows(i).Item("idincidenciaVolumetrica")) = 11 Then
                                        v_diferencial = v_vol - nullACero(Tabla.Rows(i).Item("ReiniciolecturaVolumetrica")) + nullACero(Tabla.Rows(i).Item("ConsumoVolumetricoAdicional"))
                                        v_diferencial_horas = v_vol_horas - nullACero(Tabla.Rows(i).Item("ReiniciolecturaVolumetrica")) + nullACero(Tabla.Rows(i).Item("ConsumoVolumetricoAdicional"))
                                    ElseIf nullACero(Tabla.Rows(i).Item("idincidenciaVolumetrica")) = 9 Or nullACero(Tabla.Rows(i).Item("idincidenciaVolumetrica")) = 12 Then
                                        If Not Tabla.Rows(i).Item("ConsumoVolumetricoAdicional") Is System.DBNull.Value Then
                                            v_diferencial = v_vol + nullACero(Tabla.Rows(i).Item("ConsumoVolumetricoAdicional")) - v_lectura_ant
                                            v_diferencial_horas = (v_vol_horas + Tabla.Rows(i).Item("ConsumoVolumetricoAdicional").ToString) - _
                                            Convert.ToDecimal(0 & v_lectura_ant_horas)
                                        Else
                                            v_diferencial = v_vol + nullACero(Tabla.Rows(i).Item("ConsumoVolumetricoAdicional")) - v_lectura_ant
                                            v_diferencial_horas = v_vol_horas + nullACero(Tabla.Rows(i).Item("ConsumoVolumetricoAdicional")) - v_lectura_ant_horas
                                        End If
                                    Else
                                        v_diferencial = v_vol - v_vol_ant
                                        v_diferencial_horas = v_vol_horas - v_vol_ant_horas
                                    End If
                                End If
                                v_vol_ant = v_vol
                                v_vol_ant_horas = v_vol_horas
                                v_lectura_ant = nullACero(Tabla.Rows(i).Item("HorasIntervalo")) * 3600 * nullACero(Tabla.Rows(i).Item("Caudal_LSeg")) / 1000
                                v_lectura_ant_horas = nullACero(Tabla.Rows(i).Item("HorasIntervalo"))
                                v_diferencial_acum = v_diferencial_acum + v_diferencial
                                v_diferencial_acum_horas = v_diferencial_acum_horas + v_diferencial_horas

                                Tabla.Rows(i).Item("Diferencial") = v_diferencial
                                Tabla.Rows(i).Item("Diferencial_Acum") = v_diferencial_acum
                                Tabla.Rows(i).Item("Diferencial_Acum_horas") = v_diferencial_acum_horas
                            End If
                        End If
                    Next
                End If
            ElseIf tipo = "D" Then
                If Tabla.Rows.Count > 0 Then
                    For i = 0 To Tabla.Rows.Count - 1
                        If Tabla.Rows(i).Item("SuministroMensualM3") Is System.DBNull.Value Then
                            v_diferencial = 0
                            v_diferencial_acum = v_diferencial_acum
                            Tabla.Rows(i).Item("Diferencial") = 0
                            Tabla.Rows(i).Item("Diferencial_Acum") = v_diferencial_acum
                        Else
                            v_vol = nullACero(Tabla.Rows(i).Item("SuministroMensualM3"))
                            v_diferencial = v_vol + v_vol_ant
                        End If
                        v_vol_ant = v_diferencial
                        v_lectura_ant = nullACero(Tabla.Rows(i).Item("SuministroMensualM3"))
                        v_diferencial_acum = v_diferencial_acum + v_diferencial
                        Tabla.Rows(i).Item("Diferencial") = v_diferencial
                        Tabla.Rows(i).Item("Diferencial_Acum") = v_diferencial_acum
                    Next
                End If

            End If
        End Sub

        Protected Function obtenerVolumenElectricoAcumulado(ByVal dst As DataSet) As Double
            Dim volumen As Double = 0
            If dst.Tables("Datos").Rows.Count > 0 Then
                If nullACero(dst.Tables("Datos").Rows(dst.Tables("Datos").Rows.Count - 1).Item("Diferencial_Acum")) <> 0 Then
                    volumen = dst.Tables("Datos").Rows(dst.Tables("Datos").Rows.Count - 1).Item("Diferencial_Acum")
                End If
            End If
            Return volumen
        End Function

        Protected Function obtenerVolumenElectricoAcumuladoM3(ByVal dst As DataSet) As Double
            Dim volumen As Double = 0
            If dst.Tables("Datos").Rows.Count > 0 Then
                If nullACero(dst.Tables("Datos").Rows(dst.Tables("Datos").Rows.Count - 1).Item("Diferencial_AcumM3")) <> 0 Then
                    volumen = dst.Tables("Datos").Rows(dst.Tables("Datos").Rows.Count - 1).Item("Diferencial_AcumM3")
                End If
            End If
            Return volumen
        End Function

        Protected Function obtenerVolumenHorometroAcumulado(ByVal dst As DataSet) As Double
            Dim volumen As Double = 0
            If dst.Tables("Datos").Rows.Count > 0 Then
                If nullACero(dst.Tables("Datos").Rows(dst.Tables("Datos").Rows.Count - 1).Item("Diferencial_Acum")) <> 0 Then
                    volumen = dst.Tables("Datos").Rows(dst.Tables("Datos").Rows.Count - 1).Item("Diferencial_Acum")
                End If
            End If
            Return volumen
        End Function

        Public Shared Sub QuitarRegistrosSegunFuenteDato(ByVal tipoElemento As String, ByVal dst As DataSet)
            Dim i As Int16 = 0
            Dim contador As Integer
            Select Case tipoElemento
                Case "V"
                    If Not dst.Tables("TablaMotores").Columns.Contains("BorrarFila") Then
                        dst.Tables("TablaMotores").Columns.Add("BorrarFila")
                    End If

                    contador = dst.Tables("TablaMotores").Rows.Count - 1
                    For i = 0 To contador
                        If (nullABlanco(dst.Tables("TablaMotores").Rows(i).Item("cod_fuente_dato")) <> "05") _
                            And (nullACero(dst.Tables("TablaMotores").Rows(i).Item("idIncidenciavolumetrica")) = 6 Or nullACero(dst.Tables("TablaMotores").Rows(i).Item("idIncidenciavolumetrica")) = 7) Then
                            If dst.Tables("TablaMotores").Select("cod_fuente_dato = '05' and (idincidenciavolumetrica = 6 or idincidenciavolumetrica = 7)").Length <> 0 Then
                                dst.Tables("TablaMotores").Rows(i).Item("BorrarFila") = "S"
                            End If
                        Else
                            dst.Tables("TablaMotores").Rows(i).Item("BorrarFila") = "N"
                        End If
                    Next
                    Dim dt As DataTable
                    dt = dst.Tables("TablaMotores").Copy
                    Dim filas As Integer = dt.Rows.Count - 1
                    For i = 0 To filas
                        If nullABlanco(dt.Rows(i).Item("BorrarFila")) = "S" Then
                            dst.Tables("TablaMotores").Rows(i).Delete()
                        End If
                    Next
                Case "E"
                    If Not dst.Tables("TablaAlimentacion").Columns.Contains("BorrarFila") Then
                        dst.Tables("TablaAlimentacion").Columns.Add("BorrarFila")
                    End If

                    contador = dst.Tables("TablaAlimentacion").Rows.Count - 1
                    For i = 0 To contador
                        If (nullABlanco(dst.Tables("TablaAlimentacion").Rows(i).Item("cod_fuente_dato")) <> "05" Or _
                            nullABlanco(dst.Tables("TablaAlimentacion").Rows(i).Item("cod_fuente_dato")) <> "13" Or _
                            nullABlanco(dst.Tables("TablaAlimentacion").Rows(i).Item("cod_fuente_dato")) <> "10") _
                            And (nullACero(dst.Tables("TablaAlimentacion").Rows(i).Item("idIncidenciaelectrica")) = 2 Or nullACero(dst.Tables("TablaAlimentacion").Rows(i).Item("idIncidenciaelectrica")) = 3) Then
                            If dst.Tables("TablaAlimentacion").Select("cod_fuente_dato = '05' and (idIncidenciaelectrica = 2 or idIncidenciaelectrica = 3)").Length <> 0 Then
                                dst.Tables("TablaAlimentacion").Rows(i).Item("BorrarFila") = "S"
                            End If
                        End If
                    Next
                    Dim dt As DataTable
                    dt = dst.Tables("TablaAlimentacion").Copy
                    Dim filas As Integer = dt.Rows.Count - 1
                    For i = 0 To filas
                        If nullABlanco(dt.Rows(i).Item("BorrarFila")) = "S" Then
                            dst.Tables("TablaAlimentacion").Rows(i).Delete()
                        End If
                    Next
                Case "H"
                    If Not dst.Tables("TablaHorometros").Columns.Contains("BorrarFila") Then
                        dst.Tables("TablaHorometros").Columns.Add("BorrarFila")
                    End If

                    contador = dst.Tables("TablaHorometros").Rows.Count - 1
                    For i = 0 To contador
                        If (nullABlanco(dst.Tables("TablaHorometros").Rows(i).Item("cod_fuente_dato")) <> "05" Or _
                            nullABlanco(dst.Tables("TablaHorometros").Rows(i).Item("cod_fuente_dato")) <> "13" Or _
                            nullABlanco(dst.Tables("TablaHorometros").Rows(i).Item("cod_fuente_dato")) <> "10") Then
                            dst.Tables("TablaHorometros").Rows(i).Item("BorrarFila") = "S"
                        End If
                    Next
                    Dim dt As DataTable
                    dt = dst.Tables("TablaAlimentacion").Copy
                    Dim filas As Integer = dt.Rows.Count - 1
                    For i = 0 To filas
                        If nullABlanco(dt.Rows(i).Item("BorrarFila")) = "S" Then
                            dst.Tables("TablaAlimentacion").Rows(i).Delete()
                        End If
                    Next
            End Select
        End Sub
    End Class
End Namespace