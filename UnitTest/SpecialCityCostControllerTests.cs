using ITI.Shipping.APIs.Controllers;
using ITI.Shipping.Core.Application.Abstraction;
using ITI.Shipping.Core.Application.Abstraction.SpecialCityCost.Model;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ITI.Shipping.Tests.Controllers
{
    public class SpecialCityCostControllerTests
    {
        private readonly Mock<IServiceManager> _mockServiceManager;
        private readonly SpecialCityCostController _controller;

        public SpecialCityCostControllerTests()
        {
            _mockServiceManager = new Mock<IServiceManager>();
            _controller = new SpecialCityCostController(_mockServiceManager.Object);
        }

        [Fact]
        public async Task GetAllSpecialCityCost_ReturnsOkResult_WithListOfSpecialCityCosts()
        {
            // Arrange
            var specialCityCosts = new List<SpecialCityCostDTO>
                {
                    new SpecialCityCostDTO { Id = 1, CitySettingName = "City1", MerchantName = "Merchant1" },
                    new SpecialCityCostDTO { Id = 2, CitySettingName = "City2", MerchantName = "Merchant2" }
                };
            _mockServiceManager.Setup(s => s.specialCityCostService.GetAllSpecialCityCostAsync())
                .ReturnsAsync(specialCityCosts);

            // Act
            var result = await _controller.GetAllSpecialCityCost();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<SpecialCityCostDTO>>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnValue = Assert.IsType<List<SpecialCityCostDTO>>(okResult.Value);
            Assert.Equal(2,returnValue.Count);
        }

        [Fact]
        public async Task GetSpecialCityCost_ReturnsOkResult_WithSpecialCityCost()
        {
            // Arrange
            var specialCityCost = new SpecialCityCostDTO { Id = 1,CitySettingName = "City1",MerchantName = "Merchant1" };
            _mockServiceManager.Setup(s => s.specialCityCostService.GetSpecialCityCostAsync(1))
                .ReturnsAsync(specialCityCost);

            // Act
            var result = await _controller.GetSpecialCityCost(1);

            // Assert
            var actionResult = Assert.IsType<ActionResult<SpecialCityCostDTO>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnValue = Assert.IsType<SpecialCityCostDTO>(okResult.Value);
            Assert.Equal(1,returnValue.Id);
        }

        [Fact]
        public async Task AddSpecialCityCost_ReturnsBadRequest_WhenModelIsNull()
        {
            // Act
            var result = await _controller.AddSpecialCityCost(null);

            // Assert
            var actionResult = Assert.IsType<ActionResult<SpecialCityCostDTO>>(result);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            Assert.Equal("Invalid SpecialCityCost data",badRequestResult.Value);
        }

        [Fact]
        public async Task AddSpecialCityCost_ReturnsOkResult()
        {
            // Arrange
            var specialCityCost = new SpecialCityCostDTO { Id = 1,CitySettingName = "City1",MerchantName = "Merchant1" };
            _mockServiceManager.Setup(s => s.specialCityCostService.AddAsync(specialCityCost))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.AddSpecialCityCost(specialCityCost);

            // Assert
            var actionResult = Assert.IsType<ActionResult<SpecialCityCostDTO>>(result);
            var okResult = Assert.IsType<OkResult>(actionResult.Result);
        }

        [Fact]
        public async Task UpdateSpecialCityCost_ReturnsBadRequest_WhenModelIsNull()
        {
            // Act
            var result = await _controller.UpdateSpecialCityCost(1,null);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Invalid SpecialCityCost data.",badRequestResult.Value);
        }


        [Fact]
        public async Task UpdateSpecialCityCost_ReturnsNoContent()
        {
            // Arrange
            var specialCityCost = new SpecialCityCostDTO { Id = 1,CitySettingName = "City1",MerchantName = "Merchant1" };
            _mockServiceManager.Setup(s => s.specialCityCostService.UpdateAsync(specialCityCost))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdateSpecialCityCost(1,specialCityCost);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteSpecialCityCost_ReturnsNoContent()
        {
            // Arrange
            _mockServiceManager.Setup(s => s.specialCityCostService.DeleteAsync(1))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteSpecialCityCost(1);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);
        }
    }
}

