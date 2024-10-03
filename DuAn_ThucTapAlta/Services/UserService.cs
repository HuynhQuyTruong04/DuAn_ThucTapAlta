using DuAn_ThucTapAlta.Data;
using DuAn_ThucTapAlta.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace DuAn_ThucTapAlta.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDBContext _context;

        public UserService (ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByIdAsync (int userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> CreateUserAsync (User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
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
