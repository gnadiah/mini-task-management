using Microsoft.AspNetCore.Mvc;

namespace Api.Features.Health;

[ApiController]
[Route("Api/[controller]")]
public class HealthController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> healthStatus()
    {
        return Ok(
            new
            {
                live = true,
                status = 200
            }
        );
    }
}