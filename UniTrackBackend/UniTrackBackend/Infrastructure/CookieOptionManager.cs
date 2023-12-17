namespace UniTrackBackend.Infrastructure;

public static class CookieOptionManager
{
    public static CookieOptions GenerateRefreshCookieOptions()
    {
        return new CookieOptions()
        {
            HttpOnly = true,
            Secure = true,
            Expires = DateTime.UtcNow.AddHours(2),
            Domain = "localhost",
            IsEssential = true
        };
    }
    public static CookieOptions GenerateAccessCookieOptions()
    {
        return new CookieOptions()
        { 
            HttpOnly = true,
            Secure = true,
            Expires = DateTime.UtcNow.AddMinutes(2),
            Domain = "localhost",
            IsEssential = true
        };
    }
} 