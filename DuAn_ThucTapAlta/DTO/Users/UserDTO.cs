using DuAn_ThucTapAlta.Models;

namespace DuAn_ThucTapAlta.DTO.Users
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string PassWord { get; set; }
        public string Email { get; set; }
        public int GroupId { get; set; }
        public int RoleId { get; set; }
    }
}
