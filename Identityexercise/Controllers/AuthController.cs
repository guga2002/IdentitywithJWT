using Identityexercise.ResponseAndRequest.Request;
using Identityexercise.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Identityexercise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuth _auth;

        public AuthController(IAuth auth)
        {
            _auth = auth;
                
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(SIgnInRequest sign)
        {
            var res = await _auth.SignIn(sign);

            if (res !=null) return Ok(res);

            return BadRequest(" warumatebeli");
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody]RegisterRequest usr)
        {
            var res = await _auth.Register(usr);
            if(res==true)
            {
                return Ok("warmatebuli registracia");
            }
            return BadRequest("warumatebeli registracia");
        }
    }
}
