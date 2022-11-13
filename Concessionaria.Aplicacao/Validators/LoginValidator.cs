using Concessionaria.Aplicacao.ViewModels.Login;
using FluentValidation;

namespace Concessionaria.Aplicacao.Validators
{
    public class LoginValidator : AbstractValidator<LoginRequest>
    {
        public LoginValidator()
        {
            RuleFor(l => l.NomeUsuario)
                .MaximumLength(100)
                .NotEmpty();

            RuleFor(l => l.Senha)
                .MinimumLength(6)
                .NotEmpty();
        }
    }
}
