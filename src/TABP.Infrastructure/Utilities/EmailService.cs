using FluentEmail.Core;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TABP.Domain.Abstractions.Services;
using TABP.Domain.Models.Configurations;
using TABP.Domain.Models.Email;

namespace TABP.Infrastructure.Utilities;

public class EmailService : IEmailService
{
    private readonly IFluentEmail _fluentEmail;
    private readonly ILogger<EmailService> _logger;

    public EmailService(
        IFluentEmail fluentEmail,
        ILogger<EmailService> logger)
    {
        _fluentEmail = fluentEmail;
        _logger = logger;
    }

    public async Task SendAsync(EmailDTO emailDto)
    {
        try {
            
            var email = _fluentEmail
                .To(emailDto.RecipientEmail, emailDto.RecipientName)
                .Subject(emailDto.Subject)
                .Body(emailDto.Body);

            var response = await email.SendAsync();

            if(!response.Successful)
            {
                throw new Exception(string.Join(", ", response.ErrorMessages)); // make special exception for this
            }
            _logger.LogInformation("Email sent successfully to {RecipientEmail}", emailDto.RecipientEmail);
        } catch (Exception ex) {
            _logger.LogError(ex, "Error sending email to {RecipientEmail}", emailDto.RecipientEmail);
            throw new Exception("Error sending email", ex);
        }
    }
}