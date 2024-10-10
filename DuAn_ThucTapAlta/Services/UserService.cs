using DuAn_ThucTapAlta.Data;
using DuAn_ThucTapAlta.DTO.Users;
using DuAn_ThucTapAlta.Models;
using Microsoft.EntityFrameworkCore;
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
