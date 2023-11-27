namespace UniTrackBackend.Infrastructure;

public static class CookieOptionManager
{
    public static readonly CookieOptions RefreshCookieOptions = new CookieOptions()
    {
        HttpOnly = true,
        Secure = true,
        Expires = DateTime.UtcNow.AddHours(2),
        Domain = "localhost",
        IsEssential = true
    };

    public static readonly CookieOptions AccessCookieOptions = new CookieOptions()
    { 
        HttpOnly = true,
        Secure = true,
        Expires = DateTime.UtcNow.AddMinutes(2),
        Domain = "localhost",
        IsEssential = true
    };
} 