using System.Reflection;
using Domain.Abouts;
using Domain.Roles;
using Domain.ShortUrls;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<About> Abouts { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<ShortUrl> ShortUrls { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}