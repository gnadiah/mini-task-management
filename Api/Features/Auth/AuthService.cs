using Api.Database;
using Api.Models;
using ErrorOr;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Api.Features.Auth;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ITokenService _tokenService;
    private readonly ApplicationDbContext _dbContext;
    private readonly IConfiguration _configuration;

    public AuthService(UserManager<ApplicationUser> userManager,  ITokenService tokenService,  ApplicationDbContext dbContext, IConfiguration configuration)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _dbContext = dbContext;
        _configuration = configuration;
    }

    public async Task<ErrorOr<ApplicationUser>> Register(ApplicationUser user, string password)
    {
        var errors = new List<Error>();
        if (await _userManager.FindByEmailAsync(user.Email!) is not null)
            errors.Add(AuthErrors.EmailAlreadyInUse);
        if (await _userManager.FindByNameAsync(user.UserName!) is not null)
            errors.Add(AuthErrors.UserNameAlreadyInUse);
        if (errors.Count > 0)
            return errors;

        var result = await _userManager.CreateAsync(user, password);
        if (!result.Succeeded)
            return AuthErrors.IdentityCreationFailed(result.Errors.First().Description);

        return user;
    }

    public async Task<ErrorOr<(string AccessToken, string RefreshToken)>> Login(string userName, string password)
    {
        var user = await _userManager.FindByNameAsync(userName);
        if (user is null)
            return AuthErrors.InvalidCredentials;

        if (await _userManager.IsLockedOutAsync(user))
            return AuthErrors.AccountLocked;

        if (!await _userManager.CheckPasswordAsync(user, password))
        {
            await _userManager.AccessFailedAsync(user);
            return AuthErrors.InvalidCredentials;
        }

        await _userManager.ResetAccessFailedCountAsync(user);

        var accessToken = _tokenService.GenerateAccessToken(user);
        var (refreshRaw, refreshHash) = _tokenService.GenerateRefreshToken();

        var refreshExpiresInMinutes = double.Parse(_configuration["Token:RefreshToken:ExpiresInMinutes"]!);
        await _dbContext.RefreshTokens.AddAsync(new RefreshToken
        {
            UserId = user.Id,
            User = user,
            RefreshTokenValue = refreshHash,
            ExpiresAt = DateTime.UtcNow.AddMinutes(refreshExpiresInMinutes)
        });
        await _dbContext.SaveChangesAsync();
        return (accessToken, refreshRaw);
    }

    public async Task Logout(string refreshToken)
    {
        var hashToken = _tokenService.HashToken(refreshToken);
        var currentRefreshToken =
            await _dbContext.RefreshTokens.Where(x => x.RefreshTokenValue == hashToken).FirstOrDefaultAsync();
        if (currentRefreshToken is null)
            return;
        _dbContext.RefreshTokens.Remove(currentRefreshToken);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<ErrorOr<(string AccessToken, string RefreshToken)>> Refresh(string refreshToken)
    {
        var refreshHash = _tokenService.HashToken(refreshToken);
        var currentRefreshToken =
            await _dbContext.RefreshTokens.Include(x => x.User)
                .FirstOrDefaultAsync(x => x.RefreshTokenValue == refreshHash);
        if (currentRefreshToken is null)
            return AuthErrors.RefreshTokenInvalid;

        if (DateTime.UtcNow > currentRefreshToken.ExpiresAt)
            return AuthErrors.RefreshTokenExpired;

        var accessToken = _tokenService.GenerateAccessToken(currentRefreshToken.User);
        var (newRefreshRaw, newRefreshHash) = _tokenService.GenerateRefreshToken();

        _dbContext.RefreshTokens.Remove(currentRefreshToken);
        await _dbContext.RefreshTokens.AddAsync(new RefreshToken
        {
            RefreshTokenValue = newRefreshHash,
            UserId = currentRefreshToken.User.Id,
            User = currentRefreshToken.User,
            ExpiresAt = DateTime.UtcNow.AddMinutes(double.Parse(_configuration["Token:RefreshToken:ExpiresInMinutes"]!))
        });

        await _dbContext.SaveChangesAsync();
        
        return (accessToken, newRefreshRaw);
    }
}
