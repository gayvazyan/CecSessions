using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;

namespace CecSessions.Core
{
    public static class MailSender
    {
       
        public static void Sender(string toMail, string subject, string text)
        {
            var SmtpServer = "smtp.gmail.com";
            var SmtpPort = 587;
            var SmtpUsername = "noreplaycec@gmail.com";
            var SmtpPassword = "Sa123456789!";

 

            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(SmtpUsername);
                mail.To.Add(toMail);
                mail.Subject = subject;
                mail.Body = text;
                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient(SmtpServer, SmtpPort))
                {
                    smtp.Credentials = new System.Net.NetworkCredential(SmtpUsername, SmtpPassword);
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
        }

    }
}