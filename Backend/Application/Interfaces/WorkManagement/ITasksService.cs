using Application.DTOs.Commom;
using Application.DTOs.WorkManagementDTOs.ProjectsDTOs;
using Application.DTOs.WorkManagementDTOs.TasksDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.WorkManagement
{
    public interface ITasksService
    {
        Task<ResultModel<IList<TaskDTO>>> GetItens(Guid projectId);
        Task<ResultModel<dynamic>> Get(string name);
        Task<ResultModel<dynamic>> Add(TaskCreateDTO obj);
        Task<ResultModel<dynamic>> Remove(Guid id);
        Task<ResultModel<dynamic>> RemoveAll(Guid projectId);
        Task<ResultModel<dynamic>> Update(TaskUpdateDTO taskDTO, Guid id);
    }
}
