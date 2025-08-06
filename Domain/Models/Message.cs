using Domain.Models.Auth;
using Domain.Models.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Message:BaseEntity
    {
        public string Content { get; set; } = string.Empty;
        public int ChannelId { get; set; } 
        public Channel Channel { get; set; } = null!;
        public int SenderId { get; set; } 
        public AppUser Sender { get; set; } = null!;
        public DateTime SentAt { get; set; } = DateTime.UtcNow;
        public bool IsEdited { get; set; } = false;
    }
}
