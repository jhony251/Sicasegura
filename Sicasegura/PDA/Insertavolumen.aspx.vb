Imports System.Data.SqlClient
Imports System.IO
Imports System.Data
Imports GuarderiaFluvial
Imports GuarderiaFluvial.JavaScript
Partial Class PDA_Insertavolumen
    Inherits System.Web.UI.Page

    Private conexion As SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
    Private codigoPVYCR, EM, Cauce As String
    Private CFD, Lectura, caudalmedido, enmarcha, fechahora As String
    Private Incidenciavolumetrica, ConsumoVolumetricoAdicional, ReinicioLecturaVolumetrica As String
    Private justificacion, usuario, observaciones As String
    Private comando As SqlClient.SqlCommand = New System.Data.SqlClient.SqlCommand("", conexion)
    Private objTrans As SqlTransaction


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim valores As String() = Request.QueryString("params").ToString().Split("#")

    End Sub

    Protected Sub cargardatos()
        Dim comando As SqlClient.SqlCommand = New SqlClient.SqlCommand("", conexion)
        Dim objTrans As SqlTransaction
        Try
            utiles.Comprobar_Conexion_BD(Page, conexion)
            comando.Parameters.AddWithValue("@idElementoMedida", EM)
            comando.Parameters.AddWithValue("@CodigoPVYCR", codigoPVYCR)
            comando.Parameters.AddWithValue("@cod_fuente_datos", CFD)
            objTrans = conexion.BeginTransaction()
            comando.Transaction = objTrans
            comando.CommandText = "INSERT INTO [PVYCR_DatosMotores_Estadillo]([CodigoPVYCR],[idElementoMedida],[Cod_Fuente_Dato],[Fecha_Medida] " & _
                        ",[LecturaContador_M3],[Observaciones],[idIncidenciaVolumetrica],[ConsumoVolumetricoAdicional] " & _
                        ",[ReinicioLecturaVolumetrica],[Funciona],[justificacion], login,[CaudalMedido]) " & _
                        "values (@CodigoPVYCR,@idElementoMedida,@cod_fuente_datos,@Fecha_Medida, " & _
                        "@LecturaContador_M3,@Observaciones,@idIncidenciaVolumetrica,@ConsumoVolumetricoAdicional, " & _
                        "@ReinicioLecturaVolumetrica, @Funciona,@justificacion, @login,@CaudalMedido) "

            comando.Parameters.AddWithValue("@Fecha_medida", fechahora)

            'Lectura contador M3
            If utiles.nullABlanco(Lectura) = "" Then
                comando.Parameters.AddWithValue("@LecturaContador_M3", DBNull.Value)
            Else
                comando.Parameters.AddWithValue("@LecturaContador_M3", Lectura)
            End If
            'observaciones
            comando.Parameters.AddWithValue("@Observaciones",observaciones
)
            'Id incidencia volumétrica
            comando.Parameters.AddWithValue("@idIncidenciaVolumetrica", utiles.BlancoANull(Incidenciavolumetrica))
            'Consumo volumétrico
            If utiles.nullABlanco(ConsumoVolumetricoAdicional) = "" Then
                comando.Parameters.AddWithValue("@ConsumoVolumetricoAdicional", DBNull.Value)
            Else
                comando.Parameters.AddWithValue("@ConsumoVolumetricoAdicional", ConsumoVolumetricoAdicional.Replace(",", "."))
            End If
            'Reinico lectura volum´´etrica
            If utiles.nullABlanco(ReinicioLecturaVolumetrica) = "" Then
                comando.Parameters.AddWithValue("@ReinicioLecturaVolumetrica", DBNull.Value)
            Else
                comando.Parameters.AddWithValue("@ReinicioLecturaVolumetrica", ReinicioLecturaVolumetrica.Replace(",", "."))
            End If
            'Funciona
            comando.Parameters.AddWithValue("@Funciona", enmarcha)
            comando.Parameters.AddWithValue("@login", usuario)
            comando.Parameters.AddWithValue("@Justificacion", "")
            'Caudal Medido
            If utiles.nullABlanco(caudalmedido) = "" Then
                comando.Parameters.AddWithValue("@CaudalMedido", DBNull.Value)
            Else
                comando.Parameters.AddWithValue("@CaudalMedido", caudalmedido.Replace(",", "."))
            End If

            comando.ExecuteNonQuery()

            objTrans.Commit()
        Catch Exc As System.Data.SqlClient.SqlException
            objTrans.Rollback()
            Select Case Exc.Number
                Case 547
                    'Response.Write("Error: " & Exc.Message & " num: " & Exc.Number)
                    Alert(Page, "No existe un elemento de medida para el punto con fecha medida: " & fechahora)
                Case 2627
                    Alert(Page, "El dato motor ya existe para la fecha medida: " & fechahora)
            End Select
        Catch Exc As Exception
            objTrans.Rollback()
            Alert(Page, "Error: " & Exc.Message)
        End Try

    End Sub

End Class
