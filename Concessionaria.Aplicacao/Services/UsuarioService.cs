using AutoMapper;
using Concessionaria.Aplicacao.Exceptions;
using Concessionaria.Aplicacao.Interfaces;
using Concessionaria.Aplicacao.ViewModels.Usuario;
using Concessionaria.Dominio.Core;
using Concessionaria.Dominio.Interfaces.Comum;
using Concessionaria.Dominio.Interfaces.Repositorios;
using Concessionaria.Dominio.Models;
using Concessionaria.Dominio.Models.Enumerations;
using Concessionaria.Infraestrutura.Utils;
using FluentValidation;

namespace Concessionaria.Aplicacao.Services
{
    public class UsuarioService : IUsuarioService
    {
        private IUsuarioRepository _usuarioRepository;
        private IValidator<UsuarioRequest> _validator;
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public UsuarioService(
            IUsuarioRepository usuarioRepository, 
            IValidator<UsuarioRequest> validator, 
            IUnitOfWork unitOfWork, 
            IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _validator = validator;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UsuarioResponse> AddUsuarioAsync(UsuarioRequest usuarioRequest)
        {
            var validation = _validator.Validate(usuarioRequest);

            if (!validation.IsValid)
                throw new BadRequestException(validation);

            usuarioRequest.Senha = PasswordHasher.Hash(usuarioRequest.Senha);
            var usuarioAdicionado = _mapper.Map<Usuario>(usuarioRequest);
            await _usuarioRepository.Add(usuarioAdicionado);
            await _unitOfWork.Commit();

            return _mapper.Map<UsuarioResponse>(usuarioAdicionado);
        }

        public async Task<UsuarioResponse> AtualizaUsuarioAsync(UsuarioRequest usuarioRequest, int id)
        {
            var usuarioBusca = await _usuarioRepository.GetById(filter: u => u.Id == id)
                ?? throw new BadRequestException(nameof(id), "O usuário não consta na base de dados");

            var validation = _validator.Validate(usuarioRequest);

            if (!validation.IsValid)
                throw new BadRequestException(validation);

            usuarioRequest.Senha = PasswordHasher.Hash(usuarioRequest.Senha);
            _mapper.Map<UsuarioRequest, Usuario>(usuarioRequest, usuarioBusca);
            await _usuarioRepository.Update(usuarioBusca);
            await _unitOfWork.Commit();

            return _mapper.Map<UsuarioResponse>(usuarioBusca);

        }

        public async Task<UsuarioResponse> GetById(int id)
        {
            return _mapper.Map<UsuarioResponse>(await _usuarioRepository.GetById(filter: u => u.Id == id));
        }

        public IEnumerable<TipoUsuario> GetTipoUsuarios()
        {
            return Enumeration.GetAll<TipoUsuario>();
        }

        public async Task<UsuarioResponse> RemoveUsuarioAsync(int id)
        {
            var usuarioBusca = await _usuarioRepository.GetById(filter: u => u.Id == id)??
                throw new BadRequestException(nameof(id), "O usuário não consta na base de dados");

            await _usuarioRepository.Delete(id);
            await _unitOfWork.Commit();

            return _mapper.Map<UsuarioResponse>(usuarioBusca);

        }

        public async Task<int> ContaUsuarios()
        {
            return await _usuarioRepository.CountAll();
        }
    }
}
