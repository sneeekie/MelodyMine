using Microsoft.EntityFrameworkCore;
using DataLayer.Models;
using DataLayer;
using DataLayer.Services;

namespace xUnitTest
{
    public class OrderTests : IDisposable
    {
        private readonly DbContextOptions<ApplicationDbContext> _dbContextOptions;

        public OrderTests()
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
        
            var seedOrder = new Order
            {
                OrderId = 1,
                Email = "seeded@email.com",
                BuyDate = DateTime.Now,
                OrderProductDetails = new List<OrderProductDetails>
                {
                    new OrderProductDetails { ProductId = 1, Name = "SeededProduct1", Price = 25.0 },
                    new OrderProductDetails { ProductId = 2, Name = "SeededProduct2", Price = 35.0 }
                }
            };
        
            context.Orders.Add(seedOrder);
            context.SaveChanges();
        }

        public void Dispose()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            context.Database.EnsureDeleted();
        }

        #region Tests for Orders

        [Fact]
        public void CreateOrder_SuccessfullyCreatesAnOrder()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            
            // Arrange
            var service = new MelodyMineService(context);
            int initialCount = context.Orders.Count();

            var newOrder = new Order
            {
                Email = "test@email.com",
                BuyDate = DateTime.Now,
                OrderProductDetails = new List<OrderProductDetails>
                {
                    new OrderProductDetails { ProductId = 3, Name = "TestProduct1", Price = 10.0 },
                    new OrderProductDetails { ProductId = 4, Name = "TestProduct2", Price = 15.0 }
                }
            };

            // Act
            service.CreateOrder(newOrder);

            // Assert
            Assert.Equal(initialCount + 1, context.Orders.Count()); 
            var createdOrder = context.Orders.Include(o => o.OrderProductDetails).Last();
            Assert.Equal("test@email.com", createdOrder.Email);
            Assert.Equal(2, createdOrder.OrderProductDetails.Count);
        }

        [Fact]
        public void CreateOrderProductDetails_AddsMultipleOrderProductDetails()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            
            // Arrange
            var service = new MelodyMineService(context);
            int initialCount = context.OrderProductDetails.Count();
        
            var newOrderProductDetailsList = new List<OrderProductDetails>
            {
                new OrderProductDetails { ProductId = 1, Name = "NewProduct1", Price = 25.0, OrderId = 1 },
                new OrderProductDetails { ProductId = 2, Name = "NewProduct2", Price = 35.0, OrderId = 1 }
            };

            // Act
            service.CreateOrderProductDetails(newOrderProductDetailsList);

            // Assert
            Assert.Equal(initialCount + 2, context.OrderProductDetails.Count());
        }

        [Fact]
        public void CreateOrderProductDetails_DoesNothing_WhenListIsEmpty()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            
            // Arrange
            var service = new MelodyMineService(context);
            int initialCount = context.OrderProductDetails.Count();
        
            var newOrderProductDetailsList = new List<OrderProductDetails>();

            // Act
            service.CreateOrderProductDetails(newOrderProductDetailsList);

            // Assert
            Assert.Equal(initialCount, context.OrderProductDetails.Count());
        }

        [Fact]
        public void DeleteOrder_DeletesOrder_WhenOrderExists()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            
            // Arrange
            var service = new MelodyMineService(context);
            int initialCount = context.Orders.Count();
            var existingOrder = context.Orders.First();

            // Act
            service.DeleteOrder(existingOrder);

            // Assert
            Assert.Equal(initialCount - 1, context.Orders.Count());
        }

        [Fact]
        public void DeleteOrder_DoesNothing_WhenOrderDoesNotExist()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            
            // Arrange
            var service = new MelodyMineService(context);
            int initialCount = context.Orders.Count();
            var nonExistingOrder = new Order { OrderId = 999, Email = "nonexisting@email.com", BuyDate = DateTime.Now };

            // Act
            service.DeleteOrder(nonExistingOrder);

            // Assert
            Assert.Equal(initialCount, context.Orders.Count());
        }
        
        [Fact]
        public void GetSingleOrderBy_ReturnsOrder_WhenIdExists()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            
            // Arrange
            var service = new MelodyMineService(context);
            int existingOrderId = 1;

            // Act
            var order = service.GetSingleOrderBy(existingOrderId);

            // Assert
            Assert.NotNull(order);
            Assert.Equal(existingOrderId, order.OrderId);
        }

        [Fact]
        public void GetSingleOrderBy_ReturnsNull_WhenIdDoesNotExist()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            
            // Arrange
            var service = new MelodyMineService(context);
            int nonExistingOrderId = 999;

            // Act
            var order = service.GetSingleOrderBy(nonExistingOrderId);

            // Assert
            Assert.Null(order);
        }

        [Fact]
        public void GetSingleFullOrderBy_ReturnsOrderWithDetails_WhenIdExists()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            
            // Arrange
            var service = new MelodyMineService(context);
            int existingOrderId = 1;

            // Act
            var order = service.GetSingleFullOrderBy(existingOrderId);

            // Assert
            Assert.NotNull(order);
            Assert.Equal(existingOrderId, order.OrderId);
            Assert.NotNull(order.OrderProductDetails);
            Assert.NotEmpty(order.OrderProductDetails);
        }

        [Fact]
        public void GetSingleFullOrderBy_ReturnsNull_WhenIdDoesNotExist()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            
            // Arrange
            var service = new MelodyMineService(context);
            int nonExistingOrderId = 999;

            // Act
            var order = service.GetSingleFullOrderBy(nonExistingOrderId);

            // Assert
            Assert.Null(order);
        }

        [Fact]
        public void GetSingleOrderBy_ReturnsOrder_WhenEmailExists()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            
            // Arrange
            var service = new MelodyMineService(context);
            string existingEmail = "seeded@email.com";

            // Act
            var order = service.GetSingleOrderBy(existingEmail);

            // Assert
            Assert.NotNull(order);
            Assert.Equal(existingEmail, order.Email);
        }

        [Fact]
        public void GetSingleOrderBy_ReturnsNull_WhenEmailDoesNotExist()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            
            // Arrange
            var service = new MelodyMineService(context);
            string nonExistingEmail = "nonexistent@email.com";

            // Act
            var order = service.GetSingleOrderBy(nonExistingEmail);

            // Assert
            Assert.Null(order);
        }

        [Fact]
        public void GetSingleFullOrderBy_ReturnsFullOrder_WhenEmailExists()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            
            // Arrange
            var service = new MelodyMineService(context);
            string existingEmail = "seeded@email.com";

            // Act
            var order = service.GetSingleFullOrderBy(existingEmail);

            // Assert
            Assert.NotNull(order);
            Assert.Equal(existingEmail, order.Email);
            Assert.NotNull(order.OrderProductDetails);
            Assert.True(order.OrderProductDetails.Count > 0);
        }

        [Fact]
        public void GetSingleFullOrderBy_ReturnsNull_WhenEmailDoesNotExist()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            
            // Arrange
            var service = new MelodyMineService(context);
            const string nonExistingEmail = "nonexistent@email.com";

            // Act
            var order = service.GetSingleFullOrderBy(nonExistingEmail);

            // Assert
            Assert.Null(order);
        }

        [Fact]
        public void UpdateVinylBy_UpdatesCorrectly_WhenOrderIdExists()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            
            // Arrange
            var service = new MelodyMineService(context);
            int existingOrderId = 1;

            var newOrder = new Order
            {
                Email = "updated@email.com",
                BuyDate = DateTime.Now
            };

            // Act
            service.UpdateVinylBy(existingOrderId, newOrder);

            // Assert
            var updatedOrder = context.Orders.Find(existingOrderId);
            Assert.Equal("updated@email.com", updatedOrder?.Email);
        }

        [Fact]
        public void UpdateVinylBy_DoesNothing_WhenOrderIdDoesNotExist()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            
            // Arrange
            var service = new MelodyMineService(context);
            int nonExistingOrderId = 999;

            var newOrder = new Order
            {
                Email = "updated@email.com",
                BuyDate = DateTime.Now
            };

            // Act
            service.UpdateVinylBy(nonExistingOrderId, newOrder);

            // Assert
            var order = context.Orders.Find(nonExistingOrderId);
            Assert.Null(order);
        }

        [Fact]
        public void UpdateOrderBy_UpdatesCorrectly_WhenEmailExists()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            
            // Arrange
            var service = new MelodyMineService(context);
            string existingEmail = "seeded@email.com";
            var newOrder = new Order
            {
                Email = "new@email.com",
                BuyDate = DateTime.Now
            };
            Assert.NotNull(context.Orders.FirstOrDefault(o => o.Email == existingEmail));


            // Act
            service.UpdateOrderBy(existingEmail, newOrder);

            // Assert
            var updatedOrder = context.Orders.Where(o => o.Email == newOrder.Email).FirstOrDefault();
            Assert.NotNull(updatedOrder);
            Assert.Equal(newOrder.BuyDate, updatedOrder.BuyDate);
        }
        
        [Fact]
        public void UpdateOrderBy_DoesNothing_WhenEmailDoesNotExist()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            
            // Arrange
            var service = new MelodyMineService(context);
            string nonExistingEmail = "nonexisting@email.com";
            var newOrder = new Order
            {
                Email = "new@email.com",
                BuyDate = DateTime.Now
            };
            int initialCount = context.Orders.Count();

            // Act
            service.UpdateOrderBy(nonExistingEmail, newOrder);

            // Assert
            Assert.Equal(initialCount, context.Orders.Count());
        }
        
        [Fact]
        public void GetAllOrders_ReturnsAllOrders()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            
            // Arrange
            var service = new MelodyMineService(context);
            int initialCount = context.Orders.Count();

            // Act
            var orders = service.GetAllOrders();

            // Assert
            Assert.Equal(initialCount, orders.Count());
        }

        [Fact]
        public void GetAllOrders_ReturnsEmptyList_WhenNoOrders()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            
            // Arrange
            var service = new MelodyMineService(context);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            // Act
            var orders = service.GetAllOrders();

            // Assert
            Assert.Empty(orders);
        }

        [Fact]
        public void GetAllFullOrders_ReturnsAllOrdersWithDetails()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            
            // Arrange
            var service = new MelodyMineService(context);
            int initialCount = context.Orders.Count();

            // Act
            var orders = service.GetAllFullOrders().ToList();

            // Assert
            Assert.Equal(initialCount, orders.Count);
            Assert.All(orders, order => Assert.NotEmpty(order.OrderProductDetails));
        }

        [Fact]
        public void GetAllFullOrders_ReturnsEmptyList_WhenNoOrders()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            
            // Arrange
            var service = new MelodyMineService(context);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            // Act
            var orders = service.GetAllFullOrders().ToList();

            // Assert
            Assert.Empty(orders);
        }
        
        #endregion
    }
}