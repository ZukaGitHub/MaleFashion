using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace WebApplicationCrud.Data.Smtp
{
    public static class SmtpEmailService
    {
        public static async Task<string> SendEmail(string email,string name,string body,string subject)
        {
          
                var SmtpClient = new SmtpClient()
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential()
                    {
                        UserName = "zuka101899@gmail.com",
                        Password = "vbxnxjdslfntpzjw"

                    }
                };
                MailAddress fromEmail = new MailAddress("zuka101899@gmail.com", "zukito");
                MailAddress toEmail = new MailAddress(email,name);
                MailMessage message = new MailMessage()
                {
                    From = fromEmail,
                    Subject = subject,
                    Body = body,


                };
                message.To.Add(toEmail);


                try
                {
                  await SmtpClient.SendMailAsync(message);


                }
                catch (Exception e)
                {
                    var messageError = e.Message;
                return (messageError);
                }

            return "ok";
        }
    }
}
