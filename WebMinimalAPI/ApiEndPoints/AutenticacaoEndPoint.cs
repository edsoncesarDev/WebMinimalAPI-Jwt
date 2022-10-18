using Microsoft.AspNetCore.Authorization;
using WebMinimalAPI.Models;
using WebMinimalAPI.Services;

namespace WebMinimalAPI.ApiEndPoints
{
    public static class AutenticacaoEndPoint
    {
        public static void MapAutenticacaoEndPoint(this WebApplication app)
        {
            // EndPoint Login
            var issuer = app.Configuration["Jwt:Issuer"];
            var audience = app.Configuration["Jwt:Audience"];
            var key = app.Configuration["Jwt:Key"];

            app.MapPost("/login", [AllowAnonymous] (UserModel userModel, ITokenService tokenService) =>
            {
                if (userModel is null)
                    return Results.BadRequest("Login Inválido");

                if (userModel.UserName == "Elliot" && userModel.Password == "mrRobot")
                {
                    var tokenString = tokenService.GerarToken(key, issuer, audience, userModel);

                    return Results.Ok(new { token = tokenString });
                }
                else
                {
                    return Results.BadRequest("Login Inválido");
                }

            }).Produces(StatusCodes.Status400BadRequest)
              .Produces(StatusCodes.Status200OK)
              .WithName("Login")
              .WithTags("Autenticação");
        }
    }
}
