using System.Reflection;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using MimeKit;
using UniTrackBackend.Data.Models;

namespace UniTrackBackend.Services.Messaging;

public class EmailService : IEmailService
{
    private readonly IConfiguration _config;

    public EmailService(IConfiguration config)
    {
        _config = config;
    }

    public async Task SendEmailAsync(string firstName, string lastName, string emailAddress, string link, string templateType)
    {
        var email = new MimeMessage();

        email.From.Add(new MailboxAddress("UniTrack", "unitrack@gmail.com"));
        email.To.Add(new MailboxAddress($"{firstName} {lastName}", emailAddress));

        var emailContent = "";
        switch (templateType.ToLower())
        {
            case "verification":
                email.Subject = "Confirm Email";
                emailContent = await File.ReadAllTextAsync("../UniTrackBackend.Services/Messaging/Templates/ConfirmEmailTemplate.html");
                break;
            case "resetpassword":
                emailContent = await File.ReadAllTextAsync("../UniTrackBackend.Services/Messaging/Templates/ResetPasswordTemplate.html");
                emailContent = emailContent.Replace("{LINK_TOKEN}", link);
                break;
                
        }
        email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
        {
            Text =  emailContent.Replace("{LINK_TOKEN}", link)
        };

        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(_config["Smtp:EmailHost"], 2525, false);
        await smtp.AuthenticateAsync(_config["Smtp:EmailUsername"], _config["Smtp:EmailPassword"]);

        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true); 
    }
    
}
