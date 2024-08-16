using Domain.Entities.WorkManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.EntityTypeConfig
{
    public class TasksEntityTypeConfiguration : IEntityTypeConfiguration<Tasks>
    {
        public void Configure(EntityTypeBuilder<Tasks> builder)
        {
            builder.ToTable("Tasks");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasColumnType("varchar(250)").IsRequired();
            builder.Property(x => x.Description).HasColumnType("varchar(max)").IsRequired();
            builder.Property(x => x.ProjectId).IsRequired();
            builder.Property(x => x.CreatedAt).HasColumnType("datetime").IsRequired();
            builder.Property(x => x.UpdatedAt).HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.DeletedAt).HasColumnType("datetime").IsRequired(false);

            builder.HasOne<Projects>()
                .WithMany()
                .HasForeignKey(x => x.ProjectId);
        }
    }
}
