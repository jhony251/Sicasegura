Imports System.Data.SqlClient
Imports System.IO
Imports System.Data
Imports GuarderiaFluvial
Imports GuarderiaFluvial.JavaScript

Partial Class SICAH_motores
    Inherits System.Web.UI.Page
    Dim conexion As SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
    Dim daMotores As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
    Dim dstMotores As DataSet = New System.Data.DataSet()

    Public numpaginas As Integer
    Dim pageSize As Integer = ConfigurationSettings.AppSettings("registrosPorPag")
    Dim sentenciaSel, sentenciaOrder As String
    Dim parfila As Integer = 0
    Dim ddlF As DropDownList
    Dim ddlE As DropDownList
    Dim ddlV As DropDownList
    Dim comando As SqlClient.SqlCommand = New System.Data.SqlClient.SqlCommand("", conexion)
    Dim objTrans As SqlTransaction


 


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
            cargarlistas_edicion()

        Catch
            Response.Redirect("borrarsesion.aspx")
        End Try



        'Código del calendario javascript
        ClientScript.RegisterStartupScript(Page.GetType, "getD", "<script>D=document.all;</script>")
        ClientScript.RegisterStartupScript(Page.GetType, "initD", "<script>initDXCal();</script>")
        'necesito que esté fuera del is notpostback para que se grabe la fecha de desconexion para el LOG NCM

        If Not IsPostBack Then

            Session("Ordenacion") = ""

            utiles.Comprobar_Conexion_BD(Page, conexion)
            'ncm 25/08/2008
            'If Request.QueryString("pag") <> "" Then
            '    lblPagina.Text = Request.QueryString("pag")
            'Else
            '    lblPagina.Text = "1"
            'End If

            sentenciaSel = "SELECT distinct DM.CodigoPVYCR, DM.idElementoMedida, DM.Cod_Fuente_Dato, P.DenominacionPunto,c.DenominacionCauce " & _
                           "FROM PVYCR_DatosMotores_Estadillo DM, PVYCR_Puntos P, PVYCR_Cauces C " & _
                           "WHERE(DM.CodigoPVYCR = p.CodigoPVYCR AND " & _
                            "P.CodigoCauce = C.CodigoCauce COLLATE Modern_Spanish_CI_AI) " 'and " & _
            '"DM.Cod_Fuente_Dato = '05'"

            sentenciaOrder = sentenciaSel & "order by DM.CodigoPVYCR, DM.idElementoMedida, DM.Cod_Fuente_Dato "


            'lblNumpaginas.DataBind()
        End If
    End Sub

    Protected Sub cargarlistas_edicion()

        Dim punto As String = Session("PuntoActivo").ToString().Split("-")(0).ToString()
        Dim elemento As String = Session("PuntoActivo").ToString().Split("-")(1).ToString()

        TXTCodigoCauce.Text = Left(punto, punto.Length - 3)
        TXTCodigoPunto.Text = punto
        TXTIDEM.Text = elemento
        TXTCFD.Text = "10"


        ddlEDfunciona.Items.Insert(0, New ListItem("[seleccionar]", ""))
        ddlEDfunciona.Items.Insert(1, New ListItem("SI", "1"))
        ddlEDfunciona.Items.Insert(2, New ListItem("NO", "0"))

    End Sub

    Protected Sub btnEDCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEDCancelar.Click
        limpiar_campos_edicion()

    End Sub
    Protected Sub limpiar_campos_edicion()
        'ddlEDidElemento.Items.Clear()
        'ddlEDCauce.Items.Clear()
        'ddlEDCodigoPVYCR.Items.Clear()
        'ddlEDCodFuenteDato.Items.Clear()
        'txtEDFechaMedida.Text = ""
        txtEdLecturaContador.Text = ""
        txtEDObservaciones.Text = ""
        ddlEDIncidenciasV.Items.Clear()
        txtEDConsumoVol.Text = ""
        txtEDReinicioVol.Text = ""
        ddlEDfunciona.Items.Clear()
        txtEDUsuario.Text = ""
        txtEDCaudalMedido.Text = ""
    End Sub

    

    Protected Sub btnEDAceptar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEDAceptar.Click
        Dim comando As SqlClient.SqlCommand = New SqlClient.SqlCommand("", conexion)
        Dim objTrans As SqlTransaction
        Try
            utiles.Comprobar_Conexion_BD(Page, conexion)
            ''comprobamos si existe el elemento de medida, si no existe lo insertamos para que no falle la FK que hay entre datos alimentacion y elementos de medida
            'daMotores.SelectCommand.CommandText = "select count(*) numero from pvycr_elementosmedida where codigopvycr= '" & ddlEDCodigoPVYCR.Text & "' and idelementomedida = '" & txtEDidElementomedida.Text & "' "
            'daMotores.Fill(dstMotores, "TablaExisteElemento")

            comando.Parameters.AddWithValue("@idElementoMedida", TXTIDEM.Text)
            comando.Parameters.AddWithValue("@CodigoPVYCR", TXTCodigoPunto.Text)
            comando.Parameters.AddWithValue("@cod_fuente_datos", TXTCFD.Text)
            'If dstMotores.Tables("TablaExisteElemento").Rows(0).Item("numero") = 0 Then
            '    comando.CommandText = "INSERT INTO PVYCR_ELEMENTOSMEDIDA (CODIGOPVYCR, idElementoMedida, TIPO) VALUES " & _
            '    "(@CodigoPVYCR,@idElementoMedida,'V')"
            '    comando.ExecuteNonQuery()
            'End If
            'abrimos una transaccion para que si falla algunos de los inserts haga un rollback
            objTrans = conexion.BeginTransaction()
            comando.Transaction = objTrans
            comando.CommandText = "INSERT INTO [PVYCR_DatosMotores_Estadillo]([CodigoPVYCR],[idElementoMedida],[Cod_Fuente_Dato],[Fecha_Medida] " & _
                        ",[LecturaContador_M3],[Observaciones],[idIncidenciaVolumetrica],[ConsumoVolumetricoAdicional] " & _
                        ",[ReinicioLecturaVolumetrica],[Funciona],[justificacion], login,[CaudalMedido]) " & _
                        "values (@CodigoPVYCR,@idElementoMedida,@cod_fuente_datos,@Fecha_Medida, " & _
                        "@LecturaContador_M3,@Observaciones,@idIncidenciaVolumetrica,@ConsumoVolumetricoAdicional, " & _
                        "@ReinicioLecturaVolumetrica, @Funciona,@justificacion, @login,@CaudalMedido) "

            comando.Parameters.AddWithValue("@Fecha_medida", TXTFechaMedida.Text)

            'Lectura contador M3
            If utiles.nullABlanco(txtEdLecturaContador.Text) = "" Then
                comando.Parameters.AddWithValue("@LecturaContador_M3", DBNull.Value)
            Else
                comando.Parameters.AddWithValue("@LecturaContador_M3", txtEdLecturaContador.Text.Replace(",", "."))
            End If
            'observaciones
            comando.Parameters.AddWithValue("@Observaciones", txtEDObservaciones.Text)
            'Id incidencia volumétrica
            comando.Parameters.AddWithValue("@idIncidenciaVolumetrica", utiles.BlancoANull(ddlEDIncidenciasV.Text))
            'Consumo volumétrico
            If utiles.nullABlanco(txtEDConsumoVol.Text) = "" Then
                comando.Parameters.AddWithValue("@ConsumoVolumetricoAdicional", DBNull.Value)
            Else
                comando.Parameters.AddWithValue("@ConsumoVolumetricoAdicional", txtEDConsumoVol.Text.Replace(",", "."))
            End If
            'Reinico lectura volum´´etrica
            If utiles.nullABlanco(txtEDReinicioVol.Text) = "" Then
                comando.Parameters.AddWithValue("@ReinicioLecturaVolumetrica", DBNull.Value)
            Else
                comando.Parameters.AddWithValue("@ReinicioLecturaVolumetrica", txtEDReinicioVol.Text.Replace(",", "."))
            End If
            'Funciona
            comando.Parameters.AddWithValue("@Funciona", ddlEDfunciona.Text)
            comando.Parameters.AddWithValue("@login", txtEDUsuario.Text)
            comando.Parameters.AddWithValue("@Justificacion", "")
            'Caudal Medido
            If utiles.nullABlanco(txtEDCaudalMedido.Text) = "" Then
                comando.Parameters.AddWithValue("@CaudalMedido", DBNull.Value)
            Else
                comando.Parameters.AddWithValue("@CaudalMedido", Replace(txtEDCaudalMedido.Text, ",", "."))
            End If

            

            'Dim memStream As System.IO.MemoryStream = New MemoryStream()
            'Dim sWriter As BinaryWriter = New BinaryWriter(memStream)
            'For Each b As Byte In FileUpload1.FileBytes
            ' sWriter.Write(b)
            ' Next

            'sWriter.Flush()

            ''writing to file to verify file stream converted correctly
            'Dim fstream As FileStream = New FileStream("C:/temp/" + FileUpload1.FileName, FileMode.Create)
            'memStream.WriteTo(fstream)
            'fstream.Dispose()
            'Dim lectura As SicaSegura.SICA_Lectura = New SicaSegura.SICA_Lectura(Date.Parse(TXTFechaMedida.Text), null)

            comando.ExecuteNonQuery()

            objTrans.Commit()

        Catch Exc As System.Data.SqlClient.SqlException
            objTrans.Rollback()
            Select Case Exc.Number
                Case 547
                    'Response.Write("Error: " & Exc.Message & " num: " & Exc.Number)
                    Alert(Page, "No existe un elemento de medida para el punto con fecha medida: " & TXTFechaMedida.Text)
                Case 2627
                    Alert(Page, "El dato motor ya existe para la fecha medida: " & TXTFechaMedida.Text)
            End Select
        Catch Exc As Exception
            objTrans.Rollback()
            Alert(Page, "Error: " & Exc.Message)
        End Try

        limpiar_campos_edicion()
        Response.Redirect("lecturas.aspx")

        
    End Sub

End Class
