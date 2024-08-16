using Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.WorkManagementDTOs.TasksDTOs
{
    public class TaskCreateDTO : BaseDTOValidation
    {
        public TaskCreateDTO(string name, Guid projectId, string description, Guid? collaboratorId, DateTime startTime, DateTime endTime)
        {
            AddNotifications(new ContractName(name), new ContractGuid(projectId), new ContractDescription(description));
            Name = name;
            ProjectId = projectId;
            Description = description;
            CollaboratorId = collaboratorId;
            StartTime = startTime;
            EndTime = endTime;
        }

        public Guid ProjectId { get; set; }
        public Guid? CollaboratorId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }


    }
}
