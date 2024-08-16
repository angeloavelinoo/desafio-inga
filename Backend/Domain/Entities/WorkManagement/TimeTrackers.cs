using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.WorkManagement
{
    public class TimeTrackers : Entity
    {
        public TimeTrackers(DateTime startDate, DateTime endDate, string timeZoneId, Guid taskId, Guid? collaboratorId)
        {
            StartDate = startDate;
            EndDate = endDate;
            TimeZoneId = timeZoneId;
            TaskId = taskId;
            CollaboratorId = collaboratorId;
        }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string TimeZoneId { get; set; }
        public Guid TaskId { get; set; }
        public Guid? CollaboratorId { get; set; }
    }
}
