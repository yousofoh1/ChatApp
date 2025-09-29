using Core.Dtos.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data;

public class AppUser : IdentityUser
{
    public string? FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; } = string.Empty;
    public string? ImageUrl { get; set; } = string.Empty;

    public UserRDto MapToRDto()
    {
        return new UserRDto
        {
            Id = Id,
            Email = Email,
            FirstName = FirstName,
            LastName = LastName,
            UserName = UserName,
            ImageUrl = ImageUrl
        };
    }
}

public class AppUserConfig : IEntityTypeConfiguration<AppUser>
{

    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.HasIndex(u => u.Email).IsUnique();
    }
}
