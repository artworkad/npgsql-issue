using Microsoft.EntityFrameworkCore;

namespace NpgsqlIssue;

public class MyContext : DbContext
{
    public MyContext(DbContextOptions<MyContext> options)
        : base(options)
    {
    }

    public DbSet<Book> Books { get; set; }

    public DbSet<Author> Authors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>()
            .HasOne(b => b.Author);
    }
}

public class Book
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public virtual Author Author { get; set; }

    public string AuthorName => Author.Name;
}

public class Author
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
}
