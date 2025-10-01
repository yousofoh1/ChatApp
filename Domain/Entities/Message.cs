using Domain.Entities.User;
using Domain.Entities.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Message:BaseEntity
    {
        public string Content { get; set; }
        public string SenderId { get; set; } 
        public DomainUser Sender { get; set; } = null!;
        public string ReceiverId { get; set; } 
        public DomainUser Receiver { get; set; } = null!;
        public DateTime SentAt { get; set; } = DateTime.UtcNow;
        public bool IsEdited { get; set; } = false;
        public bool IsRead { get; set; } = false;
    }
}
