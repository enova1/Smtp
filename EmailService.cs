using Smtp.Models;
using System.Net;
using System.Net.Mail;

namespace Smtp;

public interface IEmailService
{
    (bool, string) SendEmail(MailMessageSettings messageSettings, SmtpServerSettings serverSettings);
}

public class EmailService : IEmailService
{
    public (bool,string) SendEmail(MailMessageSettings messageSettings, SmtpServerSettings smtpSettings)
    {
        // Create a MailMessage object
        MailMessage mailMessage = new (messageSettings.From, messageSettings.Sender)
        {
            To = { messageSettings.To },
            From  = new MailAddress(messageSettings.From),
            Sender = new MailAddress(messageSettings.Sender),
            Subject = messageSettings.Subject,
            Body = messageSettings.Body,
            IsBodyHtml = messageSettings.IsBodyHtml,
            Priority = messageSettings.Priority,
            DeliveryNotificationOptions = messageSettings.DeliveryNotificationOptions
        };

        // Create an SmtpClient object
        SmtpClient smtpClient = new (smtpSettings.SmtpServer)
        {
            Port = smtpSettings.Port,
            Credentials = new NetworkCredential(smtpSettings.SenderEmail, smtpSettings.SenderPassword),
            EnableSsl = true
        };

        try
        {
            // Send the email
            smtpClient.Send(mailMessage);
            return (true, "Email sent successfully.");
        }
        catch (Exception ex)
        {
            return (false, ex.Message);
        }
        finally
        {
            // Dispose SmtpClient and MailMessage objects
            smtpClient.Dispose();
            mailMessage.Dispose();
        }
    }
}