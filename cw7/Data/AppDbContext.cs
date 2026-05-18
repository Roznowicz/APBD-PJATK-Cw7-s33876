using cw7.Models;
using Microsoft.EntityFrameworkCore;

namespace cw7.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Pc> Pcs { get; set; }

    public DbSet<Component> Components { get; set; }

    public DbSet<ComponentManufacturer> ComponentManufacturers { get; set; }

    public DbSet<ComponentType> ComponentTypes { get; set; }

    public DbSet<PcComponent> PcComponents { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PcComponent>()
            .HasKey(x => new { x.PcId, x.ComponentCode });
    }
}