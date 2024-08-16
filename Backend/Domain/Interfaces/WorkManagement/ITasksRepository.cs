using Domain.Entities.WorkManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.WorkManagement
{
    public interface ITasksRepository : IRepository<Tasks>
    {
        Task<IList<Tasks>> GetItensByProject(Guid projectId);
    }
}
