using Api.Hubs;
using Core.Interfaces;
using Core.Services;
using Domain.Exceptions;
using Infrastructure.Data;
using Infrastructure.Repos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Security.Authentication;
using System.Text;

namespace Api.Extensions
{
    public static class Extensions
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
                c.RoutePrefix = "swagger"; // Set Swagger UI at the app's root
            });
            app.UseCors("AllowAnyOrigin");
            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapHub<SimpleHub>("/simple");
            app.MapHub<WebRtcHub>("/webrtc");


            app.MapControllers();

            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (contextFeature != null)
                    {

                        context.Response.StatusCode = contextFeature.Error switch
                        {
                            NotFoundException => StatusCodes.Status404NotFound,
                            BadRequestException => StatusCodes.Status400BadRequest,
                            AuthenticationException => StatusCodes.Status401Unauthorized,
                            _ => StatusCodes.Status500InternalServerError
                        };


                        await context.Response.WriteAsJsonAsync(
                            new
                            {
                                Success = false,
                                Code = context.Response.StatusCode.ToString(),
                                Errors = contextFeature.Error switch
                                {
                                    IdentityException => ((IdentityException)contextFeature.Error).Errors,
                                    _ => contextFeature.Error.Message
                                }
                            }
                        );
                    }
                });
            });


        }
    }
}
