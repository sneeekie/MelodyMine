using Moq;
using Xunit;
using DataLayer.Services;
using System.Linq;
using MelodyMine.Pages;
using System.Collections.Generic;
using DataLayer.Models;

namespace xUnitTest.RazorPagesTests;

public class ShopModelTests
{
    /*[Fact]
    public void OnGet_PopulatesThePageModel_WithAListOfVinyls()
    {
        // Arrange
        var mockService = new Mock<IVinylService>();
        mockService.Setup(service => service.GetAllVinyls())
            .Returns(new List<Vinyl> 
            {
                new Vinyl { VinylId = 1, Title = "Dansktop", Artist = "Ukendt Kunstner", Price = 127, GenreId = 2, ImagePath = "https://moby-disc.dk/media/catalog/product/cache/e7dc67195437dd6c7bf40d88e25a85ce/i/m/image001_9__2.jpg" },
                new Vinyl { VinylId = 2, Title = "Ye", Artist = "Kanye West", Price = 187, GenreId = 2, ImagePath = "https://moby-disc.dk/media/catalog/product/cache/e7dc67195437dd6c7bf40d88e25a85ce/k/a/kanye-west-2018-ye-compact-disc.jpg" },
            }.AsQueryable());

        var pageModel = new ShopModel(mockService.Object);

        // Act
        pageModel.OnGet();

        // Assert
        Assert.NotNull(pageModel.Vinyls);
        Assert.Equal(2, pageModel.Vinyls.Count());
    }
    */
}