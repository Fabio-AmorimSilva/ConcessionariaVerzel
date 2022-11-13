
using AutoMapper.Execution;
using Concessionaria.Dominio.Models;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Xml.Linq;

namespace Concessionaria.Aplicacao.Params
{
    public class CarroParams
    {
        public string? Nome { get; set; }
        public string? Marca { get; set; }
        public string? Modelo { get; set; }
        public decimal? ValorMaiorQue { get; set; }
        public decimal? ValorMenorQue {get; set;}
        public int? Skip { get; set; }
        public int? Take { get; set; } = 5;


        public Expression<Func<Carro, bool>> Filter()
        {
            var predicado = PredicateBuilder.New<Carro>();

            if (!string.IsNullOrEmpty(Nome))
                predicado = predicado.And(n => EF.Functions.Like(n.Nome, $"%{Nome}%"));

            if (!string.IsNullOrEmpty(Marca))
                predicado = predicado.And(n => EF.Functions.Like(n.Marca, $"%{Marca}%"));

            if (!string.IsNullOrEmpty(Modelo))
                predicado = predicado.And(n => EF.Functions.Like(n.Modelo, $"%{Modelo}%"));

            if (ValorMaiorQue.HasValue)
                predicado = predicado.And(c => c.Valor >= ValorMaiorQue);

            if (ValorMenorQue.HasValue)
                predicado = predicado.And(c => c.Valor <= ValorMenorQue);

            return predicado.IsStarted ? predicado : null;
        }
    }
}
