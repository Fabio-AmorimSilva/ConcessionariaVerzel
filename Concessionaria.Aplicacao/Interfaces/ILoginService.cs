
using Concessionaria.Aplicacao.ViewModels.Login;
using System.Security.Claims;

namespace Concessionaria.Aplicacao.Interfaces
{
    public interface ILoginService
    { 
        Task<IEnumerable<Claim>> Login(LoginRequest login);
    }
}
