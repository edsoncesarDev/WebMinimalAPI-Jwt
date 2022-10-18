using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using WebMinimalAPI.ApiEndPoints;
using WebMinimalAPI.AppServicesExtensions;
using WebMinimalAPI.Context;
using WebMinimalAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Chamada dos métodos staticos da classe ServiceCollectionExtensions
builder.AddApiSwagger();
builder.AddPersistence();
builder.Services.AddCors();
builder.AddAutenticationJwt();

var app = builder.Build();

// chamada do método EndPoint login
app.MapAutenticacaoEndPoint();

// chamada do método EndPoint Categorias
app.MapCategoriasEndPoint();

// EndPoint do método EndPoint Produtos
app.MapProdutosEndPoint();

// Chamada dos métodos staticos da classe AplicationBuilderExtensions
var environment = app.Environment;
app.UseExceptionHandling(environment)
    .UseSwaggerMiddleware()
    .UseAppCors();


// adicionando middleware Authentication
app.UseAuthentication();

// adicionando middleware Authorization
app.UseAuthorization();


app.Run();
