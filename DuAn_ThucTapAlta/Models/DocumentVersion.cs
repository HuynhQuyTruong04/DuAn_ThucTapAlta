using System;

namespace DuAn_ThucTapAlta.Models
{
    public class DocumentVersion
    {
        public int VersionId { get; set; }
        public decimal VersionNumber { get; set; } //so phien ban
        public DateTime UploadDate { get; set; }
        public string UploadedBy {  get; set; } //nguoi tai len
        public string FilePath { get; set; } //duong dan tep
        public int DocumentId { get; set; } //khoa ngoai lien ket den bang Document
        public Document Document { get; set; }  //Navigation property ve Document
    }
}
