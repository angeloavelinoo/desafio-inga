using Application.DTOs.Profile;
using Application.Interfaces.Profile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DesafioIngaCodeApi.Controllers.Profile
{
    public class AuthenticationController(IUserService userService) : BaseApiController
    {
        private readonly IUserService _userService = userService;

        /// <summary>
        /// Authenticates a user and generates a login token.
        /// </summary>
        /// <param name="loginDTO">DTO object containing the login credentials (username and password).</param>
        /// <returns>Returns the service response with the authentication result, including a token if successful.</returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            return ServiceResponse(await _userService.Login(loginDTO));
        }
    }
}
