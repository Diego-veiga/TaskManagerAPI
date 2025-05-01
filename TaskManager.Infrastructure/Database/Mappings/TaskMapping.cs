
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;
using TaskManager.Core.Entities;


namespace TaskManager.Infrastructure.Database.Mappings
{
    [ExcludeFromCodeCoverage]
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

            builder.Property(t => t.ExpectedCompletionDate)
                    .IsRequired();

            builder.Property(t => t.Status)
                    .IsRequired();



        }
    }
}
