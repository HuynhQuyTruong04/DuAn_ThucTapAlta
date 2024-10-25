using DuAn_ThucTapAlta.Data;
using DuAn_ThucTapAlta.DTO.Users;
using DuAn_ThucTapAlta.Models;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Generators;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace DuAn_ThucTapAlta.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDBContext _context;

        public UserService(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<User> ValidateUserAsync(string email, string password)
        {
            // Tìm người dùng theo email từ database
            var user = await _context.Users
                .Include(u => u.Role) // Bao gồm vai trò người dùng
                .SingleOrDefaultAsync(u => u.Email == email);

            var hashedPassword = HashPassword(password);
            if (user.PassWord != hashedPassword)
            {
                return null; // Mật khẩu không đúng
            }

            return user;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
        }

        public async Task<int> GetRoleIdByNameAsync(string roleName)
        {
            var role = await _context.Roles.SingleOrDefaultAsync(r => r.RoleName == roleName);
            if (role == null)
            {
                throw new Exception("Vai trò không tồn tại.");
            }
            return role.RoleId;
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(s => s.UserId == id && s.IsActive);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.Where(u => u.IsActive).ToListAsync();
        }

        public async Task<User> CreateUserAsync(User user)
        {
            // Hash mật khẩu trước khi lưu
            user.PassWord = HashPassword(user.PassWord);

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateUserAsync(int id, UpdateUserRequestDTO updateDto)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(x => x.UserId == id && x.IsActive);

            if (existingUser == null)
            {
                return null;
            }

            existingUser.Email = updateDto.Email;
            existingUser.PassWord = HashPassword(updateDto.PassWord);

            await _context.SaveChangesAsync();
            return existingUser;
        }


        public async Task<bool> DeactivateUserAsync(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == id);
            if (user == null)
            {
                return false;
            }

            // Cập nhật trạng thái thành không hoạt động
            user.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ActivateUserAsync(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == id);
            if (user == null || user.IsActive)
            {
                return false;
            }

            user.IsActive = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<User>> GetInactiveUsersAsync()
        {
            return await _context.Users
                                 .Where(u => !u.IsActive)
                                 .ToListAsync();
        }

    }
}
