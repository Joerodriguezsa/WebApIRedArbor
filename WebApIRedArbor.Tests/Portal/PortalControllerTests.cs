using Moq;
using WebApIRedArbor.Controllers;
using WebApIRedArbor.Data.Contracts;
using WebApIRedArbor.Functions;
using Xunit;

namespace WebApIRedArbor.Tests.Portal
{
    public class PortalControllerTests
    {
        private readonly Mock<IRepositoryPortal> _mockRepository;
        private readonly PortalController _controller;

        public PortalControllerTests()
        {
            _mockRepository = new Mock<IRepositoryPortal>();
            _controller = new PortalController(_mockRepository.Object);
        }

        [Fact]
        public void Get_ReturnsAllPortals()
        {
            // Arrange
            var expectedPortals = new List<Models.Portal>
            {
                new Models.Portal { Id = 1, PortalName = "Portal A", State = true },
                new Models.Portal { Id = 2, PortalName = "Portal B", State = true }
            };
            _mockRepository.Setup(repo => repo.GetAllPortal()).Returns(expectedPortals);

            // Act
            var result = _controller.Get();

            // Assert
            var actionResult = Assert.IsType<List<Models.Portal>>(result);
            Assert.Equal(expectedPortals.Count, actionResult.Count);
        }

        [Fact]
        public void Get_ReturnsCorrectPortalById()
        {
            // Arrange
            int portalId = 1;
            var expectedPortal = new Models.Portal { Id = portalId, PortalName = "Portal A", State = true };
            _mockRepository.Setup(repo => repo.GetPortal(portalId)).Returns(expectedPortal);

            // Act
            var result = _controller.Get(portalId);

            // Assert
            var actionResult = Assert.IsType<ApiResponse<object>>(result);
            Assert.True(actionResult.OperacionExitosa);
            Assert.True(actionResult.ValidacionesNegocio);
            Assert.Equal("Operación exitosa", actionResult.Mensaje);
            Assert.Equal(expectedPortal, actionResult.Data);
        }

        [Fact]
        public void Post_AddsNewPortal()
        {
            // Arrange
            var newPortal = new Models.Portal { PortalName = "New Portal", State = true };
            _mockRepository.Setup(repo => repo.AddPortal(newPortal)).Returns(newPortal);

            // Act
            var result = _controller.Post(newPortal);

            // Assert
            var actionResult = Assert.IsType<ApiResponse<object>>(result);
            Assert.True(actionResult.OperacionExitosa);
            Assert.True(actionResult.ValidacionesNegocio);
            Assert.Equal("Operación exitosa -- Registro Exitoso", actionResult.Mensaje);
            Assert.Equal(newPortal, actionResult.Data);
        }

        [Fact]
        public void Update_UpdatesExistingPortal()
        {
            // Arrange
            int portalId = 1;
            var updatedPortal = new Models.Portal { Id = portalId, PortalName = "Updated Portal", State = true };
            _mockRepository.Setup(repo => repo.UpdatePortal(portalId, updatedPortal)).Returns(updatedPortal);

            // Act
            var result = _controller.Update(portalId, updatedPortal);

            // Assert
            var actionResult = Assert.IsType<ApiResponse<object>>(result);
            Assert.True(actionResult.OperacionExitosa);
            Assert.True(actionResult.ValidacionesNegocio);
            Assert.Equal("Operación exitosa -- Actualizacion Exitosa", actionResult.Mensaje);
            Assert.Equal(updatedPortal, actionResult.Data);
        }

        [Fact]
        public void Delete_DeletesExistingPortal()
        {
            // Arrange
            int portalId = 1;
            string successMessage = $"El registro con ID {portalId} ha sido eliminado correctamente.";
            _mockRepository.Setup(repo => repo.DeletePortal(portalId)).Returns(successMessage);

            // Act
            var result = _controller.Delete(portalId);

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
            int portalId = 99;
            _mockRepository.Setup(repo => repo.GetPortal(portalId)).Throws(new Exception("No se puede encontrar el portal"));

            // Act
            var result = _controller.Get(portalId);

            // Assert
            var actionResult = Assert.IsType<ApiResponse<object>>(result);
            Assert.False(actionResult.OperacionExitosa);
            Assert.False(actionResult.ValidacionesNegocio);
            Assert.Contains("No se puede encontrar el portal", actionResult.Mensaje);
        }
    }
}
