using Api.Hubs;
using Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using System.Security.Authentication;

namespace Api.Extensions;

public static class AppMiddlewares
{
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
        //app.Use((context, next) =>
        //{   
        //    context.Response.Headers.Remove("Access-Control-Allow-Origin");
        //    Console.WriteLine(context.Request.Headers.Referer);
        //    context.Response.Headers.Add("Access-Control-Allow-Origin", $"localhost:4200");
        //    return next(context);
        //});
        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapHub<SimpleHub>("/chat").RequireAuthorization();
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
