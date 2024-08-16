using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.WorkManagement
{
    public class Tasks : Entity
    {
        public Tasks(Guid projectId, string name, string description)
        {
            ProjectId = projectId;
            Name = name;
            Description = description;
        }

        public Guid ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
