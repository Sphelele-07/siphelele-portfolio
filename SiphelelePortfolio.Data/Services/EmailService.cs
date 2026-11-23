using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace SiphelelePortfolio.Data.Services;

public class SmtpOptions
{
    public string Host { get; set; } = "smtp.gmail.com";
    public int Port { get; set; } = 587;
    public bool UseStartTls { get; set; } = true;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string FromName { get; set; } = "Siphelele Portfolio";
    public string FromEmail { get; set; } = string.Empty;
}

public class EmailService : IEmailService
{
    private readonly SmtpOptions _smtp;
    private readonly ILogger<EmailService> _log;
    private const string AdminEmail = "mshini473@gmail.com";

    public EmailService(IOptions<SmtpOptions> smtp, ILogger<EmailService> log)
    {
        _smtp = smtp.Value;
        _log = log;
    }

    public async Task SendAsync(string to, string toName, string subject, string htmlBody,
                                IEnumerable<EmailAttachment>? attachments = null,
                                CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(_smtp.Password) || _smtp.Password.StartsWith("YOUR_"))
        {
            _log.LogWarning("SMTP not configured — email to {To} skipped.", to);
            return;
        }

        using var client = new SmtpClient(_smtp.Host, _smtp.Port)
        {
            Credentials = new NetworkCredential(_smtp.Username, _smtp.Password),
            EnableSsl = _smtp.UseStartTls,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            Timeout = 20000
        };

        using var message = new MailMessage
        {
            From = new MailAddress(_smtp.FromEmail, _smtp.FromName),
            Subject = subject,
            Body = WrapHtml(htmlBody),
            IsBodyHtml = true
        };
        message.To.Add(new MailAddress(to, toName));

        if (attachments is not null)
        {
            foreach (var a in attachments)
            {
                var ms = new MemoryStream(a.Content);
                message.Attachments.Add(new Attachment(ms, a.FileName, a.ContentType));
            }
        }

        await client.SendMailAsync(message, ct);
        _log.LogInformation("Email sent to {To} — {Subject}", to, subject);
    }

    public Task NotifyNewCollaborationAsync(string fullName, string email, string subject, string message)
    {
        var body = $@"
<p><strong>A new collaboration request was submitted.</strong></p>
<table style='border-collapse:collapse;margin:12px 0'>
  <tr><td style='padding:4px 12px 4px 0'><strong>Name</strong></td><td>{fullName}</td></tr>
  <tr><td style='padding:4px 12px 4px 0'><strong>Email</strong></td><td>{email}</td></tr>
  <tr><td style='padding:4px 12px 4px 0'><strong>Subject</strong></td><td>{subject}</td></tr>
</table>
<p><strong>Message:</strong></p>
<blockquote style='border-left:3px solid #b8942e;padding-left:12px;color:#444'>{message}</blockquote>
<p><a href='https://localhost:7003/Admin/Collaborations'>Open the collaboration inbox</a></p>";

        return SendAsync(AdminEmail, "Siphelele Admin", $"New collaboration — {subject}", body);
    }

    private static string WrapHtml(string inner) => $@"
<!doctype html>
<html><body style='font-family:Segoe UI,Arial,sans-serif;background:#fbf9f4;padding:24px;color:#1f2937'>
  <div style='max-width:560px;margin:0 auto;background:#ffffff;border-radius:8px;padding:32px;border-top:4px solid #b8942e'>
    <h2 style='color:#1e3a2f;margin-top:0'>Siphelele Portfolio</h2>
    {inner}
    <hr style='border:none;border-top:1px solid #e5e7eb;margin:24px 0'/>
    <p style='font-size:12px;color:#6b7280'>Automated email from your portfolio site.</p>
  </div>
</body></html>";
}