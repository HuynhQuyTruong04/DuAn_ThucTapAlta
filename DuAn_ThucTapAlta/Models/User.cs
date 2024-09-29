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
    }
}
