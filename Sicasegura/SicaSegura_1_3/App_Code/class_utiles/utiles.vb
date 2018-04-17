Imports System.Web.UI.WebControls
Imports System.Data
Imports System.Object

Namespace GuarderiaFluvial

    Public Class utiles
        Shared Function nullABlanco(ByVal valor As Object)
            'Esta función devuelve la cadena vacía en caso de que el parámetro pasado sea nulo, para evitar errores por conversión de tipos
            If valor Is System.DBNull.Value Then
                Return ""
            Else
                Return valor
            End If
        End Function

        Shared Function BlancoANull(ByVal valor As String)
            'Esta función devuelve el valor null si el valor pasado es la cadena vacía para evitar errores de fechas al almacenar en la base de datos, etc.
            If valor = "" Then
                Return System.DBNull.Value
            Else
                Return valor
            End If
        End Function

        Shared Function nullACero(ByVal valor As Object)
            'Esta función devuelve la cadena vacía en caso de que el parámetro pasado sea nulo, para evitar errores por conversión de tipos
            If valor Is System.DBNull.Value Then
                Return 0
            Else
                Return valor
            End If
        End Function

        Shared Function nullAFalse(ByVal valor As Object)
            'Esta función devuelve la cadena vacía en caso de que el parámetro pasado sea nulo, para evitar errores por conversión de tipos
            If valor Is System.DBNull.Value Then
                Return False
            Else
                Return valor
            End If
        End Function
        Shared Function nullATrue(ByVal valor As Object)
            'Esta función devuelve la cadena vacía en caso de que el parámetro pasado sea nulo, para evitar errores por conversión de tipos
            If valor Is System.DBNull.Value Then
                Return True
            Else
                Return valor
            End If
        End Function

        Shared Function nullaFechaMin(ByVal valor As Object)
            If valor Is System.DBNull.Value Then
                Return DateTime.MinValue
            Else
                Return valor
            End If
        End Function

        Shared Function ValorLista(ByVal lista As System.Web.UI.WebControls.DropDownList)
            'Esta función devuelve el valor de un control dropdownlist comprobando que haya un item seleccionado; si no, devuelve el valor null
            If lista.SelectedItem Is Nothing Then
                Return System.DBNull.Value
            Else
                Return lista.SelectedItem.Value
            End If
        End Function

        Shared Function nothingABlanco(ByVal valor As Object)
            'Esta función devuelve la cadena vacía en caso de que el parámetro pasado sea nulo, para evitar errores por conversión de tipos
            If IsNothing(valor) Then
                Return ""
            Else
                Return valor
            End If
        End Function

        Shared Function Encripta(ByVal valor As String)
            'Esta función encripta un password: 
            'letra = Al valor de cada letra en Ascii le suma 3
            'letra2 = Al valor de cada letra en Ascii le resta 5, de atrás adelante.

            '¡¡¡¡¡¡¡¡¡¡¡¡CUIDADO CON ESTA FUNCIÓN!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            'Se encuentra duplicada en el proyecto GeoCampo de la PDA. Cualquier cambio aquí
            'deberá verse reflejado en la función del otro proyecto.
            'El archivo donde se encuentra la función es el archivo Crypt.cs del proyecto "ClasesCE"

            Dim letra, letra2 As String
            letra = ""
            letra2 = ""
            Dim i As Integer

            If valor <> "" Then
                For i = 1 To Len(valor)
                    letra &= Chr(Asc(Mid(valor, i, 1)) + 3)
                    letra2 &= Chr(Asc(Mid(valor, Len(valor) - i + 1, 1)) - 5)
                Next
                If Asc(letra) = 27 Then letra = "&27;"
                If Asc(letra2) = 27 Then letra2 = "&27;"
                valor = letra & letra2
            End If

            Return valor
        End Function

        Shared Function DesEncripta(ByVal valor As String)
            'Esta función desEncripta un password
            'Se hace la inversa de encripta, pero usando sólo la variable letra2
            Dim letra As String = ""
            Dim i As Integer
            For i = 1 To Len(valor) / 2
                letra &= Chr(Asc(Mid(valor, Len(valor) - i + 1, 1)) + 5)
            Next
            valor = letra
            Return valor
        End Function

        Shared Sub EscribeLog(ByVal idTarea As Integer, ByVal idPDA As String, ByVal sql As String, ByVal conexion As System.Data.SqlClient.SqlConnection, ByVal tipo As Integer, Optional ByVal idEstado As Integer = 0, _
                                Optional ByVal idUsuario As Integer = 0, Optional ByVal idUsuarioAnterior As Integer = 0, Optional ByVal idUsuarioDestino As Integer = 0)
            Dim comandotmp As System.Data.SqlClient.SqlCommand = New System.Data.SqlClient.SqlCommand("", conexion)

            If idTarea > 0 Then
                'Si la tarea ya estaba creada, ya tenemos el idTarea
                comandotmp.CommandText = "insert into TTareasLog(idTarea,idPDA,tipo,fecha,idEstado,idUsuario,idUsuarioAnterior,idUsuarioDestino,sentenciaSQL) values(@idTarea,@idPDA,@tipo,getdate(),@idEstado,@idUsuario,@idUsuarioAnterior,@idUsuarioDestino,@sentenciaSQL)"
            Else
                'Si la tarea es nueva, hallamos el último id insertado
                comandotmp.CommandText = "insert into TTareasLog(idTarea,idPDA,tipo,fecha,idEstado,idUsuario,idUsuarioAnterior,idUsuarioDestino,sentenciaSQL) values(dbo.GetIdTarea('" & idPDA & "')-1,@idPDA,@tipo,getdate(),@idEstado,@idUsuario,@idUsuarioAnterior,@idUsuarioDestino,@sentenciaSQL)"
            End If

            comandotmp.Parameters.AddWithValue("@idTarea", idTarea)
            comandotmp.Parameters.AddWithValue("@idPDA", idPDA)
            comandotmp.Parameters.AddWithValue("@tipo", tipo)
            comandotmp.Parameters.AddWithValue("@idEstado", idEstado)
            comandotmp.Parameters.AddWithValue("@idUsuario", idUsuario)
            comandotmp.Parameters.AddWithValue("@idUsuarioAnterior", idUsuarioAnterior)
            comandotmp.Parameters.AddWithValue("@idUsuarioDestino", idUsuarioDestino)
            comandotmp.Parameters.AddWithValue("@sentenciaSQL", sql)

            comandotmp.ExecuteNonQuery()

        End Sub

        Shared Sub EscribeLog(ByVal idTarea As Integer, ByVal idPDA As String, ByVal elComando As System.Data.SqlClient.SqlCommand, ByVal conexion As System.Data.SqlClient.SqlConnection, ByVal tipo As Integer, Optional ByVal idEstado As Integer = 0, _
                        Optional ByVal idUsuario As Integer = 0, Optional ByVal idUsuarioAnterior As Integer = 0, Optional ByVal idUsuarioDestino As Integer = 0)
            'Este procedimiento escribe una acción sobre el log de tareas, generando la sentencia sql sustituyendo los parámetros por sus valores
            Dim comandotmp As System.Data.SqlClient.SqlCommand = New System.Data.SqlClient.SqlCommand("", conexion)

            Dim elSQL As String = ""
            Dim i As Integer
            elSQL = elComando.CommandText
            For i = 0 To elComando.Parameters.Count - 1
                elSQL = elSQL.Replace(elComando.Parameters(i).ParameterName, "'" & elComando.Parameters(i).Value.ToString().Replace("'", "''") & "'")
            Next

            If Not IsNothing(elComando.Transaction) Then
                comandotmp.Transaction = elComando.Transaction
            End If

            If idTarea > 0 Then
                'Si la tarea ya estaba creada, ya tenemos el idTarea
                comandotmp.CommandText = "insert into TTareasLog(idTarea,idPDA,tipo,fecha,idEstado,idUsuario,idUsuarioAnterior,idUsuarioDestino,sentenciaSQL) values(@idTarea,@idPDA,@tipo,getdate(),@idEstado,@idUsuario,@idUsuarioAnterior,@idUsuarioDestino,@sentenciaSQL)"
            Else
                'Si la tarea es nueva, hallamos el último id insertado
                comandotmp.CommandText = "insert into TTareasLog(idTarea,idPDA,tipo,fecha,idEstado,idUsuario,idUsuarioAnterior,idUsuarioDestino,sentenciaSQL) values(dbo.GetIdTarea('" & idPDA & "')-1,@idPDA,@tipo,getdate(),@idEstado,@idUsuario,@idUsuarioAnterior,@idUsuarioDestino,@sentenciaSQL)"
            End If

            comandotmp.Parameters.AddWithValue("@idTarea", idTarea)
            comandotmp.Parameters.AddWithValue("@idPDA", idPDA)
            comandotmp.Parameters.AddWithValue("@tipo", tipo)
            comandotmp.Parameters.AddWithValue("@idEstado", idEstado)
            comandotmp.Parameters.AddWithValue("@idUsuario", idUsuario)
            comandotmp.Parameters.AddWithValue("@idUsuarioAnterior", idUsuarioAnterior)
            comandotmp.Parameters.AddWithValue("@idUsuarioDestino", idUsuarioDestino)
            comandotmp.Parameters.AddWithValue("@sentenciaSQL", elSQL)

            comandotmp.ExecuteNonQuery()

        End Sub

        ''' <summary>
        ''' Función que devuelve si elusuario tiene permiso para acceder a una determinada parte de la aplicación o no
        ''' </summary>
        ''' <param name="idUsuario">El identificador del usuario en cuestión</param>
        ''' <param name="npermiso">El nombre del permiso (en realidad, el nombre del campo en la tabla) requerido</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Shared Function Permiso(ByVal idUsuario As Integer, ByVal npermiso As String) As Boolean

            Dim conexion As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationManager.AppSettings("dsn"))
            Dim comando As System.Data.SqlClient.SqlCommand = New System.Data.SqlClient.SqlCommand("", conexion)

            If conexion.State = Data.ConnectionState.Closed Then conexion.Open()

            comando.CommandText = "select administrador from tperfiles P join tusuarios U on U.idPerfil=P.id where U.id=0" & idUsuario
            If Not IsNothing(comando.ExecuteScalar) Then
                If utiles.nullAFalse(comando.ExecuteScalar) Then
                    Return utiles.nullAFalse(comando.ExecuteScalar) 'Si tiene permiso de administrador, devolvemos permiso siempre
                End If
            End If
            comando.CommandText = "select " & npermiso & " from tperfiles P join tusuarios U on U.idPerfil=P.id where U.id=0" & idUsuario
            If Not IsNothing(comando.ExecuteScalar) Then
                conexion.Close()
                Return utiles.nullAFalse(comando.ExecuteScalar)
            Else
                conexion.Close()
                Return False
            End If

        End Function

        Shared Function esAdministrador(ByVal idUsuario As Integer)
            Dim conexion As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationManager.AppSettings("dsn"))
            Dim comando As System.Data.SqlClient.SqlCommand = New System.Data.SqlClient.SqlCommand("", conexion)
            Dim valor As Integer = 0

            If conexion.State = Data.ConnectionState.Closed Then conexion.Open()

            comando.CommandText = "select p.id from tperfiles P join tusuarios U on U.idPerfil=P.id where U.id=0" & idUsuario
            valor = comando.ExecuteScalar
            If valor = 14 Then
                conexion.Close()
                Return True 'Si tiene permiso de administrador, devolvemos permiso siempre
            Else
                conexion.Close()
                Return False
            End If

        End Function

        Shared Function esFuncionario(ByVal idUsuario As Integer)
            Dim conexion As System.Data.SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationManager.AppSettings("dsn"))
            Dim comando As System.Data.SqlClient.SqlCommand = New System.Data.SqlClient.SqlCommand("", conexion)

            If conexion.State = Data.ConnectionState.Closed Then conexion.Open()

            comando.CommandText = "select idEmpresa from tusuarios U where U.id=0" & idUsuario
            If nullACero(comando.ExecuteScalar) = 0 Or nullACero(comando.ExecuteScalar) = 5 Then
                conexion.Close()
                Return True
            Else
                conexion.Close()
                Return False
            End If

        End Function

        Shared Function GradienteNivelesAzules(ByVal nivel As Integer) As String
            Select Case nivel
                Case 0
                    GradienteNivelesAzules = "darkblue"
                Case 1
                    GradienteNivelesAzules = "mediumblue"
                Case 2
                    GradienteNivelesAzules = "royalblue"
                Case 3
                    GradienteNivelesAzules = "dodgerblue"
                Case 4
                    GradienteNivelesAzules = "cornflowerblue"
                Case 5
                    GradienteNivelesAzules = "skyblue"
                Case 6
                    GradienteNivelesAzules = "lightskyblue"
                Case Else
                    GradienteNivelesAzules = "lightblue"

            End Select
        End Function
        Shared Function GradienteNivelesGranates(ByVal nivel As Integer) As String
            Select Case nivel
                Case 0
                    GradienteNivelesGranates = "DarkRed"
                Case 1
                    GradienteNivelesGranates = "Crimson" 
                Case 2
                    GradienteNivelesGranates = "Tomato"
                Case Else
                    GradienteNivelesGranates = "Coral"

            End Select
        End Function

        Shared Sub Comprobar_Conexion_BD(ByVal page As Page, ByVal conexion As System.Data.SqlClient.SqlConnection)
            Dim ruta As String
            Try
                'Throw New Exception
                If conexion.State = ConnectionState.Closed Then conexion.Open()
            Catch ex As Exception
                'If page.Request.ServerVariables("PATH_INFO").IndexOf("SICAH") > 0 Then
                '    ruta = "/sicasegura"
                'Else
                '    ruta = ""
                'End If
                conexion.Close()
                'para probar akí la ruta es 
                'page.Response.Redirect("/sicasegura/ErrorConexion.aspx?pagina=" & page.AppRelativeVirtualPath)
                'para producción es sin sicasegura.
                page.Response.Redirect("/ErrorConexion.aspx?pagina=" & page.AppRelativeVirtualPath)

            End Try

        End Sub

        Shared Sub Comprobar_Conexion_BD(ByVal page As Page, ByVal conexion As System.Data.SqlClient.SqlConnection, ByVal conexionUsuarios As Boolean)
            Dim ruta As String
            Try
                'Throw New Exception
                If conexion.State = ConnectionState.Closed Then conexion.Open()
      Catch ex As Exception
        'If page.Request.ServerVariables("PATH_INFO").IndexOf("SICAH") > 0 Then
        '    ruta = "/sicasegura"
        'Else
        '    ruta = ""
        'End If
        conexion.Close()
        'para probar akí la ruta es 
                page.Response.Redirect("/sicasegura/ErrorConexion.aspx?pagina=" & page.AppRelativeVirtualPath)
                'para producción es sin sicasegura.
                'page.Response.Write(ex.Message)
                Const fic As String = "c:\Aplicaciones\SicaSegura\root\ErrorSica.txt"
                Dim texto As String = ex.Message

                Dim sw As New System.IO.StreamWriter(fic)
                sw.WriteLine(texto)
                sw.Close()

                'page.Response.Redirect("/ErrorConexionUsuarios.aspx?pagina=" & page.AppRelativeVirtualPath)

            End Try

        End Sub
        Shared Function CerrarConexion(ByVal conexion As System.Data.SqlClient.SqlConnection)
            If conexion.State = ConnectionState.Open Then conexion.Close()
        End Function
        Shared Function QuitarRegistrosSegunPorcentaje(ByVal dst As System.Data.DataSet, ByVal nombreTabla As String, ByVal campo As String, ByVal porcentaje As Integer) As System.Data.DataSet
            Dim dstNuevo As New DataSet
            Dim i As Integer = 0, total As Integer = 0, j As Integer = 0
            Dim dt As DataTable
            dt = dst.Tables(nombreTabla)

            For i = 1 To dt.Rows.Count - 2
                If nullACero(dt.Rows(i - 1).Item(campo)) <> 0 Then
                    If (Math.Abs(nullACero(dt.Rows(i).Item(campo)) - dt.Rows(i - 1).Item(campo)) / dt.Rows(i - 1).Item(campo)) < (porcentaje / 100) And _
                    (Math.Abs(nullACero(dt.Rows(i + 1).Item(campo)) - dt.Rows(i - 1).Item(campo)) / dt.Rows(i - 1).Item(campo)) < (porcentaje / 100) Then
                        dt.Rows(i).Delete()
                        i = i - 1
                    End If
                Else
                    If nullACero(dt.Rows(i).Item(campo)) <> 0 Then
                        If (Math.Abs(nullACero(dt.Rows(i).Item(campo)) - nullACero(dt.Rows(i - 1).Item(campo))) / dt.Rows(i).Item(campo)) < (porcentaje / 100) And _
                                            (Math.Abs(nullACero(dt.Rows(i + 1).Item(campo)) - nullACero(dt.Rows(i - 1).Item(campo))) / dt.Rows(i).Item(campo)) < (porcentaje / 100) Then
                            dt.Rows(i).Delete()
                            i = i - 1
                        End If
                    End If
                End If
                If i >= dt.Rows.Count - 2 Then
                    Exit For
                End If
            Next

            dstNuevo = dst.Copy
            Return dstNuevo

        End Function
        Shared Function CalculaNIF(ByVal strA As String) As String
            '----------------------------------------------------------------------
            ' Calcular la letra del NIF
            ' Código original adaptado a Visual Basic                   (13/Sep/95)
            ' Adaptado a Visual Basic .NET (VB 9.0/2008)                (09/May/08)
            ' y convertido en función que devuelve el NIF correcto
            '----------------------------------------------------------------------
            Const cCADENA As String = "TRWAGMYFPDXBNJZSQVHLCKE"
            Const cNUMEROS As String = "0123456789"
            Dim a, b, c, NIF As Integer
            Dim sb As New StringBuilder

            strA = Trim(strA)
            If Len(strA) = 0 Then Return ""

            ' Dejar sólo los números
            For i As Integer = 0 To strA.Length - 1
                If cNUMEROS.IndexOf(strA(i)) > -1 Then
                    sb.Append(strA(i))
                End If
            Next

            strA = sb.ToString
            a = 0
            NIF = CInt(Val(strA))
            Do
                b = CInt(Int(NIF / 24))
                c = NIF - (24 * b)
                a = a + c
                NIF = b
            Loop While b <> 0
            b = CInt(Int(a / 23))
            c = a - (23 * b)

            Return strA & Mid(cCADENA, CInt(c + 1), 1)
        End Function
        ''' <summary>
        ''' Comprueba si es un NIF válido
        ''' No usar espacios ni separadores para la letra
        ''' Devuelve True si es correcto
        ''' </summary>
        Shared Function Verificar_NIF(ByVal valor As String) As Boolean
            Dim aux As String

            valor = valor.ToUpper ' ponemos la letra en mayúscula
            aux = valor.Substring(0, valor.Length - 1) ' quitamos la letra del NIF

            If aux.Length >= 7 AndAlso IsNumeric(aux) Then
                aux = CalculaNIF(aux) ' calculamos la letra del NIF para comparar con la que tenemos
            Else
                Return False
            End If

            If valor <> aux Then ' comparamos las letras
                Return False
            End If

            Return True
        End Function
        ''' <summary>
        ''' Comprueba si es un CIF
        ''' Devuelve True si es un CIF
        ''' </summary>
        ''' <remarks>
        ''' Adaptado de un código de SGF
        ''' http://www.elguille.info/colabora/vb/sgf_Verificar_NIF_CIF.htm
        ''' </remarks>
        Shared Function Verificar_CIF(ByVal valor As String) As Boolean
            Dim strLetra As String, strNumero As String, strDigit As String
            Dim strDigitAux As String
            Dim auxNum As Integer
            Dim i As Integer
            Dim suma As Integer
            Dim letras As String

            letras = "ABCDEFGHKLMPQSX"

            valor = UCase(valor)

            If Len(valor) < 9 OrElse Not IsNumeric(Mid(valor, 2, 7)) Then
                Return False
            End If

            strLetra = Mid(valor, 1, 1)     ' letra del CIF
            strNumero = Mid(valor, 2, 7)    ' Codigo de Control
            strDigit = Mid(valor, 9)        ' CIF menos primera y ultima posiciones

            If InStr(letras, strLetra) = 0 Then ' comprobamos la letra del CIF (1ª posicion)
                Return False
            End If

            For i = 1 To 7
                If i Mod 2 = 0 Then
                    suma = suma + CInt(Mid(strNumero, i, 1))
                Else
                    auxNum = CInt(Mid(strNumero, i, 1)) * 2
                    suma = suma + (auxNum \ 10) + (auxNum Mod 10)
                End If
            Next

            suma = (10 - (suma Mod 10)) Mod 10

            Select Case strLetra
                Case "K", "P", "Q", "S"
                    suma = suma + 64
                    strDigitAux = Chr(suma)
                Case "X"
                    strDigitAux = Mid(CalculaNIF(strNumero), 8, 1)
                Case Else
                    strDigitAux = CStr(suma)
            End Select

            If strDigit = strDigitAux Then
                Return True
            Else
                Return False
            End If
        End Function

        Shared Sub borrarFicheros(ByVal ruta As String)
            If System.IO.Directory.Exists(ruta) Then
                Dim ficheros As String() = System.IO.Directory.GetFiles(ruta)
                For Each fichero As String In ficheros
                    If System.IO.File.GetCreationTime(fichero) < Now.AddHours(-1) Then
                        System.IO.File.Delete(fichero)
                    End If
                Next
            End If
        End Sub

        ''' <summary>
        ''' Si el usuario es administrador o grabador de sica devuelve un true
        ''' sino devuleve un false
        ''' </summary>
        Shared Function VisibleSegunPerfil(ByVal idPerfil As Integer) As Boolean
            If idPerfil = 1 Or idPerfil = 11 Or idPerfil = 14 Then
                Return True
            Else
                Return False
            End If

        End Function
        Shared Function InsertarLog(ByVal tipoLog As String, ByVal login As String, ByVal conexion As System.Data.SqlClient.SqlConnection, ByVal page As Page)
            Dim comando As SqlClient.SqlCommand = New SqlClient.SqlCommand("", conexion)
            Dim objTrans As System.Data.SqlClient.SqlTransaction
            Try
                utiles.Comprobar_Conexion_BD(Page, conexion)
                'abrimos una transaccion para que si falla algunos de los inserts haga un rollback
                objTrans = conexion.BeginTransaction()
                comando.Transaction = objTrans
                If tipoLog = "E" Then
                    comando.CommandText = "INSERT INTO TLOG_PVY(login,fechaConexion) " & _
                        "values (@login,@fechaConexion)"

                    comando.Parameters.AddWithValue("@login", login)
                    comando.Parameters.AddWithValue("@FechaConexion", DateTime.Now)
                Else
                    comando.CommandText = "UPDATE TLOG_PVY SET fechaDesConexion = @fechaDesConexion where id = " & _
                    "(select max(id) from tlog_pvy where login = '" & login & "') "

                    comando.Parameters.AddWithValue("@FechaDesConexion", DateTime.Now)
                End If

                comando.ExecuteNonQuery()
                objTrans.Commit()
                'Catch Exc As System.Data.SqlClient.SqlException
                '    objTrans.Rollback()
                '    Select Case Exc.Number
                '        Case 547
                '            'Response.Write("Error: " & Exc.Message & " num: " & Exc.Number)
                '            'Alert(Page, "No existe un elemento de medida para el punto con fecha: " & v_Fecha)
                '            mensaje = mensaje & "No existe un elemento de medida para el punto con fecha: " & v_Fecha & " \n"
                '        Case 2627
                '            'Alert(Page, "El dato Alimentacion ya existe para la fecha: " & v_Fecha)
                '            mensaje = mensaje & "El dato Alimentacion ya existe para la fecha: " & v_Fecha & " \n"
                '    End Select
            Catch Exc As Exception
                objTrans.Rollback()
                JavaScript.Alert(page, "Error: " & Exc.Message)
            End Try
        End Function
    End Class
End Namespace
