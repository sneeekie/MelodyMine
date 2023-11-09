using Microsoft.EntityFrameworkCore;
using DataLayer.Models;
using DataLayer;
using DataLayer.Services;

namespace xUnitTest
{
    public class VinylTests : IDisposable
    {
        private readonly DbContextOptions<ApplicationDbContext> _dbContextOptions;

        public VinylTests()
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

            // Add Genres
            context.Genres.AddRange(new Genre[]
            {
                new Genre { GenreId = 1, GenreName = "Alternativ" },
                new Genre { GenreId = 2, GenreName = "HipHop"},
                new Genre { GenreId = 3, GenreName = "Pop"},
                new Genre { GenreId = 4, GenreName = "Christmas"}
            });

            // Add Vinyls
            context.Vinyls.AddRange(new Vinyl[]
            {
                new Vinyl { VinylId = 1, Title = "Dansktop", Artist = "Ukendt Kunstner", Price = 127, GenreId = 2, ImagePath = "https://moby-disc.dk/media/catalog/product/cache/e7dc67195437dd6c7bf40d88e25a85ce/i/m/image001_9__2.jpg" },
                new Vinyl { VinylId = 2, Title = "Ye", Artist = "Kanye West", Price = 187, GenreId = 2, ImagePath = "https://moby-disc.dk/media/catalog/product/cache/e7dc67195437dd6c7bf40d88e25a85ce/k/a/kanye-west-2018-ye-compact-disc.jpg" },
                new Vinyl { VinylId = 3, Title = "OK Computer", Artist = "Radioheaad", Price = 227, GenreId = 1,  ImagePath = "https://moby-disc.dk/media/catalog/product/cache/e7dc67195437dd6c7bf40d88e25a85ce/b/f/bfea3555ad38fe476532c5b54f218c09_1.jpg" },
                new Vinyl { VinylId = 4, Title = "Blonde", Artist = "Frank Ocean", Price = 777, GenreId = 3, ImagePath = "https://best-fit.transforms.svdcdn.com/production/albums/frank-ocean-blond-compressed-0933daea-f052-40e5-85a4-35e07dac73df.jpg?w=469&h=469&q=100&auto=format&fit=crop&dm=1643652677&s=6ef41cb2628eb28d736e27b42635b66e" },
                new Vinyl { VinylId = 5, Title = "Winter Wonderland", Artist = "Dean Martin", Price = 127, GenreId = 4, ImagePath = "https://moby-disc.dk/media/catalog/product/cache/e7dc67195437dd6c7bf40d88e25a85ce/m/o/moby-disc-13-09-2023_10.54.44.png"}
            });

            // Add VinylGenres to Vinyl
            context.VinylGenres.Add(new VinylGenre
            {
                VinylId = 1,
                GenreId = 1
            });

            context.SaveChanges();
        }

