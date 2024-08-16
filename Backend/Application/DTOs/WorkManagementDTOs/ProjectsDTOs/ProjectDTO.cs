using Domain.Entities.WorkManagement;
using Domain.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.WorkManagementDTOs.ProjectsDTOs
{
    public class ProjectDTO
    {
        public ProjectDTO()
        {
            
        }
        public ProjectDTO(Projects project)
        {
            Name = project.Name;
            Id = project.Id;
            CreatedAt = Tool.FormatDateToCustomPastString(project.CreatedAt);
            UpdatedAt = project.UpdatedAt.HasValue ? Tool.FormatDateToCustomPastString(project.UpdatedAt.Value): null;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CreatedAt { get; set; }
        public string? UpdatedAt { get; set; }
    }
}
