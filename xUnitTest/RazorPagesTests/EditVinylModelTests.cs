using Microsoft.AspNetCore.Mvc;

namespace xUnitTest.RazorPagesTests;

using Moq;
using Xunit;
using DataLayer.Services;
using MelodyMine.Pages;
using DataLayer.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

public class EditVinylModelTests
{
    [Fact]
    public void OnGet_WhenVinylExists_PopulatesThePageModelWithVinylAndGenres()
    {
        // Arrange
        int vinylId = 1;
        var mockVinylService = new Mock<IVinylService>();
        var mockGenreService = new Mock<IGenreService>();

        mockVinylService.Setup(service => service.GetVinylById(vinylId))
            .Returns(new Vinyl { VinylId = vinylId, Title = "Test", Artist = "Test Artist" });

        mockGenreService.Setup(service => service.GetAllGenres())
            .Returns(new List<Genre>
            {
                new Genre { GenreId = 1, GenreName = "Rock" },
                new Genre { GenreId = 2, GenreName = "Jazz" }
            }.AsQueryable());

        var pageModel = new EditVinylModel(mockVinylService.Object, mockGenreService.Object);

        // Act
        pageModel.OnGet(vinylId);

        // Assert
        Assert.NotNull(pageModel.UpdateModel);
        Assert.NotNull(pageModel.GenreOptions);
        Assert.Equal(vinylId, pageModel.UpdateModel.VinylId);
    }
    
    [Fact]
    public async Task OnPostAsync_WhenModelIsValid_UpdatesVinylAndRedirects()
    {
        // Arrange
        var mockVinylService = new Mock<IVinylService>();
        var mockGenreService = new Mock<IGenreService>();
        var vinylToUpdate = new Vinyl { VinylId = 1, Title = "Test", Artist = "Test Artist" };

        mockVinylService.Setup(service => service.UpdateVinylBy(vinylToUpdate.VinylId, vinylToUpdate))
            .Verifiable();

        var pageModel = new EditVinylModel(mockVinylService.Object, mockGenreService.Object)
        {
            UpdateModel = vinylToUpdate
        };

        // Act
        var result = await pageModel.OnPostAsync();

        // Assert
        var redirectToPageResult = Assert.IsType<RedirectToPageResult>(result);
        Assert.Equal("./Vinyls", redirectToPageResult.PageName);
        mockVinylService.Verify();
    }

}
