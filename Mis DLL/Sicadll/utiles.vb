Imports System.Data
Imports System.Object

Namespace AccesoADatos
    Partial Public Class AccesoADatos2
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
        Shared Function nothingABlanco(ByVal valor As Object)
            'Esta función devuelve la cadena vacía en caso de que el parámetro pasado sea nulo, para evitar errores por conversión de tipos
            If IsNothing(valor) Then
                Return ""
            Else
                Return valor
            End If
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
    End Class

End Namespace
