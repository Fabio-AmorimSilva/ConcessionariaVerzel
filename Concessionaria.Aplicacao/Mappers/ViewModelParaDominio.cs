
using AutoMapper;
using Concessionaria.Aplicacao.ViewModels.Carro;
using Concessionaria.Aplicacao.ViewModels.Usuario;
using Concessionaria.Dominio.Models;

namespace Concessionaria.Aplicacao.Mappers
{
    public class ViewModelParaDominio : Profile
    {
        public ViewModelParaDominio()
        {
            CreateMap<CarroRequest, Carro>();
            CreateMap<UsuarioRequest, Usuario>();
        }
    }
}
