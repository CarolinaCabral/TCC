using CoffeeExperience.Infra.CrossCutting.System;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoffeeExperience.Infra.CrossCutting.Util
{
    public class EmailSender
    {
        
        public static void EnviarEmailSac(string DeNome, string DeEmail, string Assunto, string Mensagem, bool IsHTML, MailAddress[] EmailsSac)
        {
            SendMail(DeNome, DeEmail, Mensagem, Assunto, EmailsSac.ToList(), IsHTML);
        }

        public static void EnviarEmail(string DeNome, string DeEmail, string ParaNome, string ParaEmail, string Assunto, string Mensagem, bool IsHTML)
        {
            List<MailAddress> Destinatarios = new List<MailAddress>();
            Destinatarios.Add(new MailAddress(ParaEmail, ParaNome));
            SendMail(DeNome, DeEmail, Mensagem, Assunto, Destinatarios, IsHTML);
        }

        public static void EnviarEmailMultiDestinatario(string DeNome, string DeEmail, string ParaNome, string ParaEmail, string Assunto, string Mensagem, bool IsHTML)
        {
            List<MailAddress> Destinatarios = new List<MailAddress>();
            foreach (var item in ParaEmail.Split(','))
            {
                Destinatarios.Add(new MailAddress(item, ParaNome));
            }
            SendMail(DeNome, DeEmail, Mensagem, Assunto, Destinatarios, IsHTML);
        }

        public static void EnviarEmailTecnico(string DeNome, string DeEmail, string Assunto, string Mensagem, bool IsHTML)
        {
            string[] emailsTecnicos = ConfigurationManager.AppSettings["EmailsTecnicos"].ToArrayOfStringComSeparator('|');
            List<MailAddress> EmailsTecnico = new List<MailAddress>();
            foreach (string item in emailsTecnicos)
            {
                EmailsTecnico.Add(new MailAddress(item));
            }
            SendMail(DeNome, DeEmail, Mensagem, Assunto, EmailsTecnico.ToList(), IsHTML);
        }

        public static void EnviarEmailAdministrativo(string DeNome, string DeEmail, string Assunto, string Mensagem, bool IsHTML)
        {
            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["EmailsAdministrativos"]))
            {
                string[] EmailsAdmin = ConfigurationManager.AppSettings["EmailsAdministrativos"].ToArrayOfStringComSeparator('|');
                List<MailAddress> Destinatarios = new List<MailAddress>();
                foreach (string item in EmailsAdmin)
                {
                    Destinatarios.Add(new MailAddress(item));
                }
                SendMail(DeNome, DeEmail, Mensagem, Assunto, Destinatarios, IsHTML);
            }
        }

        public static void EnviarEmailFinanceiro(string DeNome, string DeEmail, string Assunto, string Mensagem, bool IsHTML)
        {
            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["EmailsFinanceiros"]))
            {
                string[] EmailsAdmin = ConfigurationManager.AppSettings["EmailsFinanceiros"].ToArrayOfStringComSeparator('|');
                List<MailAddress> Destinatarios = new List<MailAddress>();
                foreach (string item in EmailsAdmin)
                {
                    Destinatarios.Add(new MailAddress(item));
                }
                SendMail(DeNome, DeEmail, Mensagem, Assunto, Destinatarios, IsHTML);
            }
        }

        private static void SendMail(string deNome, string deEmail, string mensagem, string titulo, List<MailAddress> destinatarios, bool IsHTML)
        {
            try
            {
                ThreadStart threadStart = delegate { Envia(deNome, deEmail, mensagem, titulo, destinatarios, IsHTML); };
                Thread newThread = new Thread(threadStart);
                newThread.Start();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void Envia(string deNome, string deEmail, string mensagem, string titulo, List<MailAddress> destinatarios, bool IsHTML)
        {
            try
            {
                SmtpClient client = new SmtpClient(ConfigurationManager.AppSettings["ServidorSMTP"], Convert.ToInt32(ConfigurationManager.AppSettings["PortaSMTP"]));
                client.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["UsuarioSMTP"], ConfigurationManager.AppSettings["SenhaSMTP"]);
                client.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);
                MailMessage message = new MailMessage();
                if (!string.IsNullOrEmpty(deNome) && !string.IsNullOrEmpty(deEmail))
                    message.From = new MailAddress(deEmail, deNome);
                else
                    message.From = new MailAddress(ConfigurationManager.AppSettings["EmailFrom"], "Contato");
                foreach (MailAddress item in destinatarios)
                {
                    message.To.Add(item);
                }
                message.Subject = titulo;
                message.Body = mensagem;
                message.IsBodyHtml = IsHTML;

                client.Send(message);
            }
            catch (Exception ex)
            {
                // throw ex;
            }
        }

    }
}

