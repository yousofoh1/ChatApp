using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos.Auth
{
    public record RegisterRequest(
            string UserName,
            string FullName,
            string Email,
            IFormFile Image,
            string Password
        );
}
