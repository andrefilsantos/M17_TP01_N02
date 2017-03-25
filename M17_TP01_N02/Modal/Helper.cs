namespace M17_TP01_N02.Modal {
    public class Helper {
        public static void SendMail(string para, string assunto, string texto, string anexo = null) {
            //objetos mail
            var mensagem = new System.Net.Mail.MailMessage();
            var credenciais = new System.Net.NetworkCredential("dev.andrefilsantos@gmail.com", "********");
            var dequem = new System.Net.Mail.MailAddress("dev.andrefilsantos@gmail.com");
            var smtp = new System.Net.Mail.SmtpClient();

            //mensagem
            mensagem.To.Add(para);
            mensagem.From = dequem;
            mensagem.Subject = assunto;
            mensagem.Body = texto;
            mensagem.IsBodyHtml = true;
            //servidor
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = credenciais;

            //anexo
            if (!string.IsNullOrEmpty(anexo)) {
                if (System.IO.File.Exists(anexo)) {
                    var ficheiroAnexo = new System.Net.Mail.Attachment(anexo);
                    mensagem.Attachments.Add(ficheiroAnexo);
                }
            }
            //enviar
            smtp.Send(mensagem);
        }
    }
}
