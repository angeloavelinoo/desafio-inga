using Domain.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt  { get; set; } = Tool.ConvertTimeZones(DateTime.UtcNow);
        public DateTime? UpdatedAt  { get; set; } 
        public DateTime? DeletedAt  { get; set; }

    }
}
