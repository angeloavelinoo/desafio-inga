using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Profile
{
    public class Collaborator : Entity
    {
        public Collaborator(Guid userId, string name)
        {
            UserId = userId;
            Name = name;
        }
        public Guid UserId { get; set; }
        public string Name { get; set; }
    }
}
