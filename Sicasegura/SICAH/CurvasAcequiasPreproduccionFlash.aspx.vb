Imports OpenFlashChart
Imports System.Drawing
Imports System.Data
Imports GuarderiaFluvial
Imports GuarderiaFluvial.utiles

Partial Class SICAH_CurvasAcequiasPreproduccionFlash
    Inherits System.Web.UI.Page

    Dim sentenciaSel As String
    Dim selectConnection As New SqlClient.SqlConnection(ConfigurationSettings.AppSettings.Item("dsnsegura_migracion"))
    Dim da As New SqlClient.SqlDataAdapter("", selectConnection)
    Dim ds As DataSet = New System.Data.DataSet()
    Dim url As String

    Protected Function getUrlGrafico(ByVal campo As Integer) As String
        Select Case campo
            Case 1
                CalculaURL(1)
                url = url & "&grafico=TipoObtencionCaudal"
            Case 2
                CalculaURL(2)
                url = url & "&grafico=Cod_Fuente_Dato"
            Case 3
                CalculaURL(3)
                url = url & "&grafico=RegimenCurva"
        End Select

        Return Server.UrlEncode(url)
    End Function


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.CacheControl = "no-cache"
        If Not IsPostBack Then
            If Session("usuarioReg") = Nothing Then
                Response.Redirect("UsuarioNoRegistrado.aspx")
                Exit Sub
            End If
        End If
        utiles.Comprobar_Conexion_BD(Page, selectConnection)
        sentenciaSel = "SELECT P.DenominacionPunto, C.DenominacionCauce " & _
                   "FROM PVYCR_Cauces C,  PVYCR_Puntos P " & _
                   "WHERE C.CodigoCauce = P.CodigoCauce and P.CodigoPVYCR = '" & Session("CodigoPVYCR").ToString & "' "

        da.SelectCommand.CommandText = sentenciaSel
        da.Fill(ds, "TablaGrafico")

        If Not Session("TablaCaudalesDesdeAcequias") Is Nothing Then
            Array_datos(Session("CodigoPVYCR"), "Q01", 1)
        Else
            Array_datos(Session("CodigoPVYCR"), "Q01", 2)
        End If

        Session("Titulo") = Session("CodigoPVYCR").ToString & " / " + ds.Tables("TablaGrafico").Rows(0).Item("DenominacionPunto").ToString & " / " & vbCrLf & ds.Tables("TablaGrafico").Rows(0).Item("DenominacionCauce").ToString

    End Sub


    Private Sub CrearVariablesGráfico(ByVal campo As String)
        Dim dt As New DataTable
        Dim dstPreproduccion As New DataSet
        If Not Session("TablaCaudalesDesdeAcequias") Is Nothing Then
            dt = Session("TablaCaudalesDesdeAcequias")
        Else
            dstPreproduccion = Session("TablaCaudales")
            dt = dstPreproduccion.Tables("TablaCaudales")
        End If


        dt.DefaultView.RowFilter = ""
        dt.DefaultView.Sort = campo
        ds.Tables.Add(campo)
        ds.Tables(campo).Columns.Add(New DataColumn(campo, GetType(System.String)))

        Dim i As Integer = 0, j As Integer = 0
        Dim strTipo As String = ""
        Dim chk As CheckBox

        For i = 0 To dt.Rows.Count - 1
            If strTipo <> utiles.nullABlanco(dt.DefaultView(i).Item(campo)) Then
                strTipo = dt.DefaultView(i).Item(campo)
                With ds.Tables(campo)
                    .Rows.Add()
                    .Rows(j).Item(campo) = strTipo

                    chk = New CheckBox
                    chk.ID = "chk" & "#" & campo & "#" & i
                    chk.Text = strTipo
                    chk.AutoPostBack = True
                    chk.CausesValidation = True
                    chk.Checked = True

                    Select Case campo
                        Case "TipoObtencionCaudal"
                            'plh_TipoObtencionCaudal.Controls.Add(chk)
                        Case "TipoObtCaudal"
                            'plh_TipoObtencionCaudal.Controls.Add(chk)
                        Case "Cod_Fuente_Dato"
                            'plh_CodFuenteDato.Controls.Add(chk)
                        Case "RegimenCurva"
                            'plh_RegimenCurva.Controls.Add(chk)
                        Case "Curva"
                            'plh_RegimenCurva.Controls.Add(chk)
                    End Select

                    AddHandler chk.CheckedChanged, AddressOf chkClick
                End With
                j = j + 1
            End If
        Next
    End Sub

    Private Sub chkClick(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim chk As CheckBox = sender
        Dim tipo As String = chk.ID.Split("#")(1)
        Select Case tipo
            Case "TipoObtencionCaudal"
                CalculaURL(1)
            Case "TipoObtCaudal"
                CalculaURL(1)
            Case "Cod_Fuente_Dato"
                CalculaURL(2)
            Case "RegimenCurva"
                CalculaURL(3)
            Case "Curva"
                CalculaURL(3)
        End Select
    End Sub

    Private Function CalculaURL(ByVal campo As Integer) As String
        url = "CurvasAcequiasPreproduccionFlashData.aspx?"
        Dim item As CheckBox

        'Select Case campo
        '    Case 1
        '        For Each item In plh_TipoObtencionCaudal.Controls
        '            url += "&" & item.Text & "=" & item.Checked
        '        Next
        '    Case 2
        '        For Each item In plh_CodFuenteDato.Controls
        '            url += "&" & item.Text & "=" & item.Checked
        '        Next
        '    Case 3
        '        For Each item In plh_RegimenCurva.Controls
        '            url += "&" & item.Text & "=" & item.Checked
        '        Next
        'End Select

        Return url
    End Function





    Private Sub Array_datos(ByVal pvycr As String, ByVal EM As String, ByVal pagina As Integer)
        Dim i, ii As Int16
        Dim str, Sql As String
        Dim dt, dt2 As Data.DataTable


        Sql = "SELECT regimen, Cod_curva FROM dbo.PVYCR_CurvasAcequias "
        Sql = Sql + "WHERE CodigoPVYCR='" & Session("CodigoPVYCR").ToString & "' AND IdElementoMedida='Q01' AND Fecha_Fin_Uso >'" & Date.Now() & "'"
        Sql = Sql + "ORDER BY CodigoPVYCR, idElementoMedida, Regimen, Fecha_Inicio_Uso"

        dt = EjecutaSQL(Sql)
        str = genera_series(dt, 1)
        Dim strdef As String
        strdef = "<script>" & _
            "jQuery(function()" & _
                "{var chart2 = new Highcharts.Chart({" & _
                    "chart: { renderTo: 'chart1' }," & _
                    "navigator: { height: 60 }, " & _
                    "title: { text: 'Curvas Acequias por Tipo Obtención Caudal', floating: false, align: 'center', x: -20, top: 20 }, " & _
                    "yAxis: {title: {text:'Caudal (m3/s)'}}, " & _
                    "xAxis: { type:'double',title: {text:'Nivel (m)'}}," & _
                    "tooltip: {" & _
                        "formatter: function() {" & _
                                "return '<b>'+ this.series.name +'</b><br/>'+ " & _
                                "'Nivel:' + this.x +' (m)<br>Caudal '+ this.y +' (m3/s)'" & _
                                       "}" & _
                          "}," & _
                    " " & str & " });});</script>"
        LIT_grafico.Text = strdef
        str = genera_series(dt, 2)
        strdef = "<script>" & _
            "jQuery(function()" & _
                "{var chart2 = new Highcharts.Chart({" & _
                    "chart: { renderTo: 'chart2' }," & _
                    "navigator: { height: 60 }, " & _
                    "title: { text: 'Curvas Acequias por Curvas', floating: false, align: 'center', x: -20, top: 20 }, " & _
                    "yAxis: {title: {text:'Caudal (m3/s)'}}, " & _
                    "xAxis: { type:'double',title: {text:'Nivel (m)'}}," & _
                    "tooltip: {" & _
                        "formatter: function() {" & _
                                "return '<b>'+ this.series.name +'</b><br/>'+ " & _
                                "'Nivel:' + this.x +' (m)<br>Caudal '+ this.y +' (m3/s)'" & _
                                       "}" & _
                          "}," & _
                    " " & str & " });});</script>"
        LIT_grafico2.Text = strdef
        If (pagina = 1) Then
            str = genera_series(dt, 3)
            strdef = "<script>" & _
                "jQuery(function()" & _
                    "{var chart2 = new Highcharts.Chart({" & _
                        "chart: { renderTo: 'chart3' }," & _
                        "navigator: { height: 60 }, " & _
                        "title: { text: 'Curvas Acequias por Código Fuente Dato', floating: false, align: 'center', x: -20, top: 20 }, " & _
                        "yAxis: {title: {text:'Caudal (m3/s)'}}, " & _
                        "xAxis: { type:'double',title: {text:'Nivel (m)'}}," & _
                        "tooltip: {" & _
                            "formatter: function() {" & _
                                    "return '<b>'+ this.series.name +'</b><br/>'+ " & _
                                    "'Nivel:' + this.x +' (m)<br>Caudal '+ this.y +' (m3/s)'" & _
                                           "}" & _
                              "}," & _
                        " " & str & " });});</script>"
            LIT_grafico3.Text = strdef
        End If
    End Sub
    Protected Function EjecutaSQL(ByVal queryString As String) As Data.DataTable
        Dim connectionString As String
        connectionString = ConfigurationManager.ConnectionStrings("PDASIGVECTOR").ToString()
        Dim dtemp As Data.DataTable
        dtemp = New Data.DataTable()
        Dim connection As System.Data.SqlClient.SqlConnection
        connection = New System.Data.SqlClient.SqlConnection(connectionString)
        Dim adapter As System.Data.SqlClient.SqlDataAdapter
        adapter = New System.Data.SqlClient.SqlDataAdapter(queryString, connection)
        adapter.Fill(dtemp)
        Return dtemp
    End Function

    Private Function genera_series(ByVal dt As DataTable, ByVal tipo As Integer) As String
        Dim dtCaudales As New DataTable
        Dim dstPreproduccion As New DataSet
        Dim Str, Sql As String
        Dim dt2 As DataTable
        Dim i, pagina As Integer
        If Not Session("TablaCaudalesDesdeAcequias") Is Nothing Then
            dtCaudales = Session("TablaCaudalesDesdeAcequias")
            pagina = 1
        Else
            dstPreproduccion = Session("TablaCaudales")
            dtCaudales = dstPreproduccion.Tables("TablaCaudales")
            pagina = 2
        End If


        Str = ""
        Str = Str & "series: ["
        For i = 0 To dt.Rows.Count - 1
            If (i > 0) Then Str = Str & ","
            Str = Str & "{type: 'spline', name: '" & dt.Rows(i).Item(0).ToString() & "', marker: {enabled: false, states :{hover:{enabled: false}}}, data:["
            Sql = "select * from PVYCR_CurvasAcequias_valores where cod_curva like '" & dt.Rows(i).Item(1).ToString() & "'"
            dt2 = EjecutaSQL(Sql)
            For ii = 0 To dt2.Rows.Count - 1
                If (ii > 0) Then Str = Str & ","
                If (dt2.Rows(ii).Item(1) > 20) Then
                    Str = Str + "[1," & dt2.Rows(ii).Item(2).ToString().Replace(",", ".") & "]"
                Else
                    Str = Str + "[" & dt2.Rows(ii).Item(1).ToString().Replace(",", ".") & "," & dt2.Rows(ii).Item(2).ToString().Replace(",", ".") & "]"
                End If

            Next
            Str = Str & "]}"
        Next

        If (tipo = 1) Then 'TOC column 2
            Dim TOCS, nombrecol As String
            Dim col As Integer
            If (pagina = 1) Then
                col = 7
            ElseIf (pagina = 2) Then
                col = 2
            End If
            TOCS = ""
            For i = 0 To dtCaudales.Rows.Count - 1
                If (TOCS.Contains(dtCaudales.Rows(i).Item(col).ToString()) = False) Then
                    TOCS = TOCS & dtCaudales.Rows(i).Item(col).ToString() & "#"
                End If
            Next
            TOCS = TOCS.Replace("#E#", "#") '' Hay que evitar los datos obtenidos por estimacion
            Dim toc As String()
            toc = TOCS.Split("#")
            If (pagina = 1) Then
                nombrecol = "TipoObtencionCaudal"
            ElseIf (pagina = 2) Then
                nombrecol = "TipoObtCaudal"
            End If
            For i = 0 To toc.Length - 2
                Dim rows As DataRow()
                rows = dtCaudales.Select(nombrecol & " like '" & toc(i) & "'")
                Dim row As DataRow
                Str = Str & ",{type: 'scatter', name: '" & toc(i) & "', data:["
                For Each row In rows
                    Dim nivel, caudal As Integer
                    If (pagina = 1) Then
                        If (row.Item(4).ToString <> "") Then
                            nivel = 4
                        Else
                            nivel = 5
                        End If
                        caudal = 10
                    ElseIf (pagina = 2) Then
                        nivel = 5
                        caudal = 6
                    End If

                    Str = Str + "[" & row.Item(nivel).ToString().Replace(",", ".") & "," & row.Item(caudal).ToString().Replace(",", ".") & "],"
                Next
                Str = Str.Substring(0, Str.Length - 1)
                Str = Str & "]}"
            Next

        ElseIf (tipo = 2) Then 'CURVA column 3
            Dim CVS, nombrecol As String
            Dim col As Integer
            If (pagina = 1) Then
                col = 8
            ElseIf (pagina = 2) Then
                col = 3
            End If
            CVS = ""
            For i = 0 To dtCaudales.Rows.Count - 1
                If (CVS.Contains(dtCaudales.Rows(i).Item(col).ToString()) = False) Then
                    CVS = CVS & dtCaudales.Rows(i).Item(col).ToString() & "#"
                End If
            Next
            Dim cv As String()
            cv = CVS.Split("#")
            If (pagina = 1) Then
                nombrecol = "RegimenCurva"
            ElseIf (pagina = 2) Then
                nombrecol = "Curva"
            End If
            For i = 0 To cv.Length - 2
                Dim rows As DataRow()
                rows = dtCaudales.Select(nombrecol & " like '" & cv(i) & "'")
                Dim row As DataRow
                Str = Str & ",{type: 'scatter', name: '" & cv(i) & "', data:["
                For Each row In rows
                    Dim nivel, caudal As Integer
                    If (pagina = 1) Then
                        If (row.Item(4).ToString <> "") Then
                            nivel = 4
                        Else
                            nivel = 5
                        End If
                        caudal = 10
                    ElseIf (pagina = 2) Then
                        nivel = 5
                        caudal = 6
                    End If
                    '' Hay que evitar los datos obtenidos por estimacion
                    If (pagina = 1) Then
                        If (row.Item("TipoObtencionCaudal").ToString() <> "E") Then
                            Str = Str + "[" & row.Item(nivel).ToString().Replace(",", ".") & "," & row.Item(caudal).ToString().Replace(",", ".") & "],"
                        End If
                    Else
                        Str = Str + "[" & row.Item(nivel).ToString().Replace(",", ".") & "," & row.Item(caudal).ToString().Replace(",", ".") & "],"
                    End If

                Next
                Str = Str.Substring(0, Str.Length - 1)
                Str = Str & "]}"
            Next

        ElseIf (tipo = 3) Then 'CFD column 2
            Dim CFD, nombrecol As String
            Dim col As Integer
            If (pagina = 1) Then
                col = 2
            ElseIf (pagina = 2) Then

            End If
            CFD = ""
            For i = 0 To dtCaudales.Rows.Count - 1
                If (CFD.Contains(dtCaudales.Rows(i).Item(col).ToString()) = False) Then
                    CFD = CFD & dtCaudales.Rows(i).Item(col).ToString() & "#"
                End If
            Next
            Dim cfds As String()
            cfds = CFD.Split("#")
            If (pagina = 1) Then
                nombrecol = "Cod_fuente_dato"
            ElseIf (pagina = 2) Then

            End If
            For i = 0 To cfds.Length - 2
                Dim rows As DataRow()
                rows = dtCaudales.Select("Cod_fuente_dato like '" & cfds(i) & "'")
                Dim row As DataRow
                Str = Str & ",{type: 'scatter', name: '" & cfds(i) & "', data:["
                For Each row In rows
                    Dim nivel, caudal As Integer
                    If (pagina = 1) Then
                        If (row.Item(4).ToString <> "") Then
                            nivel = 4
                        Else
                            nivel = 5
                        End If
                        caudal = 10
                    ElseIf (pagina = 2) Then
                        nivel = 5
                        caudal = 6
                    End If
                    '' Hay que evitar los datos obtenidos por estimacion
                    If (pagina = 1) And (row.Item("TipoObtencionCaudal").ToString() <> "E") Then
                        Str = Str + "[" & row.Item(nivel).ToString().Replace(",", ".") & "," & row.Item(caudal).ToString().Replace(",", ".") & "],"
                    End If

                Next
                Str = Str.Substring(0, Str.Length - 1)
                Str = Str & "]}"
            Next


        End If


        Str = Str & "]"
        genera_series = Str
    End Function





End Class
