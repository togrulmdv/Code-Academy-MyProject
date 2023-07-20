using Microsoft.EntityFrameworkCore;
using MyProject.Models;

namespace MyProject.Contexts;

public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
	{ }

	public DbSet<Slider> Sliders { get; set; } = null!;
	public DbSet<Shipping> Shippings { get; set; } = null!;
}
