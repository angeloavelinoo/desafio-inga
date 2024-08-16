using Application.DTOs.Commom;
using Application.DTOs.WorkManagementDTOs.TasksDTOs;
using Application.DTOs.WorkManagementDTOs.TimeTrackersDTOs;
using Domain.Entities.WorkManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.WorkManagement
{
    public interface ITimeTrackersService
    {
        Task<ResultModel<IList<TimeTrackersDTO>>> GetItens(Guid projectId);
        Task<TimeTrackers> Get(Guid projectId);
        Task<ResultModel<dynamic>> Add(TimeTrackersCreateDTO obj, Guid projectId);
        Task<ResultModel<dynamic>> Update(TimeTrackersCreateDTO obj, Guid projectId);

        Task<ResultModel<dynamic>> Remove(Guid id);
    }
}
