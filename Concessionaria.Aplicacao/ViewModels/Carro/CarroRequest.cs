
namespace Concessionaria.Aplicacao.ViewModels.Carro
{
    public class CarroRequest
    {
        public string Nome { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public decimal Valor { get; set; }
        public string Foto { get; set; }
        public int IdTipoCarro { get; set; }
    }
}
