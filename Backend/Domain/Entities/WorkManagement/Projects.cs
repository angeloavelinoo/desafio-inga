using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.WorkManagement
{
    public class Projects : Entity
    {
        public Projects(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
