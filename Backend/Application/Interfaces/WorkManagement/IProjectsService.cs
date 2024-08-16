using Application.DTOs.Commom;
using Application.DTOs.Profile.UserDTOs;
using Application.DTOs.WorkManagementDTOs.ProjectsDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.WorkManagement
{
    public interface IProjectsService
    {
        Task<ResultModel<IList<ProjectDTO>>> GetItens();
        Task<ResultModel<dynamic>> Get(Guid id);
        Task<ResultModel<dynamic>> Add(ProjectCreateDTO obj);
        Task<ResultModel<dynamic>> Remove(Guid id);
        Task<ResultModel<dynamic>> Update(ProjectCreateDTO projectCreate, Guid id);
    }
}
