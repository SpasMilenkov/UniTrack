using System.ComponentModel.DataAnnotations;
 
 namespace UniTrackBackend.Api.DTO;
 
 
 public class ResetPasswordDto
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