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
            return await _context.Users.FirstOrDefaultAsync(s => s.UserId == id);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
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
            var existingUser = await _context.Users.FirstOrDefaultAsync(x => x.UserId == id);

            if (existingUser == null)
            {
                return null;
            }

            existingUser.Email = updateDto.Email;
            existingUser.PassWord = updateDto.PassWord;

            await _context.SaveChangesAsync();
            return existingUser;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == id);
            if (user == null)
            {
                return false;
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        //kiem tra email dung dinh dang @vietjetair.com
        public bool ValidateEmailDomain(string email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@vietjetair\.com$";
            return Regex.IsMatch(email, pattern);
        }
    }
}