        public void Dispose()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            context.Database.EnsureDeleted();
        }

        #region Tests for Vinyls
        [Fact]
        public void DeleteVinylById_DeletesVinyl()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            
            // Arrange
            var service = new VinylService(context);
            int existingVinylId = 1;
            int initialCount = context.Vinyls.Count();

            // Act
            service.DeleteVinylById(existingVinylId);

            // Assert
            Assert.Equal(initialCount - 1, context.Vinyls.Count());
            Assert.Null(context.Vinyls.FirstOrDefault(v => v.VinylId == existingVinylId));
        }
        
        [Fact]
        public void DeleteVinylById_DoesNothing_WhenIdDoesNotExist()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            
            // Arrange
            var service = new VinylService(context);
            int nonExistingVinylId = 999;
            int initialCount = context.Vinyls.Count();

            // Act
            service.DeleteVinylById(nonExistingVinylId);

            // Assert
            Assert.Equal(initialCount, context.Vinyls.Count());
        }

        [Fact]
        public void UpdateVinylBy_UpdatesVinylSuccessfully_WhenIdExists()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            
            // Arrange
            var service = new VinylService(context);
            int existingVinylId = 1;
            Vinyl newVinyl = new Vinyl { Title = "New Title", Artist = "New Artist", Price = 25.99 };

            // Act
            service.UpdateVinylBy(existingVinylId, newVinyl);

            // Assert
            var updatedVinyl = context.Vinyls.Find(existingVinylId);
            Assert.Equal(newVinyl.Title, updatedVinyl?.Title);
            Assert.Equal(newVinyl.Artist, updatedVinyl?.Artist);
            Assert.Equal(newVinyl.Price, updatedVinyl?.Price);
        }

        [Fact]
        public void UpdateVinylBy_DoesNothing_WhenIdDoesNotExist()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            
            // Arrange
            var service = new VinylService(context);
            int nonExistingVinylId = 999;
            Vinyl newVinyl = new Vinyl { Title = "New Title", Artist = "New Description", Price = 25.99 };

            // Act
            service.UpdateVinylBy(nonExistingVinylId, newVinyl);

            // Assert
            var updatedVinyl = context.Vinyls.Find(nonExistingVinylId);
            Assert.Null(updatedVinyl);
        }

        [Fact]
        public void GetAllVinyls_ReturnsEmptyList_WhenNoVinylsExist()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            
            // Arrange
            context.Vinyls.RemoveRange(context.Vinyls);
            context.SaveChanges();
            var service = new VinylService(context);

            // Act
            var vinyls = service.GetAllVinyls();

            // Assert
            Assert.Empty(vinyls);
        }

        [Fact]
        public void GetAllVinylsPaged_ReturnsCorrectNumberOfVinylsOnFirstPage()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            
            // Arrange
            var service = new VinylService(context);
            int currentPage = 1;
            int pageSize = 2;

            // Act
            var vinyls = service.GetAllVinylsPaged(currentPage, pageSize);

            // Assert
            Assert.Equal(pageSize, vinyls.Count());
        }

        [Fact]
        public void GetAllFullVinyls_ReturnsEmptyList_WhenNoVinylsExist()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            
            // Arrange
            context.Vinyls.RemoveRange(context.Vinyls);
            context.SaveChanges();
            var service = new VinylService(context);

            // Act
            var vinyls = service.GetAllFullVinyls();

            // Assert
            Assert.Empty(vinyls);
        }

        [Fact]
        public void GetAllFullVinyls_ReturnsCorrectNumberOfVinyls()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            
            // Arrange
            var service = new VinylService(context);
            int expectedCount = context.Vinyls.Count();

            // Act
            var vinyls = service.GetAllFullVinyls();

            // Assert
            Assert.Equal(expectedCount, vinyls.Count());
        }

        [Fact]
        public void GetAllFullVinylsPaged_ReturnsCorrectNumberOfVinylsOnFirstPage()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            
            // Arrange
            var service = new VinylService(context);
            int currentPage = 1;
            int pageSize = 2;

            // Act
            var vinyls = service.GetAllFullVinylsPaged(currentPage, pageSize);

            // Assert
            Assert.Equal(pageSize, vinyls.Count());
        }
        
        [Fact]
        public void FilterVinylsPaged_ReturnsCorrectNumberOfVinyls()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            
            // Arrange
            var service = new VinylService(context);
            int currentPage = 1;
            int pageSize = 2;
            string? searchTerm = null;
            int? genreId = null;
            string? filterTitle = null;
            string? price = null;

            // Act
            var vinyls = service.FilterVinylsPaged(currentPage, pageSize, searchTerm, genreId, filterTitle, price).ToList();

            // Assert
            Assert.Equal(pageSize, vinyls.Count);
        }
        
        [Fact]
        public void FilterVinyls_ReturnsAllVinyls_WhenNoFiltersApplied()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            
            // Arrange
            var service = new VinylService(context);
            int expectedCount = context.Vinyls.Count();

            // Act
            var vinyls = service.FilterVinyls(null, null, null, null).ToList();

            // Assert
            Assert.Equal(expectedCount, vinyls.Count);
        }

        [Fact]
        public void FilterVinyls_ReturnsCorrectVinyls_WhenUsingGenreId()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            
            // Arrange
            var service = new VinylService(context);

            // Act
            var vinyls = service.FilterVinyls(null, 1, null, null).ToList();

            // Assert
            Assert.Single(vinyls);
        }
        
        
        #endregion
    }
}