
using AutoMapper;
using Concessionaria.Aplicacao.ViewModels.Carro;
using Concessionaria.Aplicacao.ViewModels.TipoCarro;
using Concessionaria.Aplicacao.ViewModels.TipoUsuario;
using Concessionaria.Aplicacao.ViewModels.Usuario;
using Concessionaria.Dominio.Models;
using Concessionaria.Dominio.Models.Enumerations;

namespace Concessionaria.Aplicacao.Mappers
{
    public class DominioParaViewModel : Profile
    {
        public DominioParaViewModel()
        {
            CreateMap<Carro, CarroResponse>();
            CreateMap<Usuario, UsuarioResponse>();
            CreateMap<TipoCarro, TipoCarroResponse>();
            CreateMap<TipoUsuario, TipoUsuarioResponse>();
        }
    }
}
