using Domain.Entities.WorkManagement;
using Domain.Interfaces.WorkManagement;
using Persistence.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.WorkManagement
{
    public class TimeTrackersRepository(ApplicationDbContext context) : Repository<TimeTrackers>(context), ITimeTrackersRepository
    {
    }
}
