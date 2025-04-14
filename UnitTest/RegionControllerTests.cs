using ITI.Shipping.APIs.Controllers;
using ITI.Shipping.Core.Application.Abstraction;
using ITI.Shipping.Core.Application.Abstraction.Region.Model;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ITI.Shipping.Tests.Controllers
{
    public class RegionControllerTests
    {
        private readonly Mock<IServiceManager> _mockServiceManager;
        private readonly RegionController _controller;

        public RegionControllerTests()
        {
            _mockServiceManager = new Mock<IServiceManager>();
            _controller = new RegionController(_mockServiceManager.Object);
        }

        [Fact]
        public async Task GetAllRegion_ReturnsOkResult_WithListOfRegions()
        {
            // Arrange
            var regions = new List<RegionDto>
            {
                new RegionDto { Id = 1, Governorate = "Governorate1" },
                new RegionDto { Id = 2, Governorate = "Governorate2" }
            };
            _mockServiceManager.Setup(s => s.RegionService.GetRegionsAsync())
                .ReturnsAsync(regions);

            // Act
            var result = await _controller.GetAllRegion();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<RegionDto>>(okResult.Value);
            Assert.Equal(2,returnValue.Count);
        }

        [Fact]
        public async Task GetRegion_ReturnsOkResult_WithRegion()
        {
            // Arrange
            var region = new RegionDto { Id = 1,Governorate = "Governorate1" };
            _mockServiceManager.Setup(s => s.RegionService.GetRegionAsync(1))
                .ReturnsAsync(region);

            // Act
            var result = await _controller.GetRegion(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<RegionDto>(okResult.Value);
            Assert.Equal(1,returnValue.Id);
        }

        [Fact]
        public async Task GetRegion_ReturnsNotFound_WhenRegionDoesNotExist()
        {
            // Arrange
            _mockServiceManager.Setup(s => s.RegionService.GetRegionAsync(1))
                .ReturnsAsync((RegionDto) null);

            // Act
            var result = await _controller.GetRegion(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task AddRegion_ReturnsBadRequest_WhenModelIsNull()
        {
            // Act
            var result = await _controller.AddRegion(null);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Invalid Region data",badRequestResult.Value);
        }

        [Fact]
        public async Task AddRegion_ReturnsOkResult()
        {
            // Arrange
            var region = new RegionDto { Id = 1,Governorate = "Governorate1" };
            _mockServiceManager.Setup(s => s.RegionService.AddAsync(region))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.AddRegion(region);

            // Assert
            var okResult = Assert.IsType<OkResult>(result.Result);
        }

        [Fact]
        public async Task UpdateRegion_ReturnsBadRequest_WhenModelIsNull()
        {
            // Act
            var result = await _controller.UpdateRegion(1,null);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Invalid Region data",badRequestResult.Value);
        }

        [Fact]
        public async Task UpdateRegion_ReturnsNoContent()
        {
            // Arrange
            var region = new RegionDto { Id = 1,Governorate = "Governorate1" };
            _mockServiceManager.Setup(s => s.RegionService.UpdateAsync(region))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdateRegion(1,region);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result.Result);
        }

        [Fact]
        public async Task DeleteRegion_ReturnsNoContent()
        {
            // Arrange
            _mockServiceManager.Setup(s => s.RegionService.DeleteAsync(1))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeletRegion(1);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);
        }
    }
}



