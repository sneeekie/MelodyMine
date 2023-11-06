using Microsoft.EntityFrameworkCore;
using DataLayer.Models;
using DataLayer;
using DataLayer.Services;

namespace xUnitTest
{
    public class RecordLabelTests : IDisposable
    {
        private readonly DbContextOptions<ApplicationDbContext> _dbContextOptions;

        public RecordLabelTests()
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

            var seedAddress = new Address
            {
                AddressId = 1,
                Street = "Cool Street 80",
                City = "Cool City",
                Country = "Denmark",
                Postal = 7000
            };

            var seedRecordLabel = new RecordLabel
            {
                RecordLabelId = 1,
                LabelName = "Awesome Records",
                PhoneNumber = 12345678,
                Email = "info@awesome.com",
                AddressId = 1,
                Address = seedAddress,
                Vinyls = new List<Vinyl>
                {
                    new Vinyl { VinylId = 1, Title = "The Life of Pablo", Description = "KANYE" }
                }
            };

            context.Addresses.Add(seedAddress);
            context.RecordLabels.Add(seedRecordLabel);
            context.SaveChanges();
        }

        public void Dispose()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            context.Database.EnsureDeleted();
        }

        #region RecordLabelTests
        
        [Fact]
        public void GetRecordLabelById_ReturnsCorrectRecordLabel()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            
            // Arrange
            var service = new RecordLabelService(context);
            int targetRecordLabelId = 1;

            // Act
            var recordLabels = service.GetRecordLabelById(targetRecordLabelId).ToList();

            // Assert
            Assert.Single(recordLabels);
            var recordLabel = recordLabels.First();
            Assert.Equal(targetRecordLabelId, recordLabel.RecordLabelId);
            Assert.NotNull(recordLabel.Address);
            Assert.Equal(7000, recordLabel.Address.Postal);
            Assert.Equal("The Life of Pablo", recordLabel.Vinyls.First().Title);
            Assert.Equal("KANYE", recordLabel.Vinyls.First().Description);
        }

        [Fact]
        public void GetRecordLabelByIdSimple_ReturnsCorrectRecordLabel()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            
            // Arrange
            var service = new RecordLabelService(context);
            int targetRecordLabelId = 1;

            // Act
            var recordLabel = service.GetRecordLabelByIdSimple(targetRecordLabelId);

            // Assert
            Assert.NotNull(recordLabel);
            Assert.Equal(targetRecordLabelId, recordLabel.RecordLabelId);
            Assert.Null(recordLabel.Address);
            Assert.Null(recordLabel.Vinyls);
        }

        [Fact]
        public void GetAllRecordLabels_ReturnsAllRecordLabels()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            
            // Arrange
            var service = new RecordLabelService(context);
        
            // Act
            var recordLabels = service.GetAllRecordLabels().ToList();
        
            // Assert
            Assert.NotEmpty(recordLabels);
            Assert.IsType<List<RecordLabel>>(recordLabels);
        }

        [Fact]
        public void CreateRecordLabel_AddsNewRecordLabel()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            
            // Arrange
            var service = new RecordLabelService(context);
            int initialCount = context.RecordLabels.Count();
            var newRecordLabel = new RecordLabel
            {
                LabelName = "New Records",
                PhoneNumber = 789101112,
                Email = "new@records.com"
            };

            // Act
            service.CreateRecordLabel(newRecordLabel);

            // Assert
            Assert.Equal(initialCount + 1, context.RecordLabels.Count());
            var createdRecordLabel = context.RecordLabels.Last();
            Assert.Equal("New Records", createdRecordLabel.LabelName);
        }

        [Fact]
        public void DeleteRecordLabel_RemovesRecordLabel()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            
            // Arrange
            var service = new RecordLabelService(context);
            int initialCount = context.RecordLabels.Count();
            var existingRecordLabel = context.RecordLabels.First();

            // Act
            service.DeleteRecordLabel(existingRecordLabel);

            // Assert
            Assert.Equal(initialCount - 1, context.RecordLabels.Count());
        }

        [Fact]
        public void DeleteRecordLabel_DoesNothing_WhenRecordLabelDoesNotExist()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            
            // Arrange
            var service = new RecordLabelService(context);
            int initialCount = context.RecordLabels.Count();
            var nonExistingRecordLabel = new RecordLabel { RecordLabelId = 999, LabelName = "Non-Existent" };

            // Act
            service.DeleteRecordLabel(nonExistingRecordLabel);

            // Assert
            Assert.Equal(initialCount, context.RecordLabels.Count());
        }
        #endregion
    }
}