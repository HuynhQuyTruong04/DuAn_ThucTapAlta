namespace DuAn_ThucTapAlta.Models
{
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }

        //Mot vai tro co nhieu nguoi dung
        public ICollection<User> Users { get; set; }
    }
}
