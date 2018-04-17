Imports Microsoft.VisualBasic

Public Class Utiles
    Public Shared Function frameSup() As String
        Dim resultado As String = ""
        resultado &= "<tr>"
        resultado &= "<td align=""right"" bgcolor=""DDDDBB""><img src=""images/frameSup.gif""></td>"
        resultado &= "</tr>"
        resultado &= "<tr>"
        resultado &= "<td bgcolor=""#BABA74"" style=""padding:3px;padding-left:10px;color:#4A4A04;border-top:1px solid #eeeeee"">"
        resultado &= "<a class='menusup' href='datos_motores.aspx'>Datos Motores</a> | <a class='menusup' href='datos_acequias.aspx'>Datos Acequias</a> "
        resultado &= "</td>"
        resultado &= "</tr>"
        resultado &= "<tr>"
        resultado &= "<td align=right bgcolor=""#8CA4B5"" style=""padding:2px;padding-right:10px;color:white;border-bottom:1px solid #eeeeee"">"
        'resultado &= "<a class='menuinf' href='listado_filtro.aspx?l=ecm'>Listado Consumos</a>"
        resultado &= "</td>"
        resultado &= "</tr>"

        Return resultado
    End Function
    Shared Function nullABlanco(ByVal valor As Object)
        'Esta función devuelve la cadena vacía en caso de que el parámetro pasado sea nulo, para evitar errores por conversión de tipos
        If valor Is System.DBNull.Value Then
            Return ""
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
End Class
