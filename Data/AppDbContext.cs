using Microsoft.EntityFrameworkCore;

namespace JsonApiExample.Data;

public class AppDbContext : DbContext
{
    public DbSet<Country> Countries => Set<Country>();
    public DbSet<Region> Regions => Set<Region>();

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Country>(entity =>
        {
            entity.ToTable("Country");
            entity.OwnsOne(c => c.Statistics, p => {
                p.Property(a => a.Population).HasColumnName("Population");
                p.Property(a => a.Gdp).HasColumnName("EstimatePop");
            });
        });
    }
}