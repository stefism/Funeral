using Funeral.App.ViewModels;
using System.Net;
using System.Net.Mail;

namespace Funeral.App.Services
{
    public class MailFromGmailService : IGmailService
    {
        public void SendMailFromGmail(EmailInputModel input, string userEmail)
        {
            var userSubject = input.Subject;
            var userMessage = input.Message;

            var fromAddress = new MailAddress(Credential.Username, userEmail);
            var toAddress = new MailAddress(Credential.ReceiveAddress, "From funeral contact form");
            const string fromPassword = Credential.FuneralGmailPass;
            string subject = userSubject;

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                Timeout = 20000,
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = userMessage,
            })
            {
                smtp.Send(message);
            }
        }
    }
}
