Imports System.Data
Imports System.Data.SqlClient
Imports GuarderiaFluvial.utiles

Partial Class Datos
    Inherits System.Web.UI.Page
    Dim conexion As SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsn"))
    Dim comando As New SqlCommand("", conexion)
    Dim codigoPVYCR, em As String
    Dim Utiles As GuarderiaFluvial.utiles()


    Protected Sub btnAceptar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        Dim TOC, CFD As String
        Dim rc As Object
        CFD = "19"
        If conexion.State = ConnectionState.Closed Then conexion.Open()
        If Left(em, 1) = "Q" Then
            If txtNivel.Text <> "" Then
                txtNivel.Text = Replace(txtNivel.Text, ".", ",")
                TOC = "C"
                'Tenemos que buscar la curva de gasto. Buscaremos si el funcionario ha dado de alta la curva del Libro Digital, si no es así, pondremos la P0
                comando.CommandText = "Select count(*) from PVYCR_CurvasAcequias where codigoPVYCR='" & codigoPVYCR & "' and idElementoMedida='" & em & "' and Regimen='LD'"
                If nullACero(comando.ExecuteScalar) > 0 Then
                    rc = "LD"
                Else
                    rc = "P0"
                End If
            Else
                TOC = "Q"
                rc = System.DBNull.Value
            End If
        End If

        'Insertamos en las tablas correspondientes
        Select Case Left(em, 1)
            Case "Q"
                comando.CommandText = "insert into [PVYCR_DatosAcequias_Estadillo] (codigoPVYCR, idElementoMedida, Cod_Fuente_Dato, Fecha_Medida, " & _
                            "Caudal_M3S, TipoObtencionCaudal, Calado_M) VALUES (@codigoPVYCR, @idElementoMedida, @Cod_Fuente_Dato, @Fecha_Medida, " & _
                            "@Caudal_M3S, @TipoObtencionCaudal,@calado)"

                If Not rc Is System.DBNull.Value Then
                    comando.Parameters.AddWithValue("@RegimenCurva", rc)
                Else
                    comando.CommandText = "insert into [PVYCR_DatosAcequias_Estadillo] (codigoPVYCR, idElementoMedida, Cod_Fuente_Dato, Fecha_Medida, " & _
                            "Caudal_M3S, TipoObtencionCaudal,login) VALUES (@codigoPVYCR, @idElementoMedida, @Cod_Fuente_Dato, @Fecha_Medida, " & _
                            "@Caudal_M3S, @TipoObtencionCaudal,@login)"
                End If
                comando.Parameters.AddWithValue("@Caudal_M3S", Convert.ToDouble(Replace(txtCaudal.Text, ".", ",")))
                comando.Parameters.AddWithValue("@TipoObtencionCaudal", TOC)
                comando.Parameters.AddWithValue("@calado", Convert.ToDouble(Replace(txtNivel.Text, ".", ",")))
            Case "E"
                comando.CommandText = "insert into [PVYCR_DatosAlimentacion_Estadillo] (codigoPVYCR, idElementoMedida, Cod_Fuente_Dato, Fecha_Medida, Total_kwh, login) " & _
                            "VALUES (@codigoPVYCR, @idElementoMedida, @Cod_Fuente_Dato, @Fecha_Medida, @totalkwh, @login)"
                comando.Parameters.AddWithValue("@totalkwh", Convert.ToDouble(Replace(txttotalKwh.Text, ".", ",")))
            Case "V"
                comando.CommandText = "insert into [PVYCR_DatosMotores_Estadillo] (codigoPVYCR, idElementoMedida, Cod_Fuente_Dato, Fecha_Medida, LecturaContador_M3, login) " & _
                            "VALUES (@codigoPVYCR, @idElementoMedida, @Cod_Fuente_Dato, @Fecha_Medida, @lectura, @login)"
                comando.Parameters.AddWithValue("@lectura", Convert.ToDouble(Replace(txtLecturaAlim.Text, ".", ",")))
            Case "H"
                comando.CommandText = "insert into [PVYCR_DatosHorometros_Estadillo] (codigoPVYCR, idElementoMedida, Cod_Fuente_Dato, Fecha_Medida, HorasIntervalo, login) " & _
                            "VALUES (@codigoPVYCR, @idElementoMedida, @Cod_Fuente_Dato, @Fecha_Medida, @horas, @login)"
                comando.Parameters.AddWithValue("@horas", Replace(txtHoras.Text, ",", "."))
        End Select
        comando.Parameters.AddWithValue("@codigoPVYCR", codigoPVYCR)
        comando.Parameters.AddWithValue("@idElementoMedida", em)
        comando.Parameters.AddWithValue("@Cod_Fuente_Dato", CFD)
        comando.Parameters.AddWithValue("@Fecha_Medida", txtFecha.Text & " 00:00")
        comando.Parameters.AddWithValue("@login", Session("nombreUsuario"))

        Try
            comando.ExecuteNonQuery()
            conexion.Close()
            ClientScript.RegisterStartupScript(Page.GetType(), "Alerta", "<script language='javascript'>alert('La lectura ha sido insertada con éxito');parent.document.getElementById('lecturas').src='Lecturas.aspx?codigoPVYCR=" & codigoPVYCR & "&em=" & _
                em & "';</script>")
            BorrarDatos()
        Catch ex As Exception
            Dim mensaje As String
            If InStr(ex.Message, "PRIMARY KEY") > 0 Then
                mensaje = "Esta lectura ya ha sido insertada"
            Else
                mensaje = "No se ha podido insertar la lectura"
            End If
            ClientScript.RegisterStartupScript(Page.GetType(), "Alerta", "<script language='javascript'>alert('" & mensaje & "');</script>")
            conexion.Close()
        End Try
        


    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.QueryString("codigoPVYCR") <> "" Then
            Session("codigoPVYCR") = Request.QueryString("codigoPVYCR")
        End If
        If Request.QueryString("em") <> "" Then
            Session("em") = Request.QueryString("em")
        End If

        codigoPVYCR = Session("codigoPVYCR")
        em = Session("em")

        lblTitulo.Text = "<B>INSERCIÓN DE DATOS DEL: " & codigoPVYCR & " - " & em

        Select Case Left(em, 1)
            Case "Q"
                pnlAcequias.Visible = True
                RequiredFieldValidator2.Visible = True
            Case "V"
                pnlAlimentacion.Visible = True
                RequiredFieldValidator3.Visible = True
            Case "E"
                pnlMotores.Visible = True
                RequiredFieldValidator4.Visible = True
            Case "H"
                pnlHorometros.Visible = True
                RequiredFieldValidator5.Visible = True
        End Select
    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        BorrarDatos()
    End Sub

    Protected Sub BorrarDatos()
        txtCaudal.Text = ""
        txtFecha.Text = ""
        txtHoras.Text = ""
        txttotalKwh.Text = ""
        txtLecturaAlim.Text = ""
        txtNivel.Text = ""
    End Sub

    Protected Sub calendario_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles calendario.Load
        'Comprobamos que no sea al cargar
        If calendario.SelectedDate.Year <> 1 Then txtFecha.Text = calendario.SelectedDate
    End Sub

    Protected Sub calendario_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles calendario.SelectionChanged
        txtFecha.Text = calendario.SelectedDate & " 00:00"
    End Sub
End Class
