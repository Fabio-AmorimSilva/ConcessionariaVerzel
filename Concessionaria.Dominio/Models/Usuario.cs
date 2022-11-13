
using Concessionaria.Dominio.Core;
using Concessionaria.Dominio.Models.Enumerations;

namespace Concessionaria.Dominio.Models
{
    public class Usuario: Register
    {
        public string Nome { get; set; }
        public string NomeUsuario { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
        public int IdTipoUsuario { get; set; }
    }
}
