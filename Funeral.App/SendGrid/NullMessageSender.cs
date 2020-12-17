namespace Funeral.App.SendGrid
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class NullMessageSender : ISendGridEmailSender
    {
        public Task SendEmailAsync(
            string from,
            string fromName,
            string to,
            string subject,
            string htmlContent,
            IEnumerable<EmailAttachment> attachments = null)
        {
            return Task.CompletedTask;
        }
    }
}
