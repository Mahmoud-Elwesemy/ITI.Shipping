using ITI.Shipping.APIs.Controllers;
using ITI.Shipping.Core.Application.Abstraction;
using ITI.Shipping.Core.Application.Abstraction.Product.Model;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ITI.Shipping.Tests.Controllers
{
    public class ProductControllerTests
    {
        private readonly Mock<IServiceManager> _mockServiceManager;
        private readonly ProductController _controller;

        public ProductControllerTests()
        {
            _mockServiceManager = new Mock<IServiceManager>();
            _controller = new ProductController(_mockServiceManager.Object);
        }

        [Fact]
        public async Task GetProducts_ReturnsOkResult_WithListOfProducts()
        {
            // Arrange
            var products = new List<ProductDTO>
            {
                new ProductDTO { Id = 1, Name = "Product1" },
                new ProductDTO { Id = 2, Name = "Product2" }
            };
            _mockServiceManager.Setup(s => s.productService.GetProductsAsync())
                .ReturnsAsync(products);

            // Act
            var result = await _controller.GetProducts();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<ProductDTO>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task GetProduct_ReturnsOkResult_WithProduct()
        {
            // Arrange
            var product = new ProductDTO { Id = 1, Name = "Product1" };
            _mockServiceManager.Setup(s => s.productService.GetProductAsync(1))
                .ReturnsAsync(product);

            // Act
            var result = await _controller.GetProduct(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<ProductDTO>(okResult.Value);
            Assert.Equal(1, returnValue.Id);
        }

        [Fact]
        public async Task GetProduct_ReturnsNotFound_WhenProductDoesNotExist()
        {
            // Arrange
            _mockServiceManager.Setup(s => s.productService.GetProductAsync(1))
                .ReturnsAsync((ProductDTO) null);

            // Act
            var result = await _controller.GetProduct(1);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal("Product with id 1 not found.",notFoundResult.Value);
        }

        [Fact]
        public async Task AddProduct_ReturnsBadRequest_WhenModelIsNull()
        {
            // Act
            var result = await _controller.AddProduct(null);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Invalid Product data", badRequestResult.Value);
        }

        [Fact]
        public async Task AddProduct_ReturnsOkResult()
        {
            // Arrange
            var product = new ProductDTO { Id = 1, Name = "Product1" };
            _mockServiceManager.Setup(s => s.productService.AddAsync(product))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.AddProduct(product);

            // Assert
            var okResult = Assert.IsType<OkResult>(result.Result);
        }

        [Fact]
        public async Task UpdateProduct_ReturnsBadRequest_WhenModelIsNull()
        {
            // Act
            var result = await _controller.UpdateProduct(1, null);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Invalid Product data.", badRequestResult.Value);
        }

        [Fact]
        public async Task UpdateProduct_ReturnsNoContent()
        {
            // Arrange
            var product = new ProductDTO { Id = 1, Name = "Product1" };
            _mockServiceManager.Setup(s => s.productService.UpdateAsync(product))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdateProduct(1, product);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteProduct_ReturnsNoContent()
        {
            // Arrange
            _mockServiceManager.Setup(s => s.productService.DeleteAsync(1))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteProduct(1);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);
        }
    }
}



