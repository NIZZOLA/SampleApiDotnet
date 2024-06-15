using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Store2.Api.Contracts;
using Store2.Api.Models;
using System.ComponentModel.DataAnnotations;

namespace Store2.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OperationsController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;

    public OperationsController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    [HttpGet("list-users")]
    public IActionResult ListUsers()
    {
        var users = _userManager.Users.Select(u => new
        {
            u.Id,
            u.UserName,
            u.Email
        }).ToList();

        return Ok(users);
    }

    [HttpGet("list-users-auth")]
    [Authorize]
    public IActionResult ListUsersProtected()
    {
        var users = _userManager.Users.Select(u => new
        {
            u.Id,
            u.UserName,
            u.Email
        }).ToList();

        return Ok(users);
    }

    [HttpPost("create-user")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequestModel user)
    {
        if (ModelState.IsValid)
        {
            ApplicationUser appUser = new ApplicationUser
            {
                UserName = user.Name,
                Email = user.Email
            };

            IdentityResult result = await _userManager.CreateAsync(appUser, user.Password);

            if (result.Succeeded)
                return Ok(new { message = MessageConstants.UserCreatedSuccessfully });
            else
            {
                foreach (IdentityError error in result.Errors)
                    ModelState.AddModelError("", error.Description);
            }
        }

        return BadRequest(ModelState);
    }

    [HttpPost("create-role")]
    public async Task<IActionResult> CreateRole([Required][FromBody] RoleCreateRequestModel role)
    {
        if (ModelState.IsValid)
        {
            IdentityResult result = await _roleManager.CreateAsync(new ApplicationRole() { Name = role.Name });
            if (result.Succeeded)
                return Ok(new { message = MessageConstants.RoleCreatedSuccessfully });
            else
            {
                foreach (IdentityError error in result.Errors)
                    ModelState.AddModelError("", error.Description);
            }
        }

        return BadRequest(ModelState);
    }
}