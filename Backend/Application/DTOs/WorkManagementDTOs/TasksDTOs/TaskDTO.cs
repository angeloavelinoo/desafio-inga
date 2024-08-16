using Application.Services;
using Domain.Entities.Profile;
using Domain.Entities.WorkManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.WorkManagementDTOs.TasksDTOs
{
    public class TaskDTO
    {
        public TaskDTO()
        {

        }
        public TaskDTO(Tasks task, Collaborator? collaborator, TimeTrackers timeTracker)
        {
            Id = task.Id;
            Name = task.Name;
            Description = task.Description;
            CollaboratorName = collaborator?.Name ?? "Nenhum colaborador";
            StartDate = ToolService.FormatDate(timeTracker.StartDate);
            EndDate = ToolService.FormatDate(timeTracker.EndDate);
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? CollaboratorName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }

    }
}
