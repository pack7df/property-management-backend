

using Microsoft.AspNetCore.Mvc;
using PropertyManagement.Domain.Abstractions;
using PropertyManagement.Domain.Abstractions.Repositories;
using PropertyManagement.Domain.DTO;

namespace PropertyManagement.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HostController : ControllerBase
    {
        private readonly IHostServices hostServices;

        public HostController(IHostServices hostServices)
        {
            this.hostServices = hostServices;
        }

        [HttpGet("filter")]
        public async Task<ActionResult<PaginationResult<Domain.Entities.Host>>> FilterAsync([FromQuery] FilterHostRequest request)
        {
            var result = await hostServices.FilterAsync(request);
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<ActionResult<bool>> CreateAsync([FromBody] CreateHostRequest parameters)
        {
            var result = await hostServices.CreateAsync(new Domain.Entities.Host
            {
                Email = parameters.Email,
                FullName = parameters.FullName,
                Phone = parameters.Phone
            });
            return Ok(result);
        }
    }
}
