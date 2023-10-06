using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatCloneApi.Controller;

[Route("api")]
public class ApiController : ControllerBase
{
    [HttpGet("private")]
    [Authorize]
    public IActionResult Private()
    {
        return Ok(new
        {
            Message = "Hello from a private endpoint!"
        });
    }

    [HttpGet("private-scoped")]
    [Authorize("read:messages")]
    public IActionResult Scoped()
    {
        return Ok(new
        {
            Message = "Hello from a private-scoped endpoint!"
        });
    }
}