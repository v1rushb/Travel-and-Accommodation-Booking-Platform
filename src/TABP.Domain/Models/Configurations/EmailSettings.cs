namespace TABP.Domain.Models.Configurations;

public class EmailSettings
{
    public required string DefaultFromEmail { get; set; }
    public required SmtpSettings SMTPSettings { get; set; }
}

public class SmtpSettings
{
    public required string Host { get; set; }
    public required int Port { get; set; }
    public required string User { get; set; }
    public required string Password { get; set; }
    public bool EnableSsl { get; set; } = true;
}