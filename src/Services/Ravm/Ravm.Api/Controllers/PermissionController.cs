namespace Uds.Identity.Api.Controllers;

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Ravm.Infrastructure.Common.Constants;
using Ravm.Domain.Exceptions;
using Ravm.Application.Common;

[Route("api/permissions")]
[ApiController]
[Authorize]
public class PermissionController(
    RoleManager<Role> roleManager,
    ICurrentUser currentUser
    ) : ControllerBase
{

    /// <summary>
    /// Проверяет разрешения доступа
    /// </summary>
    [HttpGet("check")]
    public async Task<IActionResult> HasPermission([FromQuery][Required] string permissionName)
    {
        var roleNames = currentUser.Roles;
        if (roleNames.Length == 0)
        {
            return Ok(false);
        }

        var roles = await roleManager.Roles.Where(a => roleNames.Contains(a.Name)).ToListAsync();

        var permissions = permissionName.Split(',');

        foreach (var role in roles)
        {
            var roleClaims = await roleManager.GetClaimsAsync(role);

            var hasPermission = roleClaims.Any(a => a.Type == ApplicationClaimTypes.Permission && permissions.Contains(a.Value));

            if (hasPermission)
                return Ok(true);
        }

        return Ok(false);
    }

    /// <summary>
    /// Получить разрешения по имени роли
    /// </summary>
    [HttpGet("{roleName}")]
    public async Task<IActionResult> GetPermissionsByRoleNameAsync(string roleName)
    {
        var role = roleManager.Roles.FirstOrDefault(a => a.Name == roleName)
            ?? throw new NotFoundException();

        var claims = await roleManager.GetClaimsAsync(role);
        var permissions = Permissions.List.Where(w => claims.Any(a => a.Value == w.Key));

        return Ok(permissions);
    }

    /// <summary>
    /// Получить сгруппированные разрешения
    /// </summary>
    [HttpGet("grouped/permissions")]
    public IActionResult GetGroupedPermissionsAsync()
    {
        var groupedPermissions = Permissions.List.GroupBy(a => a.Group).ToList();

        return Ok(groupedPermissions);
    }

    /// <summary>
    /// Получить текущие разрешения пользователя
    /// </summary>
    [HttpGet("/api/users/current/permission-keys")]
    public async Task<IActionResult> GetCurrentUserPermissions()
    {
        var roleNames = currentUser.Roles;
        if (roleNames.Length == 0)
        {
            return Ok(false);
        }

        var roles = await roleManager.Roles.Where(a => roleNames.Contains(a.Name)).ToListAsync();
        var rolePermissionClaims = new List<Claim>();
        foreach (var role in roles)
        {
            var roleClaims = await roleManager.GetClaimsAsync(role);

            rolePermissionClaims.AddRange(roleClaims.Where(a => a.Type == ApplicationClaimTypes.Permission));
        }

        return Ok(rolePermissionClaims.Select(s => s.Value).ToArray());
    }
}
