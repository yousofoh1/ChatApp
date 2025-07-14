using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Api.Extensions
{
    public static class Extensions
    {
        public static void AddAppDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(opts =>
                  opts.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
                );


            services.AddCors(o =>
            {
                o.AddPolicy("AllowAnyOrigin", p => p
                    //.AllowAnyOrigin()
                .AllowAnyHeader()
            .AllowAnyMethod()
            .SetIsOriginAllowed(_ => true) // Allow any origin (for dev only)
            .AllowCredentials()           // This is REQUIRED for SignalR
            );
            });

            services.AddSwagger();

            services.AddSignalR();
        }




        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddOpenApi();

            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(opts =>
            {
                opts.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });
        }


        //
        //
        //
        //
        //
        //
        // Extension method to configure the HTTP request pipeline
        //

        public static void UseAppDependencies(this WebApplication app)
        {
            app.MapOpenApi();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
            });
            app.UseCors("AllowAnyOrigin");
            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapHub<SimpleHub>("/simple");


            app.MapControllers();


        }
    }
}
