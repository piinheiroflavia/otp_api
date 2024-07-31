using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Teste_TOPT_Swagger.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        [HttpGet]
        [Authorize]
        public IActionResult Get() 
        {
            return Ok(new { Message = "This is a protected endpoint" });
        }


    }
}
