using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace LoggingExercise.EmailServices
{
    public class SendgridEmail
    {
        private readonly SendGridMessage msg;
        private readonly SendGridClient client;

        public SendgridEmail(string apiKey, String senderEmail = "reloar@gmail.com",string senderName = "Reloar")
        {
            msg = new SendGridMessage();
            msg.From = new EmailAddress(senderEmail, senderName);
            client = new SendGridClient(apiKey);
        }

        
        public void SendMail(Email message, Boolean isHtml, string fileName, Byte[] fileBytes)
        {
            msg.AddTo(message.To);
            msg.Subject = message.Subject;

            if (!isHtml)
            {
                msg.PlainTextContent = message.Body;
            }
            else
            {
                msg.HtmlContent = message.Body;
            }
            if (!string.IsNullOrEmpty(fileName))
            {
                string fileContents = Convert.ToBase64String(fileBytes);
                msg.AddAttachment(fileName, fileContents);
            }

            client.SendEmailAsync(msg);
        }
    }
}