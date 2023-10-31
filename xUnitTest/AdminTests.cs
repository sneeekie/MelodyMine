using System;
using Microsoft.EntityFrameworkCore;
using Xunit;
using DataLayer.Models;
using DataLayer;
using DataLayer.Services;

namespace xUnitTest
{
    public class AdminTests : IDisposable
    {
        private readonly DbContextOptions<ApplicationDbContext> _dbContextOptions;

        public AdminTests()
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

            var seedAdmin = new Admin
            {
                AdminId = 1,
                Username = "admin",
                Password = "password",
                SignedIn = false
            };

            context.Admins.Add(seedAdmin);
            context.SaveChanges();
        }

        public void Dispose()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            context.Database.EnsureDeleted();
        }

        #region AdminTests
        
        [Fact]
        public void LogIn_ReturnsTrue_WhenCredentialsAreCorrect()
        {
            using (var context = new ApplicationDbContext(_dbContextOptions))
            {
                // Arrange
                var service = new MelodyMineService(context);
                var admin = new Admin { Username = "admin", Password = "password" };

                // Act
                bool result = service.LogIn(admin);

                // Assert
                Assert.True(result);
            }
        }

        [Fact]
        public void LogIn_ReturnsFalse_WhenCredentialsAreWrong()
        {
            using (var context = new ApplicationDbContext(_dbContextOptions))
            {
                // Arrange
                var service = new MelodyMineService(context);
                var admin = new Admin { Username = "admin", Password = "wrongpassword" };

                // Act
                bool result = service.LogIn(admin);

                // Assert
                Assert.False(result);
            }
        }
        
        [Fact]
        public void LogOut_ReturnsTrue_WhenAdminExists()
        {
            using (var context = new ApplicationDbContext(_dbContextOptions))
            {
                // Arrange
                var service = new MelodyMineService(context);
                var admin = new Admin { Username = "admin", Password = "password" };

                // Make sure the admin is logged in
                service.LogIn(admin);

                // Act
                bool result = service.LogOut(admin);

                // Assert
                Assert.True(result);
            }
        }

        [Fact]
        public void LogOut_ReturnsFalse_WhenAdminDoesNotExist()
        {
            using (var context = new ApplicationDbContext(_dbContextOptions))
            {
                // Arrange
                var service = new MelodyMineService(context);
                var admin = new Admin { Username = "nonexistent", Password = "password" };

                // Act
                bool result = service.LogOut(admin);

                // Assert
                Assert.False(result);
            }
        }

        [Fact]
        public void SignedIn_ReturnsTrue_WhenAdminIsSignedIn()
        {
            using (var context = new ApplicationDbContext(_dbContextOptions))
            {
                // Arrange
                var service = new MelodyMineService(context);
                var admin = new Admin { Username = "admin", Password = "password" };
                service.LogIn(admin);

                // Act
                bool result = service.SignedIn(admin.Username);

                // Assert
                Assert.True(result);
            }
        }

        [Fact]
        public void SignedIn_ReturnsFalse_WhenAdminIsNotSignedIn()
        {
            using (var context = new ApplicationDbContext(_dbContextOptions))
            {
                // Arrange
                var service = new MelodyMineService(context);
                var admin = new Admin { Username = "admin", Password = "password" };

                // Act
                bool result = service.SignedIn(admin.Username);

                // Assert
                Assert.False(result);
            }
        }

        [Fact]
        public void SignedIn_ReturnsFalse_WhenAdminDoesNotExist()
        {
            using (var context = new ApplicationDbContext(_dbContextOptions))
            {
                // Arrange
                var service = new MelodyMineService(context);

                // Act
                bool result = service.SignedIn("nonexistent");

                // Assert
                Assert.False(result);
            }
        }
        
        [Fact]
        public void AnySignedIn_ReturnsTrue_WhenAtLeastOneAdminIsSignedIn()
        {
            using (var context = new ApplicationDbContext(_dbContextOptions))
            {
                // Arrange
                var service = new MelodyMineService(context);
                var admin = new Admin { Username = "admin", Password = "password" };

                // Act and Assert - Check if LogIn is successful
                bool loggedIn = service.LogIn(admin);
                Assert.True(loggedIn, "Admin should be logged in");

                // Act - Test AnySignedIn method
                bool result = service.AnySignedIn();

                // Assert - Final check
                Assert.True(result);
            }
        }


        [Fact]
        public void AnySignedIn_ReturnsFalse_WhenNoAdminIsSignedIn()
        {
            using (var context = new ApplicationDbContext(_dbContextOptions))
            {
                // Arrange
                var service = new MelodyMineService(context);

                // Act
                bool result = service.AnySignedIn();

                // Assert
                Assert.False(result);
            }
        }

        #endregion
    }
}
