namespace Ravm.Api.Controllers;

using System.Security.Claims;
using Devhub.Common.Paging;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Ravm.Application.Common;
using Ravm.Application.UseCases.Roles.Commands;
using Ravm.Application.UseCases.RouteClassifications.Models;
using Ravm.Domain.Exceptions;
using Ravm.Domain.Models;
using Ravm.Infrastructure.Common.Constants;


[Route("api/roles")]
[ApiController]
[Authorize]
public class RoleController(
    RoleManager<Role> roleManager,
    ICurrentUser currentUser) : ControllerBase
{
    /// <summary>
    /// Получить роли
    /// </summary>
    [HttpGet]
    public IActionResult GetRoles()
    {
        var roles = roleManager.Roles.Select(s => new RoleModel { Id = s.Id, Name = s.Name });

        if (!currentUser.IsAdmin)
        {
            roles = roles.Where(w => w.Name != Roles.Admin.Name);
        }

        return Ok(roles.ToList());
    }

    /// <summary>
    /// Получить роль по идентификатору
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetRole(Guid id)
    {
        var role = await roleManager.FindByIdAsync(id.ToString())
            ?? throw new NotFoundException(nameof(Role), id);

        var claims = await roleManager.GetClaimsAsync(role);

        var permissions = from permission in Permissions.List.AsEnumerable()
                          join claim in claims on permission.Key equals claim.Value
                          select permission;

        var permissionModels = permissions.Select(info => new PermissionInfoModel()
        {
            DisplayName = info.DisplayName,
            DisplayNameEn = info.DisplayNameEn,
            DisplayNameKa = info.DisplayNameKa,
            DisplayNameRu = info.DisplayNameRu,
            Group = info.Group,
            Key = info.Key
        });

        var result = new RoleWithPermissionModel { Id = role.Id, Name = role.Name, Permissions = permissionModels.ToList() };

        return Ok(result);
    }

    /// <summary>
    /// Получить отфильтрованные роли
    /// </summary>
    [HttpPost("table-list")]
    public async Task<IActionResult> SearchRoles([FromBody] FilteringRequest filter)
    {
        var query = roleManager.Roles;

        if (!currentUser.IsAdmin)
        {
            query = query.Where(w => w.Name != Roles.Admin.Name);
        }

        var roles = await query
            .OrderBy(a => a.Name)
            .AsFilterable(filter, out var total)
            .Select(s => new RoleModel { Id = s.Id, Name = s.Name })
            .ToPagedListAsync(filter);


        return Ok(roles);
    }

    /// <summary>
    /// Обновить роль
    /// </summary>
    [HttpPut]
    public async Task<IActionResult> UpdateRole(UpdateRoleCommond request)
    {
        var role = roleManager.Roles.FirstOrDefault(a => a.Id == request.Id) ??
            throw new NotFoundException(nameof(Role), request.Id);

        var dbPermissions = await roleManager.GetClaimsAsync(role);


        var suitablePermission = request.Permissions.Where(a => dbPermissions.Any(b => b.Value == a)).ToList();
        var noSuitablePermission = dbPermissions.Where(a => suitablePermission.Any(b => a.Value != b)).ToList();

        foreach (var claim in dbPermissions)
        {
            if (noSuitablePermission.Any(a => a.Value == claim.Value))
            {
                await roleManager.RemoveClaimAsync(role, claim);
            }
        }

        var newPermissions = request.Permissions.Where(a => dbPermissions.Any(b => b.Value != a)).ToList();

        foreach (var claim in newPermissions)
        {
            var newClaim = new Claim(ApplicationClaimTypes.Permission, claim);
            await roleManager.AddClaimAsync(role, newClaim);
        }

        var result = await roleManager.UpdateAsync(role);
        if (result.Succeeded)
            return Ok();

        return BadRequest(result);
    }

    /// <summary>
    /// Создать роль
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateRole(CreateRoleCommond request)
    {
        var newRole = new Role
        {
            Name = request.Name,
        };

        var result = await roleManager.CreateAsync(newRole);

        foreach (var permission in request.Permissions)
        {
            var claim = new Claim(ApplicationClaimTypes.Permission, permission);
            await roleManager.AddClaimAsync(newRole, claim);
        }
        return Ok(result.Succeeded);
    }

    /// <summary>
    /// Удалить роль
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRole(Guid id)
    {
        var role = await roleManager.FindByIdAsync(id.ToString())
            ?? throw new NotFoundException(nameof(Role), id);

        var result = await roleManager.DeleteAsync(role);

        return Ok(result.Succeeded);
    }

    /// <summary>
    /// Удалить роли
    /// </summary>
    [HttpDelete("delete-range")]
    public async Task<IActionResult> DeleteRoles(Guid[] ids)
    {
        var count = 0;
        foreach (var id in ids)
        {
            var role = await roleManager.FindByIdAsync(id.ToString());
            var result = await roleManager.DeleteAsync(role!);

            if (result.Succeeded)
                count++;
        }

        return Ok(count);
    }

}
