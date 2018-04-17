using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;

namespace SicaSegura
{
    public class SICA_Mailing
    {
        private string destinatarios_cco = "";
        private string destinatarios = "";
        private string asunto = "";
        private string cuerpo_HTML = "";
        private string cuerpo_raw = "";
        private string cuerpo = "";
        private bool formatoHTML = true;
        private MailMessage mailMsg = null;

        public SICA_Mailing()
        {
            mailMsg = new MailMessage();
        }
        /// <summary>
        /// Definir que el cuerpo del correo es formato de texto plano
        /// </summary>
        public void Set_formatotexto() { formatoHTML = false; }
        /// <summary>
        /// Definir que el cuerpo del correo es formato HTML
        /// </summary>
        public void Set_formatoHTML() { formatoHTML = true; }
        /// <summary>
        /// Funcion para definir los destinatarios de correo en CCO
        /// </summary>
        /// <param name="Valor">String de los correos concatenados separados por comas</param>
        public void Set_destinatarios_cco(string Valor)
        {
            destinatarios_cco = Valor;
        }
        /// <summary>
        /// Funcion para definir los destinatarios de correo 
        /// </summary>
        /// <param name="Valor">String de los correos concatenados separados por comas</param>
        public void Set_destinatarios(string Valor)
        {
            destinatarios = Valor;
        }
        /// <summary>
        /// Funcion para definir el asunto del correo 
        /// </summary>
        /// <param name="Valor">String con el asunto del correo</param>
        public void Set_asunto(string Valor)
        {
            asunto = Valor;
        }
        /// <summary>
        /// Establecer el cuerpo del correo a envial
        /// </summary>
        /// <param name="Valor">String con el cuerpo en formato HTML</param>
        public void Set_cuerpo(string Valor)
        {
            cuerpo_HTML = Valor;
        }

        public void Set_adjunto(string ruta)
        {
            Attachment archivo = new Attachment(ruta, MediaTypeNames.Application.Octet);
            mailMsg.Attachments.Add(archivo);
        }

        public void Enviar_mail()
        {
            //AlternateView plainview = AlternateView.CreateAlternateViewFromString(Generar_informe_old(), null, "text/plain");
            //mailMsg.AlternateViews.Add(plainview);
            if (formatoHTML == true) { cuerpo = cuerpo_HTML; }
            mailMsg.From = new MailAddress("Sica@chsegura.es");
            mailMsg.To.Add(new MailAddress(this.destinatarios));
            try { mailMsg.Bcc.Add(new MailAddress(this.destinatarios_cco)); } catch { }
            mailMsg.Subject = this.asunto;
            mailMsg.BodyEncoding = System.Text.Encoding.GetEncoding("utf-8");
            //System.Net.Mail.AlternateView plainView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(System.Text.RegularExpressions.Regex.Replace(Generar_informe_old(), @"<(.|\n)*?>", string.Empty), null, "text/plain");
            System.Net.Mail.AlternateView htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(cuerpo, null, "text/html");
            //mailMsg.AlternateViews.Add(plainView);
            mailMsg.AlternateViews.Add(htmlView);
            SmtpClient emailclient = new SmtpClient();
            emailclient.Host = "tragsasmtp.tragsa.es";
            emailclient.UseDefaultCredentials = true;
            try
            {
                emailclient.Send(mailMsg);
            }
            catch (Exception e)
            {

            }
        }
    }
}
