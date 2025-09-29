using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Common;

public static class FilesService
{
    public static async Task<string> SaveFileAsync(IFormFile img, IHostEnvironment env, IHttpContextAccessor http)
    {
        var path = Path.Combine(env.ContentRootPath, "wwwroot/assets");

        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(img.FileName);
        var fullPath = Path.Combine(path, fileName);

        try
        {
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await img.CopyToAsync(stream);
            }
        }catch (Exception ex)
        {
            throw new AppException("File upload failed: " + ex.Message);
        }

        //full path
        return http.HttpContext.Request.Host + "/Assets/" + fileName;
    }
}
