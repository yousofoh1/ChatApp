using Domain.Models;
using Domain.Models.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EntityConfigs
{
    public class AppUserFriendConfig:IEntityTypeConfiguration<AppUserFriend>
    {
        public void Configure(EntityTypeBuilder<AppUserFriend> builder)
        {
            builder
        .HasKey(af => new { af.AppUserId, af.FriendId });

            builder
                .HasOne(af => af.AppUser)
                .WithMany(u => u.Friends)
                .HasForeignKey(af => af.AppUserId)
                .OnDelete(DeleteBehavior.NoAction);



        }
    }
}
