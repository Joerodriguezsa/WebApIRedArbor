using Moq;
using WebApIRedArbor.Controllers;
using WebApIRedArbor.Data.Contracts;
using WebApIRedArbor.Functions;
using Xunit;

namespace WebApIRedArbor.Tests.Status
{
    public class StatusControllerTests
    {
        private readonly Mock<IRepositoryStatus> _mockRepository;
        private readonly StatusController _controller;

        public StatusControllerTests()
        {
            _mockRepository = new Mock<IRepositoryStatus>();
            _controller = new StatusController(_mockRepository.Object);
        }

        [Fact]
        public void Get_ReturnsAllStatuses()
        {
            // Arrange
            var expectedStatuses = new List<Models.Status>
            {
                new Models.Status { Id = 1, StatusName = "Active", State = true },
                new Models.Status { Id = 2, StatusName = "Inactive", State = true }
            };
            _mockRepository.Setup(repo => repo.GetAllStatus()).Returns(expectedStatuses);

            // Act
            var result = _controller.Get();

            // Assert
            var actionResult = Assert.IsType<List<Models.Status>>(result);
            Assert.Equal(expectedStatuses.Count, actionResult.Count);
        }

        [Fact]
        public void Get_ReturnsCorrectStatusById()
        {
            // Arrange
            int statusId = 1;
            var expectedStatus = new Models.Status { Id = statusId, StatusName = "Active", State = true };
            _mockRepository.Setup(repo => repo.GetStatus(statusId)).Returns(expectedStatus);

            // Act
            var result = _controller.Get(statusId);

            // Assert
            var actionResult = Assert.IsType<ApiResponse<object>>(result);
            Assert.True(actionResult.OperacionExitosa);
            Assert.True(actionResult.ValidacionesNegocio);
            Assert.Equal("Operación exitosa", actionResult.Mensaje);
            Assert.Equal(expectedStatus, actionResult.Data);
        }

        [Fact]
        public void Post_AddsNewStatus()
        {
            // Arrange
            var newStatus = new Models.Status { StatusName = "Pending", State = true };
            _mockRepository.Setup(repo => repo.AddStatus(newStatus)).Returns(newStatus);

            // Act
            var result = _controller.Post(newStatus);

            // Assert
            var actionResult = Assert.IsType<ApiResponse<object>>(result);
            Assert.True(actionResult.OperacionExitosa);
            Assert.True(actionResult.ValidacionesNegocio);
            Assert.Equal("Operación exitosa -- Registro Exitoso", actionResult.Mensaje);
            Assert.Equal(newStatus, actionResult.Data);
        }

        [Fact]
        public void Update_UpdatesExistingStatus()
        {
            // Arrange
            int statusId = 1;
            var updatedStatus = new Models.Status { Id = statusId, StatusName = "Updated Status", State = true };
            _mockRepository.Setup(repo => repo.UpdateStatus(statusId, updatedStatus)).Returns(updatedStatus);

            // Act
            var result = _controller.Update(statusId, updatedStatus);

            // Assert
            var actionResult = Assert.IsType<ApiResponse<object>>(result);
            Assert.True(actionResult.OperacionExitosa);
            Assert.True(actionResult.ValidacionesNegocio);
            Assert.Equal("Operación exitosa -- Actualizacion Exitosa", actionResult.Mensaje);
            Assert.Equal(updatedStatus, actionResult.Data);
        }

        [Fact]
        public void Delete_DeletesExistingStatus()
        {
            // Arrange
            int statusId = 1;
            string successMessage = $"El registro con ID {statusId} ha sido eliminado correctamente.";
            _mockRepository.Setup(repo => repo.DeleteStatus(statusId)).Returns(successMessage);

            // Act
            var result = _controller.Delete(statusId);

            // Assert
            var actionResult = Assert.IsType<ApiResponse<object>>(result);
            Assert.True(actionResult.OperacionExitosa);
            Assert.True(actionResult.ValidacionesNegocio);
            Assert.Equal(successMessage, actionResult.Mensaje);
        }

        [Fact]
        public void ErrorHandling_ReturnsErrorResponse()
        {
            // Arrange
            int statusId = 99;
            _mockRepository.Setup(repo => repo.GetStatus(statusId)).Throws(new Exception("No se puede encontrar el estado"));

            // Act
            var result = _controller.Get(statusId);

            // Assert
            var actionResult = Assert.IsType<ApiResponse<object>>(result);
            Assert.False(actionResult.OperacionExitosa);
            Assert.False(actionResult.ValidacionesNegocio);
            Assert.Contains("No se puede encontrar el estado", actionResult.Mensaje);
        }
    }
}
