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
    public DbSet<VinylCover> Covers { get; set; }
    public DbSet<Vinyl> Vinyls { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Genre> Genres { get; set; }
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
        // Joined Primary-Key
        modelBuilder.Entity<VinylGenre>()
            .HasKey(pc => new { pc.VinylId, pc.GenreId });
        
        // Seeding Admin-User
        modelBuilder.Entity<Admin>().HasData(new Admin
        {
            AdminId = 1,
            Username = "Administrator",
            Password = "123"
        });
        
        // Seeding Genre
        string[] genres = new string[]
        {
            "Rock",
            "Pop",
            "Jazz",
            "Classical",
            "Blues",
            "Country",
            "Reggae",
            "Hip-Hop",
            "Electronic",
            "Folk"
        };
        for (int i = 0; i < genres.Length; i++)
        {
            modelBuilder.Entity<Genre>().HasData(new Genre
            {
                GenreId = i + 1,
                GenreName = genres[i]
            });
        }
        
        // Seeding RecordLabel
        for (int i = 1; i <= 5; i++)
        {
            modelBuilder.Entity<RecordLabel>().HasData(new RecordLabel
            {
                RecordLabelId = i,
                LabelName = $"Label-{i}"
            });
        }
        
        // Seeding Vinyl
        Random random = new Random();
        for (int i = 1; i <= 50; i++)
        {
            modelBuilder.Entity<Vinyl>().HasData(new Vinyl
            {
                VinylId = i,
                Title = $"Album-{i}",
                Price = Math.Round(Math.Clamp(random.Next(10, 50) + random.NextDouble(), 10.00, 50.00), 2),
                RecordLabelId = random.Next(1, 5)
            });
        }
        
        // Seeding VinylGenre
        for (int i = 1; i <= 50; i++)
        {
            modelBuilder.Entity<VinylGenre>().HasData(new VinylGenre
            {
                VinylId = i,
                GenreId = random.Next(1, genres.Length)
            });
        }
        
        // Seeding Review
        for (int i = 1; i <= 50; i++)
        {
            modelBuilder.Entity<Review>().HasData(new Review
            {
                ReviewId = i,
                NumStars = random.Next(1, 5),
                ReviewComment = $"Review for album-{i}",
                VinylId = i
            });
        }
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}


