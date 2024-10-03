using System;

namespace DuAn_ThucTapAlta.Models
{
    public class Permission
    {
        public int PermissionId { get; set; }
        public string GroupName { get; set; }
        public string PermissionType { get; set; } //quyen truy cap

        public ICollection<Document> Documents { get; set; }
        public ICollection<WorkGroup> WorkGroups { get; set; }
    }
}
