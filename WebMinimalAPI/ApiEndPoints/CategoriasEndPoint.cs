using Microsoft.EntityFrameworkCore;
using WebMinimalAPI.Context;
using WebMinimalAPI.Models;

namespace WebMinimalAPI.ApiEndPoints
{
    public static class CategoriasEndPoint
    {
        public static void MapCategoriasEndPoint(this WebApplication app)
        {
            // EndPoint Categorias

            app.MapGet("/", () => "Catálogo de Produtos - 2022").WithTags("Categorias").ExcludeFromDescription();

            app.MapGet("/categorias", async (AppDbContext db) => await db.Categorias.ToListAsync()).WithTags("Categorias").RequireAuthorization();

            app.MapGet("/categoria/{id:int}", async (int id, AppDbContext db) =>
            {
                return await db.Categorias.FindAsync(id)
                is Categoria categoria ? Results.Ok(categoria) : Results.NotFound();

            }).WithTags("Categorias");

            app.MapPost("/categorias", async (Categoria categoria, AppDbContext db) =>
            {
                if (categoria is null)
                    return Results.BadRequest();

                db.Categorias.Add(categoria);
                await db.SaveChangesAsync();

                return Results.Created($"/categoria/{categoria.Id}", categoria);

            }).WithTags("Categorias");

            app.MapPut("/categoria/{id:int}", async (int id, Categoria categoria, AppDbContext db) =>
            {
                if (categoria.Id != id)
                    return Results.BadRequest();

                var categoriaDB = await db.Categorias.FindAsync(id);

                if (categoriaDB is null)
                    return Results.NotFound();

                categoriaDB.Nome = categoria.Nome;
                categoriaDB.ImagemUrl = categoria.ImagemUrl;

                await db.SaveChangesAsync();
                return Results.Ok(categoriaDB);

            }).WithTags("Categorias");

            app.MapDelete("/categoria/{id:int}", async (int id, AppDbContext db) =>
            {
                var categoriaDB = await db.Categorias.FindAsync(id);
                if (categoriaDB is null)
                    return Results.NotFound();

                db.Categorias.Remove(categoriaDB);
                db.SaveChanges();

                return Results.NoContent();

            }).WithTags("Categorias");
        }
    }
}
