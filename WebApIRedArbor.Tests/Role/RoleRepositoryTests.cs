using Microsoft.EntityFrameworkCore;
using WebApIRedArbor.Context;
using WebApIRedArbor.Data.Repository;
using Xunit;

namespace WebApIRedArbor.Tests.Role
{
    public class RoleRepositoryTests
    {
        private readonly RepositoryRole _repository;
        private readonly ConexionSQLServer _context;

        public RoleRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ConexionSQLServer>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new ConexionSQLServer(options);
            _repository = new RepositoryRole(_context);

            // Seed data
            SeedDatabase();
        }

        private void SeedDatabase()
        {
            // Clean up existing data
            _context.Role.RemoveRange(_context.Role);
            _context.SaveChanges();

            // Seed new data
            var roles = new List<Models.Role>
            {
                new Models.Role { Id = 1, RoleName = "Admin", State = true },
                new Models.Role { Id = 2, RoleName = "User", State = false }
            };

            _context.Role.AddRange(roles);
            _context.SaveChanges();
        }

        [Fact]
        public void GetAllRole_ShouldReturnAllRoles()
        {
            var result = _repository.GetAllRole();
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void GetRole_ShouldReturnCorrectRole()
        {
            var result = _repository.GetRole(1);
            Assert.NotNull(result);
            Assert.Equal("Admin", result.RoleName);
        }

        [Fact]
        public void GetRole_ShouldReturnNullForInvalidId()
        {
            var result = _repository.GetRole(99);
            Assert.Null(result);
        }

        [Fact]
        public void AddRole_ShouldAddRole()
        {
            var newRole = new Models.Role { RoleName = "Manager" };
            var result = _repository.AddRole(newRole);

            Assert.NotNull(result);
            Assert.Equal("Manager", result.RoleName);
            Assert.True(result.State);
            Assert.Equal(3, _context.Role.Count());
        }

        [Fact]
        public void UpdateRole_ShouldUpdateExistingRole()
        {
            var updatedRole = new Models.Role { RoleName = "UpdatedRole", State = false };
            var result = _repository.UpdateRole(1, updatedRole);

            Assert.NotNull(result);
            Assert.Equal("UpdatedRole", result.RoleName);
            Assert.False(result.State);
        }

        [Fact]
        public void UpdateRole_ShouldThrowExceptionForInvalidId()
        {
            var updatedRole = new Models.Role { RoleName = "UpdatedRole", State = false };

            Assert.Throws<Exception>(() => _repository.UpdateRole(99, updatedRole));
        }

        [Fact]
        public void DeleteRole_ShouldRemoveRole()
        {
            var result = _repository.DeleteRole(1);
            Assert.Equal("El registro con ID 1 ha sido eliminado correctamente.", result);
            Assert.Equal(1, _context.Role.Count());
        }

        [Fact]
        public void DeleteRole_ShouldReturnErrorMessageForInvalidId()
        {
            var result = _repository.DeleteRole(99);
            Assert.Equal("No se puede eliminar el registro con ID 99 porque no existe.", result);
        }

        [Fact]
        public void Exists_ShouldReturnTrueIfRoleExists()
        {
            var result = _repository.Exists(1);
            Assert.True(result);
        }

        [Fact]
        public void Exists_ShouldReturnFalseIfRoleDoesNotExist()
        {
            var result = _repository.Exists(99);
            Assert.False(result);
        }
    }
}
