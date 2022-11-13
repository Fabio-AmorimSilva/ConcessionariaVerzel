using Concessionaria.Dominio.Core;
using Concessionaria.Dominio.Models.Enumerations;

namespace Concessionaria.Dominio.Models
{
    public class Carro : Register
    {
        public string Nome { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public decimal Valor { get; set; }
        public string? Foto { get; set; }
        public int IdTipoCarro { get; set; }
        public TipoCarro TipoCarro { get; set; }
        
    }
}
