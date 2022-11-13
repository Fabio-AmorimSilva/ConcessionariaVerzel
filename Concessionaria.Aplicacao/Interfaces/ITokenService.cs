using System.Security.Claims;

namespace Concessionaria.Aplicacao.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(IEnumerable<Claim> claims);
    }
}
