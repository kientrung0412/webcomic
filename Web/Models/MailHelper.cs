using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace Do_an.Controllers.ultis
{
    public class MailHelper
    {
        public Boolean SendMail(string toEmail, string subject, string content)
        {
            try
            {
                var fromEmailAddress = ConfigurationManager.AppSettings["FromEmailAddress"];
                var fromEmailDisplayName = ConfigurationManager.AppSettings["FromEmailDisplayName"];
                var fromEmailPassword = ConfigurationManager.AppSettings["FromEmailPassword"];
                var smtpHost = ConfigurationManager.AppSettings["SMTPHost"];
                
                string body = content;
                MailMessage message = new MailMessage(new MailAddress(fromEmailAddress,fromEmailDisplayName),new MailAddress(toEmail));
                message.Subject = subject;
                message.IsBodyHtml = true;
                message.Body = body;

                var client = new SmtpClient();
                client.UseDefaultCredentials = true;
                client.Credentials = new NetworkCredential(fromEmailAddress, fromEmailPassword);
                client.Host = smtpHost;
                client.Port = 587;
                client.EnableSsl = true;
            
                client.Send(message);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}