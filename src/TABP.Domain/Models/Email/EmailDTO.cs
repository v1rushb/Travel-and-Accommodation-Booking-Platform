namespace TABP.Domain.Models.Email;

public class EmailDTO
{
    public string RecipientEmail { get; set; }
    public string RecipientName { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
}