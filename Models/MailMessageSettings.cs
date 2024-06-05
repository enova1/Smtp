using System.Net.Mail;
// ReSharper disable PropertyCanBeMadeInitOnly.Global
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace Smtp.Models;

public class MailMessageSettings
{
    /// <summary>
    /// Gets or sets who the email will be sent too.
    /// </summary>
    public required string Sender { get; set; }

    /// <summary>
    /// Gets or sets the body of the email.
    /// </summary>
    public required string Body { get; set; }

/// <summary>
/// Gets or sets who the email will be sent from.
/// </summary>
    public required string From { get; set; }

/// <summary>
/// 
/// </summary>
    public required string Subject { get; set; }

/// <summary>
/// Gets or sets the email body as HTML.
/// </summary>
    public bool IsBodyHtml { get; set; } = true;

/// <summary>
/// gets or sets the priority of the email.
/// </summary>
    public MailPriority Priority { get; set; } = MailPriority.Normal;

/// <summary>
/// Gets or sets the delivery notification options.
/// </summary>
    public DeliveryNotificationOptions DeliveryNotificationOptions { get; set; } = DeliveryNotificationOptions.None;

    public string To { get; set; }
}