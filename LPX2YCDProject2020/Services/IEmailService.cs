using LPX2YCDProject2020.Models.Account;
using System.Threading.Tasks;

namespace LPX2YCDProject2020.Services
{
    public interface IEmailService
    {
        Task SendTestEmail(UserEmailOptions userEmailOptions);
        Task SendEmailConfirmation(UserEmailOptions userEmailOptions);
        Task SendForgotPasswordEmail(UserEmailOptions userEmailOptions);
    }
}