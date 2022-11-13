
using AutoMapper;
using Concessionaria.Aplicacao.Exceptions;
using Concessionaria.Aplicacao.Interfaces;
using Concessionaria.Aplicacao.Options;
using Concessionaria.Aplicacao.Params;
using Concessionaria.Aplicacao.ViewModels.Carro;
using Concessionaria.Dominio.Core;
using Concessionaria.Dominio.Interfaces.Comum;
using Concessionaria.Dominio.Interfaces.Repositorios;
using Concessionaria.Dominio.Interfaces.Storage;
using Concessionaria.Dominio.Models;
using Concessionaria.Dominio.Models.Enumerations;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Concessionaria.Aplicacao.Services
{
    public class CarroService : ICarroService
    {
        private ICarroRepository _carroRepository;
        private IValidator<CarroRequest> _validator;
        private IUnitOfWork _unitOfWork;
        private readonly FileSettings _fileApiOptions;
        private readonly IFileStorage _fileStorage;
        private IMapper _mapper;

        public CarroService(
            ICarroRepository carroRepository, 
            IValidator<CarroRequest> validator,
            IOptions<FileSettings> options,
            IUnitOfWork unitOfWork, 
            IFileStorage fileStorage,
            IMapper mapper)
        {
            _carroRepository = carroRepository;
            _validator = validator;
            _unitOfWork = unitOfWork;
            _fileApiOptions = options.Value;
            _fileStorage = fileStorage;
            _mapper = mapper;
        }

        public async Task<CarroResponse> AddCarroAsync(CarroRequest carroRequest)
        {
            var validation = _validator.Validate(carroRequest);

            if (!validation.IsValid)
                throw new BadRequestException(validation);

            var carro = _mapper.Map<Carro>(carroRequest);
            await _carroRepository.Add(carro);
            await _unitOfWork.Commit();

            return _mapper.Map<CarroResponse>(carro);
        }

        public async Task<CarroResponse> AtualizaCarroAsync(CarroRequest carroRequest, int id)
        {
            var carroBusca = await _carroRepository.GetById(filter: c => c.Id == id)?? 
                throw new BadRequestException(nameof(id), "Carro não consta na base de dados");

            var validation = _validator.Validate(carroRequest);

            if (!validation.IsValid)
                throw new BadRequestException(validation);

            _mapper.Map<CarroRequest, Carro>(carroRequest, carroBusca);
            await _carroRepository.Update(carroBusca);
            await _unitOfWork.Commit();

            return _mapper.Map<CarroResponse>(carroBusca);

        }

        public async Task<IEnumerable<CarroResponse>> GetAllCarrosAsync(CarroParams query)
        {
            return _mapper.Map<IEnumerable<CarroResponse>>
                (await _carroRepository.GetAll(predicate: query.Filter(),
                include: i => i.Include(t => t.TipoCarro), take: query.Take, skip: query.Skip));
        }

        public async Task<CarroResponse> GetByIdAsync(int id)
        {
            return _mapper.Map<CarroResponse>(await _carroRepository
                .GetById(filter: u => u.Id == id, include: i => i.Include(t => t.TipoCarro)));
        }

        public async Task<CarroResponse> RemoveCarroAsync(int id)
        {
            var carroBusca = await _carroRepository.GetById(filter: c => c.Id == id)
                ?? throw new BadRequestException(nameof(id), "O carro não consta na base de dados");

            await _carroRepository.Delete(id);
            await _unitOfWork.Commit();

            return _mapper.Map<CarroResponse>(carroBusca);
        }

        public async Task<CarroResponse> UploadImg(int id, IFormFile img)
        {
            var entity = await _carroRepository.GetById(u => u.Id == id)
                ?? throw new BadRequestException(nameof(id), $"Carro com {id} não encontrado.");

            if (img == null || img.Length == 0)
                throw new BadRequestException(nameof(id), "Nenhuma imagem foi fornecida.");

            var extesionFile = Path.GetExtension(img.FileName);

            if (!_fileApiOptions.CarroFileTypes.Contains(extesionFile))
                throw new BadRequestException(nameof(id), "Formato de imagem invalido.");

            if (entity.Foto != null)
                await _fileStorage.RemoveFile(entity.Foto);

            await _fileStorage.IfNotExistCreateDirectory(_fileApiOptions.CarroImgDirectory);

            entity.Foto = Path.Combine(_fileApiOptions.CarroImgDirectory, Guid.NewGuid().ToString() + extesionFile);
            await _fileStorage.UploadFile(img, entity.Foto);
            await _unitOfWork.Commit();

            return _mapper.Map<CarroResponse>(entity);
        }

        public async Task<CarroResponse> RemoveImg(int id)
        {
            var entity = await _carroRepository.GetById(u => u.Id == id)
                ?? throw new BadRequestException(nameof(id), $"Carro com {id} não encontrado.");

            if (entity.Foto != null)
            {
                await _fileStorage.RemoveFile(entity.Foto);
                entity.Foto = null;
                await _unitOfWork.Commit();
                return _mapper.Map<CarroResponse>(entity);
            }
            throw new BadRequestException(nameof(id), $"Não existe nenhuma imagem no carro com id: {id}");
        }

        public string GetImg(int id)
        {
            var entity = _carroRepository.GetById(u => u.Id == id).GetAwaiter().GetResult()
                ?? throw new BadRequestException(nameof(id), $"Carro com {id} não encontrado.");

            var pathImg = _fileApiOptions.DefaultCarroImgPath;

            if (entity.Foto != null)
                pathImg = entity.Foto;

            if (string.IsNullOrEmpty(pathImg))
                throw new BadRequestException(nameof(id), "Nenhuma imagem encotrada.");

            byte[] bytes = File.ReadAllBytes(entity.Foto);
            string image = Convert.ToBase64String(bytes);

            return image;

        }
        public IEnumerable<TipoCarro> GetTipoCarros()
        {
            return Enumeration.GetAll<TipoCarro>();
        }

        public async Task<int> ContaCarros()
        {
            return await _carroRepository.CountAll();
        }
    }
}
