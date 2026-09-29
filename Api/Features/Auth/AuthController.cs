using Api.Commons;
using Api.Features.Auth.Dtos;
using Api.Features.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Features.Auth;

[ApiController]
[Route("Api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> Register([FromBody] RegisterReqDto registerReqDto)
    {
        var user = AuthMapper.ToUserApplication(registerReqDto);
        var result = await _authService.Register(user, registerReqDto.Password);
        return result.ToActionResult(this, UserMapper.ToUserResDto, StatusCodes.Status201Created);
    }

    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> Login([FromBody] LoginReqDto loginReqDto)
    {
        var result = await _authService.Login(loginReqDto.UserName, loginReqDto.Password);
        return result.ToActionResult(this, AuthMapper.ToLoginResDto);
    }

    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> Logout([FromBody] LogoutReqDto logoutReqDto)
    {
        await _authService.Logout(logoutReqDto.RefreshToken);
        return NoContent();
    }
    
    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> Refresh([FromBody] RefreshReqDto refreshReqDto)
    {
        var result = await _authService.Refresh(refreshReqDto.RefreshToken);
        return result.ToActionResult(this, AuthMapper.ToRefreshResDto);
    }
}
