using Application.Abstractions.IServices;
using Application.Dtos;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Shopezy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userServices;
        private readonly IConfiguration _config;

        public UserController(IUserService userServices, IConfiguration config)
        {
            _userServices = userServices;
            _config = config;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginUserRequsetModel model)
        {
            var log = await _userServices.LoginUserAsync(model);
            if (log != null)
            {
                return Ok(log);
            }
            return BadRequest(log);
        }
        [HttpGet("GetUsersByRole/{role}")]
        public async Task<IActionResult> GetUserByRole([FromRoute] string role)
        {
            var getRole = await _userServices.GetUsersByRoleAsync(role);
            if (getRole != null)
            {
                return Ok(getRole);
            }
            return BadRequest(getRole);
        }
    }
}