using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WizStore.Services;
using WizStore.Auth;
using WizStore.Entities;
using WizStore.Models.Users;


namespace WizStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthController(IUserService userService) : ControllerBase
    {
        private IUserService _userService = userService;

        [AllowAnonymous]
        [HttpPost("[action]")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = _userService.Authenticate(model);
            return Ok(response);
        }

    }
}
