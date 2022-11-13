using Concessionaria.Aplicacao.ViewModels.Usuario;
using Concessionaria.Dominio.Core;
using Concessionaria.Dominio.Interfaces.Repositorios;
using Concessionaria.Dominio.Models.Enumerations;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Concessionaria.Aplicacao.Validators
{
    public class UsuarioValidator: AbstractValidator<UsuarioRequest>
    {
        public UsuarioValidator(IUsuarioRepository usuarioRepository)
        {
            RuleFor(u => u.NomeUsuario)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(u => u.NomeUsuario)
                .Must(nomeUsuario => usuarioRepository.FirstQuery().AsNoTracking().All(a => a.NomeUsuario != nomeUsuario))
                .WithMessage("Nome de usuário já existe!");

            RuleFor(u => u.Email)
              .EmailAddress()
              .NotEmpty();

            RuleFor(u => u.Email)
                .Must(email => usuarioRepository.FirstQuery().AsNoTracking().All(x => x.Email != email))
                .WithMessage("Email já existe!");

            RuleFor(u => u.Nome)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(u => u.IdTipoUsuario)
                .Must(tipoUsuario => Enumeration.GetAll<TipoUsuario>().Any(tipo => tipo.Id == tipoUsuario))
                .WithMessage("Tipo de usuário não existe!");
        }
    }
}
