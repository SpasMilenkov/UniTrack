using System.ComponentModel.DataAnnotations;
 
 namespace UniTrackBackend.Api.ViewModels;
 
 
 public class ResetPasswordModel
 {
     [Required]
     [EmailAddress]
     public required string Email { get; set; }
 
     [Required]
     [DataType(DataType.Password)]
     public required string NewPassword { get; set; }
 
     [Required]
     public required string Token { get; set; }
 }