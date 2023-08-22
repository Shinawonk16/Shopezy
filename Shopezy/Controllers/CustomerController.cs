using Application.Abstractions.IServices;
using Application.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Shopezy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IConfiguration _config;

        public CustomerController(ICustomerService customerService, IConfiguration config)
        {
            _customerService = customerService;
            _config = config;
        }
        [HttpPost("RegisterCustomer")]
        public async Task<IActionResult> RegisterCustomerAsync([FromForm] CreateCustomerRequestModel model)
        {
            var register = await _customerService.CreateCustomerAsync(model);
            if (register.Status)
            {
                return Ok(register);
            }
            return BadRequest(register);
        }
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] string id, [FromForm] UpdateCustomerRequestModel model)
        {
            var update = await _customerService.UpdateCustomerAsync(id, model);
            if (update.Status)
            {
                return Ok(update);
            }
            return BadRequest(update);
        }
        [HttpGet("GetCustomerById/{id}")]
        public async Task<IActionResult> GetCustomerByIdAsync([FromRoute] string id)
        {
            var get = await _customerService.GetByIdAsync(id);
            if (get.Status)
            {
                return Ok(get);
            }
            return BadRequest(get);
        }
        public async Task<IActionResult> GetAllCustomerAsync()
        {
            var get = await _customerService.GetAllAsync();
            if (get.Status)
            {
                return Ok(get);
            }
            return BadRequest(get);
        }

    }
}