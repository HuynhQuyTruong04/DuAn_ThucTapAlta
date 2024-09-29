using DuAn_ThucTapAlta.Models;

namespace DuAn_ThucTapAlta.Services
{
    public interface IPermissionService
    {
        Task<Permission> GetPermissionByIdAsync(int permissionId);
        Task<IEnumerable<Permission>> GetAllPermissionsAsync();
        Task<Permission> CreatePermissionAsync(Permission permission);
        Task<Permission> UpdatePermissionAsync(Permission permission);
    }
}
