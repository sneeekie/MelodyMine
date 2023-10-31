using DataLayer.Models;
using System.Collections.Generic;
using System.Linq;
using DataLayer;
using DataLayer.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;

namespace xUnitTest
{
    public class GetAllGenresReturnsAllGenres
    {
        private readonly DbContextOptions<ApplicationDbContext> _dbContextOptions;

        public GetAllGenresReturnsAllGenres()
        {
            // Initialisering af in-memory database
            _dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryMelodyMineDatabase")
                .Options;
            
            using (var context = new ApplicationDbContext(_dbContextOptions))
            {
                context.Genres.AddRange(new Genre[]
                {
                    new Genre { GenreId = 1, GenreName = "Rock" },
                    new Genre { GenreId = 2, GenreName = "Jazz" },
                });

                context.SaveChanges();
            }
        }

        [Fact]
        public void GetAllGenres_ReturnsAllGenres()
        {
            using (var context = new ApplicationDbContext(_dbContextOptions))
            {
                // Arrange
                var service = new MelodyMineService(context);

                // Act
                var genres = service.GetAllGenres();

                // Assert
                Assert.Equal(2, genres.Count());
            }
        }
    }
}