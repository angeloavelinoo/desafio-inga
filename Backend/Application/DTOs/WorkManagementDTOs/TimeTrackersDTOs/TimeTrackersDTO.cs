using Domain.Entities.Profile;
using Domain.Entities.WorkManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.WorkManagementDTOs.TimeTrackersDTOs
{
    public class TimeTrackersDTO
    {
        public TimeTrackersDTO(Tasks task, Collaborator collaborator, TimeTrackers timeTrackers)
        {
            TaskName = task.Name;
            CollaboratorName = collaborator.Name;
            Id = timeTrackers.Id;
            TaskId = timeTrackers.TaskId;
            CollaboratorId = timeTrackers.CollaboratorId;
            StartTime = timeTrackers.StartDate;
            EndTime = timeTrackers.EndDate;
        }
        public string TaskName { get; set; }
        public string CollaboratorName { get; set; }
        public Guid Id { get; set; }
        public Guid TaskId { get; set; }
        public Guid? CollaboratorId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

    }
}
