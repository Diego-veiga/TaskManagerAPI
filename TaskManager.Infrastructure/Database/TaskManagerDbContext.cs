using Microsoft.EntityFrameworkCore;
using TaskManager.Core.Entities;
using TaskManager.Infrastructure.Database.Mappings;

namespace TaskManager.Infrastructure.Database
{
    public class TaskManagerDbContext : DbContext
    {

        public TaskManagerDbContext(DbContextOptions<TaskManagerDbContext> options)
            : base(options)
        {

        }
        public DbSet<TaskEntity> Tasks { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TaskMapping());
           
        }

    }
}
