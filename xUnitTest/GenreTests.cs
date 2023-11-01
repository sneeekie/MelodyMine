using DataLayer.Models;
using DataLayer;
using DataLayer.Services;
using Microsoft.EntityFrameworkCore;

namespace xUnitTest
{
    public class GenreTests : IDisposable
    {
        private readonly DbContextOptions<ApplicationDbContext> _dbContextOptions;

        public GenreTests()
        {
            // Initialise in-memory database
            _dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            SeedDatabase();
        }
        
        private void SeedDatabase()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            context.Genres.AddRange(new Genre[]
            {
                new Genre { GenreId = 1, GenreName = "Rock" },
                new Genre { GenreId = 2, GenreName = "Jazz" },
            });
            
            context.Vinyls.AddRange(new Vinyl[]
            {
                new Vinyl { VinylId = 1, Title = "My Beautiful Dark Twisted Fantasy", Price = 199.99 },
                new Vinyl { VinylId = 2, Title =  "To Pimp a Butterfly", Price = 159.99 },
            });
            context.SaveChanges();
        }
        
        public void Dispose()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            context.Database.EnsureDeleted();
        }

        [Fact]
        public void GetAllGenres_ReturnsAllGenres()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            
            // Arrange
            var service = new GenreService(context);

            // Act
            var genres = service.GetAllGenres();

            // Assert
            Assert.Equal(2, genres.Count());
        }
        
        [Fact]
        public void GetAllGenres_ReturnsEmptyList_WhenNoGenresExist()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            
            // Arrange
            context.Genres.RemoveRange(context.Genres);
            context.SaveChanges();
            var service = new GenreService(context);

            // Act
            var genres = service.GetAllGenres();

            // Assert
            Assert.Empty(genres);
        }
        
        [Fact]
        public void GetAllGenres_ReturnsAllGenres_IncludingDuplicates()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            
            // Arrange
            context.Genres.Add(new Genre { GenreId = 3, GenreName = "Rock" });
            context.SaveChanges();
            var service = new GenreService(context);

            // Act
            var genres = service.GetAllGenres();

            // Assert
            Assert.Equal(3, genres.Count());
        }
        
        [Fact]
        public void GetGenresById_ReturnsGenre_WhenGenreIdExists()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            
            // Arrange
            var service = new GenreService(context);
            int existingGenreId = 1;

            // Act
            var genres = service.GetGenresById(existingGenreId);

            // Assert
            Assert.Single(genres); // Should return only one genre
            Assert.Equal(existingGenreId, genres.First().GenreId);
        }

        [Fact]
        public void GetGenresById_ReturnsEmpty_WhenGenreIdDoesNotExist()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            
            // Arrange
            var service = new GenreService(context);
            int nonExistingGenreId = 999;

            // Act
            var genres = service.GetGenresById(nonExistingGenreId);

            // Assert
            Assert.Empty(genres);
        }

        [Fact]
        public void GetAllVinylGenres_ReturnsEmpty_WhenNoVinylGenresExist()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            
            // Arrange
            context.VinylGenres.RemoveRange(context.VinylGenres);
            context.SaveChanges();
            var service = new GenreService(context);

            // Act
            var vinylGenres = service.GetAllVinylGenres();

            // Assert
            Assert.Empty(vinylGenres);
        }
        
        [Fact]
        public void GetAllVinylGenres_ReturnsAllVinylGenres()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            
            // Arrange
            context.VinylGenres.AddRange(new VinylGenre[]
            {
                new VinylGenre { VinylId = 1, GenreId = 1 },
                new VinylGenre { VinylId = 2, GenreId = 2 },
            });
            context.SaveChanges();
            var service = new GenreService(context);

            // Act
            var vinylGenres = service.GetAllVinylGenres();

            // Assert
            Assert.Equal(2, vinylGenres.Count());
        }
        
        [Fact]
        public void CreateVinylGenre_AddsNewVinylGenre()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            
            // Arrange
            var service = new GenreService(context);
            int newVinylId = 1;
            int newGenreId = 1;
            int initialCount = context.VinylGenres.Count();

            // Act
            service.CreateVinylGenre(newVinylId, newGenreId);
        
            // Assert
            Assert.Equal(initialCount + 1, context.VinylGenres.Count());
            Assert.NotNull(context.VinylGenres.FirstOrDefault(vg => vg.VinylId == newVinylId && vg.GenreId == newGenreId));
        }
    }
}