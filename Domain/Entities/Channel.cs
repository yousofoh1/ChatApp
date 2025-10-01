using Domain.Entities.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Channel: BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public int ServerId { get; set; }
        public Server Server { get; set; } = null!;
        public ICollection<Message> Messages { get; set; } = [];


    }
}
