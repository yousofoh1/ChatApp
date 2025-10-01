using Domain.Entities.User;
using Domain.Entities.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Server:BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string OwnerId { get; set; } = string.Empty;
        public DomainUser Owner { get; set; } = null!;
        public ICollection<Channel> Channels { get; set; } = [];
        public ICollection<DomainUser> Users { get; set; } = [];


    }
}
