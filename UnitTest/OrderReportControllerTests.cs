using ITI.Shipping.APIs.Controllers;
using ITI.Shipping.Core.Application.Abstraction;
using ITI.Shipping.Core.Application.Abstraction.OrderReport.Model;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ITI.Shipping.Tests.Controllers
{
    public class OrderReportControllerTests
    {
        private readonly Mock<IServiceManager> _mockServiceManager;
        private readonly OrderReportController _controller;

        public OrderReportControllerTests()
        {
            _mockServiceManager = new Mock<IServiceManager>();
            _controller = new OrderReportController(_mockServiceManager.Object);
        }

        [Fact]
        public async Task GeTAllOrderReport_ReturnsOkResult_WithListOfOrderReports()
        {
            // Arrange
            var orderReports = new List<OrderReportDTO>
            {
                new OrderReportDTO { Id = 1, ReportDetails = "Report1" },
                new OrderReportDTO { Id = 2, ReportDetails = "Report2" }
            };
            _mockServiceManager.Setup(s => s.orderReportService.GetAllOrderReportAsync())
                .ReturnsAsync(orderReports);

            // Act
            var result = await _controller.GeTAllOrderReport();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<OrderReportDTO>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task GetOrderReport_ReturnsOkResult_WithOrderReport()
        {
            // Arrange
            var orderReport = new OrderReportDTO { Id = 1, ReportDetails = "Report1" };
            _mockServiceManager.Setup(s => s.orderReportService.GetOrderReportAsync(1))
                .ReturnsAsync(orderReport);

            // Act
            var result = await _controller.GetOrderReport(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<OrderReportDTO>(okResult.Value);
            Assert.Equal(1, returnValue.Id);
        }

        [Fact]
        public async Task GetOrderReport_ReturnsNotFound_WhenOrderReportDoesNotExist()
        {
            // Arrange
            _mockServiceManager.Setup(s => s.orderReportService.GetOrderReportAsync(1))
                .ReturnsAsync((OrderReportDTO)null);

            // Act
            var result = await _controller.GetOrderReport(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task AddOrderReport_ReturnsBadRequest_WhenModelIsNull()
        {
            // Act
            var result = await _controller.AddOrderReport(null);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Invalid OrderReport data", badRequestResult.Value);
        }

        [Fact]
        public async Task AddOrderReport_ReturnsOkResult()
        {
            // Arrange
            var orderReport = new OrderReportDTO { Id = 1, ReportDetails = "Report1" };
            _mockServiceManager.Setup(s => s.orderReportService.AddAsync(orderReport))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.AddOrderReport(orderReport);

            // Assert
            var okResult = Assert.IsType<OkResult>(result.Result);
        }

        [Fact]
        public async Task UpdateOrderReport_ReturnsBadRequest_WhenModelIsNull()
        {
            // Act
            var result = await _controller.UpdateOrderReport(1, null);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Invalid OrderReport data.", badRequestResult.Value);
        }

        [Fact]
        public async Task UpdateOrderReport_ReturnsNoContent()
        {
            // Arrange
            var orderReport = new OrderReportDTO { Id = 1, ReportDetails = "Report1" };
            _mockServiceManager.Setup(s => s.orderReportService.UpdateAsync(orderReport))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdateOrderReport(1, orderReport);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteOrderReport_ReturnsNoContent()
        {
            // Arrange
            _mockServiceManager.Setup(s => s.orderReportService.DeleteAsync(1))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteOrderReport(1);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);
        }
    }
}






