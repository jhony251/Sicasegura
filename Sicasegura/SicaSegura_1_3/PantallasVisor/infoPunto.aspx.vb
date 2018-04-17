Imports GuarderiaFluvial
Imports System.Data
Imports System.Data.SqlClient
Imports GuarderiaFluvial.JavaScript
Imports GuarderiaFluvial.SICA_GestionArboles
Imports GuarderiaFluvial.utiles
Imports System
Imports System.IO
Partial Class PantallasVisor_infoElemento
    Inherits System.Web.UI.Page
    Dim conexion As SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
    Dim daElementos As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim dstElementos As DataSet = New System.Data.DataSet()
    Dim comando As SqlClient.SqlCommand = New System.Data.SqlClient.SqlCommand("", conexion)
    Dim pageSize As Integer = ConfigurationSettings.AppSettings("registrosPorPag")
    Dim sentenciaSel, sentenciaOrder As String
    Dim parfila As Integer = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Se obtiene la url que tiene que abrir
       
        Try

            CargarFotos(Request.QueryString("clavenodo"))
            crearDataSets()


        Catch Excepcion As Exception

        End Try

    End Sub

    Public Sub CargarFotos(ByVal CodigoPunto As String)

        Dim rutaFotos As String = ConfigurationSettings.AppSettings("ruta_imagenes") & "/" & CodigoPunto
        Dim lista1() As String
        lista1 = System.IO.Directory.GetFiles(rutaFotos)
        Dim archivo As String
        Dim file, strHtml As String
        Dim i As Integer = 0
        strHtml = "<table width='100%' align='center'><tr>"
        Dim cadena As String

        For Each file In lista1
            'cadena = "fotos/%23" & Request.QueryString("idPDA") & "%23" & Request.QueryString("idTarea") & "/" & System.IO.Path.GetFileName(file)
            cadena = "http://192.168.194.20/basefotografica/images/" & Request.QueryString("clavenodo") & "/" & System.IO.Path.GetFileName(file)
            archivo = System.IO.Path.GetFileNameWithoutExtension(file)

            strHtml &= "<td align='center' valign='bottom' style='text-align:center;'><a href='" & cadena & "' rel='lightbox_plus' class='vertical' target='_blank'>" & _
                    "<img src=""..\SICAH\volcadoimagen.aspx?ruta=" & Replace(file, "#", "%23") & """ border=0></a><br>" & archivo & "</td>"
            i = i + 1
            If i Mod 4 = 0 Then
                strHtml &= "</tr><tr>"
            End If
        Next

        strHtml &= "</tr></table>"

        lblHTML.Text = strHtml
    End Sub

    Private Sub crearDataSets()
        'Criterios de filtrado
        Dim sFiltro As String = ""
        utiles.Comprobar_Conexion_BD(Page, conexion)
        sentenciaSel = " SELECT "
        sentenciaSel = sentenciaSel & " CodigoPVYCR, idElementoMedida, Tipo FROM dbo.PVYCR_ElementosMedida WHERE CodigoPVYCR='" & Request.QueryString("clavenodo") & "' "
        sentenciaOrder = " order by CodigoPVYCR, idElementoMedida "
        sentenciaSel = sentenciaSel & sentenciaOrder


        daElementos.SelectCommand.CommandText = sentenciaSel
        daElementos.Fill(dstElementos, "TablaElementos")
        Dim txtComando As String = ""
        txtComando = daElementos.SelectCommand.CommandText
        txtComando = txtComando.Substring(0, txtComando.IndexOf("order by"))


        rptElementos.DataSource = dstElementos.Tables("TablaElementos")
        rptElementos.DataBind()

    End Sub

    Protected Function checkFila() As String
        parfila = (parfila + 1) Mod 2
        Return "class=""fila" & parfila & """"
    End Function
End Class
