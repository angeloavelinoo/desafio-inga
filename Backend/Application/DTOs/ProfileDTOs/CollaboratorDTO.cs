using Domain.Entities.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Profile
{
    public class CollaboratorDTO
    {
        public CollaboratorDTO()
        {
            
        }
        public CollaboratorDTO(Collaborator user)
        {
            Id = user.Id;
            Name = user.Name;
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
