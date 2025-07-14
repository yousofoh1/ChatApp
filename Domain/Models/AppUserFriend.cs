using Domain.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class AppUserFriend
    {
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public string FriendId { get; set; }
        public AppUser Friend { get; set; }
    }
}
