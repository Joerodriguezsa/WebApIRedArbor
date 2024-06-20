using Microsoft.EntityFrameworkCore;
using WebApIRedArbor.Context;
using WebApIRedArbor.Data.Repository;
using Xunit;

namespace WebApIRedArbor.Tests.Status
{
    public class StatusRepositoryTests
    {
        private readonly RepositoryStatus _repository;
        private readonly ConexionSQLServer _context;

        public StatusRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ConexionSQLServer>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new ConexionSQLServer(options);
            _repository = new RepositoryStatus(_context);

            SeedDatabase();
        }

        private void SeedDatabase()
        {
            _context.Status.RemoveRange(_context.Status);
            _context.SaveChanges();

            var statuses = new List<Models.Status>
            {
                new Models.Status { Id = 1, StatusName = "Active", State = true },
                new Models.Status { Id = 2, StatusName = "Inactive", State = false }
            };

            _context.Status.AddRange(statuses);
            _context.SaveChanges();
        }

        [Fact]
        public void GetAllStatus_ShouldReturnAllStatuses()
        {
            var result = _repository.GetAllStatus();
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void GetStatus_ShouldReturnCorrectStatus()
        {
            var result = _repository.GetStatus(1);
            Assert.NotNull(result);
            Assert.Equal("Active", result.StatusName);
        }

        [Fact]
        public void GetStatus_ShouldReturnNullForInvalidId()
        {
            var result = _repository.GetStatus(99);
            Assert.Null(result);
        }

        [Fact]
        public void AddStatus_ShouldAddStatus()
        {
            var newStatus = new Models.Status { StatusName = "Pending" };
            var result = _repository.AddStatus(newStatus);

            Assert.NotNull(result);
            Assert.Equal("Pending", result.StatusName);
            Assert.True(result.State);
            Assert.Equal(3, _context.Status.Count());
        }

        [Fact]
        public void UpdateStatus_ShouldUpdateExistingStatus()
        {
            var updatedStatus = new Models.Status { StatusName = "Updated", State = false };
            var result = _repository.UpdateStatus(1, updatedStatus);

            Assert.NotNull(result);
            Assert.Equal("Updated", result.StatusName);
            Assert.False(result.State);
        }

        [Fact]
        public void UpdateStatus_ShouldThrowExceptionForInvalidId()
        {
            var updatedStatus = new Models.Status { StatusName = "Updated", State = false };

            Assert.Throws<Exception>(() => _repository.UpdateStatus(99, updatedStatus));
        }

        [Fact]
        public void DeleteStatus_ShouldRemoveStatus()
        {
            var result = _repository.DeleteStatus(1);
            Assert.Equal("El registro con ID 1 ha sido eliminado correctamente.", result);
            Assert.Equal(1, _context.Status.Count());
        }

        [Fact]
        public void DeleteStatus_ShouldReturnErrorMessageForInvalidId()
        {
            var result = _repository.DeleteStatus(99);
            Assert.Equal("No se puede eliminar el registro con ID 99 porque no existe.", result);
        }

        [Fact]
        public void Exists_ShouldReturnTrueIfStatusExists()
        {
            var result = _repository.Exists(1);
            Assert.True(result);
        }

        [Fact]
        public void Exists_ShouldReturnFalseIfStatusDoesNotExist()
        {
            var result = _repository.Exists(99);
            Assert.False(result);
        }
    }
}
