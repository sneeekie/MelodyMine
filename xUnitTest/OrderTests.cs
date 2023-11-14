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
                    new OrderProductDetails { OrderProductDetailsId = 1, OrderId = 1, VinylId = 1, Price = 127 },
                    new OrderProductDetails { OrderProductDetailsId = 2, OrderId = 2, VinylId = 2, Price = 187 },
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
            var service = new OrderService(context);
            int initialCount = context.Orders.Count();

            var newOrder = new Order
            {
                Email = "test@email.com",
                BuyDate = DateTime.Now,
                OrderProductDetails = new List<OrderProductDetails>
                {
                    new OrderProductDetails { OrderProductDetailsId = 3, OrderId = 3, VinylId = 3, Price = 227 },
                    new OrderProductDetails { OrderProductDetailsId = 4, OrderId = 4, VinylId = 4, Price = 777 },
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
        public void CreateOrderProductDetails_DoesNothing_WhenListIsEmpty()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            
            // Arrange
            var service = new OrderService(context);
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
            var service = new OrderService(context);
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
            var service = new OrderService(context);
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
            var service = new OrderService(context);
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
            var service = new OrderService(context);
            int nonExistingOrderId = 999;

            // Act
            var order = service.GetSingleOrderBy(nonExistingOrderId);

            // Assert
            Assert.Null(order);
        }

        [Fact]
        public void GetSingleFullOrderBy_ReturnsNull_WhenIdDoesNotExist()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            
            // Arrange
            var service = new OrderService(context);
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
            var service = new OrderService(context);
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
            var service = new OrderService(context);
            string nonExistingEmail = "nonexistent@email.com";

            // Act
            var order = service.GetSingleOrderBy(nonExistingEmail);

            // Assert
            Assert.Null(order);
        }

        [Fact]
        public void GetSingleFullOrderBy_ReturnsNull_WhenEmailDoesNotExist()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            
            // Arrange
            var service = new OrderService(context);
            const string nonExistingEmail = "nonexistent@email.com";

            // Act
            var order = service.GetSingleFullOrderBy(nonExistingEmail);

            // Assert
            Assert.Null(order);
        }
        
        [Fact]
        public void GetAllOrders_ReturnsAllOrders()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            
            // Arrange
            var service = new OrderService(context);
            int initialCount = context.Orders.Count();

            // Act
            var orders = service.GetAllOrders();

            // Assert
            Assert.Equal(initialCount, orders.Count());
        }

        [Fact]
        public void GetAllFullOrders_ReturnsAllOrdersWithDetails()
        {
            using var context = new ApplicationDbContext(_dbContextOptions);
            
            // Arrange
            var service = new OrderService(context);
            int initialCount = context.Orders.Count();

            // Act
            var orders = service.GetAllFullOrders().ToList();

            // Assert
            Assert.Equal(initialCount, orders.Count);
            Assert.All(orders, order => Assert.NotEmpty(order.OrderProductDetails));
        }
        
        #endregion
    }
}