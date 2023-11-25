using UniTrackBackend.Data.Models;

namespace UniTrackBackend.Services.Messaging;

public interface IEmailService
{
    public Task SendEmailAsync(string firstName, string lastName, string emailAddress, string link,
        string templateType);
}