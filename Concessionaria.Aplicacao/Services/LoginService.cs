
using Concessionaria.Aplicacao.Exceptions;
using Concessionaria.Aplicacao.Interfaces;
using Concessionaria.Aplicacao.ViewModels.Login;
using Concessionaria.Dominio.Interfaces.Repositorios;
using Concessionaria.Infraestrutura.Utils;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Concessionaria.Aplicacao.Services
{
    public class LoginService : ILoginService
    {
        private IUsuarioRepository _userRepository;
        private readonly IValidator<LoginRequest> _validator;

        public LoginService(IUsuarioRepository userRepository, IValidator<LoginRequest> validator)
        {
            _userRepository = userRepository;
            _validator = validator;
        }

        public async Task<IEnumerable<Claim>> Login(LoginRequest login)
        {

            var validationResult = await _validator.ValidateAsync(login);

            if (!validationResult.IsValid)
                throw new BadRequestException(validationResult);

            var result = await _userRepository.GetById(filter: u => u.NomeUsuario == login.NomeUsuario, include: i => i.Include(r => r.TipoUsuario));

            if (result == null)
                throw new BadRequestException(nameof(login.NomeUsuario), "Senha ou nome de usuário incorretos. Por favor, tente novamente.");

            if (!PasswordHasher.Verify(login.Senha, result.Senha))
                throw new BadRequestException(nameof(login.Senha), "Senha ou nome de usuário incorretos. Por favor, tente novamente.");

            return new List<Claim>
            {
                new Claim(ClaimTypes.Sid, result.Id.ToString()),
                new Claim(ClaimTypes.Email, result.Email),
                new Claim(ClaimTypes.Name, result.Nome),
                new Claim(ClaimTypes.Role, result.TipoUsuario.Name),
            };
        }
    }
}
