using Application.DTOs.Commom;
using Application.DTOs.Profile.UserDTOs;
using Application.DTOs.WorkManagementDTOs.ProjectsDTOs;
using Application.Interfaces.WorkManagement;
using Domain.Entities.Profile;
using Domain.Entities.WorkManagement;
using Domain.Interfaces.WorkManagement;
using Domain.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.WorkManagement
{
    public class ProjectsService(IProjectsRepository projectsRepository, ITasksService tasksService) : IProjectsService
    {
        private IProjectsRepository _projectsRepository = projectsRepository;
        private ITasksService _tasksService = tasksService;

        public async Task<ResultModel<dynamic>> Add(ProjectCreateDTO obj)
        {
            Projects project = new(name: obj.Name);

            if (await _projectsRepository.AlredyExist(x => x.Name == obj.Name && x.DeletedAt == null))
                return new(HttpStatusCode.Conflict, "There is already a project with this name");

            await _projectsRepository.Create(project);

            return new();
        }

        public async Task<ResultModel<dynamic>> Get(Guid id)
        {
            Projects project = await _projectsRepository.Get(x => x.Id == id);

            if(project == null)
                return new(HttpStatusCode.NotFound, "Project not found");

            ProjectDTO projectDTO = new(project);

            return new(projectDTO);
        }

        public async Task<ResultModel<IList<ProjectDTO>>> GetItens()
        {
            IList<Projects> projects = await _projectsRepository.GetItens();

            if (projects == null)
                return new(HttpStatusCode.NotFound, "Projects not found");

            List<ProjectDTO> projectsDTO = new List<ProjectDTO>();

            foreach (Projects project in projects)
            {
                ProjectDTO projectDTO = new(project);

                projectsDTO.Add(projectDTO);

            }

            return new(projectsDTO);
        }

        public async Task<ResultModel<dynamic>> Remove(Guid id)
        {
            Projects project = await _projectsRepository.Get(x => x.Id == id);

            if (project == null)
                return new(HttpStatusCode.NotFound, "Project not found");

            project.DeletedAt = Tool.ConvertTimeZones(DateTime.UtcNow);

            await _projectsRepository.Remove(project);
            await _tasksService.RemoveAll(project.Id);


            return new();
        }

        public async Task<ResultModel<dynamic>> Update(ProjectCreateDTO projectDTO, Guid id)
        {
            Projects project = await _projectsRepository.Get(x => x.Id == id);

            if(project == null)
                return new(HttpStatusCode.NotFound, "Project not found");

            project.Name = projectDTO.Name;
            project.UpdatedAt = Tool.ConvertTimeZones(DateTime.UtcNow); 

            await _projectsRepository.Update(project);

            return new();

        }
    }
}
