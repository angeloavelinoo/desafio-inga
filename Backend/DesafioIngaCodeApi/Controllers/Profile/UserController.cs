using Application.DTOs.Profile.UserDTOs;
using Application.Interfaces.Profile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DesafioIngaCodeApi.Controllers.Profile
{
    public class UserController(IUserService userService) : BaseApiController
    {
        private readonly IUserService _userService = userService;

        /// <summary>
        /// Creates a new user and automatically creates a collaborator.
        /// </summary>
        /// <param name="usuario">DTO object containing user data.</param>
        /// <returns>Returns the service response.</returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create(CreateUserDTO usuario)
        {
            return ServiceResponse(await _userService.Add(usuario));
        }

        /// <summary>
        /// Retrieves the list of all users.
        /// </summary>
        /// <returns>Returns the list of users.</returns>
        [HttpGet("List")]
        public async Task<IActionResult> Get()
        {
            return ServiceResponse(await _userService.GetItens());
        }

        /// <summary>
        /// Retrieves the data of a specific user.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <returns>Returns the user's data.</returns>
        [HttpGet]
        public async Task<IActionResult> Get(string username)
        {
            return ServiceResponse(await _userService.Get(username));
        }

        /// <summary>
        /// Removes a user and automatically removes a collaborator.
        /// </summary>
        /// <param name="username">The username of the user to be removed.</param>
        /// <returns>Returns the service response.</returns>
        [HttpDelete]
        public async Task<IActionResult> Remove(string username)
        {
            return ServiceResponse(await _userService.Remove(username));
        }

        /// <summary>
        /// Get the user from Token and update the user's username and automatically update the collaborator name
        /// </summary>
        /// <param name="usuario">Username to be changed</param>
        /// <returns>Returns the service response.</returns>
        [HttpPut]
        public async Task<IActionResult> Update(UserDTO usuario)
        {
            return ServiceResponse(await _userService.Update(usuario, User?.Identity?.Name));
        }

    }
}
