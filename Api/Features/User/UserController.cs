using Api.Commons;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Features.User;

[ApiController]
[Route("Api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetMe()
    {
        var result = await this.GetUserId().ThenAsync(_userService.GetMe);
        return result.ToActionResult(this, UserMapper.ToUserResDto);
    }

    [Authorize]
    [HttpGet("me")]
    public Task<IActionResult> GetUser()
    {
        return this.GetMe();
    }
    
    
}
