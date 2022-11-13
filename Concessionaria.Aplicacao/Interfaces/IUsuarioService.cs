using Concessionaria.Aplicacao.ViewModels.Usuario;
using Concessionaria.Dominio.Models.Enumerations;

namespace Concessionaria.Aplicacao.Interfaces
{
    public interface IUsuarioService
    {
        Task<UsuarioResponse> AddUsuarioAsync(UsuarioRequest usuarioRequest);
        Task<UsuarioResponse> AtualizaUsuarioAsync(UsuarioRequest usuarioRequest, int id);
        Task<UsuarioResponse> RemoveUsuarioAsync(int id);
        Task<UsuarioResponse> GetById(int id);
        IEnumerable<TipoUsuario> GetTipoUsuarios();
        Task<int> ContaUsuarios();
    }
}
