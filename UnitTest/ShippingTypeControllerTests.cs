using ITI.Shipping.APIs.Controllers;
using ITI.Shipping.Core.Application.Abstraction;
using ITI.Shipping.Core.Application.Abstraction.ShippingType.Model;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ITI.Shipping.Tests.Controllers
{
    public class ShippingTypeControllerTests
    {
        private readonly Mock<IServiceManager> _mockServiceManager;
        private readonly ShippingTypeController _controller;

        public ShippingTypeControllerTests()
        {
            _mockServiceManager = new Mock<IServiceManager>();
            _controller = new ShippingTypeController(_mockServiceManager.Object);
        }

        [Fact]
        public async Task GetAllShippingTypes_ReturnsOkResult_WithListOfShippingTypes()
        {
            // Arrange
            var shippingTypes = new List<ShippingTypeDTO>
            {
                new ShippingTypeDTO { Id = 1, Name = "Type1" },
                new ShippingTypeDTO { Id = 2, Name = "Type2" }
            };
            _mockServiceManager.Setup(s => s.shippingTypeService.GetAllShippingTypeAsync())
                .ReturnsAsync(shippingTypes);

            // Act
            var result = await _controller.GetAllShippingType();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<ShippingTypeDTO>>(okResult.Value);
            Assert.Equal(2,returnValue.Count);
        }

        [Fact]
        public async Task GetShippingType_ReturnsOkResult_WithShippingType()
        {
            // Arrange
            var shippingType = new ShippingTypeDTO { Id = 1,Name = "Type1" };
            _mockServiceManager.Setup(s => s.shippingTypeService.GetShippingTypeAsync(1))
                .ReturnsAsync(shippingType);

            // Act
            var result = await _controller.GetShippingType(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<ShippingTypeDTO>(okResult.Value);
            Assert.Equal(1,returnValue.Id);
        }

        [Fact]
        public async Task AddShippingType_ReturnsBadRequest_WhenModelIsNull()
        {
            // Act
            var result = await _controller.AddShippingType(null);

            // Assert
            var actionResult = Assert.IsType<ActionResult<ShippingTypeDTO>>(result);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            Assert.Equal("Invalid ShippingType data",badRequestResult.Value);
        }

        [Fact]
        public async Task AddShippingType_ReturnsOkResult()
        {
            // Arrange
            var shippingType = new ShippingTypeDTO { Id = 1,Name = "Type1" };
            _mockServiceManager.Setup(s => s.shippingTypeService.AddAsync(shippingType))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.AddShippingType(shippingType);

            // Assert
            var okResult = Assert.IsType<OkResult>(result.Result);
        }
        
        [Fact]
        public async Task UpdateShippingType_ReturnsBadRequest_WhenModelIsNull()
        {
            // Act
            var result = await _controller.UpdateShippingType(1,null);

            // Assert
            var actionResult = Assert.IsType<ActionResult<ShippingTypeDTO>>(result);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            Assert.Equal("Invalid ShippingType data",badRequestResult.Value);
        }

        [Fact]
        public async Task UpdateShippingType_ReturnsNoContent()
        {
            // Arrange
            var shippingType = new ShippingTypeDTO { Id = 1,Name = "Type1" };
            _mockServiceManager.Setup(s => s.shippingTypeService.UpdateAsync(shippingType))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdateShippingType(1,shippingType);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result.Result);
        }

        [Fact]
        public async Task DeleteShippingType_ReturnsNoContent()
        {
            // Arrange
            _mockServiceManager.Setup(s => s.shippingTypeService.DeleteAsync(1))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteShippingType(1);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);
        }
    }
}
