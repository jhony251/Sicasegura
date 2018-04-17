Imports GuarderiaFluvial
Partial Class SICAH_SubMenu
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.lblPestanyasArbol.Text = genHTML.EnlacesMenuArbol(5, "../", Page, 11, Session("claveTipo"), "o", "S")
        If Session("claveTipo") = "E" Then
            CargarValoresBusqueda()
        End If

    End Sub
    Private Sub CargarValoresBusqueda()
        Dim NombreElemento, TipoElementoMedida As String
        'NCM 05/09/2008 Cargamos la búsqueda con el código del arbol

        txtCodigoPVYCR.Text = Session("ClaveNodo")

        txtIdElementoMedida.Text = Session("idElemento")

        Select Case TipoElementoMedida
            Case "Q"
                NombreElemento = txtCodigoPVYCR.Text & " - " & txtIdElementoMedida.Text & " - CAUDAL"
            Case "V"
                NombreElemento = txtCodigoPVYCR.Text & " - " & txtIdElementoMedida.Text & " - VOLUMEN"
            Case "E"
                NombreElemento = txtCodigoPVYCR.Text & " - " & txtIdElementoMedida.Text & " - ENERGIA"
            Case "H"
                NombreElemento = txtCodigoPVYCR.Text & " - " & txtIdElementoMedida.Text & " - HORAS"
            Case Else
                NombreElemento = ""
        End Select
        txtDescripcionElementoMedida.Text = NombreElemento
    End Sub
End Class
