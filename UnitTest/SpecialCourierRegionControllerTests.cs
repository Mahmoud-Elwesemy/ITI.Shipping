using ITI.Shipping.APIs.Controllers;
using ITI.Shipping.Core.Application.Abstraction;
using ITI.Shipping.Core.Application.Abstraction.SpecialCourierRegion.Model;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ITI.Shipping.Tests.Controllers
{
    public class SpecialCourierRegionControllerTests
    {
        private readonly Mock<IServiceManager> _mockServiceManager;
        private readonly SpecialCourierRegionController _controller;

        public SpecialCourierRegionControllerTests()
        {
            _mockServiceManager = new Mock<IServiceManager>();
            _controller = new SpecialCourierRegionController(_mockServiceManager.Object);
        }

        [Fact]
        public async Task GetAllSpecialCourierRegions_ReturnsOkResult_WithListOfSpecialCourierRegions()
        {
            // Arrange
            var specialCourierRegions = new List<SpecialCourierRegionDTO>
            {
                new SpecialCourierRegionDTO { Id = 1, RegionName = "Region1", CourierName = "Courier1" },
                new SpecialCourierRegionDTO { Id = 2, RegionName = "Region2", CourierName = "Courier2" }
            };
            _mockServiceManager.Setup(s => s.SpecialCourierRegionService.GetSpecialCourierRegionsAsync())
                .ReturnsAsync(specialCourierRegions);

            // Act
            var result = await _controller.GetAllSpecialCourierRegions();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<SpecialCourierRegionDTO>>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnValue = Assert.IsType<List<SpecialCourierRegionDTO>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task GetSpecialCourierRegion_ReturnsOkResult_WithSpecialCourierRegion()
        {
            // Arrange
            var specialCourierRegion = new SpecialCourierRegionDTO { Id = 1, RegionName = "Region1", CourierName = "Courier1" };
            _mockServiceManager.Setup(s => s.SpecialCourierRegionService.GetSpecialCourierRegionAsync(1))
                .ReturnsAsync(specialCourierRegion);

            // Act
            var result = await _controller.GetSpecialCourierRegion(1);

            // Assert
            var actionResult = Assert.IsType<ActionResult<SpecialCourierRegionDTO>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnValue = Assert.IsType<SpecialCourierRegionDTO>(okResult.Value);
            Assert.Equal(1, returnValue.Id);
        }

        [Fact]
        public async Task AddSpecialCourierRegion_ReturnsBadRequest_WhenModelIsNull()
        {
            // Act
            var result = await _controller.AddSpecialCourierRegion(null);

            // Assert
            var actionResult = Assert.IsType<ActionResult<SpecialCourierRegionDTO>>(result);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            Assert.Equal("Invalid SpecialCourierRegion data", badRequestResult.Value);
        }

        [Fact]
        public async Task AddSpecialCourierRegion_ReturnsOkResult()
        {
            // Arrange
            var specialCourierRegion = new SpecialCourierRegionDTO { Id = 1, RegionName = "Region1", CourierName = "Courier1" };
            _mockServiceManager.Setup(s => s.SpecialCourierRegionService.AddAsync(specialCourierRegion))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.AddSpecialCourierRegion(specialCourierRegion);

            // Assert
            var actionResult = Assert.IsType<ActionResult<SpecialCourierRegionDTO>>(result);
            var okResult = Assert.IsType<OkResult>(actionResult.Result);
        }

        [Fact]
        public async Task UpdateSpecialCourierRegion_ReturnsBadRequest_WhenModelIsNull()
        {
            // Act
            var result = await _controller.UpdateSpecialCourierRegion(1,null);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Invalid SpecialCourierRegion data.",badRequestResult.Value);
        }



        [Fact]
        public async Task UpdateSpecialCourierRegion_ReturnsNoContent()
        {
            // Arrange
            var specialCourierRegion = new SpecialCourierRegionDTO { Id = 1, RegionName = "Region1", CourierName = "Courier1" };
            _mockServiceManager.Setup(s => s.SpecialCourierRegionService.UpdateAsync(specialCourierRegion))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdateSpecialCourierRegion(1, specialCourierRegion);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteSpecialCourierRegion_ReturnsNoContent()
        {
            // Arrange
            _mockServiceManager.Setup(s => s.SpecialCourierRegionService.DeleteAsync(1))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteSpecialCourierRegion(1);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);
        }
    }
}
