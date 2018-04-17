Imports System.ServiceModel
Imports System.ServiceModel.Activation
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.Linq
Imports System.Linq


<ServiceContract(Namespace:="")> _
<AspNetCompatibilityRequirements(RequirementsMode:=AspNetCompatibilityRequirementsMode.Allowed)> _
Public Class srvDatos
    Dim dc As New datosDataContext
    Dim porcentaje As Double = 0.01
    Dim conexion As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))

    <OperationContract()> _
    Public Sub DoWork()
        ' Add your operation implementation here
    End Sub

    ' Add more operations here and mark them with <OperationContract()>

    <OperationContract()> _
    Public Function listadoPuntos() As List(Of PVYCR_DatosAcequias_NIVUS)
        'Dim puntos As String = "OT001P01,OT005P01,VA002P30,VA006P01,VA013P01,VA015P01,VA016P01,VA055P01,VA056P01,VB042P01,VB057P01,VB071P01, VB075P01,VM011P01,VM013P01,VM022P01"
        Dim datos As List(Of PVYCR_DatosAcequias_NIVUS) = New List(Of PVYCR_DatosAcequias_NIVUS)

        'For Each punto In puntos.Split(",")
        '    Dim newDatos = From pvy In dc.PVYCR_DatosAcequias_NIVUS Where pvy.CodigoPVYCR = punto.Trim() Order By pvy.Hora Descending Select pvy

        '    'Dim newDatos = From N In dc.PVYCR_DatosAcequias_NIVUS _
        '    '                Join P In dc.TPuntos_PVY On N.CodigoPVYCR Equals P.CodigoPVYCR _
        '    '                Join C In dc.TCauces_PVY On P.CodigoCauce Equals C.CodigoCauce _
        '    '                Where N.CodigoPVYCR = punto.Trim() Order By N.Hora Descending _
        '    '                Select N.CodigoPVYCR, N.Calado_M, N.Caudal_M3S, N.Velocidad_MS, P.DenominacionPunto, C.DenominacionCauce

        '    If newDatos.Skip(0).Take(1).Count = 1 Then
        '        datos.Add(newDatos.Skip(0).Take(1).ToList()(0))
        '        'datos.Union(newDatos.Skip(0).Take(1))
        '    End If
        'Next

        Dim puntos = From pvy In dc.PVYCR_DatosAcequias_NIVUS Select pvy.CodigoPVYCR
        Dim nNumeros As Integer = puntos.Distinct().Count
        Dim lista As IQueryable(Of String) = puntos.Distinct()

        For Each pto As String In lista.ToList
            Dim newDatos = From pvy In dc.PVYCR_DatosAcequias_NIVUS Where pvy.CodigoPVYCR = pto.Trim() Order By pvy.Hora Descending Select pvy
            If newDatos.Skip(0).Take(1).Count = 1 Then
                datos.Add(newDatos.Skip(0).Take(1).ToList()(0))
                datos.Union(newDatos.Skip(0).Take(1))
            End If

        Next
        Return datos.ToList()


        ' '' '' ''Dim puntos As String = "OT001P01,OT005P01,VA002P30,VA006P01,VA013P01,VA015P01,VA016P01,VA055P01,VA056P01,VB042P01,VB057P01,VB071P01, VB075P01,VM011P01,VM013P01,VM022P01"
        ' '' '' ''Dim datos As List(Of PVYCR_DatosAcequias_NIVUS) = New List(Of PVYCR_DatosAcequias_NIVUS)
        ' '' '' ''For Each punto In puntos.Split(",")
        ' '' '' ''    Dim newDatos = From pvy In dc.PVYCR_DatosAcequias_NIVUS Where pvy.CodigoPVYCR = punto.Trim() Order By pvy.Hora Descending Select pvy

        ' '' '' ''    'Dim newDatos = From N In dc.PVYCR_DatosAcequias_NIVUS _
        ' '' '' ''    '                Join P In dc.TPuntos_PVY On N.CodigoPVYCR Equals P.CodigoPVYCR _
        ' '' '' ''    '                Join C In dc.TCauces_PVY On P.CodigoCauce Equals C.CodigoCauce _
        ' '' '' ''    '                Where N.CodigoPVYCR = punto.Trim() Order By N.Hora Descending _
        ' '' '' ''    '                Select N.CodigoPVYCR, N.Calado_M, N.Caudal_M3S, N.Velocidad_MS, P.DenominacionPunto, C.DenominacionCauce

        ' '' '' ''    If newDatos.Skip(0).Take(1).Count = 1 Then
        ' '' '' ''        datos.Add(newDatos.Skip(0).Take(1).ToList()(0))
        ' '' '' ''        'datos.Union(newDatos.Skip(0).Take(1))
        ' '' '' ''    End If
        ' '' '' ''Next
        ' '' '' ''Return datos.ToList()


    End Function

    <OperationContract()> _
    Public Function graficos(ByVal punto As String, ByVal fechaInicio As DateTime, ByVal fechaFin As DateTime) As List(Of PVYCR_DatosAcequias_NIVUS)
        If fechaFin.Year = 1 And fechaInicio.Year = 1 Then
            fechaFin = Now
            fechaInicio = Now.AddDays(-1)
        End If


        Dim datos = From N In dc.PVYCR_DatosAcequias_NIVUS _
                    Where (N.CodigoPVYCR = punto) And (N.Fecha >= fechaInicio And N.Fecha <= fechaFin) Order By N.Fecha _
                    Select N
        'Where (N.CodigoPVYCR = punto) And (N.Fecha >= fechaInicio And N.Fecha <= fechaFin) Order By N.Fecha Descending _
        Return datos.Skip(0).Take(5000).ToList
    End Function

    <OperationContract()> _
    Public Function graficoCaudal(ByVal punto As String, ByVal fechaInicio As DateTime, ByVal fechaFin As DateTime) As List(Of PVYCR_DatosAcequias_NIVUS)
        Dim datos = From N In dc.PVYCR_DatosAcequias_NIVUS _
                    Where (N.CodigoPVYCR = punto) And (N.Fecha >= fechaInicio Or N.Fecha <= fechaFin) Order By N.Fecha Descending _
                    Select N.Fecha, N.Caudal_M3S()

        Return datos.Skip(0).Take(3000)
    End Function

    <OperationContract()> _
    Public Function graficoVelocidad(ByVal punto As String, ByVal fechaInicio As DateTime, ByVal fechaFin As DateTime) As List(Of PVYCR_DatosAcequias_NIVUS)
        Dim datos = From N In dc.PVYCR_DatosAcequias_NIVUS _
                    Where (N.CodigoPVYCR = punto) And (N.Fecha >= fechaInicio Or N.Fecha <= fechaFin) Order By N.Fecha Descending _
                   Select N.Fecha, N.Velocidad_MS()

        Return datos.Skip(0).Take(3000)
    End Function

    Private Function readXML(ByVal xmlFile As String) As EnumerableRowCollection(Of System.Data.DataRow)
        'LINQ to DataSet
        Dim dts As New DataSet()
        ' Leemos los datos del documento XML y los
        ' volcamos al objeto de tipo DataSet
        dts.ReadXml(xmlFile)
        ' Declaramos una variable para obtener los datos del
        ' documento XML que cumplen la condiciSql programada

        Return dts.Tables(0).AsEnumerable

    End Function

    Private Function readXMLConsumos(ByVal xmlFile As String, ByVal nombreTabla As String) As EnumerableRowCollection(Of System.Data.DataRow)
        'LINQ to DataSet
        Dim dts As New DataSet()
        ' Leemos los datos del documento XML y los
        ' volcamos al objeto de tipo DataSet
        dts.ReadXml(xmlFile)
        ' Declaramos una variable para obtener los datos del
        ' documento XML que cumplen la condiciSql programada

        Return dts.Tables(nombreTabla).AsEnumerable

    End Function

    <OperationContract()> _
    Public Function lineaObtencionCaudal(ByVal xmlFile As String, ByVal tipoObtencionCaudal As String) As List(Of TablaAcequias)
        'Dim data As System.Data.EnumerableRowCollection(Of System.Data.DataRow) = readXML(xmlFile) 'dts.Tables(0).AsEnumerable()
        'Dim obtencionCaudal = From seleccion In data _
        '                                Where (seleccion.Field(Of String)("TOC").Equals(tipoObtencionCaudal)) _
        '                                Order By seleccion.Field(Of String)("FM") _
        '                                Select seleccion

        Dim dst As New DataSet
        dst.ReadXml(xmlFile)
        dst.Tables("T").DefaultView.RowFilter = "TOC='" & tipoObtencionCaudal & "'"
        dst.Tables("T").Columns.Add(New DataColumn("Caudal", Type.GetType("System.Double")))

        For i As Integer = 0 To dst.Tables("T").Rows.Count - 1
            dst.Tables("T").Rows(i).Item("Caudal") = dst.Tables("T").Rows(i).Item("C")
        Next

        dst.Tables("T").Columns.Remove("C")
        dst.Tables("T").Columns("Caudal").ColumnName = "C"  'Esto es para hacer la columna C del tipo doble.

        Dim dv As New DataView
        dv = dst.Tables("T").DefaultView

        Dim dstResultado As New DataSet
        dstResultado = dst.Clone

        marcarImprescindibles(dv, dstResultado)

        'Return rellenaDatos(obtencionCaudal).ToList
        Return rellenaDatos2(dstResultado).ToList
    End Function

    <OperationContract()> _
    Public Function lineaCodFuenteDato(ByVal xmlFile As String, ByVal CodFuenteDato As String) As List(Of TablaAcequias)
        'Dim data As System.Data.EnumerableRowCollection(Of System.Data.DataRow) = readXML(xmlFile)
        'Dim datos = From seleccion In data _
        '                Where seleccion.Field(Of String)("CFD").Equals(CodFuenteDato) _
        '                Select seleccion

        'Return rellenaDatos(datos).ToList

        Dim dst As New DataSet
        dst.ReadXml(xmlFile)
        dst.Tables("T").DefaultView.RowFilter = "CFD='" & CodFuenteDato & "'"
        dst.Tables("T").Columns.Add(New DataColumn("Caudal", Type.GetType("System.Double")))

        For i As Integer = 0 To dst.Tables("T").Rows.Count - 1
            dst.Tables("T").Rows(i).Item("Caudal") = dst.Tables("T").Rows(i).Item("C")
        Next

        dst.Tables("T").Columns.Remove("C")
        dst.Tables("T").Columns("Caudal").ColumnName = "C"  'Esto es para hacer la columna C del tipo doble.

        Dim dv As New DataView
        dv = dst.Tables("T").DefaultView

        Dim dstResultado As New DataSet
        dstResultado = dst.Clone

        marcarImprescindibles(dv, dstResultado)

        Return rellenaDatos2(dstResultado).ToList

    End Function

    <OperationContract()> _
    Public Function lineaRegimenCurva(ByVal xmlFile As String, ByVal RegimenCurva As String) As List(Of TablaAcequias)
        Dim dst As New DataSet
        dst.ReadXml(xmlFile)
        dst.Tables("T").DefaultView.RowFilter = "RC='" & RegimenCurva & "'"
        dst.Tables("T").Columns.Add(New DataColumn("Caudal", Type.GetType("System.Double")))

        For i As Integer = 0 To dst.Tables("T").Rows.Count - 1
            dst.Tables("T").Rows(i).Item("Caudal") = dst.Tables("T").Rows(i).Item("C")
        Next

        dst.Tables("T").Columns.Remove("C")
        dst.Tables("T").Columns("Caudal").ColumnName = "C"  'Esto es para hacer la columna C del tipo doble.

        Dim dv As New DataView
        dv = dst.Tables("T").DefaultView

        Dim dstResultado As New DataSet
        dstResultado = dst.Clone

        'marcarImprescindibles(dv, dstResultado)
        If dv.Count > 0 Then
            marcarImprescindibles2(dv, dstResultado)
        End If

        Return rellenaDatos2(dstResultado).ToList
    End Function

    Private Function rellenaDatos(ByVal dts As System.Data.EnumerableRowCollection(Of DataRow)) As List(Of TablaAcequias)
        Dim datos As List(Of TablaAcequias) = New List(Of TablaAcequias)

        For Each r As DataRow In dts
            Dim ta As New TablaAcequias()
            ta.Cod_Fuente_Dato = r("CFD")
            ta.Fecha_Medida = r("FM")
            ta.RegimenCurva = dbNullToStr(r("RC"))
            ta.TipoObtencionCaudal = r("TOC")
            ta.Caudal_M3S = dbNullToStr(r("C"))
            datos.Add(ta)
        Next
        Return datos
    End Function

    Private Function rellenaDatos2(ByVal dts As DataSet) As List(Of TablaAcequias)
        Dim datos As List(Of TablaAcequias) = New List(Of TablaAcequias)

        For Each r As DataRow In dts.Tables("T").Rows
            Dim ta As New TablaAcequias()
            If Not dts.Tables("T").Columns("CFD") Is Nothing Then
                ta.Cod_Fuente_Dato = r("CFD")
            End If
            ta.Fecha_Medida = r("FM")
            If Not dts.Tables("T").Columns("RC") Is Nothing Then
                ta.RegimenCurva = dbNullToStr(r("RC"))
            End If
            If Not dts.Tables("T").Columns("TOC") Is Nothing Then
                ta.TipoObtencionCaudal = r("TOC")
            End If
            ta.Caudal_M3S = dbNullToStr(r("C"))
            datos.Add(ta)
        Next
        Return datos
    End Function

    Private Function dbNullToStr(ByVal val As Object) As String
        If IsDBNull(val) Then
            Return ""
        Else
            Return val
        End If
    End Function

    <OperationContract()> _
        Public Function tiposDeVariables(ByVal xmlFile As String, ByVal tipoVariable As String) As List(Of String)
        Dim data As System.Data.EnumerableRowCollection(Of System.Data.DataRow) = readXML(xmlFile)

        Dim datos = From seleccion In data _
                        Select seleccion.Field(Of String)(tipoVariable)
     
        Dim datosDistinct = From d In datos Select datos.Distinct()
        Dim dt As List(Of String) = New List(Of String)
        dt.Add(tipoVariable)


        For Each r As String In datosDistinct.First()
            dt.Add(r)
        Next

        datosDistinct = Nothing
        datos = Nothing
        data = Nothing

        Return dt
    End Function

    '<OperationContract()> _
    '    Public Function pepitolospalotes(ByVal xmlFile As String, ByVal tipoVariable As String) As Dictionary(Of String, Integer)
    '    Dim data As System.Data.EnumerableRowCollection(Of System.Data.DataRow) = readXML(xmlFile)
    '    Dim tipoVariables = Me.tiposDeVariables(xmlFile, tipoVariable)
    '    Dim returValores As Dictionary(Of String, Integer) = New Dictionary(Of String, Integer)


    '    For Each tipo As String In tipoVariables
    '        Dim datos = From seleccion In data Where seleccion.Field(Of String)(tipoVariable) = tipo _
    '                    Select seleccion.Field(Of String)(tipoVariable)
    '        returValores.Add(tipo, datos.Count)
    '    Next
    '    Return returValores


    'End Function

    <OperationContract()> _
        Public Function ListaDeVariables(ByVal xmlFile As String, ByVal tipoVariable As String) As List(Of TiposVariables)
        Dim data As System.Data.EnumerableRowCollection(Of System.Data.DataRow) = readXML(xmlFile)
        If data(0).Table.Columns.Contains(tipoVariable) Then

            Dim tipoVariables = Me.tiposDeVariables(xmlFile, tipoVariable)
            Dim resultado As List(Of TiposVariables) = New List(Of TiposVariables)

            If Not tipoVariables Is Nothing Then
                For Each tipo As String In tipoVariables
                    If tipo <> tipoVariable Then
                        Dim ta As New TiposVariables
                        Dim datos = From seleccion In data Where seleccion.Field(Of String)(tipoVariable) = tipo _
                                    Select seleccion.Field(Of String)(tipoVariable)
                        ta.TipoVariable = tipoVariable
                        ta.Variable = tipo
                        ta.Valor = datos.Count
                        resultado.Add(ta)
                    End If
                Next
            End If

            Return resultado
        Else
            Dim resultado As List(Of TiposVariables) = New List(Of TiposVariables)
            Dim ta As New TiposVariables
            ta.TipoVariable = tipoVariable
            ta.Variable = Nothing
            ta.Valor = 0
            Return resultado
        End If

    End Function


    <OperationContract()> _
    Public Function lineaGrafico(ByVal xmlFile As String) As List(Of TablaListado)
        Dim data As System.Data.EnumerableRowCollection(Of System.Data.DataRow) = readXMLConsumos(xmlFile, "listado")
        
        Dim datos = From seleccion In data _
                    Select seleccion

        Dim dataFiltros As System.Data.EnumerableRowCollection(Of System.Data.DataRow) = readXMLConsumos(xmlFile, "LG")
        Dim datosFiltros = From seleccion In dataFiltros _
                           Select seleccion

        'Dim dataLecturas As System.Data.EnumerableRowCollection(Of System.Data.DataRow) = readXMLConsumos(xmlFile, "tablaAuxiliar")
        'Dim datosLecturas = From seleccion In dataLecturas _
        '                    Select seleccion

        'Return rellenaDatosConsumos(datos, datosFiltros.First, datosLecturas.Count, datosLecturas.First.Item("idElementoMedida")).ToList
        Return rellenaDatosConsumos(datos, datosFiltros.First).ToList

    End Function

    Private Function rellenaDatosConsumos(ByVal dts As System.Data.EnumerableRowCollection(Of DataRow), ByVal dts2 As System.Data.DataRow) As List(Of TablaListado)
        Dim datos As List(Of TablaListado) = New List(Of TablaListado)

        For Each r As DataRow In dts
            Dim ta As New TablaListado()
            ta.Fecha_Medida = r("FM")
            ta.Var1Titulo = dts2.Item("V1T")
            ta.Var1Valor = dbNullToStr(r("V1Va"))
            ta.Var2Titulo = dts2.Item("V2T")
            ta.Var2Valor = dbNullToStr(r("V2Va"))
            ta.Var3Titulo = dts2.Item("V3T")
            ta.Var3Valor = dbNullToStr(r("V3Va"))
            ta.Var4Titulo = dbNullToStr(dts2.Item("V4T"))
            ta.Var4Valor = dbNullToStr(r("V4Va"))
            ta.Var4Visible = dbNullToStr(dts2.Item("V4Vi"))
            ta.Var5Titulo = dbNullToStr(dts2.Item("V5T"))
            ta.Var5Valor = dbNullToStr(r("V5Va"))
            ta.Var5Visible = dbNullToStr(dts2.Item("V5Vi"))

            ta.Filtro1 = dbNullToStr(dts2.Item("Filtro1"))
            ta.numLecturas = dbNullToStr(dts2.Item("L"))
            ta.ElementoMedida = dbNullToStr(dts2.Item("Nodo"))

            Try
                ta.Filtro2 = dbNullToStr(dts2.Item("Filtro2"))
            Catch ex As Exception

            End Try

            datos.Add(ta)
        Next
        Return datos
    End Function

    Private Function marcarImprescindibles(ByVal dv As DataView, ByRef dstResultado As DataSet) As DataSet
        Dim DifFechaActual As Double
        Dim DifCaudalActual As Double
        dv.Sort = "C"

        Dim DifCaudalTotal As Double = dv(dv.Count - 1).Item("C") - dv(0).Item("C")
        dv.Sort = "FM"
        Dim DifFechaTotal As Double = CDate(dv(dv.Count - 1).Item("FM")).Ticks - CDate(dv(0).Item("FM")).Ticks

        Dim j As Integer = 1
        Dim ValorActual As Double = dv(0).Item("C")
        Dim FechaActual As DateTime = dv(0).Item("FM")


        dstResultado.Tables("T").Rows.Add()
        For Each columna In dstResultado.Tables("T").Columns
            dstResultado.Tables("T").Rows(0).Item(columna.ToString) = dv(0).Item(columna.ToString)
        Next
        Dim numfilas As Integer = dv.Count - 1


        Try
            For i As Integer = 1 To numfilas
                DifFechaActual = CDate(dv(i).Item("FM")).Ticks - FechaActual.Ticks
                DifCaudalActual = Math.Abs(dv(i).Item("C") - ValorActual)

                If Math.Max(DifFechaActual / DifFechaTotal, DifCaudalActual / DifCaudalTotal) > porcentaje Then
                    ValorActual = dv(i).Item("C")
                    FechaActual = dv(i).Item("FM")

                    dstResultado.Tables("T").Rows.Add(DirectCast(DirectCast(dv(i), System.Data.DataRowView).Row, System.Data.DataRow).ItemArray)
                    j = j + 1

                    ''Si pasa la primera regla, primero tendremos que ver si pasa la segunda regla antes de insertarlo.
                    'If PasaLaSegundaRegla(dv, i, dstResultado, DifFechaTotal, DifCaudalTotal) = True Then
                    '    dstResultado.Tables("T").Rows.Add(DirectCast(DirectCast(dv(i), System.Data.DataRowView).Row, System.Data.DataRow).ItemArray)
                    '    j = j + 1
                    'End If
                End If
            Next
        Catch ex As Exception
            Dim err As String
            err = ex.Message
        End Try

        Return dstResultado
    End Function

    Private Function PasaLaSegundaRegla(ByVal dv As DataView, ByVal i As Integer, ByVal dst As DataSet, ByVal DifFechaTotal As Double, ByVal DifCaudalTotal As Double) As Boolean
        Dim resultado As Boolean
        Dim numfilas As Integer = dv.Count
        Dim numfilasNuevo As Integer = dst.Tables(0).Rows.Count

        If numfilasNuevo <= 1 Or i >= numfilas - 2 Then
            resultado = True
        Else
            If (CDate(dv(i).Item("FM")).Ticks - CDate(dst.Tables(0).Rows(dst.Tables(0).Rows.Count - 2).Item("FM")).Ticks) / DifFechaTotal < porcentaje And _
            Math.Abs(dv(i + 1).Item("C") - dst.Tables(0).Rows(dst.Tables(0).Rows.Count - 1).Item("C")) / DifCaudalTotal < porcentaje Then
                resultado = False
            Else
                resultado = True
            End If
        End If

        Return resultado
    End Function

    Private Function marcarImprescindibles2(ByVal dv As DataView, ByRef dstResultado As DataSet) As DataSet
        Dim numReg = dv.Count
        dv.Sort = "FM"
        Dim DifFechaTotal As Double = CDate(dv(numReg - 1).Item("FM")).Ticks - CDate(dv(0).Item("FM")).Ticks

        dv.Sort = "C"
        Dim DifCaudalTotal As Double = dv(numReg - 1).Item("C") - dv(0).Item("C")

        dv.Sort = "TOC,CFD,RC,FM"
        Dim i, j As Integer
        Dim primerFiltro As New List(Of String), segundoFiltro As New List(Of String), tercerFiltro As New List(Of String)
        Dim TOCActual As String = "", CFDActual As String = "", RCActual As String = "", TOCAnterior As String = "", CFDAnterior As String = "", RCAnterior As String = ""

        For i = 0 To numReg - 1
            TOCActual = dv(i).Item("TOC")
            CFDActual = dv(i).Item("CFD")
            RCActual = dv(i).Item("RC")

            If primerFiltro.Contains(TOCActual) Then
                If segundoFiltro.Contains(TOCActual & "_" & CFDActual) Then
                    If tercerFiltro.Contains(TOCActual & "_" & CFDActual & "_" & RCActual) Then
                    Else
                        tercerFiltro.Add(TOCActual & "_" & CFDActual & "_" & RCActual)
                    End If
                Else
                    segundoFiltro.Add(TOCActual & "_" & CFDActual)
                    tercerFiltro.Add(TOCActual & "_" & CFDActual & "_" & RCActual)
                End If
            Else
                primerFiltro.Add(TOCActual)
                segundoFiltro.Add(TOCActual & "_" & CFDActual)
                tercerFiltro.Add(TOCActual & "_" & CFDActual & "_" & RCActual)
            End If


            If TOCActual & "_" & CFDActual & "_" & RCActual <> TOCAnterior & "_" & CFDAnterior & "_" & RCAnterior Then
                dstResultado.Tables(0).Rows.Add(DirectCast(DirectCast(dv(i), System.Data.DataRowView).Row, System.Data.DataRow).ItemArray)
                j = j + 1
            Else
                If SeGraba(dv(i), dstResultado.Tables(0).Rows(j - 1), DifFechaTotal, DifCaudalTotal) = True Then
                    dstResultado.Tables(0).Rows.Add(DirectCast(DirectCast(dv(i), System.Data.DataRowView).Row, System.Data.DataRow).ItemArray)
                    j = j + 1
                End If
            End If

            TOCAnterior = dv(i).Item("TOC")
            CFDAnterior = dv(i).Item("CFD")
            RCAnterior = dv(i).Item("RC")
        Next

        Return dstResultado
    End Function

    Private Function SeGraba(ByVal registroActual As DataRowView, ByVal registroAnterior As DataRow, ByVal DifFechaTotal As Double, ByVal DifCaudalTotal As Double) As Boolean
        Dim Resultado As Boolean
        Dim DifFechaActual As Double
        Dim DifCaudalActual As Double

        DifFechaActual = Math.Abs(CDate(registroActual.Item("FM")).Ticks - CDate(registroAnterior.Item("FM")).Ticks)
        DifCaudalActual = Math.Abs(registroActual.Item("C") - registroAnterior.Item("C"))

        If Math.Max(DifFechaActual / DifFechaTotal, DifCaudalActual / DifCaudalTotal) > porcentaje Then
            Resultado = True
        Else
            Resultado = False
        End If
        Return Resultado
    End Function

    <OperationContract()> _
    Public Function devuelvePunto(ByVal CodigoPVYCR As String) As String
        Dim resultado As String
        Dim comando As New Data.SqlClient.SqlCommand("", conexion)

        If conexion.State = ConnectionState.Closed Then conexion.Open()
        comando.CommandText = "SELECT CodigoPVYCR + ' - ' + DenominacionPunto as resultado FROM PVYCR_Puntos where CodigoPVYCR='" & _
                            CodigoPVYCR & "'"
        resultado = comando.ExecuteScalar

        Return resultado
    End Function

    <OperationContract()> _
    Public Function devuelveCauce(ByVal CodigoPVYCR As String) As String
        Dim resultado As String
        Dim comando As New Data.SqlClient.SqlCommand("", conexion)

        If conexion.State = ConnectionState.Closed Then conexion.Open()
        comando.CommandText = "SELECT CodigoCauce + ' - ' + DenominacionCauce as resultado FROM PVYCR_Cauces where " & _
                              "CodigoCauce=(SELECT CodigoCauce FROM PVYCR_Puntos WHERE CodigoPVYCR='" & CodigoPVYCR & "')"
        resultado = comando.ExecuteScalar

        Return resultado
    End Function
End Class
