using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Teste_TOPT_Swagger.Service.IService;

namespace Teste_TOPT_Swagger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class loginUserController : Controller
    {

        private readonly ILoginService _loginService;
        private readonly ILogger<loginUserController> _logger;


        public loginUserController(ILoginService ILoginService, ILogger<loginUserController> logger)
        {
            _loginService = ILoginService;
            _logger = logger;
        }

        /// <summary>
        /// Autenticação do usuário
        /// </summary>
        /// <param name="loginRequest">Requisição para login e senha do usuário</param>
        /// <returns>Usuário autenticado</returns>
        [HttpPost("login")]
        [AllowAnonymous]
        public  IActionResult LoginUser([FromBody] LoginRequest loginRequest)
        {
            try
            {
                _logger.LogInformation("Login attempt for user {Username}", loginRequest.Username);

                if (_loginService.AuthLogin(loginRequest.Username, loginRequest.Password))
                {
                    var token = _loginService.GenerateJwtToken(loginRequest.Username);
                    return Ok(new { Token = token, loginRequest.Username });
                }

                _logger.LogWarning("Invalid login attempt for user {Username}", loginRequest.Username);
                return Unauthorized("Invalid username or password");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during login.");
                return StatusCode(500, "Internal server error");
            }
        }

       public class LoginRequest
       {
            public string Username { get; set;}
            public string Password { get; set;}
       }

    }
}


