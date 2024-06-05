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

        // Create an SmtpClient object
        SmtpClient smtpClient = new()
        { 
            Host = smtpSettings.SmtpServer,
            Port = smtpSettings.Port,
            Credentials = new NetworkCredential(smtpSettings.UserName, smtpSettings.SenderPassword),
            EnableSsl = true
        };
        // Create a MailMessage object
        MailMessage mailMessage = new();
        foreach (var recipient in messageSettings.To.Split(',').ToList())
        {
            mailMessage.To.Add(new MailAddress(recipient));
        }
        mailMessage.From = new MailAddress(messageSettings.From);
        mailMessage.Sender = new MailAddress(messageSettings.Sender);
        mailMessage.Subject = messageSettings.Subject;
        mailMessage.Body = messageSettings.Body;
        mailMessage.IsBodyHtml = messageSettings.IsBodyHtml;
        mailMessage.Priority = messageSettings.Priority;
        mailMessage.DeliveryNotificationOptions = messageSettings.DeliveryNotificationOptions;

        try
        {
            // Send the email
smtpClient.Send(mailMessage);
            return (true, "Email sent successfully");
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