using Core.Interfaces;
using Core.Services;
using Infrastructure.Data;
using Infrastructure.Repos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Api.Extensions;

public static class AppDependencies
{
    public static void AddAppDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(opts =>
              opts.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
            );

        services.AddIdentity<AppUser, IdentityRole>(options =>
        {
            options.User.RequireUniqueEmail = true;
        })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        services.AddScoped<IUOW, UOW>();
        services.AddScoped<IServicesUOW, ServiceUOW>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = "yousofo",
                ValidAudience = "yousofo_audience",
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("yousofo-secret-code-yousofo-secret-code"))
            };

            //options.Events = new JwtBearerEvents
            //{
            //    OnMessageReceived = context =>
            //    {
            //        var accessToken = context.Request.Query["access_token"];

            //        // If the request is for our hub...
            //        var path = context.HttpContext.Request.Path;
            //        if (!string.IsNullOrEmpty(accessToken) &&
            //            path.StartsWithSegments("/chat"))
            //        {
            //            // Read token from query string
            //            context.Token = accessToken;
            //        }

            //        return Task.CompletedTask;
            //    }
            //};
        });

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
}
