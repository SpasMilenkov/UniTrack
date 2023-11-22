using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace UniTrackBackend.Services.Commons.Attributes;


public class EmailListAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value is not List<string> emailList)
        {
            return new ValidationResult("No emails provided"); // or return an error if null is not allowed
        }

        foreach (var email in emailList)
        {
            try
            {
                var addr = new MailAddress(email);
                if (addr.Address != email)
                {
                    return new ValidationResult($"Invalid email format: {email}");
                }
            }
            catch
            {
                return new ValidationResult($"Invalid email format: {email}");
            }
        }

        return ValidationResult.Success ?? throw new InvalidOperationException();
    }
}
