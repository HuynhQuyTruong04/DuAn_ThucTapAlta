using System;

namespace DuAn_ThucTapAlta.Models
{
    public class DocumentVersion
    {
        public int VersionId { get; set; }
        public decimal VersionNumber { get; set; } //so phien bang
        public DateTime UploadDate { get; set; }
        public string UploadedBy { get; set; }
        public string FilePath { get; set; } //duong dan tep
        public long FileSize { get; set; } 
        public int DocumentId { get; set; } //khoa ngoai lien ket den bang Document
        public Document Document { get; set; }  //Navigation property ve Document

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
