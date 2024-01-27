using ExamCSharp;
using Microsoft.EntityFrameworkCore;

public class ApplicationContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    public ApplicationContext() => Database.EnsureCreated();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=game.db");
    }
}