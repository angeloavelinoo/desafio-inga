using Domain.Entities.WorkManagement;
using Domain.Interfaces;
using Domain.Interfaces.WorkManagement;
using Microsoft.EntityFrameworkCore;
using Persistence.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.WorkManagement
{
    public class TasksRepository(ApplicationDbContext context) : Repository<Tasks>(context), ITasksRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<IList<Tasks>> GetItensByProject(Guid projectId)
            => await _context.Set<Tasks>().Where(x => x.DeletedAt == null && x.ProjectId == projectId).ToListAsync();
    }
}
