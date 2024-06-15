using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Store2.Api.Contracts;
using Store2.Api.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Store2.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly string _authSecret;

    public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _authSecret = configuration["AuthSecret"];
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginRequestModel loginRequest)
    {
        if (ModelState.IsValid)
        {
            ApplicationUser appUser = await _userManager.FindByEmailAsync(loginRequest.Email);
            if (appUser != null)
            {
                await _signInManager.SignOutAsync();
                Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(appUser, loginRequest.Password, false, false);
                if (result.Succeeded)
                {
                    var token = GenerateJwtToken(appUser);
                    return Ok(new { token });
                }
            }
            return Unauthorized(new { error = MessageConstants.InvalidEmailOrPassword });
        }

        return BadRequest(ModelState);
    }

    private string GenerateJwtToken(ApplicationUser user)
    {
        long iat = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
        var claims = new[]
        {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, iat.ToString())
    };

        var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_authSecret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: null,
            audience: null,
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}