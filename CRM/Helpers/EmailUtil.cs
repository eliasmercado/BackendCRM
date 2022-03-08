using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Helpers
{
    public class EmailUtil
    {
        public static void EnviarEmail(string nombre, string email, string asunto, string contenido)
        {
            string servidor = "smtp.gmail.com";
            int puerto = 587;

            string gmailUser = "informaciones.crm@gmail.com";
            string gmailPass = "proy2CRM";

            MimeMessage mensaje = new();
            mensaje.From.Add(new MailboxAddress("Informaciones CRM", gmailUser));
            mensaje.To.Add(new MailboxAddress(nombre, email));
            mensaje.Subject = asunto;

            BodyBuilder cuerpoMensaje = new();
            cuerpoMensaje.HtmlBody = contenido;

            mensaje.Body = cuerpoMensaje.ToMessageBody();

            SmtpClient clientSmtp = new();
            clientSmtp.CheckCertificateRevocation = false;
            clientSmtp.Connect(servidor, puerto, MailKit.Security.SecureSocketOptions.StartTls);
            clientSmtp.Authenticate(gmailUser, gmailPass);
            clientSmtp.Send(mensaje);
            clientSmtp.Disconnect(true);
        }
    }
}
