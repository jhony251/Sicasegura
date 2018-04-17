Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports GuarderiaFluvial

Public Class FuncionesGenerales

    Shared Function CalculoUltimoValorX(ByVal midatatable As DataTable) As Double

        Dim puntos As ArrayList = New ArrayList()
        'inicializo valores

        puntos.Add(0)
        puntos.Add(Convert.ToDouble(("0" & midatatable.Rows.Item(0).Item("A3_M").ToString)))
        puntos.Add(Convert.ToDouble("0" & midatatable.Rows.Item(0).Item("A3_M").ToString) + _
                   Convert.ToDouble("0" & midatatable.Rows.Item(0).Item("A1_M").ToString))
        puntos.Add(Convert.ToDouble("0" & midatatable.Rows.Item(0).Item("A1_M").ToString) + _
                   Convert.ToDouble("0" & midatatable.Rows.Item(0).Item("A2_M").ToString) + _
                   Convert.ToDouble("0" & midatatable.Rows.Item(0).Item("A3_M").ToString))

        puntos.Add(CDbl((((Convert.ToDouble(("0" & midatatable.Rows.Item(0).Item("A1_M").ToString)) + Convert.ToDouble(("0" & midatatable.Rows.Item(0).Item("A2_M").ToString)))) + Convert.ToDouble(("0" & midatatable.Rows.Item(0).Item("A3_M").ToString)) + Convert.ToDouble(("0" & midatatable.Rows.Item(0).Item("B1_M").ToString)))))

        puntos.Add(CDbl((Convert.ToDouble(("0" & midatatable.Rows.Item(0).Item("A3_M").ToString)) + Convert.ToDouble(("0" & midatatable.Rows.Item(0).Item("A1_M").ToString)) + Convert.ToDouble(("0" & midatatable.Rows.Item(0).Item("B2_M").ToString)))))

        puntos.Add(Convert.ToDouble(("0" & midatatable.Rows.Item(0).Item("A3_M").ToString)) + Convert.ToDouble(("0" & midatatable.Rows.Item(0).Item("B3_M").ToString)))
        puntos.Add(Convert.ToDouble(("0" & midatatable.Rows.Item(0).Item("B4_M").ToString)))
        puntos.Add((CDbl(Convert.ToDouble(("0" & midatatable.Rows.Item(0).Item("B4_M").ToString))) / 2))

        puntos.Add(CDbl(Convert.ToDouble(("0" & midatatable.Rows.Item(0).Item("B4_M").ToString))) / 2)


        Dim max As Double = 0
        Dim i As Integer
        Dim numPuntos As Integer = puntos.Count
        For i = 0 To numPuntos - 1
            If puntos(i) > max Then
                max = puntos.Item(i)
            End If
        Next

        Return max
    End Function
    Shared Function CalcularNumRegFiltrados(ByVal filtro As String, ByVal conexion As System.Data.SqlClient.SqlConnection, ByVal sFiltro As String, ByVal tabla As String, ByVal page As Page) As String
        'ncm calculamos el nº de registros que devolverá el filtro
        If filtro <> "" Then
            utiles.Comprobar_Conexion_BD(Page, conexion)
            Dim comando As SqlCommand = New System.Data.SqlClient.SqlCommand("", conexion)
            comando.CommandText = "select count(*) from " & tabla & " " & sFiltro
            Return utiles.nullACero(comando.ExecuteScalar())
        Else
            Return DBNull.Value.ToString
        End If

    End Function

End Class
