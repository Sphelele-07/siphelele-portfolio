namespace SiphelelePortfolio.Data.Services;

public record EmailAttachment(string FileName, byte[] Content, string ContentType = "application/pdf");

public interface IEmailService
{
    Task SendAsync(string to, string toName, string subject, string htmlBody,
                   IEnumerable<EmailAttachment>? attachments = null,
                   CancellationToken ct = default);

    Task NotifyNewCollaborationAsync(string fullName, string email, string subject, string message);
}