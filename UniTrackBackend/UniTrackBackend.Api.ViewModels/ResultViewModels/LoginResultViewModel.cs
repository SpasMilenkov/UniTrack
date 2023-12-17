namespace UniTrackBackend.Api.ViewModels.ResultViewModels;

public class LoginResultViewModel
{
    private string _userRole = null!;

    public required string UserId { get; set; }

    public required string UserRole 
    {
        get => _userRole;
        set => _userRole = value.ToUpper();
    }

    public required string AvatarUrl { get; set; }
}