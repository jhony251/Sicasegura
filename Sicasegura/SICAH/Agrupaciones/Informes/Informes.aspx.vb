Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports GuarderiaFluvial.JavaScript


Imports NineRays.Reporting.Wizards
Imports NineRays.Reporting.DOM
Imports NineRays.Basics.Drawing

Partial Class Informes
    Inherits System.Web.UI.Page
    Private components As System.ComponentModel.IContainer
    Protected WithEvents reportGenerator1 As NineRays.Reporting.Components.ReportGenerator

    Dim conexion As SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("PDASIGVECTOR").ConnectionString.ToString())
    'Dim conexion_pruebas As SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsn_pruebas"))
    Dim conexion_dbsica As SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("DBSICA").ConnectionString.ToString())
    Dim conexion_ld As SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("RAACS").ConnectionString.ToString())
    Dim da As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    'Dim da_pruebas As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion_pruebas)
    Dim da_dbsica As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion_dbsica)
    Dim da_ld As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion_ld)
    Dim dst As DataSet = New System.Data.DataSet()
    Dim informe As String = ""
    Dim anyo As Integer
    Dim AccesoADatos As New Sicadll.AccesoADatos.AccesoADatos2


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.components = New System.ComponentModel.Container
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(Informes))
        Me.reportGenerator1 = New NineRays.Reporting.Components.ReportGenerator(Me.components)
        crearDataSets()
        CType(dst, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.reportGenerator1.DataSources = New NineRays.Reporting.Components.ObjectPointerCollection(New String() {"DataSet"}, New Object() {Me.dst})
        CType(dst, System.ComponentModel.ISupportInitialize).EndInit()

        'Depende del tipo de captación que sea se muestra un informe u otro. 
        If informe <> "" Then
            reportGenerator1.LoadTemplate(Server.MapPath(informe & ".rst"))

            'Añadimos el volumen
            Dim volumenTotal As Double
            Dim numLecturas As Integer

            If dst.Tables.Contains("Datos") Then
                If informe.StartsWith("Capt2") Then
                    numLecturas = dst.Tables("Datos").Rows.Count
                    For i As Integer = 0 To numLecturas - 1
                        With dst.Tables("Datos").Rows(i)
                            volumenTotal += nullACero(.Item("diferencia"))
                        End With
                    Next
                End If

                If informe.StartsWith("Capt3") Then
                    For j As Integer = 1 To 2
                        numLecturas = dst.Tables("Datos" & j).Rows.Count
                        For i As Integer = 0 To numLecturas - 1
                            With dst.Tables("Datos" & j).Rows(i)
                                volumenTotal += nullACero(.Item("diferencia"))
                            End With
                        Next
                    Next
                End If
                If informe.StartsWith("Capt4") Then
                    Dim volumenMes As Double = 0
                    For j As Integer = 1 To 12
                        numLecturas = dst.Tables("Datos" & j).Rows.Count
                        volumenMes = 0
                        For i As Integer = 0 To numLecturas - 1
                            With dst.Tables("Datos" & j).Rows(i)
                                volumenTotal += nullACero(.Item("diferencia"))
                                volumenMes += nullACero(.Item("diferencia"))
                            End With
                        Next
                        numLecturas = dst.Tables("Datos" & j & "_2").Rows.Count
                        dst.Tables("Datos" & j & "_2").Columns.Add(New DataColumn("volumenMes", Type.GetType("System.Double")))
                        dst.Tables("Datos" & j & "_2").Columns.Add(New DataColumn("volumenAnyo", Type.GetType("System.Double")))
                        For i As Integer = 0 To numLecturas - 1
                            With dst.Tables("Datos" & j & "_2").Rows(i)
                                volumenTotal += nullACero(.Item("diferencia"))
                                volumenMes += nullACero(.Item("diferencia"))
                                .Item("volumenMes") = volumenMes
                                .Item("volumenAnyo") = volumenTotal
                            End With
                        Next
                    Next
                End If
            End If

            Select Case informe
                Case "Capt2_lamina"
                    CType(reportGenerator1.Template.Pages(3).ControlByName("textBox141"), NineRays.Reporting.DOM.TextBox).Text = "AÑO " & anyo
                    CType(reportGenerator1.Template.Pages(3).ControlByName("textBox159"), NineRays.Reporting.DOM.TextBox).Text = volumenTotal
                Case "Capt2_tuberia"
                    CType(reportGenerator1.Template.Pages(3).ControlByName("textBox117"), NineRays.Reporting.DOM.TextBox).Text = "AÑO " & anyo
                    CType(reportGenerator1.Template.Pages(3).ControlByName("textBox119"), NineRays.Reporting.DOM.TextBox).Text = volumenTotal
                Case "Capt3_lamina"
                    CType(reportGenerator1.Template.Pages(3).ControlByName("textBox256"), NineRays.Reporting.DOM.TextBox).Text = "AÑO " & anyo
                    CType(reportGenerator1.Template.Pages(4).ControlByName("textBox295"), NineRays.Reporting.DOM.TextBox).Text = "AÑO " & anyo
                    CType(reportGenerator1.Template.Pages(4).ControlByName("textBox301"), NineRays.Reporting.DOM.TextBox).Text = volumenTotal
                Case "Capt3_tuberia"
                    CType(reportGenerator1.Template.Pages(3).ControlByName("textBox179"), NineRays.Reporting.DOM.TextBox).Text = "AÑO " & anyo
                    CType(reportGenerator1.Template.Pages(4).ControlByName("textBox217"), NineRays.Reporting.DOM.TextBox).Text = "AÑO " & anyo
                    CType(reportGenerator1.Template.Pages(4).ControlByName("textBox236"), NineRays.Reporting.DOM.TextBox).Text = volumenTotal
                Case "Capt4_tuberia"
                    CType(reportGenerator1.Template.Pages(3).ControlByName("textBox335"), NineRays.Reporting.DOM.TextBox).Text = "MES/AÑO (01/" & anyo & ") "
                    CType(reportGenerator1.Template.Pages(4).ControlByName("textBox352"), NineRays.Reporting.DOM.TextBox).Text = "MES/AÑO (01/" & anyo & ") "
                    CType(reportGenerator1.Template.Pages(5).ControlByName("textBox404"), NineRays.Reporting.DOM.TextBox).Text = "MES/AÑO (02/" & anyo & ") "
                    CType(reportGenerator1.Template.Pages(6).ControlByName("textBox438"), NineRays.Reporting.DOM.TextBox).Text = "MES/AÑO (02/" & anyo & ") "
                    CType(reportGenerator1.Template.Pages(7).ControlByName("textBox473"), NineRays.Reporting.DOM.TextBox).Text = "MES/AÑO (03/" & anyo & ") "
                    CType(reportGenerator1.Template.Pages(8).ControlByName("textBox507"), NineRays.Reporting.DOM.TextBox).Text = "MES/AÑO (03/" & anyo & ") "
                    CType(reportGenerator1.Template.Pages(9).ControlByName("textBox542"), NineRays.Reporting.DOM.TextBox).Text = "MES/AÑO (04/" & anyo & ") "
                    CType(reportGenerator1.Template.Pages(10).ControlByName("textBox576"), NineRays.Reporting.DOM.TextBox).Text = "MES/AÑO (04/" & anyo & ") "
                    CType(reportGenerator1.Template.Pages(11).ControlByName("textBox611"), NineRays.Reporting.DOM.TextBox).Text = "MES/AÑO (05/" & anyo & ") "
                    CType(reportGenerator1.Template.Pages(12).ControlByName("textBox645"), NineRays.Reporting.DOM.TextBox).Text = "MES/AÑO (05/" & anyo & ") "
                    CType(reportGenerator1.Template.Pages(13).ControlByName("textBox680"), NineRays.Reporting.DOM.TextBox).Text = "MES/AÑO (06/" & anyo & ") "
                    CType(reportGenerator1.Template.Pages(14).ControlByName("textBox714"), NineRays.Reporting.DOM.TextBox).Text = "MES/AÑO (06/" & anyo & ") "
                    CType(reportGenerator1.Template.Pages(15).ControlByName("textBox749"), NineRays.Reporting.DOM.TextBox).Text = "MES/AÑO (07/" & anyo & ") "
                    CType(reportGenerator1.Template.Pages(16).ControlByName("textBox783"), NineRays.Reporting.DOM.TextBox).Text = "MES/AÑO (07/" & anyo & ") "
                    CType(reportGenerator1.Template.Pages(17).ControlByName("textBox818"), NineRays.Reporting.DOM.TextBox).Text = "MES/AÑO (08/" & anyo & ") "
                    CType(reportGenerator1.Template.Pages(18).ControlByName("textBox852"), NineRays.Reporting.DOM.TextBox).Text = "MES/AÑO (08/" & anyo & ") "
                    CType(reportGenerator1.Template.Pages(19).ControlByName("textBox887"), NineRays.Reporting.DOM.TextBox).Text = "MES/AÑO (09/" & anyo & ") "
                    CType(reportGenerator1.Template.Pages(20).ControlByName("textBox921"), NineRays.Reporting.DOM.TextBox).Text = "MES/AÑO (09/" & anyo & ") "
                    CType(reportGenerator1.Template.Pages(21).ControlByName("textBox956"), NineRays.Reporting.DOM.TextBox).Text = "MES/AÑO (10/" & anyo & ") "
                    CType(reportGenerator1.Template.Pages(22).ControlByName("textBox990"), NineRays.Reporting.DOM.TextBox).Text = "MES/AÑO (10/" & anyo & ") "
                    CType(reportGenerator1.Template.Pages(23).ControlByName("textBox1025"), NineRays.Reporting.DOM.TextBox).Text = "MES/AÑO (11/" & anyo & ") "
                    CType(reportGenerator1.Template.Pages(24).ControlByName("textBox1059"), NineRays.Reporting.DOM.TextBox).Text = "MES/AÑO (11/" & anyo & ") "
                    CType(reportGenerator1.Template.Pages(25).ControlByName("textBox1094"), NineRays.Reporting.DOM.TextBox).Text = "MES/AÑO (12/" & anyo & ") "
                    CType(reportGenerator1.Template.Pages(26).ControlByName("textBox1128"), NineRays.Reporting.DOM.TextBox).Text = "MES/AÑO (12/" & anyo & ") "
                Case "Capt4_lamina"
                    CType(reportGenerator1.Template.Pages(3).ControlByName("textBox1165"), NineRays.Reporting.DOM.TextBox).Text = "MES/AÑO (01/" & anyo & ") "
                    CType(reportGenerator1.Template.Pages(4).ControlByName("textBox1200"), NineRays.Reporting.DOM.TextBox).Text = "MES/AÑO (01/" & anyo & ") "
                    CType(reportGenerator1.Template.Pages(5).ControlByName("textBox1236"), NineRays.Reporting.DOM.TextBox).Text = "MES/AÑO (02/" & anyo & ") "
                    CType(reportGenerator1.Template.Pages(6).ControlByName("textBox1272"), NineRays.Reporting.DOM.TextBox).Text = "MES/AÑO (02/" & anyo & ") "
                    CType(reportGenerator1.Template.Pages(7).ControlByName("textBox1308"), NineRays.Reporting.DOM.TextBox).Text = "MES/AÑO (03/" & anyo & ") "
                    CType(reportGenerator1.Template.Pages(8).ControlByName("textBox1344"), NineRays.Reporting.DOM.TextBox).Text = "MES/AÑO (03/" & anyo & ") "
                    CType(reportGenerator1.Template.Pages(9).ControlByName("textBox1380"), NineRays.Reporting.DOM.TextBox).Text = "MES/AÑO (04/" & anyo & ") "
                    CType(reportGenerator1.Template.Pages(10).ControlByName("textBox1416"), NineRays.Reporting.DOM.TextBox).Text = "MES/AÑO (04/" & anyo & ") "
                    CType(reportGenerator1.Template.Pages(11).ControlByName("textBox1452"), NineRays.Reporting.DOM.TextBox).Text = "MES/AÑO (05/" & anyo & ") "
                    CType(reportGenerator1.Template.Pages(12).ControlByName("textBox1488"), NineRays.Reporting.DOM.TextBox).Text = "MES/AÑO (05/" & anyo & ") "
                    CType(reportGenerator1.Template.Pages(13).ControlByName("textBox1524"), NineRays.Reporting.DOM.TextBox).Text = "MES/AÑO (06/" & anyo & ") "
                    CType(reportGenerator1.Template.Pages(14).ControlByName("textBox1560"), NineRays.Reporting.DOM.TextBox).Text = "MES/AÑO (06/" & anyo & ") "
                    CType(reportGenerator1.Template.Pages(15).ControlByName("textBox1596"), NineRays.Reporting.DOM.TextBox).Text = "MES/AÑO (07/" & anyo & ") "
                    CType(reportGenerator1.Template.Pages(16).ControlByName("textBox1632"), NineRays.Reporting.DOM.TextBox).Text = "MES/AÑO (07/" & anyo & ") "
                    CType(reportGenerator1.Template.Pages(17).ControlByName("textBox1668"), NineRays.Reporting.DOM.TextBox).Text = "MES/AÑO (08/" & anyo & ") "
                    CType(reportGenerator1.Template.Pages(18).ControlByName("textBox1704"), NineRays.Reporting.DOM.TextBox).Text = "MES/AÑO (08/" & anyo & ") "
                    CType(reportGenerator1.Template.Pages(19).ControlByName("textBox1740"), NineRays.Reporting.DOM.TextBox).Text = "MES/AÑO (09/" & anyo & ") "
                    CType(reportGenerator1.Template.Pages(20).ControlByName("textBox1776"), NineRays.Reporting.DOM.TextBox).Text = "MES/AÑO (09/" & anyo & ") "
                    CType(reportGenerator1.Template.Pages(21).ControlByName("textBox1812"), NineRays.Reporting.DOM.TextBox).Text = "MES/AÑO (10/" & anyo & ") "
                    CType(reportGenerator1.Template.Pages(22).ControlByName("textBox1848"), NineRays.Reporting.DOM.TextBox).Text = "MES/AÑO (10/" & anyo & ") "
                    CType(reportGenerator1.Template.Pages(23).ControlByName("textBox1884"), NineRays.Reporting.DOM.TextBox).Text = "MES/AÑO (11/" & anyo & ") "
                    CType(reportGenerator1.Template.Pages(24).ControlByName("textBox1920"), NineRays.Reporting.DOM.TextBox).Text = "MES/AÑO (11/" & anyo & ") "
                    CType(reportGenerator1.Template.Pages(25).ControlByName("textBox1956"), NineRays.Reporting.DOM.TextBox).Text = "MES/AÑO (12/" & anyo & ") "
                    CType(reportGenerator1.Template.Pages(26).ControlByName("textBox1992"), NineRays.Reporting.DOM.TextBox).Text = "MES/AÑO (12/" & anyo & ") "
            End Select
        End If

        Dim a As NineRays.Reporting.RegisterKeyAttribute = New NineRays.Reporting.RegisterKeyAttribute("4761628416024064132096")
        SharpShooterWebViewer1.Source = reportGenerator1
    End Sub
    Shared Function nullACero(ByVal valor As Object)
        'Esta función devuelve la cadena vacía en caso de que el parámetro pasado sea nulo, para evitar errores por conversión de tipos
        If valor Is System.DBNull.Value Then
            Return 0
        Else
            Return valor
        End If
    End Function
    Shared Function nullABlanco(ByVal valor As Object)
        'Esta función devuelve la cadena vacía en caso de que el parámetro pasado sea nulo, para evitar errores por conversión de tipos
        If valor Is System.DBNull.Value Then
            Return ""
        Else
            Return valor
        End If
    End Function
    Private Sub crearDataSets()
        Dim comando_dbsica As New SqlCommand("", conexion_dbsica)
        Dim NumInscripcion As Integer
        If Request.QueryString("SCV").ToString() = "SI" Then
            NumInscripcion = Request.QueryString("Numpal").ToString()
        Else
            comando_dbsica.CommandText = "Select s.NumInscripcion from sica_sist_sistemas S inner join [sica_sist_sistemas-Usuarios] SU on S.id=SU.id_sistema and " & _
                        "id_usuario=" & Session("id_usuario")
            If conexion_dbsica.State = ConnectionState.Closed Then conexion_dbsica.Open()
            NumInscripcion = comando_dbsica.ExecuteScalar
            conexion_dbsica.Close()
        End If
        
        da_ld.SelectCommand.CommandText = "select * from AprovechamientosInscritos where NUMPAL=" & NumInscripcion
        da_ld.Fill(dst, "TablaAprovechamientos")
        da_ld.SelectCommand.CommandText = "select * from TitularesAprovechamientos where NNUMREG1=" & NumInscripcion
        da_ld.Fill(dst, "TablaTitulares")

        'Teniendo en cuenta el caudal máximo, es una captación u otra (i.e un informe u otro)
        If dst.Tables("TablaAprovechamientos").Rows.Count > 0 Then
            If dst.Tables("TablaAprovechamientos").Rows(0).Item("caudal1") < 4 Then
                informe = "Capt1_"
            ElseIf dst.Tables("TablaAprovechamientos").Rows(0).Item("caudal1") >= 4 And dst.Tables("TablaAprovechamientos").Rows(0).Item("caudal1") < 100 Then
                informe = "Capt2_"
            ElseIf dst.Tables("TablaAprovechamientos").Rows(0).Item("caudal1") >= 100 And dst.Tables("TablaAprovechamientos").Rows(0).Item("caudal1") < 300 Then
                informe = "Capt3_"
            ElseIf dst.Tables("TablaAprovechamientos").Rows(0).Item("caudal1") >= 300 Then
                informe = "Capt4_"
            Else    'null
                informe = ""
            End If
        End If

        'Teniendo en cuenta la sec el informe será de tubería o de lámina libre
        If informe <> "" Then
            If dst.Tables("TablaAprovechamientos").Rows.Count > 0 Then
                Select Case dst.Tables("TablaAprovechamientos").Rows(0).Item("sec")
                    Case "A", "B"
                        informe &= "lamina"
                    Case "K", "C"
                        informe &= "tuberia"
                    Case Else
                        informe = ""
                End Select
            End If
        End If
        'informe = "Capt4_lamina"    '*********** PROVISIONAL PARA PRUEBAS

        dst.Tables("TablaAprovechamientos").Columns.Add(New DataColumn("seccion", Type.GetType("System.String")))
        For i As Integer = 0 To dst.Tables("TablaAprovechamientos").Rows.Count - 1
            With dst.Tables("TablaAprovechamientos").Rows(i)
                .Item("seccion") = .Item("sec") & " - " & .Item("tomo") & " - " & .Item("hoja")
            End With
        Next

        dst.Tables("TablaTitulares").Columns.Add(New DataColumn("NombreYApellidos", Type.GetType("System.String")))
        For i As Integer = 0 To dst.Tables("TablaTitulares").Rows.Count - 1
            With dst.Tables("TablaTitulares").Rows(i)
                .Item("NombreYApellidos") = nullABlanco(.Item("nombre")) & " " & nullABlanco(.Item("apellido"))
            End With
        Next

        da_dbsica.SelectCommand.CommandText = "SELECT P.ID, P.CodigoPVYCR, P.EM, P.CFD, P.TOC, P.FI, P.FF " & _
                                                "FROM [SICA_SIST_Sistemas-Punto] AS SP INNER JOIN " & _
                                                "SICA_SIST_PuntoSistema AS P ON P.ID = SP.ID_PuntoSistema INNER JOIN " & _
                                                "SICA_SIST_Sistemas ON SP.ID_Sistema = SICA_SIST_Sistemas.ID " & _
                                                "WHERE (SICA_SIST_Sistemas.NumInscripcion = " & NumInscripcion & ") "




        'da_dbsica.SelectCommand.CommandText = "Select p.* from sica_sist_sistemas S " & _
        '        " inner join [sica_sist_sistemas-Usuarios] SU on S.id=SU.id_sistema and id_usuario= " & Session("id_usuario") & _
        '        " inner join [SICA_SIST_Sistemas-Punto] SP on SP.ID_Sistema=s.ID " & _
        '        " inner join SICA_SIST_PuntoSistema P on p.ID=SP.ID_PuntoSistema "

        'Esto lo pongo por si acaso, pero se supone que los de tubería a presión serán H,E y V y los de lámina libre Q.
        If informe.EndsWith("tuberia") Then
            da_dbsica.SelectCommand.CommandText &= " AND left(EM,1)<>'Q'"
        Else
            da_dbsica.SelectCommand.CommandText &= " AND left(EM,1)='Q'"
        End If


        da_dbsica.Fill(dst, "TablaPuntos")

        Dim strSql As String = ""
        'For i As Integer = 0 To dst.Tables("TablaPuntos").Rows.Count - 1
        '    With dst.Tables("TablaPuntos").Rows(i)
        '        strSql = AccesoADatos.SacarLecturasPreYPro(.Item("em"), " WHERE CodigoPVYCR='" & .Item("codigopvycr") & "' AND idElementoMedida='" & .Item("em") & "' and cod_fuente_dato='19' ", _
        '             "ORDER BY Fecha_Medida DESC", conexion_pruebas)(1)
        '        da_pruebas.SelectCommand.CommandText = strSql
        '        da_pruebas.Fill(dst, "Datos")
        '    End With
        'Next

        'Lo dejo preparado para varios puntos y varios elementos de medida, pero en un principio sólo habrá un punto y un elemento de medida. 
        If dst.Tables("TablaPuntos").Rows.Count > 0 Then


            With dst.Tables("TablaPuntos").Rows(0)
                Select Case Left(informe, 6)
                    Case "Capt1_"
                        strSql = AccesoADatos.SacarLecturasPreYPro(.Item("em"), " WHERE CodigoPVYCR='" & .Item("codigopvycr") & "' AND idElementoMedida='" & .Item("em").ToString().Trim() & "' and (cod_fuente_dato='10' OR cod_fuente_dato='05') ", _
                            "ORDER BY Fecha_Medida", conexion)(1)
                    Case Else
                        If (Today.Month < 10) Then
                            strSql = AccesoADatos.SacarLecturasPreYPro(.Item("em"), " WHERE CodigoPVYCR='" & .Item("codigopvycr") & "' AND idElementoMedida='" & .Item("em").ToString().Trim() & "' and (cod_fuente_dato='10' OR cod_fuente_dato='05') AND fecha_medida between '01/10/" & Today.Year - 1 & "' AND '01/10/" & Today.Year & "' ", _
                            "ORDER BY Fecha_Medida", conexion)(1)
                        Else
                            strSql = AccesoADatos.SacarLecturasPreYPro(.Item("em"), " WHERE CodigoPVYCR='" & .Item("codigopvycr") & "' AND idElementoMedida='" & .Item("em").ToString().Trim() & "' and (cod_fuente_dato='10' OR cod_fuente_dato='05') AND fecha_medida between '01/10/" & Today.Year & "' AND '01/10/" & Today.Year + 1 & "' ", _
                            "ORDER BY Fecha_Medida", conexion)(1)
                        End If
                        
                End Select
                strSql = strSql.Replace("UNION", " UNION")
                da.SelectCommand.CommandText = strSql
                da.Fill(dst, "Datos")
            End With
            dst.Tables("Datos").Columns.Add(New DataColumn("fecha_medidaS", Type.GetType("System.String")))
            dst.Tables("Datos").Columns.Add(New DataColumn("usuario", Type.GetType("System.String")))
            Dim numLecturas As Integer = dst.Tables("Datos").Rows.Count - 1

            If informe.EndsWith("tuberia") Then
                dst.Tables("Datos").Columns.Add(New DataColumn("lectura", Type.GetType("System.Double")))
                dst.Tables("Datos").Columns.Add(New DataColumn("diferencia", Type.GetType("System.Double")))
                For i As Integer = 0 To dst.Tables("Datos").Rows.Count - 1
                    With dst.Tables("Datos").Rows(i)
                        Select Case Left(.Item("idElementoMedida"), 1)
                            Case "V"
                                .Item("lectura") = CalculaM3(nullACero(.Item("LecturaContador_M3")), .Item("codigoPVYCR"), .Item("idElementoMedida"), .Item("fecha_medida"))
                            Case "E"
                                .Item("lectura") = CalculaM3(nullACero(.Item("Total_kwh")), .Item("codigoPVYCR"), .Item("idElementoMedida"), .Item("fecha_medida"))
                            Case "H"
                                .Item("lectura") = CalculaM3(nullACero(.Item("HorasIntervalo")), .Item("codigoPVYCR"), .Item("idElementoMedida"), .Item("fecha_medida"))
                        End Select
                        If i > 0 Then
                            .Item("diferencia") = dst.Tables("Datos").Rows(i).Item("lectura") - dst.Tables("Datos").Rows(i - 1).Item("lectura")
                        Else
                            .Item("diferencia") = dst.Tables("Datos").Rows(i).Item("lectura") - CalculaPrimeraLectura(.Item("lectura"), .Item("codigoPVYCR"), .Item("idElementoMedida"), .Item("fecha_medida"))
                        End If
                        .Item("usuario") = "Regante"
                    End With
                Next
            Else
                dst.Tables("Datos").Columns.Add(New DataColumn("caudal_ls", Type.GetType("System.Double")))
                dst.Tables("Datos").Columns.Add(New DataColumn("volumen", Type.GetType("System.Double")))
                dst.Tables("Datos").Columns.Add(New DataColumn("diferencia", Type.GetType("System.Double")))

                For i As Integer = 0 To dst.Tables("Datos").Rows.Count - 1
                    With dst.Tables("Datos").Rows(i)
                        .Item("caudal_ls") = CalculaM3(nullACero(.Item("Caudal_M3S")), .Item("codigoPVYCR"), .Item("idElementoMedida"), .Item("fecha_medida"), "caudal")
                        .Item("volumen") = CalculaM3(nullACero(.Item("Caudal_M3S")), .Item("codigoPVYCR"), .Item("idElementoMedida"), .Item("fecha_medida"), "volumen")
                        If i > 0 Then
                            .Item("diferencia") = dst.Tables("Datos").Rows(i).Item("volumen") - dst.Tables("Datos").Rows(i - 1).Item("volumen")
                        Else
                            .Item("diferencia") = dst.Tables("Datos").Rows(i).Item("volumen") - CalculaPrimeraLectura(.Item("Caudal_M3S"), .Item("codigoPVYCR"), .Item("idElementoMedida"), .Item("fecha_medida"))
                        End If
                        .Item("usuario") = "Regante"
                    End With
                Next
            End If

            Select Case Left(informe, 6)
                Case "Capt1_"
                    'Aunque lo normal es que sólo exista una lectura, voy a coger la última (de cada año) por si acaso
                    Dim anyoActual As Integer = 0
                    For i = numLecturas To 0 Step -1
                        With dst.Tables("Datos").Rows(i)
                            .Item("fecha_medidaS") = CDate(.Item("fecha_medida")).Day.ToString + "/" + CDate(.Item("fecha_medida")).Month.ToString + "/" + CDate(.Item("fecha_medida")).Year.ToString
                            If anyoActual <> CDate(.Item("fecha_medida")).Year Then
                                anyoActual = CDate(.Item("fecha_medida")).Year
                            Else
                                dst.Tables("Datos").Rows.RemoveAt(i)
                            End If
                        End With
                    Next

                Case "Capt2_"
                    dst.Tables("Datos").Columns.Add(New DataColumn("mes", Type.GetType("System.String")))
                    anyo = dst.Tables("Datos").Rows(0).Item("fecha_medida").year

                    'Se supone que sólo habrá una lectura por mes, pero quitaremos las otras por si acaso (nos quedaremos con la última).
                    Dim mesActual As String = ""
                    Dim intMeses As ArrayList = New ArrayList()
                    For i = numLecturas To 0 Step -1
                        With dst.Tables("Datos").Rows(i)
                            .Item("mes") = SacaMes(CDate(.Item("fecha_medida")).Month)
                            .Item("fecha_medidaS") = CDate(.Item("fecha_medida")).Day.ToString + "/" + CDate(.Item("fecha_medida")).Month.ToString + "/" + CDate(.Item("fecha_medida")).Year.ToString
                            If mesActual <> .Item("mes") Then
                                mesActual = .Item("mes")
                                intMeses.Add(CDate(.Item("fecha_medida")).Month)
                            Else
                                dst.Tables("Datos").Rows.RemoveAt(i)
                            End If
                        End With
                    Next

                    'Añadimos los meses que faltan
                    AnyadirMeses(intMeses)
                Case "Capt3_"
                    dst.Tables("Datos").Columns.Add(New DataColumn("mes", Type.GetType("System.String")))
                    dst.Tables("Datos").Columns.Add(New DataColumn("Semana", Type.GetType("System.String")))
                    dst.Tables("Datos").Columns.Add(New DataColumn("Tamanyo", GetType(Vector)))
                    anyo = dst.Tables("Datos").Rows(0).Item("fecha_medida").year

                    'Se supone que sólo habrá una lectura por mes, pero quitaremos las otras por si acaso (nos quedaremos con la última).
                    Dim mesYSemanaActual As String = ""
                    Dim intMesesYSemanas As ArrayList = New ArrayList()
                    For i = numLecturas To 0 Step -1
                        With dst.Tables("Datos").Rows(i)
                            .Item("mes") = SacaMes(CDate(.Item("fecha_medida")).Month)
                            .Item("semana") = SacaSemana(CDate(.Item("fecha_medida")).Day)
                            .Item("fecha_medidaS") = CDate(.Item("fecha_medida")).Day.ToString + "/" + CDate(.Item("fecha_medida")).Month.ToString + "/" + CDate(.Item("fecha_medida")).Year.ToString
                            If mesYSemanaActual <> .Item("mes") & .Item("semana") Then
                                mesYSemanaActual = .Item("mes") & .Item("semana")
                                intMesesYSemanas.Add(CDate(.Item("fecha_medida")).Month & "-" & .Item("semana"))
                            Else
                                dst.Tables("Datos").Rows.RemoveAt(i)
                            End If
                        End With
                    Next

                    'Añadimos los meses y semanas que faltan
                    AnyadirSemanas(intMesesYSemanas)

                    'Lo separamos en dos tablas diferentes, para el diseño del informe.
                    Dim dt1, dt2 As New DataTable()
                    dt1 = dst.Tables("Datos").Copy
                    dt1.TableName = "Datos1"
                    dt2 = dst.Tables("Datos").Copy
                    dt2.TableName = "Datos2"
                    dst.Tables.Add(dt1)
                    dst.Tables.Add(dt2)

                    numLecturas = dst.Tables("Datos").Rows.Count
                    For i As Integer = numLecturas - 1 To 0 Step -1
                        If SacaMes(dst.Tables("Datos").Rows(i).Item("mes"), True) <= 6 Then
                            dt2.Rows.RemoveAt(i)
                        Else
                            dt1.Rows.RemoveAt(i)
                        End If
                    Next
                Case "Capt4_"
                    dst.Tables("Datos").Columns.Add(New DataColumn("dia", Type.GetType("System.String")))
                    dst.Tables("Datos").Columns.Add(New DataColumn("mes", Type.GetType("System.Int16")))
                    anyo = dst.Tables("Datos").Rows(0).Item("fecha_medida").year

                    'Se supone que sólo habrá una lectura por día, pero quitaremos las otras por si acaso (nos quedaremos con la última).
                    Dim fechaActual As String = ""
                    Dim fechas As ArrayList = New ArrayList()
                    For i = numLecturas To 0 Step -1
                        With dst.Tables("Datos").Rows(i)
                            .Item("dia") = SacaDia(CDate(.Item("fecha_medida")).Day)
                            .Item("mes") = CDate(.Item("fecha_medida")).Month
                            .Item("fecha_medidaS") = CDate(.Item("fecha_medida")).Day.ToString + "/" + CDate(.Item("fecha_medida")).Month.ToString + "/" + CDate(.Item("fecha_medida")).Year.ToString
                            If fechaActual <> .Item("fecha_medidaS") Then
                                fechaActual = .Item("fecha_medidaS")
                                fechas.Add(.Item("fecha_medidaS"))
                            Else
                                dst.Tables("Datos").Rows.RemoveAt(i)
                            End If
                        End With
                    Next

                    'Añadimos los días que faltan
                    AnyadirDias(fechas)

                    'Lo separamos en doce tablas diferentes, para el diseño del informe.
                    Dim dt(24) As DataTable
                    For i As Integer = 1 To 12
                        For j As Integer = 1 To 2
                            If j = 1 Then
                                dt(i * 2 - 1) = dst.Tables("Datos").Copy
                                dt(i * 2 - 1).TableName = "Datos" & i
                                dst.Tables.Add(dt(i * 2 - 1))
                            Else
                                dt(i * 2) = dst.Tables("Datos").Copy
                                dt(i * 2).TableName = "Datos" & i & "_" & j
                                dst.Tables.Add(dt(i * 2))
                            End If
                        Next
                    Next

                    numLecturas = dst.Tables("Datos").Rows.Count
                    For i As Integer = numLecturas - 1 To 0 Step -1
                        For j As Integer = 1 To 12
                            If dst.Tables("Datos").Rows(i).Item("mes") <> j Then
                                dt(j * 2).Rows.RemoveAt(i)
                                dt(j * 2 - 1).Rows.RemoveAt(i)
                            Else
                                If CInt(dst.Tables("Datos").Rows(i).Item("dia")) > 15 Then
                                    dt(j * 2 - 1).Rows.RemoveAt(i)
                                Else
                                    dt(j * 2).Rows.RemoveAt(i)
                                End If
                            End If
                        Next
                    Next
            End Select
        End If
    End Sub

    Private Function SacaMes(ByVal mes As Object, Optional ByVal Rev As Boolean = False) As Object
        If Rev = False Then
            Dim resultado As String = UCase(Left(MonthName(mes), 1)) & Mid(MonthName(mes), 2, 2)
            Return resultado
        Else
            Dim resultado As Integer
            Select Case mes
                Case "Ene"
                    resultado = 1
                Case "Feb"
                    resultado = 2
                Case "Mar"
                    resultado = 3
                Case "Abr"
                    resultado = 4
                Case "May"
                    resultado = 5
                Case "Jun"
                    resultado = 6
                Case "Jul"
                    resultado = 7
                Case "Ago"
                    resultado = 8
                Case "Sep"
                    resultado = 9
                Case "Oct"
                    resultado = 10
                Case "Nov"
                    resultado = 11
                Case "Dic"
                    resultado = 12
            End Select
            Return resultado
        End If

    End Function
    Private Function SacaSemana(ByVal dia As Integer) As String
        Dim semana As String = ""
        Select Case dia
            Case 0 To 7
                semana = "01-07"
            Case 8 To 15
                semana = "08-15"
            Case 16 To 23
                semana = "16-23"
            Case 24 To 31
                semana = "24-31"
        End Select
        Return semana
    End Function

    Private Sub AnyadirMeses(ByVal intMeses As ArrayList)
        Dim existeMes As Boolean = False
        Dim dr As DataRow

        For i As Integer = 1 To 12
            If intMeses.Contains(i) Then
                existeMes = True
            End If
            If existeMes = False Then
                With dst.Tables("Datos").Rows
                    dr = dst.Tables("Datos").NewRow()
                    .InsertAt(dr, i - 1)
                    With dst.Tables("Datos").Rows(i - 1)
                        .Item("mes") = SacaMes(i)
                    End With
                End With
            End If
            existeMes = False
        Next
    End Sub
    Private Sub AnyadirSemanas(ByVal intSemanas As ArrayList)
        Dim existeMes As Boolean = False
        Dim dr As DataRow
        Dim strSemana As String
        Dim tamanyo As Vector

        For i As Integer = 1 To 12
            For j = 0 To 3
                If j = 0 Then
                    tamanyo.Width = 119
                    tamanyo.Height = 236
                Else
                    tamanyo.Width = 0
                    tamanyo.Height = 0
                End If

                strSemana = SacaSemana(8 * j)
                If intSemanas.Contains(i & "-" & strSemana) Then
                    existeMes = True
                End If
                If existeMes = False Then
                    With dst.Tables("Datos").Rows
                        dr = dst.Tables("Datos").NewRow()
                        .InsertAt(dr, 4 * (i - 1) + j)
                        With dst.Tables("Datos").Rows(4 * (i - 1) + j)
                            .Item("mes") = SacaMes(i)
                            .Item("semana") = strSemana
                        End With
                    End With
                End If
                existeMes = False
                dst.Tables("Datos").Rows(4 * (i - 1) + j).Item("Tamanyo") = tamanyo
            Next
        Next
    End Sub
    Private Sub AnyadirDias(ByVal intDias As ArrayList)
        Dim existeDia As Boolean = False
        Dim dr As DataRow

        For j = 1 To 12
            For i As Integer = 1 To 31
                If intDias.Contains(i & "/" & j & "/" & anyo) Then
                    existeDia = True
                End If
                If existeDia = False Then
                    With dst.Tables("Datos").Rows
                        dr = dst.Tables("Datos").NewRow()
                        .InsertAt(dr, 31 * (j - 1) + i - 1)
                        With dst.Tables("Datos").Rows(31 * (j - 1) + i - 1)
                            .Item("dia") = SacaDia(i)
                            .Item("mes") = j
                        End With
                    End With
                End If
                existeDia = False
            Next
        Next
    End Sub


    Private Function SacaDia(ByVal dia As Integer) As String
        Select Case dia
            Case Is < 10
                Return "0" & dia.ToString()
            Case Else
                Return dia.ToString()
        End Select
    End Function

    Private Function CalculaM3(ByVal valor As Double, ByVal codigoPVYCR As String, ByVal em As String, ByVal fecha As DateTime, Optional ByVal columna As String = "") As Double
        Dim resultado, relacion, litros, m3 As Double
        If conexion.State = ConnectionState.Closed Then conexion.Open()
        Dim comando As SqlCommand = New SqlCommand("", conexion)
        Select Case Left(em, 1)
            Case "H"
                comando.CommandText = "Select caudal_LSeg from PVYCR_Motobombas where codigoPVYCR='" & codigoPVYCR & "' and " & fecha & " between fechaInicial and isnull(fechaFin,'31/12/9000')"
                relacion = nullACero(comando.ExecuteScalar)
                valor = valor * 3600    'El tiempo en segundos, venía dado en horas
                litros = valor * relacion
                resultado = litros / 1000      '(1000l ---> 1m3)
            Case "E"
                comando.CommandText = "Select E_RelacionM3_KWH from PVYCR_Contadores where codigoPVYCR='" & codigoPVYCR & "' and " & fecha & " between fechaInicial and isnull(fechaFin,'31/12/9000')"
                relacion = nullACero(comando.ExecuteScalar)
                relacion = 20
                resultado = valor * relacion       'El valor viene dado en kwh
            Case "V"
                resultado = valor          'El valor viene dado en m3
            Case "Q"
                Select Case columna
                    Case "caudal"
                        litros = valor * 1000       'El valor viene dado en m3 
                        resultado = litros          'Queremos resultado en litros
                    Case "volumen"
                        resultado = valor * 86400 * 365         'El resultado son m3 en un segundo, multiplicamos por los segundo de un año (86400seg. en un día)
                End Select
        End Select
        conexion.Close()
        Return resultado
    End Function

    Private Function CalculaPrimeraLectura(ByVal valor As Double, ByVal codigoPVYCR As String, ByVal EM As String, ByVal fechaMedida As DateTime) As Double
        Dim strSql As String = ""
        Dim resultado As Double = valor
        strSql = AccesoADatos.SacarLecturasPreYPro(EM, " WHERE CodigoPVYCR='" & codigoPVYCR & "' AND idElementoMedida='" & EM & "' and cod_fuente_dato='19' AND Fecha_Medida<'" & fechaMedida & "' ", _
                                "order by fecha_medida desc", conexion)(1)
        da.SelectCommand.CommandText = strSql
        da.Fill(dst, "DatosPrimeraLectura")
        If dst.Tables("DatosPrimeraLectura").Rows.Count > 0 Then
            Select Case Left(EM, 1)
                Case "V"
                    resultado = dst.Tables("DatosPrimeraLectura").Rows(0).Item("LecturaContador_M3")
                Case "E"
                    resultado = dst.Tables("DatosPrimeraLectura").Rows(0).Item("Total_kwh")
                Case "H"
                    resultado = dst.Tables("DatosPrimeraLectura").Rows(0).Item("HorasIntervalo")
                Case "Q"
                    resultado = CalculaM3(dst.Tables("DatosPrimeraLectura").Rows(0).Item("Caudal_M3S"), codigoPVYCR, EM, fechaMedida, "volumen")
            End Select
        End If
    End Function
End Class


