using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
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

        // Many-to-many relation between Vinyl & Genre through VinylGenre
        modelBuilder.Entity<Vinyl>()
            .HasMany(v => v.VinylGenres)
            .WithOne(vg => vg.Vinyl)
            .HasForeignKey(vg => vg.VinylId);

        modelBuilder.Entity<Genre>()
            .HasMany(g => g.VinylGenres)
            .WithOne(vg => vg.Genre)
            .HasForeignKey(vg => vg.GenreId);

        // Seed data
        
        // Seed Admin
        var admins = new List<
        
        // Seeding Addresses
        var addresses = new List<Address>
        {
            new Address { AddressId = 1, Street = "Birkedommervej", StreetNumber = 29, City = "Copenhagen", Postal = 2400, Country = "Denmark" },
            new Address { AddressId = 2, Street = "Dronningsgade", StreetNumber = 8, City = "Fredericia", Postal = 7000, Country = "Denmark"}
        };
        modelBuilder.Entity<Address>().HasData(addresses);

        // Seeding Genres
        var genres = new List<Genre>
        {
            new Genre { GenreId = 1, GenreName = "Alternativ" },
            new Genre { GenreId = 2, GenreName = "HipHop"},
        };
        modelBuilder.Entity<Genre>().HasData(genres);

        // Seeding Vinyls
        var vinyls = new List<Vinyl>
        {
            new Vinyl { VinylId = 1, Title = "Dansktop", Artist = "Ukendt Kunstner", Price = 127, ImagePath = "https://moby-disc.dk/media/catalog/product/cache/e7dc67195437dd6c7bf40d88e25a85ce/i/m/image001_9__2.jpg" },
            new Vinyl { VinylId = 2, Title = "My Beautiful Dark Twisted Fantasy", Artist = "Kanye West", Price = 187, ImagePath = "https://moby-disc.dk/media/catalog/product/cache/e7dc67195437dd6c7bf40d88e25a85ce/d/d/dd049cf1a2c28004de4cd37cb021b315_1.jpg" },
            new Vinyl { VinylId = 3, Title = "OK Computer", Artist = "Radioheaad", Price = 227, ImagePath = "https://moby-disc.dk/media/catalog/product/cache/e7dc67195437dd6c7bf40d88e25a85ce/b/f/bfea3555ad38fe476532c5b54f218c09_1.jpg" },
            // Tilføj yderligere vinylplader her...
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
            new OrderProductDetails { OrderProductDetailsId = 2, OrderId = 2, VinylId = 2, Title = "OK Computer", Price = 227 },
        };
        modelBuilder.Entity<OrderProductDetails>().HasData(orderDetails);

        // Seeding VinylGenres (relation mellem Vinyl og Genre)
        var vinylGenres = new List<VinylGenre>
        {
            new VinylGenre { VinylId = 1, GenreId = 1 },
            new VinylGenre { VinylId = 2, GenreId = 2}
        };
        modelBuilder.Entity<VinylGenre>().HasData(vinylGenres);

        // ...Resten af OnModelCreating...
    }
}