
using Concessionaria.Aplicacao.ViewModels.TipoCarro;
using Concessionaria.Dominio.Core;

namespace Concessionaria.Aplicacao.ViewModels.Carro
{
    public class CarroResponse : Register
    {
        public string Nome { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public decimal Valor { get; set; }
        public string Foto { get; set; }
        public TipoCarroResponse TipoCarro { get; set; }
    }
}
