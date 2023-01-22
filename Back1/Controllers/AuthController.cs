using Back1.Models;
using Back1.Services;
using Back1.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Back1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IUserService _userService;
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        //Action d'enregistrement
        // /api/auth/register
        //[Authorize]
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody]RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                var result = await _userService.RegisterUserAsync(model);
                //var result1 = await _userManager.AddToRoleAsync(user, model.Role);
                if (result.IsSuccess)
                    return Ok(result); // Status Code: 200

                return BadRequest(result);
            }

            return BadRequest("Some proprieties are not valid"); //Status code: 400
        }

        // Action de login
        [HttpPost("Login")] 
        public async Task<IActionResult> LoginAsync([FromBody]LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.LoginUserAsync(model);
                if (result != null)
                {
                    return Ok(result);
                }
                return BadRequest("User not found");
            }
            return BadRequest("Email or Password incorrecte");
        }

    }
}
