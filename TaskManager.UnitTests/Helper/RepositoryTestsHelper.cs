using Microsoft.EntityFrameworkCore;
using TaskManager.Infrastructure.Database;

namespace TaskManager.UnitTests.Helper
{
    public class RepositoryTestsHelper
    {
        public TaskManagerDbContext GetInMemoryAppDbContext(string? DatabaseName = null)
        {
            DbContextOptions<TaskManagerDbContext> options;
            var builder = new DbContextOptionsBuilder<TaskManagerDbContext>();
            builder.UseInMemoryDatabase(DatabaseName ?? "RepositoryTestes"); ;
            options = builder.Options;
            TaskManagerDbContext taskManagerDbContext = new TaskManagerDbContext(options);
            taskManagerDbContext.Database.EnsureDeleted();
            taskManagerDbContext.Database.EnsureCreated();
            return taskManagerDbContext;
        }
    }
}
