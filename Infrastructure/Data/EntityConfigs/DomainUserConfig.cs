using Domain.Models;
using Domain.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.EntityConfigs
{
    public class DomainUserConfig : IEntityTypeConfiguration<DomainUser>
    {
        public void Configure(EntityTypeBuilder<DomainUser> builder)
        {
            builder.HasKey(u => u.Id);
            builder.HasMany(u => u.OwnedServers).WithOne(s => s.Owner).HasForeignKey(u => u.OwnerId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(u => u.Servers).WithMany(s => s.Users);
            builder.HasMany(u => u.Messages).WithOne(m => m.User).HasForeignKey(m => m.UserId).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.Friends).WithMany(f => f.FriendOf).UsingEntity(j => j.ToTable("UserFriend"));


        }
    }
}
