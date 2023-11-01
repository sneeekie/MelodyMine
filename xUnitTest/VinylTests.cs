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
                new Genre { GenreId = 1, GenreName = "Rock" },
                new Genre { GenreId = 2, GenreName = "Jazz" },
            });

            // Add RecordLabel & Address
            context.RecordLabels.Add(new RecordLabel
            {
                RecordLabelId = 1,
                LabelName = "MyLabel",
                Address = new Address
                {
                    AddressId = 1,
                    Street = "Some Street",
                    City = "Some City",
                    Country = "Denmark"
                }
            });

            // Add Vinyls
            context.Vinyls.AddRange(new Vinyl[]
            {
                new Vinyl
                {
                    VinylId = 1,
                    Title = "My Vinyl",
                    Price = 19.99,
                    RecordLabelId = 1
                },
                new Vinyl
                {
                    VinylId = 2,
                    Title = "Another Vinyl",
                    Price = 29.99,
                    RecordLabelId = 1
                }
            });

            // Add Reviews to Vinyl
            context.Reviews.Add(new Review
            {
                ReviewId = 1,
                NumStars = 4,
                ReviewComment = "Great!",
                VinylId = 1
            });

            // Add Covers to Vinyl
            context.Covers.Add(new VinylCover
            {
                VinylCoverId = 1,
                Path = "some/url",
                VinylId = 1
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
        public void CreateVinyl_AddsNewVinyl()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            
            // Arrange
            var service = new MelodyMineService(context);
            var newVinyl = new Vinyl { VinylId = 3, Title = "Graduation", Price = 29.99 };
            int initialCount = context.Vinyls.Count();

            // Act
            service.CreateVinyl(newVinyl);

            // Assert
            Assert.Equal(initialCount + 1, context.Vinyls.Count());
            Assert.NotNull(context.Vinyls.FirstOrDefault(v => v.VinylId == newVinyl.VinylId));
        }
        
        [Fact]
        public void DeleteVinylById_DeletesVinyl()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            
            // Arrange
            var service = new MelodyMineService(context);
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
            var service = new MelodyMineService(context);
            int nonExistingVinylId = 999;
            int initialCount = context.Vinyls.Count();

            // Act
            service.DeleteVinylById(nonExistingVinylId);

            // Assert
            Assert.Equal(initialCount, context.Vinyls.Count());
        }
        
        [Fact]
        public void GetSingleVinylBy_ReturnsVinyl_WhenIdExists()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            
            // Arrange
            var service = new MelodyMineService(context);
            int existingVinylId = 1;

            // Act
            var result = service.GetSingleVinylBy(existingVinylId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(existingVinylId, result.VinylId);
        }
        
        [Fact]
        public void GetSingleVinylBy_ReturnsNull_WhenIdDoesNotExist()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            
            // Arrange
            var service = new MelodyMineService(context);
            int nonExistingVinylId = 999;

            // Act
            var result = service.GetSingleVinylBy(nonExistingVinylId);

            // Assert
            Assert.Null(result);
        }
        
        [Fact]
        public void GetSingleFullVinylBy_ReturnsVinylWithAllIncludedEntities_WhenIdExists()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            
            // Arrange
            var service = new MelodyMineService(context);
            int existingVinylId = 1;

            // Act
            var result = service.GetSingleFullVinylBy(existingVinylId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(existingVinylId, result.VinylId);
            Assert.NotNull(result.Reviews);
            Assert.NotNull(result.Covers);
            Assert.NotNull(result.Genres);
            Assert.NotNull(result.RecordLabel);
            Assert.NotNull(result.RecordLabel.Address);
        }
        
        [Fact]
        public void GetSingleFullVinylBy_ReturnsNull_WhenIdDoesNotExist()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            
            // Arrange
            var service = new MelodyMineService(context);
            int nonExistingVinylId = 999;

            // Act
            var result = service.GetSingleFullVinylBy(nonExistingVinylId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetSingleFullVinylBy_ReturnsVinylWithAllData_WhenVinylExists()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            
            // Arrange
            var service = new MelodyMineService(context);
            int existingVinylId = 1;

            // Act
            var vinyl = service.GetSingleFullVinylBy(existingVinylId);

            // Assert
            Assert.NotNull(vinyl);
            Assert.NotNull(vinyl.Reviews);
            Assert.NotNull(vinyl.Covers);
            Assert.NotNull(vinyl.Genres);
            Assert.NotNull(vinyl.RecordLabel);
            Assert.NotNull(vinyl.RecordLabel.Address);
        }
        
        [Fact]
        public void GetSingleFullVinylBy_ReturnsNull_WhenVinylDoesNotExist()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            
            // Arrange
            var service = new MelodyMineService(context);
            int nonExistingVinylId = 999;

            // Act
            var vinyl = service.GetSingleFullVinylBy(nonExistingVinylId);

            // Assert
            Assert.Null(vinyl);
        }
        
        [Fact]
        public void GetSingleVinylBy_ReturnsVinyl_WhenTitleExists()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            
            // Arrange
            var service = new MelodyMineService(context);
            string existingTitle = "My Vinyl";

            // Act
            var vinyl = service.GetSingleVinylBy(existingTitle);

            // Assert
            Assert.NotNull(vinyl);
            Assert.Equal(existingTitle, vinyl.Title);
        }

        [Fact]
        public void GetSingleVinylBy_ReturnsNull_WhenTitleDoesNotExist()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            
            // Arrange
            var service = new MelodyMineService(context);
            string nonExistingTitle = "Non Existing Title";

            // Act
            var vinyl = service.GetSingleVinylBy(nonExistingTitle);

            // Assert
            Assert.Null(vinyl);
        }

        [Fact]
        public void GetSingleVinylBy_IsCaseSensitive()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            
            // Arrange
            var service = new MelodyMineService(context);
            string caseSensitiveTitle = "my vinyl";

            // Act
            var vinyl = service.GetSingleVinylBy(caseSensitiveTitle);

            // Assert
            Assert.Null(vinyl);
        }

        [Fact]
        public void GetSingleFullVinylBy_ReturnsVinylWithAllData_WhenTitleExists()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            
            // Arrange
            var service = new MelodyMineService(context);
            string existingTitle = "My Vinyl";

            // Act
            var vinyl = service.GetSingleFullVinylBy(existingTitle);

            // Assert
            Assert.NotNull(vinyl);
            Assert.Equal(existingTitle, vinyl.Title);
            Assert.NotNull(vinyl.Reviews);
            Assert.NotNull(vinyl.Covers);
        }

        [Fact]
        public void GetSingleFullVinylBy_ReturnsNull_WhenTitleDoesNotExist()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            
            // Arrange
            var service = new MelodyMineService(context);
            string nonExistingTitle = "Non Existing Title";

            // Act
            var vinyl = service.GetSingleFullVinylBy(nonExistingTitle);

            // Assert
            Assert.Null(vinyl);
        }

        [Fact]
        public void GetSingleFullVinylBy_IsCaseSensitive()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            
            // Arrange
            var service = new MelodyMineService(context);
            string caseSensitiveTitle = "my vinyl";

            // Act
            var vinyl = service.GetSingleFullVinylBy(caseSensitiveTitle);

            // Assert
            Assert.Null(vinyl);
        }

        [Fact]
        public void UpdateVinylBy_UpdatesVinylSuccessfully_WhenIdExists()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            
            // Arrange
            var service = new MelodyMineService(context);
            int existingVinylId = 1;
            Vinyl newVinyl = new Vinyl { Title = "New Title", Description = "New Description", Price = 25.99 };

            // Act
            service.UpdateVinylBy(existingVinylId, newVinyl);

            // Assert
            var updatedVinyl = context.Vinyls.Find(existingVinylId);
            Assert.Equal(newVinyl.Title, updatedVinyl?.Title);
            Assert.Equal(newVinyl.Description, updatedVinyl?.Description);
            Assert.Equal(newVinyl.Price, updatedVinyl?.Price);
        }

        [Fact]
        public void UpdateVinylBy_DoesNothing_WhenIdDoesNotExist()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            
            // Arrange
            var service = new MelodyMineService(context);
            int nonExistingVinylId = 999;
            Vinyl newVinyl = new Vinyl { Title = "New Title", Description = "New Description", Price = 25.99 };

            // Act
            service.UpdateVinylBy(nonExistingVinylId, newVinyl);

            // Assert
            var updatedVinyl = context.Vinyls.Find(nonExistingVinylId);
            Assert.Null(updatedVinyl);
        }

        [Fact]
        public void UpdateVinylByTitle_UpdatesCorrectly_WhenTitleExists()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            
            // Arrange
            var service = new MelodyMineService(context);
            string existingTitle = "My Vinyl";
            Vinyl newVinyl = new Vinyl { Title = "New Title", Price = 29.99 };

            // Act
            service.UpdateVinylBy(existingTitle, newVinyl);

            // Assert
            var updatedVinyl = context.Vinyls.Where(v => v.Title == "New Title").FirstOrDefault();
            Assert.NotNull(updatedVinyl);
            Assert.Equal(29.99, updatedVinyl.Price);
        }

        [Fact]
        public void UpdateVinylByTitle_DoesNothing_WhenTitleDoesNotExist()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            
            // Arrange
            var service = new MelodyMineService(context);
            string nonExistingTitle = "Non Existing";
            Vinyl newVinyl = new Vinyl { Title = "New Title", Price = 29.99 };
            int initialCount = context.Vinyls.Count();

            // Act
            service.UpdateVinylBy(nonExistingTitle, newVinyl);

            // Assert
            Assert.Equal(initialCount, context.Vinyls.Count());
        }

        [Fact]
        public void GetAllVinyls_ReturnsAllVinyls()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            
            // Arrange
            var service = new MelodyMineService(context);

            // Act
            var vinyls = service.GetAllVinyls();

            // Assert
            Assert.Equal(2, vinyls.Count());
        }

        [Fact]
        public void GetAllVinyls_ReturnsVinylsWithAssociatedData()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            
            // Arrange
            var service = new MelodyMineService(context);

            // Act
            var vinyls = service.GetAllVinyls().ToList();

            // Assert
            Assert.NotNull(vinyls[0].Reviews);
            Assert.NotNull(vinyls[0].Covers);
            Assert.NotNull(vinyls[0].Genres);
        }

        [Fact]
        public void GetAllVinyls_ReturnsEmptyList_WhenNoVinylsExist()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            
            // Arrange
            context.Vinyls.RemoveRange(context.Vinyls);
            context.SaveChanges();
            var service = new MelodyMineService(context);

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
            var service = new MelodyMineService(context);
            int currentPage = 1;
            int pageSize = 2;

            // Act
            var vinyls = service.GetAllVinylsPaged(currentPage, pageSize);

            // Assert
            Assert.Equal(pageSize, vinyls.Count());
        }
        
        [Fact]
        public void GetAllFullVinyls_ReturnsAllVinylsWithAllAssociatedData()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            
            // Arrange
            var service = new MelodyMineService(context);

            // Act
            var vinyls = service.GetAllFullVinyls();

            // Assert
            Assert.Equal(2, vinyls.Count());
            Assert.All(vinyls, v => Assert.NotNull(v.Reviews));
            Assert.All(vinyls, v => Assert.NotNull(v.Covers));
            Assert.All(vinyls, v => Assert.NotNull(v.Genres));
            Assert.All(vinyls, v => Assert.NotNull(v.RecordLabel));
        }

        [Fact]
        public void GetAllFullVinyls_ReturnsEmptyList_WhenNoVinylsExist()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            
            // Arrange
            context.Vinyls.RemoveRange(context.Vinyls);
            context.SaveChanges();
            var service = new MelodyMineService(context);

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
            var service = new MelodyMineService(context);
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
            var service = new MelodyMineService(context);
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
            var service = new MelodyMineService(context);
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
        public void FilterVinylsPaged_ReturnsCorrectVinyls_WhenUsingSearchTerm()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            
            // Arrange
            var service = new MelodyMineService(context);
            int currentPage = 1;
            int pageSize = 2; 
            string searchTerm = "My Vinyl";
            int? genreId = null;
            string? filterTitle = null;
            string? price = null;

            // Act
            var vinyls = service.FilterVinylsPaged(currentPage, pageSize, searchTerm, genreId, filterTitle, price).ToList();

            // Assert
            Assert.Single(vinyls);
            Assert.Equal("My Vinyl", vinyls[0].Title);
        }
        
        [Fact]
        public void FilterVinyls_ReturnsAllVinyls_WhenNoFiltersApplied()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            
            // Arrange
            var service = new MelodyMineService(context);
            int expectedCount = context.Vinyls.Count();

            // Act
            var vinyls = service.FilterVinyls(null, null, null, null).ToList();

            // Assert
            Assert.Equal(expectedCount, vinyls.Count);
        }

        [Fact]
        public void FilterVinyls_ReturnsCorrectVinyls_WhenUsingSearchTerm()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            
            // Arrange
            var service = new MelodyMineService(context);

            // Act
            var vinyls = service.FilterVinyls("My Vinyl", null, null, null).ToList();

            // Assert
            Assert.Single(vinyls);
            Assert.Equal("My Vinyl", vinyls[0].Title);
        }

        [Fact]
        public void FilterVinyls_ReturnsCorrectVinyls_WhenUsingGenreId()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            
            // Arrange
            var service = new MelodyMineService(context);

            // Act
            var vinyls = service.FilterVinyls(null, 1, null, null).ToList();

            // Assert
            Assert.Single(vinyls);
        }
        
        
        #endregion
    }
}