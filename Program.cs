using Microsoft.EntityFrameworkCore;
using NpgsqlIssue;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<MyContext>(opt => 
    opt.UseLazyLoadingProxies()
       .UseNpgsql(@"Host=localhost;Username=postgres;Password=password;Database=postgres"));

var app = builder.Build();

app.MapGet("/books", async (MyContext db) =>
    await db.Books.ToListAsync());

app.MapGet("/authors", async (MyContext db) =>
    await db.Books.Select(x => x.AuthorName).ToListAsync());

app.MapPost("/books", async (Book book, MyContext db) =>
{
    db.Books.Add(book);
    await db.SaveChangesAsync();

    return Results.Created($"/books/{book.Id}", book);
});

app.Run();