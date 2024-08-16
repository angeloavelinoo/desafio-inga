using Domain.Entities.Profile;
using Domain.Interfaces.Profile;
using Persistence.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Profile
{
    public class UserRepository(ApplicationDbContext context) : Repository<User>(context), IUserRepository
    {
    }
}
