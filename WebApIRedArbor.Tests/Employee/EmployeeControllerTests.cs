using Moq;
using WebApIRedArbor.Controllers;
using WebApIRedArbor.Data.Contracts;
using WebApIRedArbor.Functions;
using Xunit;

namespace WebApIRedArbor.Tests.Employee
{
    public class EmployeeControllerTests
    {
        private readonly Mock<IRepositoryEmployee> _mockRepository;
        private readonly RedArborController _controller;

        public EmployeeControllerTests()
        {
            _mockRepository = new Mock<IRepositoryEmployee>();
            _controller = new RedArborController(_mockRepository.Object);
        }

        [Fact]
        public void Get_ReturnsAllEmployees()
        {
            // Arrange
            var employees = new List<Models.Employee>
            {
                new Models.Employee { Id = 1, Name = "John Doe" },
                new Models.Employee { Id = 2, Name = "Jane Smith" }
            };
            _mockRepository.Setup(repo => repo.GetAllEmployees()).Returns(employees);

            // Act
            var result = _controller.Get();

            // Assert
            var actionResult = Assert.IsType<List<Models.Employee>>(result);
            Assert.Equal(2, actionResult.Count);
        }

        [Fact]
        public void Get_ReturnsEmployeeById()
        {
            // Arrange
            int id = 1;
            var employee = new Models.Employee { Id = id, Name = "John Doe" };
            _mockRepository.Setup(repo => repo.GetEmployee(id)).Returns(employee);

            // Act
            var result = _controller.Get(id);

            // Assert
            var actionResult = Assert.IsType<ApiResponse<object>>(result);
            Assert.True(actionResult.OperacionExitosa);
            Assert.Equal(employee, actionResult.Data);
        }

        [Fact]
        public void Get_ReturnsNotFoundForInvalidId()
        {
            // Arrange
            int id = 999;
            _mockRepository.Setup(repo => repo.GetEmployee(id)).Returns((Models.Employee)null);

            // Act
            var result = _controller.Get(id);

            // Assert
            var actionResult = Assert.IsType<ApiResponse<object>>(result);
            Assert.False(actionResult.OperacionExitosa);
            Assert.Equal($"El registro con ID {id} no fue encontrado.", actionResult.Mensaje);
        }

        [Fact]
        public void Post_CreatesNewEmployee()
        {
            // Arrange
            var newEmployee = new Models.Employee { Id = 1, Name = "New Employee" };
            _mockRepository.Setup(repo => repo.AddEmployee(newEmployee)).Returns(newEmployee);

            // Act
            var result = _controller.Post(newEmployee);

            // Assert
            var actionResult = Assert.IsType<ApiResponse<object>>(result);
            Assert.True(actionResult.OperacionExitosa);
            Assert.Equal("Operación exitosa -- Registro Exitoso", actionResult.Mensaje);
            Assert.Equal(newEmployee, actionResult.Data);
        }

        [Fact]
        public void Update_UpdatesEmployee()
        {
            // Arrange
            int id = 1;
            var employeeToUpdate = new Models.Employee { Id = id, Name = "Updated Employee" };
            _mockRepository.Setup(repo => repo.UpdateEmployee(id, employeeToUpdate)).Returns(employeeToUpdate);

            // Act
            var result = _controller.Update(id, employeeToUpdate);

            // Assert
            var actionResult = Assert.IsType<ApiResponse<object>>(result);
            Assert.True(actionResult.OperacionExitosa);
            Assert.Equal("Operación exitosa -- Actualización Exitosa", actionResult.Mensaje);
            Assert.Equal(employeeToUpdate, actionResult.Data);
        }

        [Fact]
        public void Delete_DeletesEmployee()
        {
            // Arrange
            int id = 1;
            _mockRepository.Setup(repo => repo.DeleteEmployee(id)).Returns(true);

            // Act
            var result = _controller.Delete(id);

            // Assert
            var actionResult = Assert.IsType<ApiResponse<object>>(result);
            Assert.True(actionResult.OperacionExitosa);
            Assert.Equal($"El registro con ID {id} ha sido eliminado correctamente.", actionResult.Mensaje);
        }

        [Fact]
        public void Delete_ReturnsNotFoundForInvalidId()
        {
            // Arrange
            int id = 999;
            _mockRepository.Setup(repo => repo.DeleteEmployee(id)).Returns(false);

            // Act
            var result = _controller.Delete(id);

            // Assert
            var actionResult = Assert.IsType<ApiResponse<object>>(result);
            Assert.False(actionResult.OperacionExitosa);
            Assert.Equal($"El registro con ID {id} no fue encontrado.", actionResult.Mensaje);
        }
    }
}
