
using Concessionaria.Dominio.Models.Enumerations;

namespace Concessionaria.Aplicacao.ViewModels.Usuario
{
    public class UsuarioRequest
    {
        public string Nome { get; set; }
        public string NomeUsuario { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public int IdTipoUsuario { get; set; }
    }
}
