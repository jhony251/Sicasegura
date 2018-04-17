Imports Microsoft.VisualBasic

Public Class Utiles
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
End Class
