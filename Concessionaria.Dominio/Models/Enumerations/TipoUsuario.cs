
using Concessionaria.Dominio.Core;

namespace Concessionaria.Dominio.Models.Enumerations
{
    public class TipoUsuario : Enumeration
    {
        public static TipoUsuario Admin = new(1, nameof(Admin));
        public static TipoUsuario Common = new(2, nameof(Common));

        public TipoUsuario(int id, string name) : base(id, name)
        {
        }
    }
}
