using Concessionaria.Aplicacao.Contants;
using Concessionaria.Aplicacao.Interfaces;
using Concessionaria.Aplicacao.Params;
using Concessionaria.Aplicacao.ViewModels.Pagination;
using Concessionaria.Aplicacao.ViewModels.Usuario;
using Concessionaria.Dominio.Core;
using Concessionaria.Dominio.Models.Enumerations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Concessionaria.API.Controllers
{
    [ApiController]
    [Route("api/admin/usuario")]
    [Authorize(Roles = Roles.Admin)]
    public class UsuarioAdminController : ControllerBase
    {
        public IUsuarioService _usuarioService;

        public UsuarioAdminController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<PaginationResponse<UsuarioResponse>> GetAll([FromQuery] UsuarioParams query)
        {
            return new PaginationResponse<UsuarioResponse>
            {
                Info = await _usuarioService.GetAllUsuarios(query),
                TotalPages = await _usuarioService.ContaUsuarios(),
                Take = query.Take,
                Skip = query.Skip,
            };
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioResponse>> Post(UsuarioRequest usuario)
        {
            return await _usuarioService.AddUsuarioAsync(usuario);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<UsuarioResponse>> Put(UsuarioRequest usuario, [FromRoute] int id)
        {
            return await _usuarioService.AtualizaUsuarioAsync(usuario, id);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<UsuarioResponse>> Delete([FromRoute] int id)
        {
            return await _usuarioService.RemoveUsuarioAsync(id);
        }

        [HttpGet("tipo-usuarios")]
        public IEnumerable<TipoUsuario> GetTiposUsuarios()
        {
            return _usuarioService.GetTipoUsuarios();
        }
    }
}
