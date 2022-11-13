using Concessionaria.Aplicacao.Interfaces;
using Concessionaria.Aplicacao.ViewModels.Usuario;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Concessionaria.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/usuario")]
    public class UsuarioController : ControllerBase
    {
        private IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] UsuarioRequest usuarioRequest)
        {
            usuarioRequest.IdTipoUsuario = 2;
            await _usuarioService.AddUsuarioAsync(usuarioRequest);
            return Ok();
        }

        [HttpGet]
        public async Task<UsuarioResponse> Get()
        {
            int id = GetUserId();
            return await _usuarioService.GetById(id);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UsuarioRequest usuarioRequest)
        {
            int id = GetUserId();
            usuarioRequest.IdTipoUsuario = 2;
            await _usuarioService.AtualizaUsuarioAsync(usuarioRequest, id);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            int id = GetUserId();
            await _usuarioService.RemoveUsuarioAsync(id);
            return Ok();
        }

        private int GetUserId()
        {
            return int.Parse(User.FindFirst(ClaimTypes.Sid)!.Value);
        }
    }
}
