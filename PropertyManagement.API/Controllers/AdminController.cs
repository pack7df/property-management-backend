

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
    public class AdminController : ControllerBase
    {
        private readonly ISecurityServices securityServices;

        public AdminController(ISecurityServices securityServices)
        {
            this.securityServices = securityServices;
        }

        [HttpPost("auth")]
        public async Task<ActionResult> Auth([FromBody] AuthenticationRequest request)
        {
            var result = await securityServices.AuthenticateAsync(request.UserName, request.Password);
            if (result == null) return Unauthorized();
            return Ok(result);
        }
    }
}
