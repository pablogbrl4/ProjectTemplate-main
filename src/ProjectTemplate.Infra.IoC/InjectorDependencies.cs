using Microsoft.Extensions.DependencyInjection;
using ProjectTemplate.Application.Interfaces;
using ProjectTemplate.Application.Services;
using ProjectTemplate.Domain.Interfaces.Repositories;
using ProjectTemplate.Domain.Interfaces.Services;
using ProjectTemplate.Domain.Services;
using ProjectTemplate.Infra.Data.Repositories;
using ProjectTemplate.Infra.Data.UnitOfWork;

namespace ProjectTemplate.Infra.IoC
{
    public static class InjectorDependencies
    {
        public static void Registrer(IServiceCollection services)
        {
            //Application
            services.AddTransient(typeof(IBaseApp<,>), typeof(BaseServicoApp<,>));

            //Domínio
            services.AddTransient(typeof(IBaseServico<>), typeof(BaseServico<>));

            //Repositorio
            services.AddTransient(typeof(IBaseRepositorio<>), typeof(BaseRepositorio<>));

            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}
