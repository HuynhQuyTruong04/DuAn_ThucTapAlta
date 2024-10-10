using DuAn_ThucTapAlta.DTO.Logins;
using DuAn_ThucTapAlta.DTO.Users;
using DuAn_ThucTapAlta.Models;

namespace DuAn_ThucTapAlta.Services
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(int id);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> CreateUserAsync(User user);
        Task<User> UpdateUserAsync(int id, UpdateUserRequestDTO updateDto);
        Task<bool> DeleteUserAsync(int id);
    }
}

