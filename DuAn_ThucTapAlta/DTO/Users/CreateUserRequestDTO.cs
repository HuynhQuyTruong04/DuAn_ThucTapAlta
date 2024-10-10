namespace DuAn_ThucTapAlta.DTO.Users
{
    public class CreateUserRequestDTO
    {
        public int UserId { get; set; }
        public string PassWord { get; set; }
        public string Email { get; set; }
        public int GroupId { get; set; }
        public int RoleId {  get; set; }
    }
}
