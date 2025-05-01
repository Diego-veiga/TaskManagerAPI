
using TaskManager.Core.Entities;
using TaskManager.Infrastructure.Database;
using TaskManager.Infrastructure.Repositories;
using TaskManager.UnitTests.Helper;

namespace TaskManager.UnitTests.Repository
{
    public class BaseRepositoryTests
    {

        [Fact]
        public void Add_ShouldAddEntityToDatabase()
        {
            // Arrange
            var context = new RepositoryTestsHelper().GetInMemoryAppDbContext();
            var repository = new BaseRepository<TaskEntity>(context);
            var entity = new TaskEntity("Task01", "Description01", DateTime.Now);
  

            // Act
            repository.Add(entity);
            context.SaveChanges();

            // Assert
            var entityInDb = context.Set<TaskEntity>().Find(entity.Id);
            Assert.NotNull(entityInDb);
        }

        [Fact]
        public void Delete_ShouldPropertyActiveEqualFalse()
        {
            // Arrange
            var context = new RepositoryTestsHelper().GetInMemoryAppDbContext();
            var repository = new BaseRepository<TaskEntity>(context);
            var entity = new TaskEntity("Task01", "Description01", DateTime.Now);
       
            repository.Add(entity);
            context.SaveChanges();

            // Act
            repository.Delete(entity);
            context.SaveChanges();
            var userDeleted = context.Tasks.Find(entity.Id);

            // Assert
            Assert.False(userDeleted.Active);
        }

        [Fact]
        public void Update_ShouldMarkEntityAsModified()
        {
            // Arrange
            var context = new RepositoryTestsHelper().GetInMemoryAppDbContext();
            var repository = new BaseRepository<TaskEntity>(context);
            var entity = new TaskEntity("Task01", "Description01", DateTime.Now);

            repository.Add(entity);
            context.SaveChanges();


            // Act
            var entityForUpdate = context.Tasks.Find(entity.Id);
            entityForUpdate.Title = "Updated Entity";
            repository.Update(entityForUpdate);
            context.SaveChanges();

            // Assert
            var entityInDb = context.Set<TaskEntity>().Find(entity.Id);
            Assert.Equal("Updated Entity", entityInDb.Title);
            
        }

        [Fact]
        public void FindByParam_ShouldReturnIQueryable()
        {
            // Arrange
            var context = new RepositoryTestsHelper().GetInMemoryAppDbContext();
            var repository = new BaseRepository<TaskEntity>(context);
            var entity = new TaskEntity("Task01", "Description01", DateTime.Now);

            repository.Add(entity);
            context.SaveChanges();


            // Act
            var entityInDb = repository.FindByParam( r => r.Id == entity.Id);

            // Assert

            Assert.NotNull(entityInDb);
        }

        [Fact]
        public void Get_ShouldReturnIQueryable()
        {
            // Arrange
            var context = new RepositoryTestsHelper().GetInMemoryAppDbContext();
            var repository = new BaseRepository<TaskEntity>(context);
            var entity = new TaskEntity("Task01", "Description01", DateTime.Now);

            repository.Add(entity);
            context.SaveChanges();


            // Act
            var entityInDb = repository.Get();

            // Assert

            Assert.NotNull(entityInDb.Count());
        }

    }
}
