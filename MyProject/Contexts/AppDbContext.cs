using Microsoft.EntityFrameworkCore;
using MyProject.Models;

namespace MyProject.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }

    public DbSet<Student> Students { get; set; }
}
