using Microsoft.EntityFrameworkCore;
using Moq;
using WebApIRedArbor.Context;
using WebApIRedArbor.Data.Contracts;
using WebApIRedArbor.Data.Repository;
using Xunit;

namespace WebApIRedArbor.Tests.Employee
{
    public class EmployeeRepositoryTests
    {
        private readonly DbContextOptions<ConexionSQLServer> _options;

        public EmployeeRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<ConexionSQLServer>()
                .UseInMemoryDatabase(databaseName: "PruebaRedArbor")
                .Options;

            SeedDatabase();
        }

        private void SeedDatabase()
        {
            using (var context = new ConexionSQLServer(_options))
            {
                context.Database.EnsureCreated();

                context.Employee.AddRange(
                    new Models.Employee
                    {
                        Id = 1,
                        CompanyId = 1,
                        CreatedOn = DateTime.Now,
                        Email = "employee1@example.com",
                        Fax = "123456789",
                        Name = "Employee 1",
                        Password = "password1",
                        PortalId = 1,
                        RoleId = 1,
                        StatusId = 1,
                        Telephone = "987654321",
                        Username = "employee1"
                    },
                    new Models.Employee
                    {
                        Id = 2,
                        CompanyId = 1,
                        CreatedOn = DateTime.Now,
                        Email = "employee2@example.com",
                        Fax = "987654321",
                        Name = "Employee 2",
                        Password = "password2",
                        PortalId = 1,
                        RoleId = 1,
                        StatusId = 1,
                        Telephone = "123456789",
                        Username = "employee2"
                    },
                    new Models.Employee
                    {
                        Id = 3,
                        CompanyId = 2,
                        CreatedOn = DateTime.Now,
                        Email = "employee3@example.com",
                        Fax = "654321987",
                        Name = "Employee 3",
                        Password = "password3",
                        PortalId = 2,
                        RoleId = 2,
                        StatusId = 2,
                        Telephone = "456789123",
                        Username = "employee3"
                    }
                );

                context.SaveChanges();
            }
        }

        [Fact]
        public void GetAllEmployees_ReturnsAllEmployees()
        {
            // Arrange
            using (var context = new ConexionSQLServer(_options))
            {
                var mockPortalRepository = new Mock<IRepositoryPortal>();
                var mockCompanyRepository = new Mock<IRepositoryCompany>();
                var mockRoleRepository = new Mock<IRepositoryRole>();
                var mockStatusRepository = new Mock<IRepositoryStatus>();

                var repository = new RepositoryEmployee(
                    context,
                    mockPortalRepository.Object,
                    mockCompanyRepository.Object,
                    mockRoleRepository.Object,
                    mockStatusRepository.Object
                );

                // Act
                var employees = repository.GetAllEmployees();

                // Assert
                Assert.NotNull(employees);
            }
        }

        [Fact]
        public void AddEmployee_AddsNewEmployee()
        {
            // Arrange
            using (var context = new ConexionSQLServer(_options))
            {
                var mockPortalRepository = new Mock<IRepositoryPortal>();
                var mockCompanyRepository = new Mock<IRepositoryCompany>();
                var mockRoleRepository = new Mock<IRepositoryRole>();
                var mockStatusRepository = new Mock<IRepositoryStatus>();

                mockPortalRepository.Setup(repo => repo.Exists(It.IsAny<int>())).Returns(true);
                mockCompanyRepository.Setup(repo => repo.Exists(It.IsAny<int>())).Returns(true);
                mockRoleRepository.Setup(repo => repo.Exists(It.IsAny<int>())).Returns(true);
                mockStatusRepository.Setup(repo => repo.Exists(It.IsAny<int>())).Returns(true);


                var repository = new RepositoryEmployee(
                    context,
                    mockPortalRepository.Object,
                    mockCompanyRepository.Object,
                    mockRoleRepository.Object,
                    mockStatusRepository.Object
                );

                var newEmployee = new Models.Employee
                {
                    CompanyId = 1,
                    CreatedOn = DateTime.Now,
                    Email = "newemployee@example.com",
                    Fax = "987654321",
                    Name = "New Employee",
                    Password = "newpassword",
                    PortalId = 1,
                    RoleId = 1,
                    StatusId = 1,
                    Telephone = "123456789",
                    Username = "newemployee"
                };

                // Act
                var addedEmployee = repository.AddEmployee(newEmployee);

                // Assert
                Assert.NotNull(addedEmployee);
                Assert.Equal(4, addedEmployee.Id);
                Assert.Equal(4, context.Employee.Count());
            }
        }

        [Fact]
        public void UpdateEmployee_UpdatesEmployee()
        {
            // Arrange
            using (var context = new ConexionSQLServer(_options))
            {
                var mockPortalRepository = new Mock<IRepositoryPortal>();
                var mockCompanyRepository = new Mock<IRepositoryCompany>();
                var mockRoleRepository = new Mock<IRepositoryRole>();
                var mockStatusRepository = new Mock<IRepositoryStatus>();

                mockPortalRepository.Setup(repo => repo.Exists(It.IsAny<int>())).Returns(true);
                mockCompanyRepository.Setup(repo => repo.Exists(It.IsAny<int>())).Returns(true);
                mockRoleRepository.Setup(repo => repo.Exists(It.IsAny<int>())).Returns(true);
                mockStatusRepository.Setup(repo => repo.Exists(It.IsAny<int>())).Returns(true);

                var repository = new RepositoryEmployee(
                    context,
                    mockPortalRepository.Object,
                    mockCompanyRepository.Object,
                    mockRoleRepository.Object,
                    mockStatusRepository.Object
                );

                var employeeToUpdate = context.Employee.Find(1);
                employeeToUpdate.Name = "Updated Employee";

                // Act
                var updatedEmployee = repository.UpdateEmployee(1, employeeToUpdate);

                // Assert
                Assert.NotNull(updatedEmployee);
                Assert.Equal("Updated Employee", updatedEmployee.Name);
            }
        }

        [Fact]
        public void DeleteEmployee_DeletesEmployee()
        {
            // Arrange
            using (var context = new ConexionSQLServer(_options))
            {
                var mockPortalRepository = new Mock<IRepositoryPortal>();
                var mockCompanyRepository = new Mock<IRepositoryCompany>();
                var mockRoleRepository = new Mock<IRepositoryRole>();
                var mockStatusRepository = new Mock<IRepositoryStatus>();

                mockPortalRepository.Setup(repo => repo.Exists(It.IsAny<int>())).Returns(true);
                mockCompanyRepository.Setup(repo => repo.Exists(It.IsAny<int>())).Returns(true);
                mockRoleRepository.Setup(repo => repo.Exists(It.IsAny<int>())).Returns(true);
                mockStatusRepository.Setup(repo => repo.Exists(It.IsAny<int>())).Returns(true);

                var repository = new RepositoryEmployee(
                    context,
                    mockPortalRepository.Object,
                    mockCompanyRepository.Object,
                    mockRoleRepository.Object,
                    mockStatusRepository.Object
                );

                // Act
                var result = repository.DeleteEmployee(1);

                // Assert
                Assert.True(result);
                Assert.Equal(2, context.Employee.Count());
            }
        }
    }
}
