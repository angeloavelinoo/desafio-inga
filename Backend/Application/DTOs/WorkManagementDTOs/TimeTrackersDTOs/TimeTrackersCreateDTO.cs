using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.WorkManagementDTOs.TimeTrackersDTOs
{
    public class TimeTrackersCreateDTO : BaseDTOValidation
    {
        public TimeTrackersCreateDTO(Guid taskId, Guid? collaboratorId, DateTime startTime, DateTime endTime)
        {
            TaskId = taskId;
            CollaboratorId = collaboratorId;
            StartTime = startTime;
            EndTime = endTime;
        }

        public Guid TaskId { get; set; }
        public Guid? CollaboratorId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
