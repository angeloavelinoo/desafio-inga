using Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.WorkManagementDTOs.ProjectsDTOs
{
    public class ProjectCreateDTO : BaseDTOValidation
    {
        public ProjectCreateDTO(string name)
        {
            AddNotifications(new ContractName(name));
            Name = name;
        }

        public string Name { get; set; }
    }
}
