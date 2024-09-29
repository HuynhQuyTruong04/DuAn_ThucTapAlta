using System;

namespace DuAn_ThucTapAlta.Models
{
    public class Permission
    {
        public int PermissionId { get; set; }
        public string GroupName { get; set; }
        public string PermissionType { get; set; } //quyen truy cap

        public int DocumentId { get; set; } //khoa ngoai lien ket den bang Document
        public Document Document { get; set; }  //Navigation property ve Document

        //Navigation property - Mot quyen co the duoc rang cho nhieu nhom
        public ICollection<WorkGroup> WorkGroups { get; set; }
    }
}
