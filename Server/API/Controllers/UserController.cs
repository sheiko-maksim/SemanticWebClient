using API.Services;
using API.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AuthService authService;
        private readonly IConfiguration configuration;

        public UserController(AuthService authService, IConfiguration configuration)
        {
            this.authService = authService;
            this.configuration = configuration;
        }

        [HttpPost("signup")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SignUpResponseViewModel>> SignUp(
            [FromBody] SignUpRequestViewModel body)
        {
            if (body == null || !this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var result = await this.authService.SignUpAsync(body);
            if (result.Success)
            {
                return this.Ok(result);
            }

            return this.BadRequest(result);
        }


        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SignInResponseViewModel>> SignIn(
            [FromBody] SignInRequestViewModel body)
        {
            if (body == null || !this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var result = await this.authService.LoginAsync(body);
            if (result.Success)
            {
                return this.Ok(result);
            }

            return this.BadRequest(result);
        }



    }
}
