Imports System.Data
Imports System.Data.SqlClient
Imports Sicadll
Imports GuarderiaFluvial.utiles

Partial Class Lecturas
    Inherits System.Web.UI.Page
    Dim conexion As New SqlConnection(ConfigurationManager.AppSettings("dsn"))
    Dim conexion_pruebas As New SqlConnection(ConfigurationManager.AppSettings("dsn_pruebas"))
    Dim codigoPVYCR As String = ""
    Dim EM As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim LCA As SicaSegura.SICA_LibroControl
        Dim Titulo_Inscripcion As Object
        Dim Objeto_LC As Object
        Dim InterfazLibroControl As SicaSegura.SICA_Interfaz.SICA_LibroControl = New SicaSegura.SICA_Interfaz.SICA_LibroControl()
        Try
            Objeto_LC = Session("LCA")
            Titulo_Inscripcion = Session("USERNAME")
            LCA = Objeto_LC

            Dim menu As String = "Cerrar sesion-borrarSesion.aspx#Información de usuario-index.aspx"
            HTML_Links_Sup_Izq.Text = InterfazLibroControl.Get_Enlaces_Sup_Izq(menu.Split("#"))
            HTML_Subcabecera_Logos.Text = InterfazLibroControl.Get_Cabecera()
            HTML_Menu_Navegacion.Text = InterfazLibroControl.Get_Menu_Navegacion("dos")
            HTML_Pie_Logo_Corporativo.Text = InterfazLibroControl.Get_pie_logo_corporativo()
            HTML_Pie_pagina.Text = InterfazLibroControl.Get_pie_pagina()
            HTML_Título_Aprovechamiento.Text = LCA.Agrupacion.Get_Descripcion()
            HTML_Listado_de_puntos_de_control.Text = PopulateMenuLateralPuntos(LCA.Agrupacion.Get_PuntosDeContros())

            Dim titulares As String() = (New SicaSegura.SICA_Titular_Aprovechamiento()).GetListadoTitularesRaacs(LCA.ID_Inscripcion)
            Dim titular As String() = titulares(0).Replace("$%", "$").Split("$")
            Dim otros As String = ""
            If (titulares.Length > 1) Then
                otros = " (y otros)"
            End If
            
            HTML_Título_Aprovechamiento.Text = "Inscripción: " & LCA.ID_Inscripcion.ToString() & " -- " & LCA.Agrupacion.Get_Descripcion() & " <br>Titular: " & titular(1) + " " + titular(2) + otros



        Catch
            Response.Redirect("borrarsesion.aspx")
        End Try

        PopulateMenuLateralPuntos(LCA.Agrupacion.Get_PuntosDeContros)

        If Request.QueryString("codigoPVYCR") <> "" Then
            codigoPVYCR = Request.QueryString("codigoPVYCR")
        End If

        If Request.QueryString("EM") <> "" Then
            EM = Request.QueryString("EM")
        End If

        If Session("codigoPVYCR-EM_activo") <> "" Then
            Dim elemento As String() = Session("codigoPVYCR-EM_activo").ToString().Split("-")
            codigoPVYCR = elemento(0)
            EM = elemento(1)
        End If

        If (codigoPVYCR <> "" And EM <> "") Then
            ucPaginador.conexion = New SqlConnection(ConfigurationManager.AppSettings("dsn"))
            'ucPaginador.da = New SqlDataAdapter("", ucPaginador.conexion) 'jms30x
            ucPaginador.comando = New SqlCommand("", ucPaginador.conexion)
            If Not IsPostBack Then
                'pnlPanelSelectorPunto.Visible = False
                pnlPanelLecturas.Visible = True
                'HTML_Cabecera_grid.Text = codigoPVYCR & "-" & EM
                CargarDataSets()
            End If
        End If
        If (codigoPVYCR = "" And EM = "") Then
            codigoPVYCR = LCA.Agrupacion.Get_PuntosDeContros()(0).Split("-")(0)
            EM = LCA.Agrupacion.Get_PuntosDeContros()(0).Split("-")(1)
            Response.Redirect("lecturas.aspx?codigoPVYCR=" & codigoPVYCR & "&EM=" & EM)
        End If



    End Sub

    Private Function PopulateMenuLateralPuntos(ByVal puntos As String()) As String
        Dim i As Integer
        Dim codigo As String = ""
        For i = 0 To puntos.Length - 1
            codigo = codigo & "<li><a href='lecturas.aspx?codigoPVYCR=" & puntos(i).ToUpper().Split("-")(0) & "&EM=" & puntos(i).ToUpper().Split("-")(1).Trim() & "'>" & puntos(i).ToUpper() & "</a>"

        Next i
        Return codigo

    End Function


    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        'ucPaginador.conexion.Close()
        'ucPaginador.da.Dispose() 'jms30x
        'ucPaginador.comando.Dispose()
    End Sub

    Private Sub CargarDataSets()
        ucPaginador.cadenaConexion = ConfigurationManager.AppSettings("dsn")
        ucPaginador.itemsporPagina = ConfigurationManager.AppSettings("registrosPorPag")

        Dim AccesoADatos As New Sicadll.AccesoADatos.AccesoADatos2

        Dim fechaInicio As DateTime = CalculaAnyoHidrologico()
        Dim fechaFin As DateTime = Today
        Dim strSQL As String = AccesoADatos.SacarLecturasPreYPro(EM, " WHERE CodigoPVYCR='" & codigoPVYCR & "' AND idElementoMedida='" & EM & "' " & _
                                                                 "AND Fecha_Medida between '" & fechaInicio & "' and '" & fechaFin & "'", "ORDER BY Fecha_Medida DESC", conexion, False)(1)
        DGV_Volumen.DataSource = AccesoADatos.SacarLecturasPreYPro(EM, " WHERE CodigoPVYCR='" & codigoPVYCR & "' AND idElementoMedida='" & EM & "' " & _
                                                                 "AND Fecha_Medida between '" & fechaInicio & "' and '" & fechaFin & "'", "ORDER BY Fecha_Medida DESC", conexion, False)(0)
        DGV_Volumen.DataBind()

        If DGV_Volumen.Columns.Count > 0 Then
            DGV_Volumen.HeaderRow.Cells(0).Visible = False
            DGV_Volumen.HeaderRow.Cells(1).Visible = False
            DGV_Volumen.HeaderRow.Cells(2).Visible = False
            DGV_Volumen.HeaderRow.Cells(5).Visible = False
            DGV_Volumen.HeaderRow.Cells(6).Visible = False
            DGV_Volumen.HeaderRow.Cells(7).Visible = False
            DGV_Volumen.HeaderRow.Cells(8).Visible = False
            DGV_Volumen.HeaderRow.Cells(9).Visible = False
            DGV_Volumen.HeaderRow.Cells(10).Visible = False
            DGV_Volumen.HeaderRow.Cells(11).Visible = False
            DGV_Volumen.HeaderRow.Cells(12).Visible = False
            DGV_Volumen.HeaderRow.Cells(13).Visible = False

            Dim i As Integer
            For i = 0 To DGV_Volumen.Rows.Count - 1
                DGV_Volumen.Rows(i).Cells(0).Visible = False
                DGV_Volumen.Rows(i).Cells(1).Visible = False
                DGV_Volumen.Rows(i).Cells(2).Visible = False
                DGV_Volumen.Rows(i).Cells(5).Visible = False
                DGV_Volumen.Rows(i).Cells(6).Visible = False
                DGV_Volumen.Rows(i).Cells(7).Visible = False
                DGV_Volumen.Rows(i).Cells(8).Visible = False
                DGV_Volumen.Rows(i).Cells(9).Visible = False
                DGV_Volumen.Rows(i).Cells(10).Visible = False
                DGV_Volumen.Rows(i).Cells(11).Visible = False
                DGV_Volumen.Rows(i).Cells(12).Visible = False
                DGV_Volumen.Rows(i).Cells(13).Visible = False
                If DGV_Volumen.Rows(i).Cells(12).Text = 1 Then
                    DGV_Volumen.Rows(i).Cells(4).ForeColor = Drawing.Color.Red
                End If
            Next
        End If

        Select Case Left(EM, 1)
            Case "Q"
                pnlLecturasQ.Visible = True
                ucPaginador.controlMuestra = "rptLecturasQ"
            Case "V"
                pnlLecturasV.Visible = True
                ucPaginador.controlMuestra = "rptLecturasV"
            Case "E"
                pnlLecturasE.Visible = True
                ucPaginador.controlMuestra = "rptLecturasE"
            Case "H"
                pnlLecturasH.Visible = True
                ucPaginador.controlMuestra = "rptLecturasH"
        End Select

        ucPaginador.sentenciaSQL = strSQL
        ucPaginador.nombreTabla = "Datos"
        ucPaginador.comando.CommandText = Replace(ucPaginador.sentenciaSQL, "ORDER BY Fecha_Medida DESC", "")
        Dim numReg As Integer = ucPaginador.comando.ExecuteScalar

        If numReg = 0 Then
            ucPaginador.Visible = False
            lblNohay.Visible = True
        End If

        If Request.QueryString("pag") <> "" Then
            ucPaginador.numPagina = Request.QueryString("pag")
        Else
            ucPaginador.numPagina = 1
        End If

        ucPaginador.getRegistros()

        fechaInicio = "01/10/2010"
        fechaFin = "30/09/2012"
        codigoPVYCR = "CM022P01"
        EM = "H01"
        Dim acumulado(4) As Object

        acumulado = AccesoADatos.obtenerAcumulado(codigoPVYCR, EM, conexion, fechaInicio, fechaFin)
        conexion.Close()
    End Sub

    Private Function CalculaAnyoHidrologico() As DateTime
        Dim fecha As DateTime

        Select Case Today.Month
            Case Is >= 10
                fecha = "01/10/" & Today.Year
            Case Else
                fecha = "01/10/" & Today.Year - 1
        End Select

        Return fecha
    End Function

    Protected Function GetEstilo(ByVal eldataitem As DataRowView) As String
        'Return "grid" & eldataitem("estadillo")
        Dim estilo As String
        If (eldataitem("estadillo") = 1) Then
            estilo = "color:red;"
        Else
            estilo = ""
        End If
        Return estilo
    End Function

    Protected Sub LBAddlectura_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LBAddlectura.Click
        Session("PuntoActivo") = codigoPVYCR & "-" & EM
        Select Case Left(EM, 1)
            Case "V"
                Response.Redirect("inserta-lectura-volumen.aspx")
            Case "Q"
                Response.Redirect("inserta-lectura-caudal.aspx")
            Case "H"
                Response.Redirect("inserta-lectura-horas.aspx")
            Case "E"
                Response.Redirect("inserta-lectura-electricidad.aspx")
            Case Else
                Response.Redirect("")
        End Select
    End Sub
End Class
