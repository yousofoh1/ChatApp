using Domain.Models.Auth;
using Domain.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Server:BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string OwnerId { get; set; } = string.Empty;
        public AppUser Owner { get; set; } = null!;
        public ICollection<Channel> Channels { get; set; } = [];


    }
}
