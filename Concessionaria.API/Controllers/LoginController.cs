using Concessionaria.Aplicacao.Interfaces;
using Concessionaria.Aplicacao.ViewModels.Login;
using Microsoft.AspNetCore.Mvc;

namespace Concessionaria.API.Controllers
{
    [ApiController]
    [Route("api/login")]
    public class LoginController : ControllerBase
    {
        private ITokenService _tokenService;
        private ILoginService _loginService;
        private IUsuarioService _usuarioService;

        public LoginController(
            ITokenService tokenService,
            ILoginService loginService,
            IUsuarioService usuarioService)
        {
            _tokenService = tokenService;
            _loginService = loginService;
            _usuarioService = usuarioService;
        }

        [HttpPost]
        public async Task<ActionResult> LoginRequest([FromBody] LoginRequest model)
        {
            var result = await _loginService.Login(model);
            var token = _tokenService.GenerateToken(result);

            return Ok(new
            {
                Token = token
            });
        }
    }
}
