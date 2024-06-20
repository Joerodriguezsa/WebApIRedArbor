using Microsoft.EntityFrameworkCore;
using WebApIRedArbor.Context;
using WebApIRedArbor.Data.Repository;
using Xunit;

namespace WebApIRedArbor.Tests.Company
{
    public class CompanyRepositoryTests
    {
        private readonly RepositoryCompany _repository;
        private readonly ConexionSQLServer _context;

        public CompanyRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ConexionSQLServer>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new ConexionSQLServer(options);
            _repository = new RepositoryCompany(_context);

            // Seed data
            SeedDatabase();
        }

        private void SeedDatabase()
        {
            // Clean up existing data
            _context.Company.RemoveRange(_context.Company);
            _context.SaveChanges();

            // Seed new data
            var companies = new List<Models.Company>
            {
                new Models.Company { Id = 1, CompanyName = "Company1", State = true },
                new Models.Company { Id = 2, CompanyName = "Company2", State = false }
            };

            _context.Company.AddRange(companies);
            _context.SaveChanges();
        }

        [Fact]
        public void GetAllCompany_ShouldReturnAllCompanies()
        {
            var result = _repository.GetAllCompany();
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void GetCompany_ShouldReturnCorrectCompany()
        {
            var result = _repository.GetCompany(1);
            Assert.NotNull(result);
            Assert.Equal("Company1", result.CompanyName);
        }

        [Fact]
        public void GetCompany_ShouldReturnNullForInvalidId()
        {
            var result = _repository.GetCompany(99);
            Assert.Null(result);
        }

        [Fact]
        public void AddCompany_ShouldAddCompany()
        {
            var newCompany = new Models.Company { CompanyName = "Company3" };
            var result = _repository.AddCompany(newCompany);

            Assert.NotNull(result);
            Assert.Equal("Company3", result.CompanyName);
            Assert.True(result.State);
            Assert.Equal(3, _context.Company.Count());
        }

        [Fact]
        public void UpdateCompany_ShouldUpdateExistingCompany()
        {
            var updatedCompany = new Models.Company { CompanyName = "UpdatedCompany", State = false };
            var result = _repository.UpdateCompany(1, updatedCompany);

            Assert.NotNull(result);
            Assert.Equal("UpdatedCompany", result.CompanyName);
            Assert.False(result.State);
        }

        [Fact]
        public void UpdateCompany_ShouldThrowExceptionForInvalidId()
        {
            var updatedCompany = new Models.Company { CompanyName = "UpdatedCompany", State = false };

            Assert.Throws<Exception>(() => _repository.UpdateCompany(99, updatedCompany));
        }

        [Fact]
        public void DeleteCompany_ShouldRemoveCompany()
        {
            var result = _repository.DeleteCompany(1);
            Assert.Equal("El registro con ID 1 ha sido eliminado correctamente.", result);
            Assert.Equal(1, _context.Company.Count());
        }

        [Fact]
        public void DeleteCompany_ShouldReturnErrorMessageForInvalidId()
        {
            var result = _repository.DeleteCompany(99);
            Assert.Equal("No se puede eliminar el registro con ID 99 porque no existe.", result);
        }

        [Fact]
        public void Exists_ShouldReturnTrueIfCompanyExists()
        {
            var result = _repository.Exists(1);
            Assert.True(result);
        }

        [Fact]
        public void Exists_ShouldReturnFalseIfCompanyDoesNotExist()
        {
            var result = _repository.Exists(99);
            Assert.False(result);
        }
    }
}
