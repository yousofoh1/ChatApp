using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos.Auth;

public class UserRDto
{
    public string Id { get; set; } = null!; 
    public string UserName { get; set; } = null!; 
    public string FullName { get; set; } = null!; 
    public string Email { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
}
