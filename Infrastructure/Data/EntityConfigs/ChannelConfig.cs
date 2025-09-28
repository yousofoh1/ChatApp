using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.EntityConfigs
{
    public class ChannelConfig : IEntityTypeConfiguration<Channel>
    {
        public void Configure(EntityTypeBuilder<Channel> builder)
        {
            builder.HasOne(c => c.Server)
                .WithMany(s => s.Channels)
                .HasForeignKey(c => c.ServerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.Messages)
                .WithOne(m => m.Channel)
                .HasForeignKey(m => m.ChannelId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
