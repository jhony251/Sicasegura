Imports System.Data.SqlClient
Imports System.Data
Imports GuarderiaFluvial
Partial Class SICAH_ListarElementosMedida
    Inherits System.Web.UI.Page

    Dim conexion As SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura"))
    Dim daElementos As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim dstElementos As DataSet = New System.Data.DataSet()
    Dim comando As SqlClient.SqlCommand = New System.Data.SqlClient.SqlCommand("", conexion)

    Public numpaginas As Integer
    Dim pageSize As Integer = ConfigurationSettings.AppSettings("registrosPorPag")
    Dim sentenciaSel, sentenciaOrder As String
    Dim parfila As Integer = 0
    Protected Sub Page_PreLoad(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreLoad
        ucPaginacionE.ruta = "../"
        ucPaginacionA.ruta = "../"
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.MaintainScrollPositionOnPostBack = True

        If Not IsPostBack Then
            If Session("usuarioReg") = Nothing Then
                Response.Redirect("UsuarioNoRegistrado.aspx")
                Exit Sub
            End If
            lblCabecera.Text = genHTML.cabecera(Page)
            lblPestanyas.Text = genHTML.pestanyasMenu(4, "../", Page, Session("idperfil"), Session("usuarioReg"))

            If Request.QueryString("pag") <> "" Then
                ucPaginacionA.lblPaginatext = Request.QueryString("pag")
            Else
                ucPaginacionA.lblPaginatext = "1"
            End If
            'creamos datasets
            crearDataSets()
            'ponemos visibles los paneles y cargamos los datos en pantalla
            If Request.QueryString("tipo").ToString = "Q" Then
                pnlAcequias.Visible = True
                pnlEnergia.Visible = False
                rptAcequias.DataSource = dstElementos.Tables("TablaAcequias")
                rptAcequias.DataBind()
                ucPaginacionA.lblNumpaginasDatabind()
            ElseIf Request.QueryString("tipo").ToString = "E" Then
                pnlAcequias.Visible = False
                pnlEnergia.Visible = False
                rptEnergia.DataSource = dstElementos.Tables("TablaAlimentacion")
                rptEnergia.DataBind()
                ucPaginacionE.lblNumpaginasDatabind()
            ElseIf Request.QueryString("tipo").ToString = "V" Then
                pnlAcequias.Visible = False
                pnlEnergia.Visible = False
                rptEnergia.DataSource = dstElementos.Tables("TablaMotores")
                rptEnergia.DataBind()
            ElseIf Request.QueryString("tipo").ToString = "H" Then
                pnlAcequias.Visible = False
                pnlEnergia.Visible = False
                rptEnergia.DataSource = dstElementos.Tables("TablaHorometros")
                rptEnergia.DataBind()
            End If
        End If
    End Sub
    Protected Function checkFila() As String
        parfila = (parfila + 1) Mod 2
        Return "class=""fila" & parfila & """"
    End Function
    Protected Function obtenerNomElemento() As String
        Return Request.QueryString("nomElem")
    End Function

    Private Sub crearDataSets()
        'Criterios de filtrado
        Dim sFiltro As String = ""
        'dependiendo del tipo seleccionaremos los datos de una tabla u otra
        If Request.QueryString("tipo").ToString = "Q" Then
            sentenciaSel = "SELECT CodigoPVYCR,Cod_Fuente_Dato,Fecha_Medida,Escala_M,Calado_M,Observaciones " & _
                    ",TipoObtencionCaudal,RegimenCurva,NumeroParada,Caudal_M3S,DUDA_CALIDAD " & _
                    "FROM PVYCR_DatosAcequias where codigoPVYCR = '" & Request.QueryString("codigoPVYCR").ToString & "'"

            If txtfiltroFechaA.Text <> "[Fecha Medida]" And txtfiltroFechaA.Text <> "" Then
                sFiltro = " and cast(datepart(dd,Fecha_Medida) as varchar(2))+'/'+ cast(datepart(mm,Fecha_Medida)as varchar(2))+'/' + cast(datepart(yy,Fecha_Medida)as varchar(4)) = " & _
                "cast(datepart(dd,'" & txtfiltroFechaA.Text & "') as varchar(2))+'/'+ cast(datepart(mm,'" & txtfiltroFechaA.Text & "')as varchar(2))+'/' + cast(datepart(yy,'" & txtfiltroFechaA.Text & "')as varchar(4))"
            Else
                sFiltro = ""
            End If

            sentenciaOrder = " order by codigoPVYCR, Fecha_Medida desc"

            If sFiltro <> "" Then
                sentenciaSel = sentenciaSel & sFiltro & sentenciaOrder
            Else
                sentenciaSel = sentenciaSel & sentenciaOrder
            End If

            daElementos.SelectCommand.CommandText = sentenciaSel
            'datos acequias
            daElementos.Fill(dstElementos, (CInt(ucPaginacionA.lblPaginatext) - 1) * ucPaginacionA.pageSize, ucPaginacionA.pageSize, "TablaAcequias")
            'Cálculo del número de páginas
            Dim txtComando As String = ""
            txtComando = daElementos.SelectCommand.CommandText
            txtComando = txtComando.Substring(0, txtComando.IndexOf("order by"))
            ucPaginacionA.calcularPags(txtComando)
        ElseIf Request.QueryString("tipo").ToString = "E" Then
            sentenciaSel = ""
            'datos energía
            daElementos.Fill(dstElementos, (CInt(ucPaginacionE.lblPaginatext) - 1) * ucPaginacionE.pageSize, ucPaginacionE.pageSize, "TablaAlimentacion")
            'Cálculo del número de páginas
            Dim txtComando As String = ""
            txtComando = daElementos.SelectCommand.CommandText
            txtComando = txtComando.Substring(0, txtComando.IndexOf("order by"))
            ucPaginacionE.calcularPags(txtComando)
        ElseIf Request.QueryString("tipo").ToString = "V" Then
            sentenciaSel = ""
            'datos volumetricos
            'daElementos.Fill(dstElementos, (CInt(ucPaginacion.lblPaginatext) - 1) * ucPaginacion.pageSize, ucPaginacion.pageSize, "TablaMotores")

        ElseIf Request.QueryString("tipo").ToString = "H" Then
            sentenciaSel = ""
            'datos horómetro
            'daElementos.Fill(dstElementos, (CInt(ucPaginacion.lblPaginatext) - 1) * ucPaginacion.pageSize, ucPaginacion.pageSize, "TablaHorometros")

        End If
   
    End Sub


    Protected Sub btnFiltroAceptar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFiltroAceptarA.Click
        ucPaginacionA.lblPaginatext = "1"
        ucPaginacionE.lblNumpaginasDatabind()
        crearDataSets()
        If Request.QueryString("tipo").ToString = "Q" Then
            rptAcequias.DataSource = dstElementos.Tables("TablaAcequias")
            rptAcequias.DataBind()
            ucPaginacionA.lblNumpaginasDatabind()
        ElseIf Request.QueryString("tipo").ToString = "E" Then
            rptEnergia.DataSource = dstElementos.Tables("TablaAlimentacion")
            rptEnergia.DataBind()
            ucPaginacionE.lblNumpaginasDatabind()
        ElseIf Request.QueryString("tipo").ToString = "V" Then
            rptEnergia.DataSource = dstElementos.Tables("TablaMotores")
            rptEnergia.DataBind()
        ElseIf Request.QueryString("tipo").ToString = "H" Then
            rptEnergia.DataSource = dstElementos.Tables("TablaHorometros")
            rptEnergia.DataBind()
        End If
    End Sub
    Protected Sub btnFiltrocancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFiltroCancelarA.Click
        ucPaginacionA.lblPaginatext = "1"
        ucPaginacionE.lblPaginatext = "1"
        txtfiltroFechaA.Text = ""
        crearDataSets()
        If Request.QueryString("tipo").ToString = "Q" Then
            rptAcequias.DataSource = dstElementos.Tables("TablaAcequias")
            rptAcequias.DataBind()
            ucPaginacionA.lblNumpaginasDatabind()
        ElseIf Request.QueryString("tipo").ToString = "E" Then
            rptEnergia.DataSource = dstElementos.Tables("TablaAlimentacion")
            rptEnergia.DataBind()
            ucPaginacionE.lblNumpaginasDatabind()
        ElseIf Request.QueryString("tipo").ToString = "V" Then
            rptEnergia.DataSource = dstElementos.Tables("TablaMotores")
            rptEnergia.DataBind()
        ElseIf Request.QueryString("tipo").ToString = "H" Then
            rptEnergia.DataSource = dstElementos.Tables("TablaHorometros")
            rptEnergia.DataBind()
        End If
    End Sub
    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        If ucPaginacionA.lblMuevetext = "si" Then
            crearDataSets()
            rptAcequias.DataSource = dstElementos.Tables("TablaAcequias")
            rptAcequias.DataBind()
            ucPaginacionA.lblMuevetext = "no"
        End If
        If ucPaginacionE.lblMuevetext = "si" Then
            crearDataSets()
            rptAcequias.DataSource = dstElementos.Tables("TablaEnergia")
            rptAcequias.DataBind()
            ucPaginacionE.lblMuevetext = "no"
        End If

    End Sub

 
    Protected Sub btnFiltroCancelarE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFiltroCancelarE.Click
        pnlAcequias.Visible = False
        pnlEnergia.Visible = False
    End Sub
End Class
