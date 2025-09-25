using Domain.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.User
{
    public class RefreshToken : BaseEntity
    {
        public string UserId { get; set; }=string.Empty;
        //[ForeignKey("UserId")]
        //public AppUser AppUser { get; set; }
        public string Token { get; set; }= string.Empty;
        public DateTime ExpiresOn { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime? RevokenOn { get; set; }
        public bool IsExpired => DateTime.UtcNow >= ExpiresOn;
        public bool IsValid => RevokenOn is null && !IsExpired;
    }
}
