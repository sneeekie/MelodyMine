using System;
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
    public DbSet<VinylGenre> VinylGenres { get; set; }
    public DbSet<OrderProductDetails> OrderProductDetails { get; set; }
    public DbSet<Admin> Admins { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured)
            return;
        
        optionsBuilder
            .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information)
            .EnableSensitiveDataLogging(true)
            .UseNpgsql("Host=localhost;Database=MelodyMineDb;Username=Adrian;Password=123456;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Relation between Order & Address
        modelBuilder.Entity<Order>()
            .HasOne<Address>(o => o.Address)
            .WithMany()
            .HasForeignKey(o => o.AddressId);
        
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
        
        // Seed en adresse
        var seedAddress = new Address
        {
            AddressId = 1,
            Street = "Main Street",
            StreetNumber = 123,
            City = "Anytown",
            Postal = 12345,
            Country = "Denmark"
        };
        modelBuilder.Entity<Address>().HasData(seedAddress);
        
        // Seeding Order
        var seedOrder = new Order
        {
            OrderId = 1,
            Email = "customer@example.com",
            BuyDate = DateTime.UtcNow,
            AddressId = seedAddress.AddressId
        };
        modelBuilder.Entity<Order>().HasData(seedOrder);
        
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

        var albums = new[]
        {
            new
            {
                Id = 1, Title = "Dansktop", Artist = "Ukendt Kunstner", Price = 299.99, LabelId = 1, GenreId = 1,
                CoverUrl =
                    "https://1265745076.rsc.cdn77.org/1024/jpg/137381-ukendt-kunstner-dansktop-LP-653a3a115cc88.jpg"
            },
            new
            {
                Id = 2, Title = "808s & Heartbreak", Artist = "Kanye West", Price = 349.99, LabelId = 2, GenreId = 2,
                CoverUrl = "https://1265745076.rsc.cdn77.org/360/jpg/12153-kanye-west-808s-heartbreak-LP-5acfdad3e3ed8.jpg"
            },
            new
            {
                Id = 3, Title = "Ye", Artist = "Kanye West", Price = 399.99, LabelId = 3, GenreId = 3,
                CoverUrl = "https://1265745076.rsc.cdn77.org/360/jpg/21701-5e8fa10e7e35d.jpg"
            },
            new
            {
                Id = 4, Title = "Daytona", Artist = "Pusha T", Price = 249.99, LabelId = 4, GenreId = 4,
                CoverUrl = "https://1265745076.rsc.cdn77.org/360/jpg/46525-pusha-t-daytona-LP-5b7ff01019397.jpg"
            },
            new
            {
                Id = 5, Title = "AT.LONG.LAST.A$AP", Artist = "A$AP Rocky", Price = 199.99, LabelId = 5, GenreId = 5,
                CoverUrl = "https://1265745076.rsc.cdn77.org/360/jpg/8147-a-ap-rocky-at-long-last-a-ap-LP-62ecd211f0860.jpg"
            }
        };

        foreach (var album in albums)
        {
            modelBuilder.Entity<Vinyl>().HasData(new Vinyl
            {
                VinylId = album.Id,
                Title = album.Title,
                Description = album.Artist,
                Price = album.Price,
                RecordLabelId = album.LabelId
            });

            modelBuilder.Entity<VinylCover>().HasData(new VinylCover
            {
                VinylCoverId = album.Id,
                Path = album.CoverUrl,
                VinylId = album.Id
            });

            modelBuilder.Entity<VinylGenre>().HasData(new VinylGenre
            {
                VinylId = album.Id,
                GenreId = album.GenreId
            });

            modelBuilder.Entity<Review>().HasData(new Review
            {
                ReviewId = album.Id,
                NumStars = random.Next(1, 6), // Tilføjelse af en tilfældig stjernebedømmelse
                ReviewComment = $"Review for album {album.Title}",
                VinylId = album.Id
            });
        }
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}