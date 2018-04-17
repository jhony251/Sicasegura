Imports Microsoft.VisualBasic
Imports System.Data
Imports GuarderiaFluvial

Public Class SICAH_FuncionesArbolExt
  '############## FUNCIONES ARBOL EXT ########################

  Shared Function ObtenerNivelNodo(ByVal idPadre As Integer, ByVal Pagina As Page, ByVal tipo As String) As Integer
        Dim conexion As SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
    Dim sentenciaSel As String = ""
        Dim da As SqlClient.SqlDataAdapter = New SqlClient.SqlDataAdapter("", conexion)
    Dim dst As DataSet = New DataSet()

    utiles.Comprobar_Conexion_BD(Pagina, conexion)
    Dim nivel As Integer
    Select Case tipo
      Case "C"
        sentenciaSel = "SELECT IdArbolPadre FROM PVYCR_Arbol WHERE IdArbol=" & idPadre
        da.SelectCommand.CommandText = sentenciaSel
        da.Fill(dst, "TablaArbolAuxiliar")

        If dst.Tables("TablaArbolAuxiliar").Rows.Count = 0 Then
          nivel = 0
        Else
          If dst.Tables("TablaArbolAuxiliar").Rows.Item(0).Item("IdArbolPadre") Is System.DBNull.Value Then
            nivel = 1
          Else
            nivel = ObtenerNivelNodo(dst.Tables("TablaArbolAuxiliar").Rows.Item(0).Item("IdArbolPadre"), Pagina, "C") + 1
          End If
        End If
        Return nivel
      Case "P"
        sentenciaSel = "SELECT X + 1 FROM PVYCR_Arbol WHERE IdArbol=" & idPadre
        da.SelectCommand.CommandText = sentenciaSel
        Return da.SelectCommand.ExecuteScalar()
      Case "E"
        sentenciaSel = "SELECT X + 1 FROM PVYCR_Arbol WHERE IdArbol=" & idPadre
        da.SelectCommand.CommandText = sentenciaSel
        Return da.SelectCommand.ExecuteScalar()
    End Select
  End Function
  Shared Function GetY(ByVal IdArbolPadre As Integer, ByVal x As Integer, ByVal tipo As String, ByVal pagina As Page) As Integer
    Dim conexion As SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
    Dim sentenciaSel As String = ""
    Dim da As SqlClient.SqlDataAdapter = New SqlClient.SqlDataAdapter("", conexion)
    Dim dst As DataSet = New DataSet()
    Dim y As Integer = 0
    utiles.Comprobar_Conexion_BD(pagina, conexion)
    sentenciaSel = "SELECT MAX(y) + 1 FROM PVYCR_Arbol WHERE idarbolpadre=" & IdArbolPadre & " and tipo = '" & tipo & "' AND X=" & x
    da.SelectCommand.CommandText = sentenciaSel
    y = utiles.nullACero(da.SelectCommand.ExecuteScalar)
    ' si es cero es porque no hay ningún cauce y la y valdrá 0, sino valdrá el max(y) + 1
    If y < 0 Then
      y = y + 1
    End If
    Return y

  End Function
  Shared Function ObtenerStrNodo(ByVal caucePadre As String, ByVal tipo As String, ByVal pagina As Page) As String
    Dim conexion As SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
    Dim sentenciaSel As String = ""
    Dim da As SqlClient.SqlDataAdapter = New SqlClient.SqlDataAdapter("", conexion)
    Dim dst As DataSet = New DataSet()
    utiles.Comprobar_Conexion_BD(pagina, conexion)
    If tipo = "E" Then
      sentenciaSel = "select strnodoPadre from pvycr_arbol where codigoPVYCR = '" & caucePadre & "' "
    Else
      sentenciaSel = "select strnodoPadre from pvycr_arbol where CODIGOCAUCE='" & caucePadre & "' "
    End If
    da.SelectCommand.CommandText = sentenciaSel

    Return utiles.nothingABlanco(da.SelectCommand.ExecuteScalar)
  End Function
  Shared Function ObtenerIdPadre(ByVal claveNodo As String, ByVal pagina As Page, ByVal tipo As String) As Integer
    Dim conexion As SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
    Dim sentenciaSel As String = ""
    Dim da As SqlClient.SqlDataAdapter = New SqlClient.SqlDataAdapter("", conexion)
    Dim dst As DataSet = New DataSet()
    Dim p() As String = claveNodo.Split("#")
    Select Case p(1)
      Case "E"
        sentenciaSel = "select idarbol from pvycr_arbol where claveNodo='" & p(0) & "#P" & "' "
    End Select

    'sentenciaSel = "select idarbolPadre from pvycr_arbol where claveNodo='" & claveNodo & "' "
    utiles.Comprobar_Conexion_BD(pagina, conexion)
    da.SelectCommand.CommandText = sentenciaSel
    Dim idArbolPadre As Integer
    idArbolPadre = da.SelectCommand.ExecuteScalar
    Return idArbolPadre

    End Function
    Shared Function ObtenerIconoPuntoE(ByVal codigoPVYCR As String, ByVal idElemento As String, ByVal conexion As System.Data.SqlClient.SqlConnection, ByVal page As Page, ByVal tipoPunto As String, ByVal tipoElemen As String) As String
        'obtenemos tipo del icono para saber qué icono escoger.
        'si es tipo = 1 --> amarillos (xxxxY32)
        'si es tipo = 2 --> naranjas (xxxxO32)
        'si es tipo = 3 --> verdes (xxxxG32)
        'si es tipo = 4 --> azules (xxxxO32)

        Dim da As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
        Dim sentenciatipopunto = "select top 1 tipo from pvycr_clasificacionelementos where codigoPVYCR = '" & codigoPVYCR & "' and em = '" & idElemento & "' "
        utiles.Comprobar_Conexion_BD(page, conexion)
        da.SelectCommand.CommandText = sentenciatipopunto
        Dim tipoIconoE As String = da.SelectCommand.ExecuteScalar

        If tipoIconoE = Nothing Then
            tipoIconoE = "0"
        End If

        Select Case tipoPunto
            Case "G"
                Select Case tipoElemen
                    Case "Q"
                        Select Case tipoIconoE
                            Case "0"
                                Return "icoCuaQY32.png'"
                            Case "1"
                                Return "icoCuaQO32.png'"
                            Case "2"
                                Return "coCuaQG32.png'"
                            Case "3"
                                Return "icoCuaQB32.png'"
                        End Select
                    Case "N"
                        Select Case tipoIconoE
                            Case "0"
                                Return "icoCuaNQY32.png'"
                            Case "1"
                                Return "icoCuaNQO32.png'"
                            Case "2"
                                Return "coCuaNQG32.png'"
                            Case "3"
                                Return "icoCuaNQB32.png'"
                        End Select
                End Select 'tipo elemento
            Case "M"
                Select Case tipoElemen
                    Case "Q"
                        Select Case tipoIconoE
                            Case "0"
                                Return "icoCirQY32.png'"
                            Case "1"
                                Return "icoCirQO32.png'"
                            Case "2"
                                Return "icoCirQG32.png'"
                            Case "3"
                                Return "icoCirQB32.png'"
                        End Select
                    Case "V"
                        Select Case tipoIconoE
                            Case "0"
                                Return "icoCirVY32.png'"
                            Case "1"
                                Return "icoCirVO32.png'"
                            Case "2"
                                Return "icoCirVG32.png'"
                            Case "3"
                                Return "icoCirVB32.png'"
                        End Select
                    Case "E"
                        Select Case tipoIconoE
                            Case "0"
                                Return "icoCirEY32.png'"
                            Case "1"
                                Return "icoCirEO32.png'"
                            Case "2"
                                Return "icoCirEG32.png'"
                            Case "3"
                                Return "icoCirEB32.png'"
                        End Select
                    Case "H"
                        Select Case tipoIconoE
                            Case "0"
                                Return "icoCirHY32.png'"
                            Case "1"
                                Return "icoCirHO32.png'"
                            Case "2"
                                Return "icoCirHG32.png'"
                            Case "3"
                                Return "icoCirHB32.png'"
                        End Select
                End Select 'tipo elemento
        End Select 'tipo punto
    End Function
End Class
