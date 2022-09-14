

using CleanArchitecture.Application.Common.Models.Emails;

namespace CleanArchitecture.Application.Common.Interfaces.Emails
{
    public interface IEmailService
    {
        Task Send(EmailOptions emailOptions);
    }
}
