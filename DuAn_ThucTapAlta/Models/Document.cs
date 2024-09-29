using System;

namespace DuAn_ThucTapAlta.Models
{
    public class Document
    {
        public int DocumentId { get; set; }
        public string DocumentName { get; set; }
        public string DocumentType { get; set; } //loai tai lieu
        public DateTime CreateDate { get; set; } 
        public string Creator { get; set; }
        public string Status { get; set; }
        public decimal LastedVersion { get; set; } //phien ban moi nhat, kieu decimal de luu so thap phan

        public int FlightId { get; set; } //khoa ngoai lien ket den bang Flight
        public Flight Flight { get; set; }  //Navigation property ve Flight
        public ICollection<DocumentVersion> DocumentVersions { get; set; } //Navigation property - Mot tai lieu co nhieu phien ban
        public ICollection<Permission> Permissions { get; set; } //Navigation property - Mot tai lieu co nhieu quyen truy cap      
    }
}
