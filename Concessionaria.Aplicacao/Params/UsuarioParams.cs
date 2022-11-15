
using Concessionaria.Dominio.Models;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Concessionaria.Aplicacao.Params
{
    public class UsuarioParams
    {
        public string? Nome { get; set; }
        public string? NomeUsuario { get; set; }
        public string? Email { get; set; }
        public int? Skip { get; set; }
        public int? Take { get; set; } = 5;


        public Expression<Func<Usuario, bool>> Filter()
        {
            var predicado = PredicateBuilder.New<Usuario>();

            if (!string.IsNullOrEmpty(Nome))
                predicado = predicado.And(n => EF.Functions.Like(n.Nome, $"%{Nome}%"));

            if (!string.IsNullOrEmpty(NomeUsuario))
                predicado = predicado.And(n => EF.Functions.Like(n.NomeUsuario, $"%{NomeUsuario}%"));

            if (!string.IsNullOrEmpty(Email))
                predicado = predicado.And(n => EF.Functions.Like(n.Email, $"%{Email}%"));

            return predicado.IsStarted ? predicado : null;
        }
    }
}
