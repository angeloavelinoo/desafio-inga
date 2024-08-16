using Domain.Entities.Profile;
using Domain.Entities.WorkManagement;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.DataContext
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        DbSet<User> User { get; set; }
        DbSet<Collaborator> Collaborators { get; set; }
        DbSet<Projects> Projects { get; set; }
        DbSet<Tasks> Tasks { get; set; }
        DbSet<TimeTrackers> TimeTrackers { get; set; }
    }
}
