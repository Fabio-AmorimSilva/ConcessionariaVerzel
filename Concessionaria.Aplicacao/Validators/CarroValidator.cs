
using Concessionaria.Aplicacao.ViewModels.Carro;
using Concessionaria.Dominio.Core;
using Concessionaria.Dominio.Models.Enumerations;
using FluentValidation;

namespace Concessionaria.Aplicacao.Validators
{
    public class CarroValidator : AbstractValidator<CarroRequest>
    {
        public CarroValidator()
        {
            RuleFor(c => c.Nome)
                .MaximumLength(50)
                .NotNull()
                .NotEmpty();

            RuleFor(c => c.Marca)
                .MaximumLength(30)
                .NotNull()
                .NotEmpty();

            RuleFor(c => c.Valor)
                .NotEmpty();

            RuleFor(c => c.IdTipoCarro)
                .Must(tipoCarro => Enumeration.GetAll<TipoCarro>().Any(tipo => tipo.Id == tipoCarro))
                .WithMessage("O tipo de carro não existe");
        }
    }
}
