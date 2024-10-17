namespace DuAn_ThucTapAlta.DTO.Users
{
    public class UserRegisterDto
    {
        public string Email { get; set; }
        public string PassWord { get; set; }
        public string ConfirmPassWord { get; set; }
        public int RoleId { get; set; }
        public int GroupId { get; set; }
    }
}
