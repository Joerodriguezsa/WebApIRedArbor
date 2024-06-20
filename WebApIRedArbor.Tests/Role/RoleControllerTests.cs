using Moq;
using WebApIRedArbor.Controllers;
using WebApIRedArbor.Data.Contracts;
using WebApIRedArbor.Functions;
using Xunit;

namespace WebApIRedArbor.Tests.Role
{
    public class RoleControllerTests
    {
        private readonly Mock<IRepositoryRole> _mockRepository;
        private readonly RoleController _controller;

        public RoleControllerTests()
        {
            _mockRepository = new Mock<IRepositoryRole>();
            _controller = new RoleController(_mockRepository.Object);
        }

        [Fact]
        public void Get_ReturnsAllRoles()
        {
            // Arrange
            var expectedRoles = new List<Models.Role>
            {
                new Models.Role { Id = 1, RoleName = "Role A", State = true },
                new Models.Role { Id = 2, RoleName = "Role B", State = true }
            };
            _mockRepository.Setup(repo => repo.GetAllRole()).Returns(expectedRoles);

            // Act
            var result = _controller.Get();

            // Assert
            var actionResult = Assert.IsType<List<Models.Role>>(result);
            Assert.Equal(expectedRoles.Count, actionResult.Count);
        }

        [Fact]
        public void Get_ReturnsCorrectRoleById()
        {
            // Arrange
            int roleId = 1;
            var expectedRole = new Models.Role { Id = roleId, RoleName = "Role A", State = true };
            _mockRepository.Setup(repo => repo.GetRole(roleId)).Returns(expectedRole);

            // Act
            var result = _controller.Get(roleId);

            // Assert
            var actionResult = Assert.IsType<ApiResponse<object>>(result);
            Assert.True(actionResult.OperacionExitosa);
            Assert.True(actionResult.ValidacionesNegocio);
            Assert.Equal("Operación exitosa", actionResult.Mensaje);
            Assert.Equal(expectedRole, actionResult.Data);
        }

        [Fact]
        public void Post_AddsNewRole()
        {
            // Arrange
            var newRole = new Models.Role { RoleName = "New Role", State = true };
            _mockRepository.Setup(repo => repo.AddRole(newRole)).Returns(newRole);

            // Act
            var result = _controller.Post(newRole);

            // Assert
            var actionResult = Assert.IsType<ApiResponse<object>>(result);
            Assert.True(actionResult.OperacionExitosa);
            Assert.True(actionResult.ValidacionesNegocio);
            Assert.Equal("Operación exitosa -- Registro Exitoso", actionResult.Mensaje);
            Assert.Equal(newRole, actionResult.Data);
        }

        [Fact]
        public void Update_UpdatesExistingRole()
        {
            // Arrange
            int roleId = 1;
            var updatedRole = new Models.Role { Id = roleId, RoleName = "Updated Role", State = true };
            _mockRepository.Setup(repo => repo.UpdateRole(roleId, updatedRole)).Returns(updatedRole);

            // Act
            var result = _controller.Update(roleId, updatedRole);

            // Assert
            var actionResult = Assert.IsType<ApiResponse<object>>(result);
            Assert.True(actionResult.OperacionExitosa);
            Assert.True(actionResult.ValidacionesNegocio);
            Assert.Equal("Operación exitosa -- Actualizacion Exitosa", actionResult.Mensaje);
            Assert.Equal(updatedRole, actionResult.Data);
        }

        [Fact]
        public void Delete_DeletesExistingRole()
        {
            // Arrange
            int roleId = 1;
            string successMessage = $"El registro con ID {roleId} ha sido eliminado correctamente.";
            _mockRepository.Setup(repo => repo.DeleteRole(roleId)).Returns(successMessage);

            // Act
            var result = _controller.Delete(roleId);

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
            int roleId = 99;
            _mockRepository.Setup(repo => repo.GetRole(roleId)).Throws(new Exception("No se puede encontrar el rol"));

            // Act
            var result = _controller.Get(roleId);

            // Assert
            var actionResult = Assert.IsType<ApiResponse<object>>(result);
            Assert.False(actionResult.OperacionExitosa);
            Assert.False(actionResult.ValidacionesNegocio);
            Assert.Contains("No se puede encontrar el rol", actionResult.Mensaje);
        }
    }
}
