
using Concessionaria.Aplicacao.Params;
using Concessionaria.Aplicacao.ViewModels.Carro;
using Concessionaria.Dominio.Models.Enumerations;
using Microsoft.AspNetCore.Http;

namespace Concessionaria.Aplicacao.Interfaces
{
    public interface ICarroService
    {
        Task<CarroResponse> AddCarroAsync(CarroRequest carroRequest);
        Task<CarroResponse> AtualizaCarroAsync(CarroRequest carroRequest, int id);
        Task<CarroResponse> RemoveCarroAsync(int id);
        Task<IEnumerable<CarroResponse>> GetAllCarrosAsync(CarroParams query);
        Task<CarroResponse> GetByIdAsync(int id);
        IEnumerable<TipoCarro> GetTipoCarros();
        Task<CarroResponse> UploadImg(int id, IFormFile img);
        Task<CarroResponse> RemoveImg(int id);
        FileStream GetImg(int id);
        Task<int> ContaCarros();
    }
}
