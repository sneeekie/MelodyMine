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
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured)
            return;
        
        optionsBuilder
            .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information)
            .EnableSensitiveDataLogging(true)
            .UseNpgsql("Host=localhost;Database=MelodyMineDb;Username=Adrian;Password=123456;");
    }
    
    // DbSet properties
    public DbSet<Vinyl> Vinyls { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderProductDetails> OrderProductDetails { get; set; }
    public DbSet<VinylGenre> VinylGenres { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure keys for VinylGenre
        modelBuilder.Entity<VinylGenre>()
            .HasKey(vg => new { vg.VinylId, vg.GenreId });

        // Relation between Order & Address
        modelBuilder.Entity<Order>()
            .HasOne(o => o.Address)
            .WithMany()
            .HasForeignKey(o => o.AddressId);

        // Relation between OrderProductDetails & Order
        modelBuilder.Entity<OrderProductDetails>()
            .HasOne(opd => opd.Order)
            .WithMany(o => o.OrderProductDetails)
            .HasForeignKey(opd => opd.OrderId);

        // Many-to-many relation between Vinyl & Genre, through VinylGenre
        modelBuilder.Entity<Vinyl>()
            .HasMany(v => v.VinylGenres)
            .WithOne(vg => vg.Vinyl)
            .HasForeignKey(vg => vg.VinylId);

        modelBuilder.Entity<Genre>()
            .HasMany(g => g.VinylGenres)
            .WithOne(vg => vg.Genre)
            .HasForeignKey(vg => vg.GenreId);

        #region DataSeeding
        
        // Seeding Addresses
        var addresses = new List<Address>
        {
            new Address { AddressId = 1, Street = "Birkedommervej", StreetNumber = 29, City = "Copenhagen", Postal = 2400, Country = "Denmark", CardNumber = 1244444444444444},
            new Address { AddressId = 2, Street = "Dronningsgade", StreetNumber = 8, City = "Fredericia", Postal = 7000, Country = "Denmark", CardNumber = 1331131331131331},
        };
        modelBuilder.Entity<Address>().HasData(addresses);

        // Seeding Genres
        var genres = new List<Genre>
        {
            new Genre { GenreId = 1, GenreName = "Alternativ" },
            new Genre { GenreId = 2, GenreName = "HipHop"},
            new Genre { GenreId = 3, GenreName = "Pop"},
            new Genre { GenreId = 4, GenreName = "Christmas"}
        };
        modelBuilder.Entity<Genre>().HasData(genres);

        // Seeding Vinyls
        var vinyls = new List<Vinyl>
        {
            new Vinyl { VinylId = 1, Title = "Dansktop", Artist = "Ukendt Kunstner", Price = 127, GenreId = 2, ImagePath = "https://moby-disc.dk/media/catalog/product/cache/e7dc67195437dd6c7bf40d88e25a85ce/i/m/image001_9__2.jpg" },
            new Vinyl { VinylId = 2, Title = "Ye", Artist = "Kanye West", Price = 187, GenreId = 2, ImagePath = "https://moby-disc.dk/media/catalog/product/cache/e7dc67195437dd6c7bf40d88e25a85ce/k/a/kanye-west-2018-ye-compact-disc.jpg" },
            new Vinyl { VinylId = 3, Title = "OK Computer", Artist = "Radioheaad", Price = 227, GenreId = 1,  ImagePath = "https://moby-disc.dk/media/catalog/product/cache/e7dc67195437dd6c7bf40d88e25a85ce/b/f/bfea3555ad38fe476532c5b54f218c09_1.jpg" },
            new Vinyl { VinylId = 4, Title = "Blonde", Artist = "Frank Ocean", Price = 777, GenreId = 3, ImagePath = "https://best-fit.transforms.svdcdn.com/production/albums/frank-ocean-blond-compressed-0933daea-f052-40e5-85a4-35e07dac73df.jpg?w=469&h=469&q=100&auto=format&fit=crop&dm=1643652677&s=6ef41cb2628eb28d736e27b42635b66e" },
            new Vinyl { VinylId = 5, Title = "Winter Wonderland", Artist = "Dean Martin", Price = 127, GenreId = 4, ImagePath = "https://moby-disc.dk/media/catalog/product/cache/e7dc67195437dd6c7bf40d88e25a85ce/m/o/moby-disc-13-09-2023_10.54.44.png"}
        };
        modelBuilder.Entity<Vinyl>().HasData(vinyls);

        // Seeding Orders
        var orders = new List<Order>
        {
            new Order { OrderId = 1, Email = "john@example.com", BuyDate = DateTime.UtcNow, AddressId = 1 },
            new Order { OrderId = 2, Email = "adrian@example.com", BuyDate = DateTime.UtcNow, AddressId = 2 },
        };
        modelBuilder.Entity<Order>().HasData(orders);

        // Seeding OrderProductDetails
        var orderDetails = new List<OrderProductDetails>
        {
            new OrderProductDetails { OrderProductDetailsId = 1, OrderId = 1, VinylId = 1, Title = "Dansktop", Price = 127 },
            new OrderProductDetails { OrderProductDetailsId = 2, OrderId = 2, VinylId = 2, Title = "Ye", Price = 187 },
        };
        modelBuilder.Entity<OrderProductDetails>().HasData(orderDetails);

        // Seeding VinylGenres (relation between Vinyl & Genre)
        var vinylGenres = new List<VinylGenre>
        {
            new VinylGenre { VinylId = 1, GenreId = 1 },
            new VinylGenre { VinylId = 2, GenreId = 2},
            new VinylGenre { VinylId = 3, GenreId = 2},
            new VinylGenre { VinylId = 4, GenreId = 3},
            new VinylGenre { VinylId = 5, GenreId = 4}
        };
        modelBuilder.Entity<VinylGenre>().HasData(vinylGenres);
        
        #endregion
    }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}