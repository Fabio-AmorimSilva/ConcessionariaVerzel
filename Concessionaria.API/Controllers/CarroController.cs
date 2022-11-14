using Concessionaria.Aplicacao.Interfaces;
using Concessionaria.Aplicacao.Params;
using Concessionaria.Aplicacao.ViewModels.Carro;
using Concessionaria.Aplicacao.ViewModels.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Concessionaria.API.Controllers
{
    [ApiController]
    [Route("api/carro")]
    public class CarroController : ControllerBase
    {
        private readonly ICarroService _carroService;

        public CarroController(ICarroService carroService)
        {
            _carroService = carroService;
        }

        [HttpGet]
        public async Task<PaginationResponse<CarroResponse>> GetAllContacts([FromQuery] CarroParams carroParams)
        {
            return new PaginationResponse<CarroResponse>
            {
                Info = await _carroService.GetAllCarrosAsync(carroParams),
                TotalPages = await _carroService.ContaCarros(),
                Skip = carroParams.Skip,
                Take = carroParams.Take,
            };
        }
    }
}
