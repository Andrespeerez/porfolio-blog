using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Post> Posts => Set<Post>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
        modelBuilder.Entity<User>().HasIndex(u => u.Slug).IsUnique();

        modelBuilder.Entity<Category>().HasIndex(c => c.Slug).IsUnique();

        modelBuilder.Entity<Post>().HasIndex(p => p.Slug).IsUnique();
        modelBuilder.Entity<Post>()
            .HasMany(p => p.Categories)
            .WithMany(c => c.Posts)
            .UsingEntity(
                "PostCategory",
                l => l.HasOne(typeof(Category)).WithMany().HasForeignKey("CategoryId"),
                r => r.HasOne(typeof(Post)).WithMany().HasForeignKey("PostId")
            );
    }
}