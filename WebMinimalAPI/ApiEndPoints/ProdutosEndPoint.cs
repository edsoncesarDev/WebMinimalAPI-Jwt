using Microsoft.EntityFrameworkCore;
using WebMinimalAPI.Context;
using WebMinimalAPI.Models;

namespace WebMinimalAPI.ApiEndPoints
{
    public static class ProdutosEndPoint
    {
        public static void MapProdutosEndPoint(this WebApplication app)
        {
            app.MapGet("/produtos", async (AppDbContext db) => await db.Produtos.ToListAsync()).WithTags("Produtos").RequireAuthorization();

            app.MapGet("/produto/{id:int}", async (int id, AppDbContext db) =>
            {
                return await db.Produtos.FindAsync(id)
                is Produto produto ? Results.Ok(produto) : Results.NotFound();

            }).WithTags("Produtos");

            app.MapPost("/produto", async (Produto produto, AppDbContext db) =>
            {
                if (produto is null)
                    return Results.BadRequest();

                db.Produtos.Add(produto);
                await db.SaveChangesAsync();

                return Results.Created($"/produto/{produto.Id}", produto);

            }).WithTags("Produtos");

            app.MapPut("/produto/{id:int}", async (int id, Produto produto, AppDbContext db) =>
            {
                if (produto.Id != id)
                    return Results.BadRequest();

                var produtoDB = await db.Produtos.FindAsync(id);

                if (produtoDB is null)
                    return Results.NotFound();

                produtoDB.Nome = produto.Nome;
                produtoDB.Descricao = produto.Descricao;
                produtoDB.Preco = produto.Preco;
                produtoDB.ImagemUrl = produto.ImagemUrl;
                produtoDB.Estoque = produto.Estoque;
                produtoDB.DataCadastro = produto.DataCadastro;
                produtoDB.CategoriaId = produto.CategoriaId;

                await db.SaveChangesAsync();
                return Results.Ok(produtoDB);

            }).WithTags("Produtos");

            app.MapDelete("/produto/{id:int}", async (int id, AppDbContext db) =>
            {
                var produtoDB = await db.Produtos.FindAsync(id);
                if (produtoDB is null)
                    return Results.NotFound();

                db.Produtos.Remove(produtoDB);
                db.SaveChanges();

                return Results.NoContent();

            }).WithTags("Produtos");
        }
    }
}
