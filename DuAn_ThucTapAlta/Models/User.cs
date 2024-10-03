using System;

namespace DuAn_ThucTapAlta.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int GroupID { get; set; }
        public WorkGroup WorkGroup { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }

        public ICollection<DocumentVersion> DocumentVersions { get; set; }
        public ICollection<Document> Documents { get; set; }
        public ICollection<Flight> Flights { get; set; }
    }
}
