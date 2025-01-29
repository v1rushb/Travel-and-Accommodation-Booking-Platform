using TABP.Domain.Models.Email;

namespace TABP.Domain.Abstractions.Services;

public interface IEmailService
{
    Task SendAsync(EmailDTO email);
}