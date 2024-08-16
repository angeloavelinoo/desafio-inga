using Application.DTOs.Commom;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DesafioIngaCodeApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class BaseApiController : ControllerBase
    {
        public IActionResult ServiceResponse<T>(ResultModel<T> response)
        {
            if (response.Status == (int)HttpStatusCode.Redirect)
                return Redirect(response.Message);

            return StatusCode(response.Status, response);
        }
    }
}
