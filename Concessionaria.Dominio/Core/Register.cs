
namespace Concessionaria.Dominio.Core
{
    public abstract class Register
    {
        public int Id { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime? AtualizadoEm { get; set; }
    }
}
