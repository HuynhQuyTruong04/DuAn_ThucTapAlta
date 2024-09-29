using System;

namespace DuAn_ThucTapAlta.Models
{
    public class WorkGroup
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public int Member {  get; set; } //so luong thanh vien
        public DateTime CreateDate { get; set; } //ngay tao nhom
        public string CreatedBy { get; set; } //nguoi tao nhom

        //Navigation property - Mot nhom co nhieu nguoi dung
        public ICollection<User> Users { get; set; }

        //Navigation property - Mot nhom co nhieu quyen
        public ICollection<Permission> Permissions { get; set; }
    }
}
