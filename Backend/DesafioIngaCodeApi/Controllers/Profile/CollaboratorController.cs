using Application.Interfaces.Profile;
using Application.Services.Profile;
using Microsoft.AspNetCore.Mvc;

namespace DesafioIngaCodeApi.Controllers.Profile
{
    public class CollaboratorController(ICollaboratorService collaboratorService) : BaseApiController
    {
        private readonly ICollaboratorService _collaboratorService = collaboratorService;

        /// <summary>
        /// Retrieves the list of all collaborators.
        /// </summary>
        /// <returns>Returns the list of collaborators.</returns>
        [HttpGet("List")]
        public async Task<IActionResult> Get()
        {
            return ServiceResponse(await _collaboratorService.GetItens());
        }

    }
}
