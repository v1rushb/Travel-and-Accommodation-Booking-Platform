using TABP.Domain.Models.Email;

namespace TABP.Domain.Abstractions.Services;

/// <summary>
/// Defines the contract for sending email messages.
/// </summary>
public interface IEmailService
{
    /// <summary>
    /// Sends an email asynchronously.
    /// </summary>
    /// <param name="email">An email DTO containing the email details.</param>
    /// <exception cref="Exceptions.EmailSendingException">
    /// Thrown when the email fails to send.
    /// </exception>
    Task SendAsync(EmailDTO email);
}