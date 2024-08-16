using Application.DTOs.WorkManagementDTOs.ProjectsDTOs;
using Application.Interfaces.WorkManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DesafioIngaCodeApi.Controllers.WorkManagement
{
    public class ProjectsController(IProjectsService projectsService) : BaseApiController
    {
        private readonly IProjectsService _projectsService = projectsService;


        /// <summary>
        /// Creates a new project
        /// </summary>
        /// <param name="project">DTO object containing project data.</param>
        /// <returns>Returns the service response.</returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create(ProjectCreateDTO project)
        {
            return ServiceResponse(await _projectsService.Add(project));
        }

        /// <summary>
        /// Retrieves the list of all projects.
        /// </summary>
        /// <returns>Returns the list of projects.</returns>
        [HttpGet("List")]
        public async Task<IActionResult> Get()
        {
            return ServiceResponse(await _projectsService.GetItens());
        }

        /// <summary>
        /// Retrieves the data of a specific project.
        /// </summary>
        /// <param name="id">The projectname of the project.</param>
        /// <returns>Returns the project's data.</returns>
        [HttpGet]
        public async Task<IActionResult> Get(Guid id)
        {
            return ServiceResponse(await _projectsService.Get(id));
        }

        /// <summary>
        /// Removes a project
        /// </summary>
        /// <param name="name">The projectname of the project to be removed.</param>
        /// <returns>Returns the service response.</returns>
        [HttpDelete]
        public async Task<IActionResult> Remove(Guid id)
        {
            return ServiceResponse(await _projectsService.Remove(id));
        }

        /// <summary>
        /// Update the project's name 
        /// </summary>
        /// <param name="id">id of the project you want to update</param>
        /// <param name="project">name to be changed</param>
        /// <returns>Returns the service response.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update( Guid id, ProjectCreateDTO project)
        {
            return ServiceResponse(await _projectsService.Update(project, id));
        }
    }
}
