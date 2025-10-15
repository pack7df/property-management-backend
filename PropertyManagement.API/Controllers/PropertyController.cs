using Microsoft.AspNetCore.Mvc;
using PropertyManagement.Domain.Abstractions;
using PropertyManagement.Domain.Abstractions.Repositories;
using PropertyManagement.Domain.DTO;
using PropertyManagement.Domain.Entities;

namespace PropertyManagement.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PropertyController : Controller
    {
        private readonly IPropertyServices propertyServices;

        public PropertyController(IPropertyServices propertyServices)
        {
            this.propertyServices = propertyServices;
        }

        [HttpGet("filter")]
        public async Task<ActionResult<PaginationResult<Property>>> FilterAsync([FromQuery] FilterPropertyRequest request)
        {
            var result = await propertyServices.FilterAsync(request);
            result.Items = result.Items.Select(p => new Property
            {
                CreatedAt = p.CreatedAt,
                HostId = p.HostId,
                Id = p.Id,
                Location = p.Location,
                Name = p.Name,
                PricePerNight = p.PricePerNight,
                Status = p.Status,
            }).ToList();
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<ActionResult<bool>> CreateAsync([FromBody] PropertyRequests parameters)
        {
            var result = await propertyServices.CreateAsync(new Property
            {
                HostId = parameters.HostId,
                Location = parameters.Location,
                Name = parameters.Name,
                PricePerNight = parameters.PricePerNight,
            });
            return Ok(result);
        }

        [HttpGet("sync")]
        public async Task<ActionResult<bool>> SyncAsync([FromQuery] Guid id)
        {
            var result = await propertyServices.SyncronizeAsync(id);
            return Ok(result);
        }

        [HttpPost("booking")]
        public async Task<ActionResult<bool>> BookingAsync([FromBody] BookingRequest parameters)
        {
            var result = await propertyServices.BookingAsync(parameters);
            return Ok(result);
        }

        [HttpDelete("delete")]
        public async Task<ActionResult<bool>> DeleteAsync([FromQuery] Guid id)
        {
            var result = await propertyServices.DeleteAsync(id);
            return Ok(result);
        }

        [HttpPatch("update")]
        public async Task<ActionResult<bool>> UpdateAsync([FromBody] UpdatePropertyRequest parameters)
        {
            var result = await propertyServices.UpdateAsync(new Property
            {
            });
            return Ok(result);
        }
    }
}
