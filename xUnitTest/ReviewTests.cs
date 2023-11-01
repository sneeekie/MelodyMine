using Microsoft.EntityFrameworkCore;
using DataLayer.Models;
using DataLayer;
using DataLayer.Services;

namespace xUnitTest
{
    public class ReviewTests : IDisposable
    {
        private readonly DbContextOptions<ApplicationDbContext> _dbContextOptions;

        public ReviewTests()
        {
            // Initialize in-memory database
            _dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            SeedDatabase();
        }

        private void SeedDatabase()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);

            var seedVinyl = new Vinyl
            {
                VinylId = 1,
                Title = "Test Title",
                Description = "Test Description"
            };

            var seedReviews = new List<Review>
            {
                new Review { ReviewId = 1, NumStars = 5, VinylId = 1 },
                new Review { ReviewId = 2, NumStars = 4, VinylId = 1 }
            };

            context.Vinyls.Add(seedVinyl);
            context.Reviews.AddRange(seedReviews);
            context.SaveChanges();
        }

        public void Dispose()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            context.Database.EnsureDeleted();
        }

        #region ReviewTests
        
        [Fact]
        public void GetReviewsByVinylID_ReturnsCorrectReviews()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            
            // Arrange
            var service = new ReviewService(context);
            int targetVinylId = 1;
            var expectedReviewIds = new List<int> { 1, 2 };

            // Act
            var reviews = service.getReviewsByVinylID(targetVinylId).ToList();

            // Assert
            Assert.Equal(expectedReviewIds.Count, reviews.Count);
            Assert.All(reviews, r => Assert.Equal(targetVinylId, r.VinylId));
            Assert.All(reviews, r => Assert.Contains(r.ReviewId, expectedReviewIds));
        }
        
        [Fact]
        public void CreateReview_AddsNewReviewToDatabase()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            
            // Arrange
            var service = new ReviewService(context);
            int initialCount = context.Reviews.Count();
            var newReview = new Review
            {
                ReviewId = 3,
                NumStars = 4,
                VinylId = 1
            };

            // Act
            service.CreateReview(newReview);

            // Assert
            Assert.Equal(initialCount + 1, context.Reviews.Count());
            var insertedReview = context.Reviews.Find(newReview.ReviewId);
            Assert.NotNull(insertedReview);
            Assert.Equal(newReview.NumStars, insertedReview.NumStars);
            Assert.Equal(newReview.VinylId, insertedReview.VinylId);
        }
        #endregion
    }
}
