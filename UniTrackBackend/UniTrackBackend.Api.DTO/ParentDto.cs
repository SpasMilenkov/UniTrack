using System.ComponentModel.DataAnnotations;
using UniTrackBackend.Services.Commons.Attributes;

namespace UniTrackBackend.Api.DTO;

public class ParentDto
{
    [Required] public string UserId { get; set; }
    [Required] public List<int> ChildIds { get; set; }
}