using System.Security.Claims;

namespace Concessionaria.Aplicacao.Interfaces
{
    public interface IAuthService
    {
        int GetUserId(ClaimsPrincipal claims);
    }
}
