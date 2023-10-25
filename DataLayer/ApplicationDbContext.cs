using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Logging;

namespace DataLayer;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
        builder.UseNpgsql("Host=localhost;Database=MelodyMineDb;Username=Adrian;Password=123456;");
        return new ApplicationDbContext(builder.Options);
    }
}

public class ApplicationDbContext : DbContext
{
    private Random random = new Random();
    public DbSet<RecordLabel> RecordLabels { get; set; }
    public DbSet<VinylCover> VinylCovers { get; set; }
    public DbSet<Vinyl> Vinyls { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<VinylGenre> VinylGenres { get; set; }
    public DbSet<OrderProductDetails> OrderProductDetails { get; set; }
    public DbSet<Admin> Admins { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information)
            .EnableSensitiveDataLogging(true)
            .UseNpgsql("Host=localhost;Database=MelodyMineDb;Username=Adrian;Password=123456;");
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Join Primary-Key
        modelBuilder.Entity<VinylGenre>()
            .HasKey(pc => new { pc.VinylId, pc.GenreId });
        
        // Seed Admin-User
        modelBuilder.Entity<Admin>().HasData(new Admin
        {
            AdminId = 1,
            Username = "Administrator",
            Password = "123"
        });
        
        // Seed Category
        modelBuilder.Entity<Genre>().HasData(new Genre
        {
            
        });
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}


