using Microsoft.EntityFrameworkCore;
using Project_polsl.Models;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<PostSection> PostSections { set; get; }
}