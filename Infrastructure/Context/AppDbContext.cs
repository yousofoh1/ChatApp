using Domain.Models.Auth;
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
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Add your4 model configurations here
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            modelBuilder.Entity<AppUser>().OwnsMany(e => e.RefreshTokens, b =>
            {
                b.ToTable("RefreshTokens").WithOwner().HasForeignKey(e => e.UserId);
                //b.WithOwner().HasForeignKey(e => e.UserId);
                b.HasKey(e => new { e.Id, e.UserId });
            });

        }
        // Define DbSet properties for your entities
        // public DbSet<YourEntity> YourEntities { get; set; }
    }
}
