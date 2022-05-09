using Microsoft.Extensions.DependencyInjection;
using Orizon.Rest.Chat.Application.Interfaces;
using Orizon.Rest.Chat.Application.Services;
using Orizon.Rest.Chat.Domain.Interfaces.Repositories;
using Orizon.Rest.Chat.Domain.Interfaces.Services;
using Orizon.Rest.Chat.Domain.Services;
using Orizon.Rest.Chat.Infra.Data.Repositories;
using Orizon.Rest.Chat.Infra.Data.UnitOfWork;

namespace Orizon.Rest.Chat.Infra.IoC
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
