
using Concessionaria.Aplicacao.ViewModels.TipoUsuario;
using Concessionaria.Dominio.Core;

namespace Concessionaria.Aplicacao.ViewModels.Usuario
{
    public class UsuarioResponse : Register
    {
        public string Nome { get; set; }
        public string NomeUsuario { get; set; }
        public string Email { get; set; }
        public TipoUsuarioResponse TipoUsuario { get; set; }
        
    }
}
