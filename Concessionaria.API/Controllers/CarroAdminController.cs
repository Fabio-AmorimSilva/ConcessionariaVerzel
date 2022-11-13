using Concessionaria.Aplicacao.Contants;
using Concessionaria.Aplicacao.Interfaces;
using Concessionaria.Aplicacao.Params;
using Concessionaria.Aplicacao.ViewModels.Carro;
using Concessionaria.Aplicacao.ViewModels.Pagination;
using Concessionaria.Dominio.Models.Enumerations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Concessionaria.API.Controllers
{
    [ApiController]
    [Route("api/admin/carro")]
    [Authorize(Roles = Roles.Admin)]
    public class CarroAdminController : ControllerBase
    {
        private readonly ICarroService _carroService;

        public CarroAdminController(ICarroService carroService)
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

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CarroResponse>> Get(int id)
        {
            return await _carroService.GetByIdAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult<CarroResponse>> Post([FromBody] CarroRequest carroRequest)
        {
            return await _carroService.AddCarroAsync(carroRequest);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<CarroResponse>> Put([FromBody] CarroRequest carroRequest, [FromRoute] int id)
        {
            return await _carroService.AtualizaCarroAsync(carroRequest, id);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<CarroResponse>> Delete([FromRoute] int id)
        {
            return await _carroService.RemoveCarroAsync(id);
        }

        [HttpPut("img/{id:int}")]
        public async Task<ActionResult> PutImg([FromForm] IFormFile img, [FromRoute] int id)
        {
            var result = await _carroService.UploadImg(id, img);
            return Ok(result);
        }

        [HttpDelete("img/{id:int}")]
        public async Task<ActionResult> DeleteImg([FromRoute] int id)
        {
            var result = await _carroService.RemoveImg(id);
            return Ok(result);
        }

        [HttpGet("img/{id:int}")]
        [AllowAnonymous]
        public string GetImg([FromRoute] int id)
        {
            var result = _carroService.GetImg(id);
            return result;
        }

        [HttpGet("tipos-carro")]
        public IEnumerable<TipoCarro> GetTipoCarros()
        {
            return _carroService.GetTipoCarros();
        }
    }
}