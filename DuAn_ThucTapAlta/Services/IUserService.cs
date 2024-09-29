using DuAn_ThucTapAlta.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DuAn_ThucTapAlta.Services
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync (int userId);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> CreateUserAsync (User user);
        Task<User> UpdateUserAsync(User user);
    }
}
