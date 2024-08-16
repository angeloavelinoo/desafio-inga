using Application.DTOs.WorkManagementDTOs.TimeTrackersDTOs;
using Application.Interfaces.WorkManagement;
using Application.Services.WorkManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DesafioIngaCodeApi.Controllers.WorkManagement
{
    public class TimeTrackersController(ITimeTrackersService TimeTrackersService) : BaseApiController
    {
        private readonly ITimeTrackersService _TimeTrackersService = TimeTrackersService;


        /// <summary>
        /// Creates a new time tracker
        /// </summary>
        /// <param name="timeTracker">DTO object containing time tracker data.</param>
        /// <returns>Returns the service response.</returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create(TimeTrackersCreateDTO timeTracker, Guid projectId)
        {
            return ServiceResponse(await _TimeTrackersService.Add(timeTracker, projectId));
        }

        /// <summary>
        /// Retrieves the list of all TimeTrackers.
        /// </summary>
        /// <returns>Returns the list of TimeTrackers.</returns>
        [HttpGet("List")]
        public async Task<IActionResult> Get(Guid taskId)
        {
            return ServiceResponse(await _TimeTrackersService.GetItens(taskId));
        }


        /// <summary>
        /// Removes a task
        /// </summary>
        /// <param name="id">The id of the time tracker to be removed.</param>
        /// <returns>Returns the service response.</returns>
        [HttpDelete]
        public async Task<IActionResult> Remove(Guid id)
        {
            return ServiceResponse(await _TimeTrackersService.Remove(id));
        }

    }
}
