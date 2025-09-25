using Domain.Models;
using Domain.Models.User;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Context
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<AppUser>(options)
    {
        
        public DbSet<DomainUser> Users { get; set; }
        public DbSet<Server> Servers { get; set; }
        public DbSet<Channel> Channels{ get; set; }
        public DbSet<Message> Messages{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Add your4 model configurations here
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            modelBuilder.Entity<DomainUser>().OwnsMany(e => e.RefreshTokens, b =>
            {
                b.ToTable("RefreshTokens").WithOwner().HasForeignKey(e => e.UserId);
                //b.WithOwner().HasForeignKey(e => e.UserId);
                b.HasKey(e => new { e.Id, e.UserId });
            });












            //assign users roles
            //modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            //new IdentityUserRole<string> { RoleId = "1", UserId = "1" },
            //new IdentityUserRole<string> { RoleId = "3", UserId = "2" }
            //);

        }
        // Define DbSet properties for your entities
        // public DbSet<YourEntity> YourEntities { get; set; }
    }
}
