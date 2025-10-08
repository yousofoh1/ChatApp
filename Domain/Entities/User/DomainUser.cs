using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Domain.Attributes;


namespace Domain.Entities.User
{
    public class DomainUser
    {
        public string Id { get; set; }
        [StringLength(50),Searchable]
        public string FirstName { get; set; } = "";
        [StringLength(50)]
        public string LastName { get; set; } = "";

        public ICollection<DomainUser> Friends { get; set; } = [];
        public ICollection<Message> Messages { get; set; } = [];
        public ICollection<DomainUser> FriendOf { get; set; } = [];
        public ICollection<Server> OwnedServers { get; set; } = [];
        public ICollection<Server> Servers { get; set; } = [];

        public string ImageUrl { get; set; } = "";
        public List<RefreshToken> RefreshTokens { get; set; } = [];

    }
}
