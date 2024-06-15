using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Store2.Api.Models;

namespace Store2.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
[Authorize] // Require authentication for all actions in this controller
public class SecuredController : ControllerBase
{
    private readonly RoleManager<ApplicationRole> _roleManager;

    public SecuredController(RoleManager<ApplicationRole> roleManager)
    {
        _roleManager = roleManager;
    }

    [HttpGet("list-roles")]
    public IActionResult ListRoles()
    {
        var roles = _roleManager.Roles.Select(r => new
        {
            r.Id,
            r.Name
        }).ToList();

        return Ok(roles);
    }
}
