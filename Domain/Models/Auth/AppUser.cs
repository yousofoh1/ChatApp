using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Auth
{
    public class AppUser : IdentityUser
    {
        [StringLength(50)]
        public string FirstName { get; set; } = "";
        [StringLength(50)]
        public string LastName { get; set; } = "";

        public ICollection<AppUserFriend> Friends{ get; set; } = [];

        public string ImageUrl { get; set; }= "";
        public List<RefreshToken> RefreshTokens { get; set; } = [];
 
    }
}
