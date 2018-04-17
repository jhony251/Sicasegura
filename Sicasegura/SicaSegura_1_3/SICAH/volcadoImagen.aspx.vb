Imports System.Drawing
Partial Class SICAH_volcadoImagen
    Inherits System.Web.UI.Page
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim ruta As String

        ruta = Request.QueryString("ruta")

        Dim objImage, objThumbnail As System.Drawing.Image
        Dim strFilename As String
        Dim shtWidth, shtHeight, MaxHeight, W, H As Short

        strFilename = ruta

        objImage = Image.FromFile(strFilename)


        shtWidth = 150
        shtHeight = 150
        W = shtWidth

        If shtWidth <> Nothing Then
            MaxHeight = 150
            If objImage.Width > shtWidth Then
                shtWidth = objImage.Width / (objImage.Height / MaxHeight)
                shtHeight = objImage.Height / (objImage.Width / shtWidth)
                If shtWidth > W Then
                    H = shtHeight
                    shtHeight = MaxHeight / (shtWidth / W)
                    shtWidth = W / (H / MaxHeight)
                End If
            Else
                shtWidth = objImage.Width
                shtHeight = objImage.Height
            End If
        Else
            If objImage.Width > shtWidth Then
                shtHeight = objImage.Height / (objImage.Width / shtWidth)
            Else
                shtWidth = objImage.Width
                shtHeight = objImage.Height
            End If
        End If

        objThumbnail = objImage.GetThumbnailImage(shtWidth, shtHeight, Nothing, System.IntPtr.Zero)
        Response.ContentType = "image/jpeg"
        objThumbnail.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg)
        objImage.Dispose()
        objThumbnail.Dispose()
    End Sub

End Class
