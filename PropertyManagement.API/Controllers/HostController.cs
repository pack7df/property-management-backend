

using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Mvc;
using PropertyManagement.Domain.Abstractions;
using PropertyManagement.Domain.Abstractions.Repositories;
using PropertyManagement.Domain.DTO;
using PropertyManagement.Domain.Entities;

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
            result.Items = result.Items.Select(i => new Domain.Entities.Host
            {
                Email = i.Email,
                FullName = i.FullName,
                Phone = i.Phone,
            }).ToList();
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<ActionResult<bool>> CreateAsync([FromBody] HostRequests parameters)
        {
            var result = await hostServices.CreateAsync(new Domain.Entities.Host
            {
                Email = parameters.Email,
                FullName = parameters.FullName,
                Phone = parameters.Phone
            });
            return Ok(result);
        }

        [HttpDelete("delete")]
        public async Task<ActionResult<bool>> DeleteAsync([FromQuery] Guid id)
        {
            var result = await hostServices.DeleteAsync(id);
            return Ok(result);
        }

        [HttpPatch("update")]
        public async Task<ActionResult<bool>> UpdateAsync([FromBody] UpdateHostRequest parameters)
        {
            var result = await hostServices.UpdateAsync(new Domain.Entities.Host
            {
                Email = parameters.Email,
                FullName = parameters.FullName,
                Phone = parameters.Phone,
                Id = parameters.Id
            });
            return Ok(result);
        }
    }
}
