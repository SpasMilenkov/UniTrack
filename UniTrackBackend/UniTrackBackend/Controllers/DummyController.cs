using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UniTrackBackend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DummyController : ControllerBase
{
    [HttpGet("test")]
    [Authorize]
    public string Test()
    {
        return "You are logged in";
    }
}