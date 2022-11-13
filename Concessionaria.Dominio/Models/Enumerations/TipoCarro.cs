
using Concessionaria.Dominio.Core;

namespace Concessionaria.Dominio.Models.Enumerations
{
    public class TipoCarro : Enumeration
    {
        public static TipoCarro Sedan = new(1, nameof(Sedan));
        public static TipoCarro SUV = new(2, nameof(SUV));
        public static TipoCarro Hatch = new(3, nameof(Hatch));
        public static TipoCarro Conversivel = new(4, nameof(Conversivel));
        public static TipoCarro Coupe = new(5, nameof(Coupe));

        public TipoCarro(int id, string name) : base(id, name)
        {
        }
    }
}
