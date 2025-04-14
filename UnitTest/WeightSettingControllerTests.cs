using ITI.Shipping.APIs.Controllers;
using ITI.Shipping.Core.Application.Abstraction;
using ITI.Shipping.Core.Application.Abstraction.WeightSetting.Model;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ITI.Shipping.Tests.Controllers
{
    public class WeightSettingControllerTests
    {
        private readonly Mock<IServiceManager> _mockServiceManager;
        private readonly WeightSettingController _controller;

        public WeightSettingControllerTests()
        {
            _mockServiceManager = new Mock<IServiceManager>();
            _controller = new WeightSettingController(_mockServiceManager.Object);
        }

        [Fact]
        public async Task GetAllWeightSetting_ReturnsOkResult_WithListOfWeightSettings()
        {
            // Arrange
            var weightSettings = new List<WeightSettingDTO>
            {
                new WeightSettingDTO { Id = 1, MinWeight = 0, MaxWeight = 10, CostPerKg = 5 },
                new WeightSettingDTO { Id = 2, MinWeight = 10, MaxWeight = 20, CostPerKg = 10 }
            };
            _mockServiceManager.Setup(s => s.weightSettingService.GetAllWeightSettingAsync())
                .ReturnsAsync(weightSettings);

            // Act
            var result = await _controller.GetAllWeightSetting();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<WeightSettingDTO>>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnValue = Assert.IsType<List<WeightSettingDTO>>(okResult.Value);
            Assert.Equal(2,returnValue.Count);
        }

        [Fact]
        public async Task GetWeightSetting_ReturnsOkResult_WithWeightSetting()
        {
            // Arrange
            var weightSetting = new WeightSettingDTO { Id = 1,MinWeight = 0,MaxWeight = 10,CostPerKg = 5 };
            _mockServiceManager.Setup(s => s.weightSettingService.GetWeightSettingAsync(1))
                .ReturnsAsync(weightSetting);

            // Act
            var result = await _controller.GetWeightSetting(1);

            // Assert
            var actionResult = Assert.IsType<ActionResult<WeightSettingDTO>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnValue = Assert.IsType<WeightSettingDTO>(okResult.Value);
            Assert.Equal(1,returnValue.Id);
        }

        [Fact]
        public async Task AddWeightSetting_ReturnsBadRequest_WhenModelIsNull()
        {
            // Act
            var result = await _controller.AddWeightSetting(null);

            // Assert
            var actionResult = Assert.IsType<ActionResult<WeightSettingDTO>>(result);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            Assert.Equal("Invalid WeightSetting data",badRequestResult.Value);
        }

        [Fact]
        public async Task AddWeightSetting_ReturnsOkResult()
        {
            // Arrange
            var weightSetting = new WeightSettingDTO { Id = 1,MinWeight = 0,MaxWeight = 10,CostPerKg = 5 };
            _mockServiceManager.Setup(s => s.weightSettingService.AddAsync(weightSetting))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.AddWeightSetting(weightSetting);

            // Assert
            var actionResult = Assert.IsType<ActionResult<WeightSettingDTO>>(result);
            var okResult = Assert.IsType<OkResult>(actionResult.Result);
        }

        [Fact]
        public async Task UpdateWeightSetting_ReturnsBadRequest_WhenModelIsNull()
        {
            // Act
            var result = await _controller.UpdateWeightSetting(1,null);

            // Assert
            var actionResult = Assert.IsType<ActionResult<WeightSettingDTO>>(result);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            Assert.Equal("Invalid SpecialCourierRegion data.",badRequestResult.Value);
        }

        [Fact]
        public async Task UpdateWeightSetting_ReturnsNoContent()
        {
            // Arrange
            var weightSetting = new WeightSettingDTO { Id = 1,MinWeight = 0,MaxWeight = 10,CostPerKg = 5 };
            _mockServiceManager.Setup(s => s.weightSettingService.UpdateAsync(weightSetting))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdateWeightSetting(1,weightSetting);

            // Assert
            var actionResult = Assert.IsType<ActionResult<WeightSettingDTO>>(result);
            var noContentResult = Assert.IsType<NoContentResult>(actionResult.Result);
        }

        [Fact]
        public async Task DeleteWeightSetting_ReturnsNoContent()
        {
            // Arrange
            _mockServiceManager.Setup(s => s.weightSettingService.DeleteAsync(1))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteWeightSetting(1);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);
        }

    }
}
