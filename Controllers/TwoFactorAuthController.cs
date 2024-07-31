using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Teste_TOPT_Swagger.Service;

namespace Teste_TOPT_Swagger.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TwoFactorAuthController : ControllerBase
    {
        private readonly TwoFactorAuthService _twoFactorAuthService;

        public TwoFactorAuthController(TwoFactorAuthService twoFactorAuthService)
        {
            _twoFactorAuthService = twoFactorAuthService;
        }

        [HttpGet("GenerateTotp")]
        [Authorize]
        public IActionResult GenerateTotp()
        {
            var totpCode = _twoFactorAuthService.GenerateTotp();
            return Ok(new { totpCode });
        }

        [HttpPost("ValidateTotp")]
        [Authorize]
        public IActionResult ValidateTotp([FromBody] string totpCode)
        {
            var isValid = _twoFactorAuthService.ValidateTotp(totpCode);
     
            if (isValid)
            {
                return Ok(new { Message = "2FA verification successful" });
            }

            return Unauthorized(new { Message = "Invalid 2FA code" });
        }

  
    }

}