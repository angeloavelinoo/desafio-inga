using Application.DTOs.WorkManagementDTOs.TasksDTOs;
using Application.Interfaces.WorkManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DesafioIngaCodeApi.Controllers.WorkManagement
{
    public class TasksController(ITasksService TasksService) : BaseApiController
    {
        private readonly ITasksService _TasksService = TasksService;


        /// <summary>
        /// Creates a new task
        /// </summary>
        /// <param name="task">DTO object containing task data.</param>
        /// <returns>Returns the service response.</returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create(TaskCreateDTO task)
        {

            return ServiceResponse(await _TasksService.Add(task));
        }

        /// <summary>
        /// Retrieves the list of all Tasks.
        /// </summary>
        /// <returns>Returns the list of Tasks.</returns>
        [HttpGet("List/{projectId}")]
        public async Task<IActionResult> Get(Guid projectId)
        {
            return ServiceResponse(await _TasksService.GetItens(projectId));
        }

        /// <summary>
        /// Retrieves the data of a specific task.
        /// </summary>
        /// <param name="name">The taskname of the task.</param>
        /// <returns>Returns the task's data.</returns>
        [HttpGet]
        public async Task<IActionResult> Get(string name)
        {
            return ServiceResponse(await _TasksService.Get(name));
        }

        /// <summary>
        /// Removes a task
        /// </summary>
        /// <param name="name">The taskname of the task to be removed.</param>
        /// <returns>Returns the service response.</returns>
        [HttpDelete]
        public async Task<IActionResult> Remove(Guid id)
        {
            return ServiceResponse(await _TasksService.Remove(id));
        }

        /// <summary>
        /// Update a task
        /// </summary>
        /// <param name="id">name of the task you want to update</param>
        /// <param name="task">name to be changed</param>
        /// <returns>Returns the service response.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(TaskUpdateDTO task, Guid id)
        {
            return ServiceResponse(await _TasksService.Update(task, id));
        }
    }
}
