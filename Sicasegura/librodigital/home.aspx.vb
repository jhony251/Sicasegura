Imports System.Data
Imports System.Data.SqlClient
Imports Utiles

Partial Class home
    Inherits System.Web.UI.Page
    Dim conexion As SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsn"))
    Dim conexion_dbsica As SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsn_dbsica"))
    Dim da As SqlDataAdapter = New SqlDataAdapter("", conexion)
    Dim da_dbsica As SqlDataAdapter = New SqlDataAdapter("", conexion_dbsica)
    Dim dst As DataSet = New DataSet
    Dim comando As New SqlCommand("", conexion)
    Dim comando_dbsica As New SqlCommand("", conexion_dbsica)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim htmlcabecera As String
        arbol.Text = Me.get_arbol_aprovechamiento
        Dim nombrePDF As String = "informes.aspx"

        htmlcabecera = "<table width=100% borde=1 class='cabecera'><tr><td width='10%'>"
        htmlcabecera = htmlcabecera & Session.Item("USERNAME") & "</td>"

        htmlcabecera = htmlcabecera & "<td width='10%'></td>"
        htmlcabecera = htmlcabecera & "<td width='55%' align='right' style='padding-right:50px;'><a href='" & nombrePDF & "' target='_blank'><img src='images/pdf.png' border='0'></a></td>"
        htmlcabecera = htmlcabecera & "<td width='10%'>" & Me.get_prevision_meteorologica & "</td>"
        htmlcabecera = htmlcabecera & "<td width='15%' valign='middle'>" & Me.get_notificaciones & "</td>"
        htmlcabecera = htmlcabecera & "</tr><table>"
        Cabecera.Text = htmlcabecera
    End Sub
    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        'If ucPaginacion.lblMuevetext = "si" Then
        'crearDataSets()
        '    ucPaginacion.lblMuevetext = "no"
        'End If
    End Sub
    Private Function get_notificaciones() As String
        Return "<table><tr><td valign='middle'><img src='Resources/Images/sobre.png' width=20px></td><td valign='middle'>Tiene X notificaciones</td></tr></table>"
    End Function
    Private Function get_prevision_meteorologica() As String
        Return "<table><tr><td valign='middle'><a href='http://www.aemet.es/es/eltiempo/prediccion/municipios/murcia-id30030'><img border=0 width=20px src='Resources/Images/meteorologia.png'></a></td><td valign='middle'> Previsión</td></tr></table>"

    End Function
    Private Function get_arbol_aprovechamiento() As String
        Dim arbol As String
        arbol = ""
        Dim codigoCauce As String
        Dim codigopvycr_ant As String = ""

        da_dbsica.SelectCommand.CommandText = "Select s.* from sica_sist_sistemas S inner join [sica_sist_sistemas-Usuarios] SU on S.id=SU.id_sistema and " & _
                        "id_usuario=" & Session("id_usuario")
        da_dbsica.Fill(dst, "TablaSistemas")
        arbol = arbol & "<ul>"
        For i As Integer = 0 To dst.Tables("TablaSistemas").Rows.Count - 1
            If i > 0 Then arbol = arbol & "</li></ul>"
            arbol = arbol & "<li><b>" & dst.Tables("TablaSistemas").Rows(i).Item("Descripcion") & "</b><ul>"
            da_dbsica.SelectCommand.CommandText = "Select p.* from sica_sist_puntosistema P inner join [sica_sist_sistemas-punto] SP on p.id=sp.id_puntosistema " & _
                        "where id_sistema=" & dst.Tables("TablaSistemas").Rows(i).Item("id") & " order by codigopvycr"
            If dst.Tables.Contains("TablaPuntos") = True Then dst.Tables.Remove("TablaPuntos")
            da_dbsica.Fill(dst, "TablaPuntos")

            For j As Integer = 0 To dst.Tables("TablaPuntos").Rows.Count - 1
                If codigopvycr_ant <> dst.Tables("TablaPuntos").Rows(j).Item("codigopvycr") Then
                    If codigopvycr_ant <> "" Then arbol = arbol & "</ul></li>"
                    'Vamos a buscar los datos del cauce
                    If conexion.State = ConnectionState.Closed Then conexion.Open()
                    If dst.Tables.Contains("TablaCauces") Then dst.Tables.Remove("TablaCauces")
                    comando.CommandText = "select codigocauce from pvycr_puntos where codigopvycr='" & dst.Tables("TablaPuntos").Rows(j).Item("codigopvycr") & "'"
                    codigoCauce = comando.ExecuteScalar

                    da.SelectCommand.CommandText = "SElect NombreTitular, NIFTitular, DireccionTitular, MunicipioTitular, CodPostalTitular, " & _
                        "ProvinciaTitular, TelefonoTitular, ReferenciaExpedienteLibroAprovechamiento, NumeroRegistroAntiguo, ReferenciaExpedientesRegistroAguas, " & _
                        "OtrosExpedientes, Seccion, Tomo, Hoja, Inscripcion, Inscripcion_estado, Inscripcion_Relacionada from PVYCR_Cauces where " & _
                        "CodigoCauce='" & codigoCauce & "'"
                    da.Fill(dst, "TablaCauces")

                    'Ahora ya tenemos los elementos de medida, vamos a buscar los puntos de cont    rol y sus descripciones en la b.d. de sica
                    comando.CommandText = "Select denominacionPunto from PVYCR_Puntos " & _
                            "where codigopvycr='" & dst.Tables("TablaPuntos").Rows(j).Item("codigopvycr") & "'"

                    codigopvycr_ant = dst.Tables("TablaPuntos").Rows(j).Item("codigopvycr")
                    arbol = arbol & "<li><b>" & dst.Tables("TablaPuntos").Rows(j).Item("codigopvycr") & "</b> " & comando.ExecuteScalar & "<ul>"
                End If

                arbol = arbol & "<li><img src='Resources/Images/iconosEM/" & _
                ObtenerIcono(dst.Tables("TablaPuntos").Rows(j).Item("codigoPVYCR"), Left(dst.Tables("TablaPuntos").Rows(j).Item("em"), 1), dst.Tables("TablaPuntos").Rows(j).Item("em")) & _
                "'><a href='#' onclick=""document.getElementById('datos').src='Datos.aspx?codigoPVYCR=" & dst.Tables("TablaPuntos").Rows(j).Item("codigopvycr") & "&em=" & _
                dst.Tables("TablaPuntos").Rows(j).Item("em") & "';document.getElementById('lecturas').src='Lecturas.aspx?codigoPVYCR=" & dst.Tables("TablaPuntos").Rows(j).Item("codigopvycr") & "&em=" & _
                dst.Tables("TablaPuntos").Rows(j).Item("em") & "';document.getElementById('datos').style.display='block';javascript:rellenarInformacion('" & _
                dst.Tables("TablaCauces").Rows(0).Item("NombreTitular") & "','" & dst.Tables("TablaCauces").Rows(0).Item("NIFTitular") & "','" & _
                dst.Tables("TablaCauces").Rows(0).Item("DireccionTitular") & "','" & dst.Tables("TablaCauces").Rows(0).Item("MunicipioTitular") & "','" & _
                dst.Tables("TablaCauces").Rows(0).Item("CodPostalTitular") & "','" & dst.Tables("TablaCauces").Rows(0).Item("ProvinciaTitular") & "','" & _
                dst.Tables("TablaCauces").Rows(0).Item("TelefonoTitular") & "','" & dst.Tables("TablaCauces").Rows(0).Item("ReferenciaExpedienteLibroAprovechamiento") & "','" & _
                dst.Tables("TablaCauces").Rows(0).Item("NumeroRegistroAntiguo") & "','" & dst.Tables("TablaCauces").Rows(0).Item("ReferenciaExpedientesRegistroAguas") & "','" & _
                dst.Tables("TablaCauces").Rows(0).Item("OtrosExpedientes") & "','" & dst.Tables("TablaCauces").Rows(0).Item("Seccion") & "','" & _
                dst.Tables("TablaCauces").Rows(0).Item("Tomo") & "','" & dst.Tables("TablaCauces").Rows(0).Item("Hoja") & "','" & _
                dst.Tables("TablaCauces").Rows(0).Item("Inscripcion") & "','" & dst.Tables("TablaCauces").Rows(0).Item("Inscripcion_estado") & "','" & _
                dst.Tables("TablaCauces").Rows(0).Item("Inscripcion_Relacionada") & "');""><b>" & dst.Tables("TablaPuntos").Rows(j).Item("codigopvycr") & _
                                "</b> - " & dst.Tables("TablaPuntos").Rows(j).Item("em") & "</a>"
                arbol = arbol & " " & TextoEM(dst.Tables("TablaPuntos").Rows(j).Item("em")) & "</li>"

                If j = dst.Tables("TablaPuntos").Rows.Count - 1 And i = dst.Tables("TablaSistemas").Rows.Count - 1 Then arbol = arbol & "</li>"
            Next

            If i = dst.Tables("TablaSistemas").Rows.Count - 1 Then
                arbol = arbol & "</li>"
            Else
                arbol = arbol & "</ul>"
            End If

            codigopvycr_ant = ""
        Next
        arbol = arbol & "</ul>"

        Return arbol
    End Function

    Private Function TextoEM(ByVal em As String) As String
        Select Case Left(em, 1)
            Case "Q"
                Return "Elemento de medida Caudalímetro"
            Case "E"
                Return "Elemento de medida Energía"
            Case "V"
                Return "Elemento de medida Volumétrico"
            Case "H"
                Return "Elemento de medida Horómetro"
            Case "D"
                Return "Elemento de medida Diferencial"
        End Select
    End Function

    Private Function ObtenerIcono(ByVal codigoPVYCR As String, ByVal tipoElemen As String, ByVal idElementoMedida As String) As String
        Dim tipoPunto, tipoIconoE As String
        Dim icono As String = "vacio14.png"
        tipoPunto = ObtenerTipoPunto(codigoPVYCR)
        tipoIconoE = ObtenerIconoPuntoE(codigoPVYCR, Trim(idElementoMedida))
        Select Case tipoPunto
            Case "G"
                Select Case tipoElemen
                    Case "Q"
                        Select Case tipoIconoE
                            Case "0"
                                icono = "icocuaQY14.png"
                            Case "1"
                                icono = "icocuaQO14.png"
                            Case "2"
                                icono = "icocuaQG14.png"
                            Case "3"
                                icono = "icocuaQB14.png"
                        End Select
                    Case "N"
                        Select Case tipoIconoE
                            Case "0"
                                icono = "icocuaNY14.png"
                            Case "1"
                                icono = "icocuaNO14.png"
                            Case "2"
                                icono = "icocuaNG14.png"
                            Case "3"
                                icono = "icocuaNB14.png"
                        End Select
                End Select 'tipo elemento
            Case "M"
                Select Case tipoElemen
                    Case "Q"
                        Select Case tipoIconoE
                            Case "0"
                                icono = "icocirQY14.png"
                            Case "1"
                                icono = "icocirQO14.png"
                            Case "2"
                                icono = "icocirQG14.png"
                            Case "3"
                                icono = "icocirQB14.png"
                        End Select
                    Case "V"
                        Select Case tipoIconoE
                            Case "0"
                                icono = "icocirVY14.png"
                            Case "1"
                                icono = "icocirVO14.png"
                            Case "2"
                                icono = "icocirVG14.png"
                            Case "3"
                                icono = "icocirVB14.png"
                        End Select
                    Case "E"
                        Select Case tipoIconoE
                            Case "0"
                                icono = "icocirEY14.png"
                            Case "1"
                                icono = "icocirEO14.png"
                            Case "2"
                                icono = "icocirEG14.png"
                            Case "3"
                                icono = "icocirEB14.png"
                        End Select
                    Case "H"
                        Select Case tipoIconoE
                            Case "0"
                                icono = "icocirHY14.png"
                            Case "1"
                                icono = "icocirHO14.png"
                            Case "2"
                                icono = "icocirHG14.png"
                            Case "3"
                                icono = "icocirHB14.png"
                        End Select
                End Select 'tipo elemento
        End Select 'tipo punto
        Return icono
    End Function

    Protected Function ObtenerTipoPunto(ByVal codigoPVYCR As String) As String
        'obtenemos el punto si es M o G
        Dim resultado As String
        Dim sentenciatipopunto = "select tipoPunto from PVYCR_puntos where codigoPVYCR = '" & codigoPVYCR & "'"
        If conexion.State = ConnectionState.Closed Then conexion.Open()
        comando.CommandText = sentenciatipopunto
        resultado = comando.ExecuteScalar
        conexion.Close()
        Return resultado
    End Function

    Protected Function ObtenerIconoPuntoE(ByVal codigoPVYCR As String, ByVal idElemento As String) As String
        'obtenemos tipo del icono para saber qué icono escoger.
        'si es tipo = 1 --> amarillos (xxxxY32)
        'si es tipo = 2 --> naranjas (xxxxO32)
        'si es tipo = 3 --> verdes (xxxxG32)
        'si es tipo = 4 --> azules (xxxxB32)
        Dim sentenciatipopunto = "select top 1 tipo from pvycr_clasificacionelementos where codigoPVYCR = '" & codigoPVYCR & "' and em = '" & idElemento & "' "
        If conexion_dbsica.State = ConnectionState.Closed Then conexion_dbsica.Open()
        comando_dbsica.CommandText = sentenciatipopunto
        Dim icono As String = comando_dbsica.ExecuteScalar
        conexion_dbsica.Close()

        If icono = Nothing Then
            icono = "0"
        End If
        Return icono
    End Function
End Class
