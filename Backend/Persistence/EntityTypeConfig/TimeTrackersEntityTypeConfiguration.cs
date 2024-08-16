using Domain.Entities.Profile;
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
    public class TimeTrackersEntityTypeConfiguration : IEntityTypeConfiguration<TimeTrackers>
    {
        public void Configure(EntityTypeBuilder<TimeTrackers> builder)
        {
            builder.ToTable("TimeTrackers");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.StartDate).HasColumnType("datetime").IsRequired();
            builder.Property(x => x.EndDate).HasColumnType("datetime").IsRequired();
            builder.Property(x => x.TimeZoneId).HasColumnType("varchar(200)").IsRequired();
            builder.Property(x => x.TaskId).IsRequired();
            builder.Property(x => x.CollaboratorId);
            builder.Property(x => x.CreatedAt).HasColumnType("datetime").IsRequired();
            builder.Property(x => x.UpdatedAt).HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.DeletedAt).HasColumnType("datetime").IsRequired(false);

            builder.HasOne<Tasks>()
                .WithMany()
                .HasForeignKey(x => x.TaskId);

            builder.HasOne<Collaborator>()
                .WithMany()
                .HasForeignKey(x => x.CollaboratorId);
        }
    }
}
