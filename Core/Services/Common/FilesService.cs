using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Common;

public class FilesService
{
    public static async Task<string> SaveFile(IFormFile img, IHostEnvironment env, HttpContext http)
    {
        var path = Path.Combine(env.ContentRootPath, "wwwroot/assets");

        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(img.FileName);
        var fullPath = Path.Combine(path, fileName);

        using (var stream = new FileStream(fullPath, FileMode.Create))
        {
            await img.CopyToAsync(stream);
        }

        //full path
        return http.Request.Host + "/Assets/" + fileName;
    }
}
