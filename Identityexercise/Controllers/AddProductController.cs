using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Identityexercise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "AdminOnly")]
    public class AddProductController : ControllerBase
    {
        [HttpGet("Getdetails")]
        public IActionResult GetDetails()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userName = User.Identity?.Name;
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

            return Ok($"{userId};{userName};{userRole}");
        }
    }
}
