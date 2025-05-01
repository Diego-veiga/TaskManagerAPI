
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Core.Entities;


namespace TaskManager.Infrastructure.Database.Mappings
{
    public class TaskMapping : IEntityTypeConfiguration<TaskEntity>
    {
        public void Configure(EntityTypeBuilder<TaskEntity> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Title)
                   .IsRequired()
                   .HasMaxLength(25);

            builder.Property(t => t.Description)
                    .IsRequired();

            builder.Property(t => t.expectedCompletionDate)
                    .IsRequired();

            builder.Property(t => t.Status)
                    .IsRequired();



        }
    }
}
