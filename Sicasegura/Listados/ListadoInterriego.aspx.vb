Imports System.Data
Imports GuarderiaFluvial
Imports GuarderiaFluvial.JavaScript
Imports GuarderiaFluvial.utiles
Imports System.Data.SqlClient
Imports System
Imports NineRays.Reporting.Wizards
Imports NineRays.Reporting.DOM
Imports NineRays.Basics.Drawing
Partial Class Listados_ListadoInterriego
    Inherits System.Web.UI.Page
    Dim conexion As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_listados"))
    Dim conexion_insert As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
    Dim daInterriego As System.Data.SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim dstInterriego As DataSet = New System.Data.DataSet()
    'Dim dstListados As DataSet = New System.Data.DataSet()
    Dim sentenciaSel, sentenciaOrder As String
    Dim comando As SqlClient.SqlCommand = New System.Data.SqlClient.SqlCommand("", conexion_insert)
    Protected reportgenerator1 As NineRays.Reporting.Components.ReportGenerator = New NineRays.Reporting.Components.ReportGenerator()
    Dim dstListados As DataSet = New System.Data.DataSet()
    Dim semestreDesde1, semestreHasta1, semestreDesde2, semestreHasta2 As Date
    Dim volumenA1, volumenA2, v_caudaltotal_m32, v_caudaltotal_m31, caudal_m3s2, caudal_m3s, sumavolumenA1, sumavolumenA2, sumavolumenA3, sumaQmedioConsumido, totalQmedioConsumido As Decimal


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            utiles.Comprobar_Conexion_BD(Page, conexion_insert)
            'creamos el dataset que vamos a pasar al informe
            CrearDataSetListados()
            ' inicializamos variables
            'Dim fechainicio As DateTime = Session("InterriegoFechaIni")
            'Dim fechafin As DateTime = Session("InterriegoFechaFin")
            Dim idAgru As Integer = Session("InterriegoIdAgru")
            semestreDesde1 = obtenerAnyoHidrologicoDesde()
            semestreHasta1 = DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Month, 6, Convert.ToDateTime(obtenerAnyoHidrologicoDesde())))
            semestreDesde2 = DateAdd(DateInterval.Month, 6, Convert.ToDateTime(obtenerAnyoHidrologicoDesde()))
            semestreHasta2 = obtenerAnyoHidrologicoHasta()
            Dim dias1 As Integer = DateDiff(DateInterval.Day, semestreDesde1, semestreHasta1) + 1
            Dim dias2 As Integer = DateDiff(DateInterval.Day, semestreDesde2, semestreHasta2) + 1
            Dim dias3 As Integer = DateDiff(DateInterval.Day, semestreDesde1, semestreHasta2) + 1

            'obtenemos los datos necesarios para rellenar el dataset de listados
            CrearDatasetDatos(semestreDesde1, semestreHasta1, semestreDesde2, semestreHasta2, idAgru, dias1, dias2, dias3)
            'rellenamos el dataset listados
            RellenarDatasetListados(dias1, semestreDesde1, semestreHasta1, dias2, semestreDesde2, semestreHasta2, dias3, idAgru)
            'rellenamos el dataset listados

            'mostramos el report
            CType(dstListados, System.ComponentModel.ISupportInitialize).BeginInit()

            Me.reportgenerator1.DataSources = New NineRays.Reporting.Components.ObjectPointerCollection(New String() {"Contadores"}, New Object() {dstListados})

            CType(dstListados, System.ComponentModel.ISupportInitialize).EndInit()

            reportgenerator1.LoadTemplate(Server.MapPath("ListadoInterriego.rst"))
            Dim a As NineRays.Reporting.RegisterKeyAttribute = New NineRays.Reporting.RegisterKeyAttribute("4761628416024064132096")
            SharpShooterWebViewer1.Source = reportgenerator1
            'SharpShooterWebViewer1.ViewMode = NineRays.Reporting.Web.ViewMode.HtmlSinglePage
            If Request.QueryString("format") Is Nothing Then
                Response.Redirect(SharpShooterWebViewer1.PdfLink)
            End If

        End If
    End Sub
    Protected Sub crearDataSetListados()
        dstListados.Tables.Add("TablaContador")
        dstListados.Tables("TablaContador").Columns.Add(New DataColumn("codigoCauce"))
        dstListados.Tables("TablaContador").Columns.Add(New DataColumn("DenominacionCauce"))
        dstListados.Tables("TablaContador").Columns.Add(New DataColumn("codigoPVYCR"))
        dstListados.Tables("TablaContador").Columns.Add(New DataColumn("volumen_captado"))
        dstListados.Tables("TablaContador").Columns.Add(New DataColumn("volumen_legal"))
        dstListados.Tables("TablaContador").Columns.Add(New DataColumn("porcentaje_consumido"))
        dstListados.Tables("TablaContador").Columns.Add(New DataColumn("SuperficieRealAproximada_HAS"))
        dstListados.Tables("TablaContador").Columns.Add(New DataColumn("SuperficieInscrita_HAS"))
        dstListados.Tables("TablaContador").Columns.Add(New DataColumn("superficieTotal"))
        dstListados.Tables("TablaContador").Columns.Add(New DataColumn("Periodo1"))
        dstListados.Tables("TablaContador").Columns.Add(New DataColumn("Periodo2"))
        dstListados.Tables("TablaContador").Columns.Add(New DataColumn("Periodo3"))
        dstListados.Tables("TablaContador").Columns.Add(New DataColumn("titulo"))

        'datos primer semestre
        dstListados.Tables("TablaContador").Columns.Add(New DataColumn("DotacionTeoricaG"))
        dstListados.Tables("TablaContador").Columns.Add(New DataColumn("DotacionTeoricaM"))
        dstListados.Tables("TablaContador").Columns.Add(New DataColumn("porcentaje_calculado"))
        dstListados.Tables("TablaContador").Columns.Add(New DataColumn("volumen_a"))
        dstListados.Tables("TablaContador").Columns.Add(New DataColumn("caudal_m3s"))
        'NCM parametro para ocultar los campos del primer periodo
        dstListados.Tables("TablaContador").Columns.Add(New DataColumn("verCampo1"))
        'NCM parametro para ocultar los campos del segundo periodo
        dstListados.Tables("TablaContador").Columns.Add(New DataColumn("verCampo2"))
        'datos segundo semestre
        dstListados.Tables("TablaContador").Columns.Add(New DataColumn("DotacionTeoricaG2"))
        dstListados.Tables("TablaContador").Columns.Add(New DataColumn("DotacionTeoricaM2"))
        dstListados.Tables("TablaContador").Columns.Add(New DataColumn("porcentaje_calculado2"))
        dstListados.Tables("TablaContador").Columns.Add(New DataColumn("volumen_a2"))
        dstListados.Tables("TablaContador").Columns.Add(New DataColumn("caudal_m3s2"))
        'datos  año hidrologico
        dstListados.Tables("TablaContador").Columns.Add(New DataColumn("QmedioDerivado"))
        dstListados.Tables("TablaContador").Columns.Add(New DataColumn("QmedioConsumido"))
        dstListados.Tables("TablaContador").Columns.Add(New DataColumn("porc_total"))
        dstListados.Tables("TablaContador").Columns.Add(New DataColumn("volumen_a3"))

        dstListados.Tables.Add("TablaTotales")
        dstListados.Tables("TablaTotales").Columns.Add(New DataColumn("SumHas"))
        dstListados.Tables("TablaTotales").Columns.Add(New DataColumn("SumSuperficie"))
        dstListados.Tables("TablaTotales").Columns.Add(New DataColumn("Caudaltotal_m31"))
        dstListados.Tables("TablaTotales").Columns.Add(New DataColumn("caudalPorDias1"))
        dstListados.Tables("TablaTotales").Columns.Add(New DataColumn("caudalPorseg1"))
        dstListados.Tables("TablaTotales").Columns.Add(New DataColumn("Caudaltotal_m32"))
        dstListados.Tables("TablaTotales").Columns.Add(New DataColumn("caudalPorDias2"))
        dstListados.Tables("TablaTotales").Columns.Add(New DataColumn("caudalPorseg2"))
        dstListados.Tables("TablaTotales").Columns.Add(New DataColumn("Caudaltotal_m33"))
        dstListados.Tables("TablaTotales").Columns.Add(New DataColumn("caudalPorDias3"))
        dstListados.Tables("TablaTotales").Columns.Add(New DataColumn("caudalPorseg3"))
        dstListados.Tables("TablaTotales").Columns.Add(New DataColumn("totalQmedioConsumido"))
        dstListados.Tables("TablaTotales").Columns.Add(New DataColumn("SumaQmedioConsumido"))

    End Sub
    Protected Sub CrearDatasetDatos(ByVal fechainicio1 As DateTime, ByVal fechafin1 As DateTime, ByVal fechainicio2 As DateTime, ByVal fechafin2 As DateTime, ByVal idAgru As Integer, ByVal dias As Integer, ByVal dias2 As Integer, ByVal dias3 As Integer)
        Dim cteDotacion1 As Decimal = ObtenerCteDotacion(fechainicio1, fechafin1, idAgru, dias)
        Dim cteDotacion2 As Decimal = ObtenerCteDotacion(fechainicio2, fechafin2, idAgru, dias2)
        'obtenemos cauce y los puntos del mismo
        sentenciaSel = "SELECT C.CodigoCauce,C.DenominacionCauce,P.CODIGOPVYCR,isnull(SuperficieRealAproximada_HAS,0) SuperficieRealAproximada_HAS," & _
        "isnull(SuperficieInscrita_HAS,0) SuperficieInscrita_HAS,(isnull(SuperficieRealAproximada_HAS,0)+isnull(SuperficieInscrita_HAS,0)) superficieTotal, " & _
         Replace(cteDotacion1, ",", ".") & " * isnull(SuperficieRealAproximada_HAS,0)/86400 DotacionteoricaG," & Replace(cteDotacion1, ",", ".") & " * isnull(SuperficieInscrita_HAS,0)/86400 DotacionTeoricaM, " & _
         Replace(cteDotacion2, ",", ".") & " * isnull(SuperficieRealAproximada_HAS,0)/86400 DotacionteoricaG2," & Replace(cteDotacion2, ",", ".") & " * isnull(SuperficieInscrita_HAS,0)/86400 DotacionTeoricaM2, " & _
        "VolumenMaximoAnualTeorico_M3 vol_legal, a.descripcion desc_agru " & _
        "FROM PVYCR_Cauces C, PVYCR_PUNTOS P, PVYCR_AGRUPACIONES A, PVYCR_AGRUPACIONES_ELEMENTOSMEDIDA AE " & _
        "where A.idagrupacion = AE.idagrupacion and C.CODIGOCAUCE = P.CODIGOCAUCE AND AE.codigoPVYCR = p.codigoPVYCR And " & _
        "a.idagrupacion = " & idAgru

        daInterriego.SelectCommand.CommandText = sentenciaSel
        daInterriego.Fill(dstInterriego, "TablaContador")

        'obtenemos los totales para cada periodo
        sentenciaSel = "SELECT isnull(sum(a.CAUDAL_M3S),0) Caudaltotal_m31,  isnull(sum(a.CAUDAL_M3S),0)/" & dias & " caudalPorDias1, (isnull(sum(a.CAUDAL_M3S),0)/" & dias & ")/86400 caudalPorseg1 " & _
            "FROM PVYCR_Cauces C, PVYCR_PUNTOS P, PVYCR_DATOSACEQUIAS A, PVYCR_agrupaciones G, PVYCR_AGRUPACIONES_ELEMENTOSMEDIDA GE " & _
            "where C.CODIGOCAUCE = P.CODIGOCAUCE AND P.CODIGOPVYCR = A.CODIGOPVYCR and G.idagrupacion = GE.idagrupacion and " & _
            "GE.codigoPVYCR = p.codigoPVYCR AND G.idagrupacion = " & idAgru & " AND " & _
            "Fecha_Medida between '" & fechainicio1 & " 00:00:00' and '" & fechafin1 & " 23:59:59'"

        daInterriego.SelectCommand.CommandText = sentenciaSel
        daInterriego.Fill(dstInterriego, "TablaTotalCaudalP1")

        sentenciaSel = "SELECT isnull(sum(a.CAUDAL_M3S),0) Caudaltotal_m32,  isnull(sum(a.CAUDAL_M3S),0)/" & dias2 & " caudalPorDias2, (isnull(sum(a.CAUDAL_M3S),0)/" & dias2 & ")/86400 caudalPorseg2 " & _
            "FROM PVYCR_Cauces C, PVYCR_PUNTOS P, PVYCR_DATOSACEQUIAS A, PVYCR_agrupaciones G, PVYCR_AGRUPACIONES_ELEMENTOSMEDIDA GE " & _
            "where C.CODIGOCAUCE = P.CODIGOCAUCE AND P.CODIGOPVYCR = A.CODIGOPVYCR and G.idagrupacion = GE.idagrupacion and " & _
            "GE.codigoPVYCR = p.codigoPVYCR AND G.idagrupacion = " & idAgru & " AND " & _
            "Fecha_Medida between '" & fechainicio2 & " 00:00:00' and '" & fechafin2 & " 23:59:59'"

        daInterriego.SelectCommand.CommandText = sentenciaSel
        daInterriego.Fill(dstInterriego, "TablaTotalCaudalP2")

        'totales de superficie
        sentenciaSel = "SELECT sum(SuperficieRealAproximada_HAS) sumhas,sum((isnull(SuperficieRealAproximada_HAS,0)+isnull(SuperficieInscrita_HAS,0))) sumsuperficie " & _
                    "FROM PVYCR_Cauces C, PVYCR_PUNTOS P, PVYCR_AGRUPACIONES A, PVYCR_AGRUPACIONES_ELEMENTOSMEDIDA AE " & _
                    "where A.idagrupacion = AE.idagrupacion and C.CODIGOCAUCE = P.CODIGOCAUCE AND AE.codigoPVYCR = p.codigoPVYCR And a.idagrupacion = " & idAgru
        daInterriego.SelectCommand.CommandText = sentenciaSel
        daInterriego.Fill(dstInterriego, "TablaTotalCaudal")
        'obtenemos el caudal para el año hidrologico que será el campo volumen captado
        sentenciaSel = "SELECT isnull(sum(a.CAUDAL_M3S),0) Caudaltotal_m33,  isnull(sum(a.CAUDAL_M3S),0)/" & dias3 & " caudalPorDias3, (isnull(sum(a.CAUDAL_M3S),0)/" & dias3 & ")/86400 caudalPorseg3 " & _
            "FROM PVYCR_Cauces C, PVYCR_PUNTOS P, PVYCR_DATOSACEQUIAS A, PVYCR_agrupaciones G, PVYCR_AGRUPACIONES_ELEMENTOSMEDIDA GE " & _
            "where C.CODIGOCAUCE = P.CODIGOCAUCE AND P.CODIGOPVYCR = A.CODIGOPVYCR and G.idagrupacion = GE.idagrupacion and " & _
            "GE.codigoPVYCR = p.codigoPVYCR AND G.idagrupacion = " & idAgru & " AND " & _
            "Fecha_Medida between '" & obtenerAnyoHidrologicoDesde() & " 00:00:00' and '" & obtenerAnyoHidrologicoHasta() & " 23:59:59'"

        daInterriego.SelectCommand.CommandText = sentenciaSel
        daInterriego.Fill(dstInterriego, "TablaTotalCaudalP3")


    End Sub
    Protected Sub RellenarDatasetListados(ByVal dias As Integer, ByVal fechaini As Date, ByVal fechafin As Date, ByVal dias2 As Integer, ByVal fechaini2 As Date, ByVal fechafin2 As Date, ByVal dias3 As Integer, ByVal idAgru As Integer)
        Dim i As Integer
        Dim rw As DataRow
        Dim rwTotal As DataRow
        Dim porcentaje_consumido, volCaptado As Decimal
        Dim v_QmedioConsumido, v_Qmedioderivado As Decimal

        If dstInterriego.Tables("TablaContador").Rows.Count > 0 Then
            rwTotal = dstListados.Tables("TablaTotales").NewRow
            'recorreremos todo el dataset e iremos calculando el diferencial y añadiendo la columna al dataset
            For i = 0 To dstInterriego.Tables("TablaContador").Rows.Count - 1
                'inicializamos variables v_caudaltotal_m31,v_caudaltotal_m31
                rw = dstListados.Tables("TablaContador").NewRow
                'obtenbemos los valores para el primer semestre
                ValoresPrimersemestre(i, rw, dias, idAgru, fechaini, fechafin)
                'obtenemos los valores para el segundo semestre
                ValoresSegundosemestre(i, rw, dias2, idAgru, fechaini2, fechafin2)

                'informacion común a los dos periodos
                volCaptado = CargarVolCaptado(dstInterriego.Tables("TablaContador").Rows(i).Item("CodigoCauce").ToString)
                'si el volumen captado no tiene filas pondremos el porcentaje consumido a nulo
                If volCaptado = 0 Then
                    rw("porcentaje_consumido") = DBNull.Value.ToString
                    rw("volumen_captado") = DBNull.Value.ToString
                Else
                    'comprobamos si tiene volumen legal
                    If dstInterriego.Tables("TablaContador").Rows(i).Item("Vol_legal").ToString = "" Then
                        rw("porcentaje_consumido") = DBNull.Value.ToString
                        rw("volumen_legal") = DBNull.Value.ToString
                    Else
                        porcentaje_consumido = (volCaptado * 100) / dstInterriego.Tables("TablaContador").Rows(i).Item("Vol_legal")
                        rw("porcentaje_consumido") = String.Format("{0:#,##0.00}", porcentaje_consumido)
                    End If
                    rw("volumen_captado") = String.Format("{0:#,##0.000}", volCaptado)
                    rw("volumen_legal") = String.Format("{0:#,##0.000}", dstInterriego.Tables("tablacontador").Rows(i).Item("vol_legal"))

                End If

                'datos comunes para todos los periodos
                rw("periodo1") = " De " & fechaini & " a " & fechafin
                rw("periodo2") = " De " & fechaini2 & " a " & fechafin2
                rw("periodo3") = " Total año hidrológico"
                rw("titulo") = " INTERRIEGO " & " - " & dstInterriego.Tables("TablaContador").Rows(i).Item("Desc_agru").ToString.ToUpper
                'estos campos nos servirá por si en un futuro quieren ocultar alguno de los semestres, de momento los visualizamos todos
                rw("verCampo1") = True
                rw("verCampo2") = True
                rw("codigoCauce") = dstInterriego.Tables("TablaContador").Rows(i).Item("codigoCauce")
                rw("codigoPVYCR") = dstInterriego.Tables("TablaContador").Rows(i).Item("codigoPVYCR")
                rw("DenominacionCauce") = dstInterriego.Tables("TablaContador").Rows(i).Item("DenominacionCauce")
                rw("SuperficieRealAproximada_HAS") = String.Format("{0:#,##0.0000}", dstInterriego.Tables("TablaContador").Rows(i).Item("SuperficieRealAproximada_HAS"))
                rw("SuperficieInscrita_HAS") = String.Format("{0:#,##0.0000}", dstInterriego.Tables("TablaContador").Rows(i).Item("SuperficieInscrita_HAS"))
                rw("superficieTotal") = String.Format("{0:#,##0.0000}", dstInterriego.Tables("TablaContador").Rows(i).Item("superficieTotal"))
                'datos año hidrológico
                v_Qmedioderivado = ((caudal_m3s * dias) + (caudal_m3s2 * dias2)) / dias3
                rw("Qmedioderivado") = String.Format("{0:#,##0.000}", v_Qmedioderivado)
                rw("volumen_a3") = String.Format("{0:#,##0}", volumenA1 + volumenA2)
                v_QmedioConsumido = ((volumenA1 + volumenA2) / 365) / 86400
                sumaQmedioConsumido = sumaQmedioConsumido + v_QmedioConsumido
                rw("Qmedioconsumido") = String.Format("{0:#,##0.000}", v_QmedioConsumido)
                If v_Qmedioderivado = 0 Or v_QmedioConsumido = 0 Then
                    rw("porc_total") = String.Format("{0:#,##0.00}", 0)
                Else
                    rw("porc_total") = String.Format("{0:#,##0.00}", (v_QmedioConsumido / v_Qmedioderivado))
                End If

                'añadimos la fila al datarow
                dstListados.Tables("TablaContador").Rows.Add(rw)

            Next
            rwTotal("SumSuperficie") = String.Format("{0:#,##0.0000}", dstInterriego.Tables("TablaTotalCaudal").Rows(0).Item("SumSuperficie"))
            rwTotal("SumHas") = String.Format("{0:#,##0.0000}", dstInterriego.Tables("TablaTotalCaudal").Rows(0).Item("SumHas"))

            rwTotal("Caudaltotal_m31") = String.Format("{0:#,##0}", sumavolumenA1)
            If sumavolumenA1 <> 0 Then
                rwTotal("caudalPorDias1") = String.Format("{0:#,##0}", sumavolumenA1 / dias)
                rwTotal("caudalPorSeg1") = String.Format("{0:#,##0.000}", (sumavolumenA1 / dias) / 86400)
            Else
                rwTotal("caudalPorDias1") = String.Format("{0:#,##0}", 0)
                rwTotal("caudalPorSeg1") = String.Format("{0:#,##0.000}", 0)
            End If


            If sumavolumenA2 <> 0 Then
                rwTotal("caudalPorDias2") = String.Format("{0:#,##0}", sumavolumenA2 / dias2)
                rwTotal("caudalPorSeg2") = String.Format("{0:#,##0.000}", (sumavolumenA2 / dias2) / 86400)
            Else
                rwTotal("caudalPorDias2") = String.Format("{0:#,##0}", 0)
                rwTotal("caudalPorSeg2") = String.Format("{0:#,##0.000}", 0)
            End If
            rwTotal("Caudaltotal_m32") = String.Format("{0:#,##0}", sumavolumenA2)
            'totales finales por año hidrologico
            sumavolumenA3 = sumavolumenA1 + sumavolumenA2

            If sumavolumenA3 <> 0 Then
                rwTotal("caudaltotal_m33") = String.Format("{0:#,##0}", sumavolumenA3)
                rwTotal("caudalPorDias3") = String.Format("{0:#,##0.000}", sumavolumenA3 / dias3)
                rwTotal("caudalPorSeg3") = String.Format("{0:#,##0.000}", (sumavolumenA3 / dias3) / 86400)
            Else
                rwTotal("caudaltotal_m33") = String.Format("{0:#,##0}", 0)
                rwTotal("caudalPorDias3") = String.Format("{0:#,##0}", 0)
                rwTotal("caudalPorSeg3") = String.Format("{0:#,##0}", 0)
            End If

            rwTotal("sumaQmedioconsumido") = String.Format("{0:#,##0.000}", sumaQmedioConsumido)
            totalQmedioConsumido = (((sumavolumenA1 / dias) / 86400 * dias) + ((sumavolumenA2 / dias) / 86400 * dias2)) / dias3
            rwTotal("totalQmedioconsumido") = String.Format("{0:#,##0.000}", totalQmedioConsumido)

            'If dstInterriego.Tables("TablaTotalCaudalP3").Rows.Count > 0 Then
            '    rwTotal("caudaltotal_m33") = String.Format("{0:#,##0.000}", dstInterriego.Tables("TablaTotalCaudalP3").Rows(0).Item("Caudaltotal_m33"))
            '    rwTotal("caudalPorDias3") = String.Format("{0:#,##0.000}", dstInterriego.Tables("TablaTotalCaudalP3").Rows(0).Item("caudalPorDias3"))
            '    rwTotal("caudalPorSeg3") = String.Format("{0:#,##0.000}", dstInterriego.Tables("TablaTotalCaudalP3").Rows(0).Item("caudalPorSeg3"))
            'Else
            '    rwTotal("caudaltotal_m33") = String.Format("{0:#,##0.000}", 0)
            '    rwTotal("caudalPorDias3") = String.Format("{0:#,##0.000}", 0)
            '    rwTotal("caudalPorSeg3") = String.Format("{0:#,##0.000}", 0)
            'End If

            dstListados.Tables("TablaTotales").Rows.Add(rwTotal)
        End If
    End Sub

    Protected Function obtenerAnyoHidrologicoDesde() As String
        If DateTime.Now.Month < 10 Then
            Return "01/10/" & DateTime.Now.Year - 1
        Else
            Return "01/10/" & DateTime.Now.Year
        End If
    End Function
    Protected Function obtenerAnyoHidrologicoHasta() As String
        If DateTime.Now.Month < 10 Then
            Return "30/09/" & DateTime.Now.Year
        Else
            Return "30/09/" & DateTime.Now.Year + 1
        End If
    End Function
    Protected Function CargarVolCaptado(ByVal cauce As String) As Decimal
        Dim comando As SqlCommand = New SqlCommand("", conexion)
        Dim volcaptado As Decimal
        utiles.Comprobar_Conexion_BD(Page, conexion)

        comando.CommandText = "SELECT sum(isnull(a.CAUDAL_M3S,0)) volumen_captado " & _
     "FROM PVYCR_Cauces C, PVYCR_PUNTOS P, PVYCR_DATOSACEQUIAS A, PVYCR_agrupaciones G, PVYCR_AGRUPACIONES_ELEMENTOSMEDIDA GE " & _
     "where C.CODIGOCAUCE = P.CODIGOCAUCE AND P.CODIGOPVYCR = A.CODIGOPVYCR and G.idagrupacion = GE.idagrupacion and " & _
     "GE.codigoPVYCR = p.codigoPVYCR AND G.idagrupacion = 1 AND P.CODIGOCAUCE = '" & cauce & "' and " & _
      "Fecha_Medida between '" & obtenerAnyoHidrologicoDesde() & " 00:00:00' and '" & obtenerAnyoHidrologicoHasta() & " 23:59:59'"
        volcaptado = Convert.ToDecimal("0" & comando.ExecuteScalar)
        Return volcaptado
    End Function

    Protected Function obtenerPrimerSemestre() As String
        semestreHasta1 = DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Month, 6, Convert.ToDateTime(obtenerAnyoHidrologicoDesde())))
        Return " De " & obtenerAnyoHidrologicoDesde() & " hasta " & semestreHasta1
    End Function
    Protected Function obtenerSegundoSemestre() As String
        semestreHasta2 = DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Month, 6, Convert.ToDateTime(obtenerAnyoHidrologicoDesde())))
        Return " De " & semestreHasta1 & " hasta " & obtenerAnyoHidrologicoHasta()
    End Function
    Protected Sub ValoresPrimersemestre(ByVal i As Integer, ByVal rw As DataRow, ByVal dias As Integer, ByVal idagru As Integer, ByVal fechaini As Date, ByVal fechafin As Date)
        Dim porcentaje_calculado As Decimal

        'calculamos el caudal para cada punto 
        caudal_m3s = ObtenerCaudal(idagru, dstInterriego.Tables("TablaContador").Rows(i).Item("codigoPVYCR"), fechaini, fechafin)
        'calculamos los valores del informe de interriego
        If caudal_m3s = 0 Then
            porcentaje_calculado = 0
            caudal_m3s = 0
            volumenA1 = 0
        Else
            porcentaje_calculado = (dstInterriego.Tables("TablaContador").Rows(i).Item("DotacionTeoricaG") + dstInterriego.Tables("TablaContador").Rows(i).Item("DotacionTeoricaM")) / caudal_m3s
            volumenA1 = caudal_m3s * porcentaje_calculado * 86400 * dias
        End If
        'If dstInterriego.Tables("TablaTotalCaudalP1").Rows.Count > 0 Then
        '    v_caudaltotal_m31 = String.Format("{0:#,##0.000}", dstInterriego.Tables("TablaTotalCaudalP1").Rows(0).Item("Caudaltotal_m31"))
        '    rw("caudalPorDias1") = String.Format("{0:#,##0.000}", dstInterriego.Tables("TablaTotalCaudalP1").Rows(0).Item("caudalPorDias1"))
        '    rw("caudalPorSeg1") = String.Format("{0:#,##0.000}", dstInterriego.Tables("TablaTotalCaudalP1").Rows(0).Item("caudalPorSeg1"))
        'Else
        '    v_caudaltotal_m31 = String.Format("{0:#,##0.000}", 0)
        '    rw("caudalPorDias1") = String.Format("{0:#,##0.000}", 0)
        '    rw("caudalPorSeg1") = String.Format("{0:#,##0.000}", 0)
        'End If

        sumavolumenA1 = sumavolumenA1 + volumenA1
  
        rw("porcentaje_calculado") = String.Format("{0:#,##0.00}", porcentaje_calculado)
        rw("volumen_a") = String.Format("{0:#,##0}", volumenA1)
        rw("caudal_m3s") = String.Format("{0:#,##0.000}", caudal_m3s)
        rw("DotacionTeoricaG") = String.Format("{0:#,##0.0000}", dstInterriego.Tables("TablaContador").Rows(i).Item("DotacionTeoricaG"))
        rw("DotacionTeoricaM") = String.Format("{0:#,##0.0000}", dstInterriego.Tables("TablaContador").Rows(i).Item("DotacionTeoricaM"))
    End Sub
    Protected Sub ValoresSegundosemestre(ByVal i As Integer, ByVal rw As DataRow, ByVal dias As Integer, ByVal idagru As Integer, ByVal fechaini As Date, ByVal fechafin As Date)
        Dim porcentaje_calculado2 As Decimal
        'calculamos los valores del informe de interriego
        caudal_m3s2 = ObtenerCaudal(idagru, dstInterriego.Tables("TablaContador").Rows(i).Item("codigoPVYCR"), fechaini, fechafin)
        If caudal_m3s2 = 0 Then
            porcentaje_calculado2 = 0
            volumenA2 = 0
        Else
            porcentaje_calculado2 = (dstInterriego.Tables("TablaContador").Rows(i).Item("DotacionTeoricaG2") + dstInterriego.Tables("TablaContador").Rows(i).Item("DotacionTeoricaM2")) / caudal_m3s2
            volumenA2 = caudal_m3s2 * porcentaje_calculado2 * 86400 * dias
        End If
        'If dstInterriego.Tables("TablaTotalCaudalP2").Rows.Count > 0 Then
        '    v_caudaltotal_m32 = String.Format("{0:#,##0.000}", dstInterriego.Tables("TablaTotalCaudalP2").Rows(0).Item("Caudaltotal_m32"))
        '    rw("caudalPorDias2") = String.Format("{0:#,##0.000}", dstInterriego.Tables("TablaTotalCaudalP2").Rows(0).Item("caudalPorDias2"))
        '    rw("caudalPorSeg2") = String.Format("{0:#,##0.000}", dstInterriego.Tables("TablaTotalCaudalP2").Rows(0).Item("caudalPorSeg2"))
        'Else
        '    v_caudaltotal_m32 = String.Format("{0:#,##0.000}", 0)
        '    rw("caudalPorDias2") = String.Format("{0:#,##0.000}", 0)
        '    rw("caudalPorSeg2") = String.Format("{0:#,##0.000}", 0)
        '    'rw("SumHas") = String.Format("{0:#,##0.0000}", 0)
        '    'rw("SumSuperficie") = String.Format("{0:#,##0.0000}", 0)
        'End If
        'rw("Caudaltotal_m32") = String.Format("{0:#,##0.000}", v_caudaltotal_m32)

        sumavolumenA2 = sumavolumenA2 + volumenA2
        rw("porcentaje_calculado2") = String.Format("{0:#,##0.00}", porcentaje_calculado2)
        rw("volumen_a2") = String.Format("{0:#,##0}", volumenA2)
        rw("caudal_m3s2") = String.Format("{0:#,##0.000}", caudal_m3s2)
        rw("DotacionTeoricaG2") = String.Format("{0:#,##0.0000}", dstInterriego.Tables("TablaContador").Rows(i).Item("DotacionTeoricaG2"))
        rw("DotacionTeoricaM2") = String.Format("{0:#,##0.0000}", dstInterriego.Tables("TablaContador").Rows(i).Item("DotacionTeoricaM2"))
    End Sub
    Protected Function ObtenerCaudal(ByVal idagru As Integer, ByVal codigoPVYCR As String, ByVal fechainicio1 As Date, ByVal fechafin1 As Date)
        Dim comando As SqlCommand = New SqlCommand("", conexion)
        Dim caudal As Decimal
        utiles.Comprobar_Conexion_BD(Page, conexion)

        'obtenemos el caudal para cada periodo de tiempo
        comando.CommandText = "SELECT isnull(SUM(a.CAUDAL_M3S),0) caudal_m3s " & _
            "FROM PVYCR_Cauces C, PVYCR_PUNTOS P, PVYCR_DATOSACEQUIAS A, PVYCR_agrupaciones G, PVYCR_AGRUPACIONES_ELEMENTOSMEDIDA GE " & _
            "where C.CODIGOCAUCE = P.CODIGOCAUCE AND P.CODIGOPVYCR = A.CODIGOPVYCR and G.idagrupacion = GE.idagrupacion and " & _
            "GE.codigoPVYCR = p.codigoPVYCR AND G.idagrupacion = " & idagru & " AND A.codigoPVYCR = '" & codigoPVYCR & "' and " & _
            "Fecha_Medida between '" & fechainicio1 & " 00:00:00' and '" & fechafin1 & " 23:59:59'"

        'daInterriego.SelectCommand.CommandText = sentenciaSel
        'daInterriego.Fill(dstInterriego, "TablaCaudal1")
        caudal = Convert.ToDecimal("0" & comando.ExecuteScalar)
        Return caudal
    End Function
    Protected Function ObtenerCteDotacion(ByVal fechaini As Date, ByVal fechafin As Date, ByVal idagru As Integer, ByVal dias As Integer)
        Dim comando As SqlCommand = New SqlCommand("", conexion)
        Dim cant As Decimal = 0
        Dim CteDotacion As Decimal = 0
        utiles.Comprobar_Conexion_BD(Page, conexion)
        Dim mesAnyoDesde As String = Month(fechaini).ToString & Year(fechaini).ToString
        Dim mesAnyoHasta As String = Month(fechafin).ToString & Year(fechafin).ToString
        'para obtener las ctes de la dotación seleccionamos de la tabla pvycr_aguaporAgruYMes
        comando.CommandText = "select sum(cantidad_m3s) cant from PVYCR_AguaPorAgruYMes where idAgru = " & idagru & _
        " and cast( mes as varchar(2))+cast(anyo as varchar(4)) >= '" & mesAnyoDesde & "' and cast( mes as varchar(2))+cast(anyo as varchar(4)) <= '" & mesAnyoHasta & "' "

        cant = Convert.ToDecimal("0" & comando.ExecuteScalar)
        CteDotacion = cant / dias
        Return CteDotacion

    End Function

End Class
