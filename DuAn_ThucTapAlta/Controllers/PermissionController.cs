using DuAn_ThucTapAlta.Models;
using DuAn_ThucTapAlta.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DuAn_ThucTapAlta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionService _permissionService;

        public PermissionController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [HttpGet("{id}")]
        [Authorize]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> GetPermission(int id)
        {
            var permission = await _permissionService.GetPermissionByIdAsync(id);

            if (permission == null)
            {
                return NotFound("Quyền không tồn tại!");
            }

            return Ok(permission);
        }

        [HttpGet]
        [Authorize]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> GetAllPermissions()
        {
            var permissions = await _permissionService.GetAllPermissionsAsync();
            return Ok(permissions);
        }

        [HttpPost]
        [Authorize]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> CreatePermission([FromBody] Permission permission)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (permission == null)
            {
                return BadRequest("Quyền không hợp lệ!");
            }

            var createPermission = await _permissionService.CreatePermissionAsync(permission);

            return CreatedAtAction(nameof(GetPermission), new { id = createPermission.PermissionId }, createPermission);
        }

        [HttpPut("{id}")]
        [Authorize]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> UpdatePermission(int id, [FromBody] Permission permission)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != permission.PermissionId)
            {
                return BadRequest("ID không hợp lệ!");
            }

            var updatePermission = await _permissionService.UpdatePermissionAsync(permission);

            if (updatePermission == null)
            {
                return NotFound("Quyền không tồn tại!");
            }

            return Ok(updatePermission);
        }

        [HttpDelete("{id}")]
        [Authorize]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> DeletePermission(int id)
        {
            var isDelete = await _permissionService.DeletePermissionAsync(id);

            if (!isDelete)
            {
                return NotFound("Quyền không tồn tại!");
            }

            return NoContent();
        }
    }
}
