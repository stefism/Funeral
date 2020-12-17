using Funeral.App.ViewModels;

namespace Funeral.App.Services
{
    public interface IGmailService
    {
        void SendMailFromGmail(EmailInputModel input, string userEmail);
    }
}
