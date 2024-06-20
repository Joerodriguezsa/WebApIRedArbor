using Moq;
using WebApIRedArbor.Controllers;
using WebApIRedArbor.Data.Contracts;
using WebApIRedArbor.Functions;
using Xunit;

namespace WebApIRedArbor.Tests.Company
{
    public class CompanyControllerTests
    {
        private readonly Mock<IRepositoryCompany> _mockRepository;
        private readonly CompanyController _controller;

        public CompanyControllerTests()
        {
            _mockRepository = new Mock<IRepositoryCompany>();
            _controller = new CompanyController(_mockRepository.Object);
        }

        [Fact]
        public void Get_ReturnsAllCompanies()
        {
            // Arrange
            var expectedCompanies = new List<Models.Company>
            {
                new Models.Company { Id = 1, CompanyName = "Company A", State = true },
                new Models.Company { Id = 2, CompanyName = "Company B", State = true }
            };
            _mockRepository.Setup(repo => repo.GetAllCompany()).Returns(expectedCompanies);

            // Act
            var result = _controller.Get();

            // Assert
            var actionResult = Assert.IsType<List<Models.Company>>(result);
            Assert.Equal(expectedCompanies.Count, actionResult.Count);
        }

        [Fact]
        public void Get_ReturnsCorrectCompanyById()
        {
            // Arrange
            int companyId = 1;
            var expectedCompany = new Models.Company { Id = companyId, CompanyName = "Company A", State = true };
            _mockRepository.Setup(repo => repo.GetCompany(companyId)).Returns(expectedCompany);

            // Act
            var result = _controller.Get(companyId);

            // Assert
            var actionResult = Assert.IsType<ApiResponse<object>>(result);
            Assert.True(actionResult.OperacionExitosa);
            Assert.True(actionResult.ValidacionesNegocio);
            Assert.Equal("Operación exitosa", actionResult.Mensaje);
            Assert.Equal(expectedCompany, actionResult.Data);
        }

        [Fact]
        public void Post_AddsNewCompany()
        {
            // Arrange
            var newCompany = new Models.Company { CompanyName = "New Company", State = true };
            _mockRepository.Setup(repo => repo.AddCompany(newCompany)).Returns(newCompany);

            // Act
            var result = _controller.Post(newCompany);

            // Assert
            var actionResult = Assert.IsType<ApiResponse<object>>(result);
            Assert.True(actionResult.OperacionExitosa);
            Assert.True(actionResult.ValidacionesNegocio);
            Assert.Equal("Operación exitosa -- Registro Exitoso", actionResult.Mensaje);
            Assert.Equal(newCompany, actionResult.Data);
        }

        [Fact]
        public void Update_UpdatesExistingCompany()
        {
            // Arrange
            int companyId = 1;
            var updatedCompany = new Models.Company { Id = companyId, CompanyName = "Updated Company", State = true };
            _mockRepository.Setup(repo => repo.UpdateCompany(companyId, updatedCompany)).Returns(updatedCompany);

            // Act
            var result = _controller.Update(companyId, updatedCompany);

            // Assert
            var actionResult = Assert.IsType<ApiResponse<object>>(result);
            Assert.True(actionResult.OperacionExitosa);
            Assert.True(actionResult.ValidacionesNegocio);
            Assert.Equal("Operación exitosa -- Actualizacion Exitosa", actionResult.Mensaje);
            Assert.Equal(updatedCompany, actionResult.Data);
        }

        [Fact]
        public void Delete_DeletesExistingCompany()
        {
            // Arrange
            int companyId = 1;
            string successMessage = $"El registro con ID {companyId} ha sido eliminado correctamente.";
            _mockRepository.Setup(repo => repo.DeleteCompany(companyId)).Returns(successMessage);

            // Act
            var result = _controller.Delete(companyId);

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
            int companyId = 99;
            _mockRepository.Setup(repo => repo.GetCompany(companyId)).Throws(new Exception("No se puede encontrar la compañía"));

            // Act
            var result = _controller.Get(companyId);

            // Assert
            var actionResult = Assert.IsType<ApiResponse<object>>(result);
            Assert.False(actionResult.OperacionExitosa);
            Assert.False(actionResult.ValidacionesNegocio);
            Assert.Contains("No se puede encontrar la compañía", actionResult.Mensaje);
        }
    }
}
