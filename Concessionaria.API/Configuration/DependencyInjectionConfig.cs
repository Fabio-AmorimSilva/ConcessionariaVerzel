using Concessionaria.Aplicacao.Interfaces;
using Concessionaria.Aplicacao.Services;
using Concessionaria.Aplicacao.Validators;
using Concessionaria.Dominio.Interfaces.Comum;
using Concessionaria.Dominio.Interfaces.Repositorios;
using Concessionaria.Dominio.Interfaces.Storage;
using Concessionaria.Dominio.Models;
using Concessionaria.Infraestrutura.Context;
using Concessionaria.Infraestrutura.Repositories;
using Concessionaria.Infraestrutura.Storage;
using Concessionaria.Infraestrutura.UnitOfWork;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Configuration;
using ConfigurationManager = Microsoft.Extensions.Configuration.ConfigurationManager;

namespace Concessionaria.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddSingleton<IFileStorage, FileStorage>();

            services.AddScoped<ApplicationDbContext>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<ICarroService, CarroService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthService, AuthService>();

            services.AddScoped<IUsuarioRepository, UsuarioRepository<Usuario>>();
            services.AddScoped<ICarroRepository, CarroRepository<Carro>>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.Configure<Aplicacao.Options.FileSettings>(configuration.GetSection("FileSettings"));

            services.AddFluentValidation(fv => {
                fv.AutomaticValidationEnabled = false;
                fv.RegisterValidatorsFromAssemblyContaining<UsuarioValidator>();
                }
            );

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            return services;
        }
    }
}
