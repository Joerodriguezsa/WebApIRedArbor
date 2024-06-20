using Microsoft.EntityFrameworkCore;
using WebApIRedArbor.Context;
using WebApIRedArbor.Data.Repository;
using Xunit;

namespace WebApIRedArbor.Tests.Portal
{
    public class PortalRepositoryTests
    {
        private readonly RepositoryPortal _repository;
        private readonly ConexionSQLServer _context;

        public PortalRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ConexionSQLServer>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new ConexionSQLServer(options);
            _repository = new RepositoryPortal(_context);

            // Seed data
            SeedDatabase();
        }

        private void SeedDatabase()
        {
            // Clean up existing data
            _context.Portal.RemoveRange(_context.Portal);
            _context.SaveChanges();

            // Seed new data
            var portals = new List<Models.Portal>
            {
                new Models.Portal { Id = 1, PortalName = "Portal1", State = true },
                new Models.Portal { Id = 2, PortalName = "Portal2", State = false }
            };

            _context.Portal.AddRange(portals);
            _context.SaveChanges();
        }

        [Fact]
        public void GetAllPortal_ShouldReturnAllPortals()
        {
            var result = _repository.GetAllPortal();
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void GetPortal_ShouldReturnCorrectPortal()
        {
            var result = _repository.GetPortal(1);
            Assert.NotNull(result);
            Assert.Equal("Portal1", result.PortalName);
        }

        [Fact]
        public void GetPortal_ShouldReturnNullForInvalidId()
        {
            var result = _repository.GetPortal(99);
            Assert.Null(result);
        }

        [Fact]
        public void AddPortal_ShouldAddPortal()
        {
            var newPortal = new Models.Portal { PortalName = "Portal3" };
            var result = _repository.AddPortal(newPortal);

            Assert.NotNull(result);
            Assert.Equal("Portal3", result.PortalName);
            Assert.True(result.State);
            Assert.Equal(3, _context.Portal.Count());
        }

        [Fact]
        public void UpdatePortal_ShouldUpdateExistingPortal()
        {
            var updatedPortal = new Models.Portal { PortalName = "UpdatedPortal", State = false };
            var result = _repository.UpdatePortal(1, updatedPortal);

            Assert.NotNull(result);
            Assert.Equal("UpdatedPortal", result.PortalName);
            Assert.False(result.State);
        }

        [Fact]
        public void UpdatePortal_ShouldThrowExceptionForInvalidId()
        {
            var updatedPortal = new Models.Portal { PortalName = "UpdatedPortal", State = false };

            Assert.Throws<Exception>(() => _repository.UpdatePortal(99, updatedPortal));
        }

        [Fact]
        public void DeletePortal_ShouldRemovePortal()
        {
            var result = _repository.DeletePortal(1);
            Assert.Equal("El registro con ID 1 ha sido eliminado correctamente.", result);
            Assert.Equal(1, _context.Portal.Count());
        }

        [Fact]
        public void DeletePortal_ShouldReturnErrorMessageForInvalidId()
        {
            var result = _repository.DeletePortal(99);
            Assert.Equal("No se puede eliminar el registro con ID 99 porque no existe.", result);
        }

        [Fact]
        public void Exists_ShouldReturnTrueIfPortalExists()
        {
            var result = _repository.Exists(1);
            Assert.True(result);
        }

        [Fact]
        public void Exists_ShouldReturnFalseIfPortalDoesNotExist()
        {
            var result = _repository.Exists(99);
            Assert.False(result);
        }
    }
}
