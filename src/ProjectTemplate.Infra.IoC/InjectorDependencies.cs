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
            services.AddTransient<IApontamentoApp, ApontamentoApp>();
            services.AddTransient<IChatApp, ChatApp>();
            services.AddTransient<IChatConversasApp, ChatConversasApp>();
            services.AddTransient<ICicloAuditoriaApp, CicloAuditoriaApp>();
            services.AddTransient<IDadosAuditorApp, DadosAuditorApp>();
            services.AddTransient<IProxyApp, ProxyApp>();

            //Domínio
            services.AddTransient(typeof(IBaseServico<>), typeof(BaseServico<>));
            services.AddTransient<IApontamentoService, ApontamentoService>();
            services.AddTransient<IChatService, ChatService>();
            services.AddTransient<IChatConversasService, ChatConversasService>();
            services.AddTransient<ICicloAuditoriaService, CicloAuditoriaService>();
            services.AddTransient<IDadosAuditorService, DadosAuditorService>();

            //Repositorio
            services.AddTransient(typeof(IBaseRepositorio<>), typeof(BaseRepositorio<>));
            services.AddTransient<IApontamentoRepository, ApontamentoRepository>();
            services.AddTransient<IChatRepository, ChatRepository>();
            services.AddTransient<IChatConversasRepository, ChatConversasRepository>();
            services.AddTransient<ICicloAuditoriaRepository, CicloAuditoriaRepository>();
            services.AddTransient<IDadosAuditorRepository, DadosAuditorRepository>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}
