using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Orizon.Rest.Chat.Application.Interfaces;
using Orizon.Rest.Chat.Application.Services;
using Orizon.Rest.Chat.Domain.Interfaces.Repositories;
using Orizon.Rest.Chat.Domain.Interfaces.Services;
using Orizon.Rest.Chat.Domain.Services;
using Orizon.Rest.Chat.Infra.Data.Contexto;
using Orizon.Rest.Chat.Infra.Data.Provider;
using Orizon.Rest.Chat.Infra.Data.Repositories;

namespace Orizon.Rest.Chat.Infra.IoC
{
    public static class InjectorDependencies
    {
        public static void Registrer(IServiceCollection services, IConfiguration configuration)
        {
            // Context
            services.AddSingleton(new DativaDbContext(new SqlConnectionProvider(), configuration.GetConnectionString("Dativa")));
            services.AddSingleton(new FaturiFeDbContext(new SqlConnectionProvider(), configuration.GetConnectionString("FaturiFe")));
            services.AddSingleton(new PrefatDbContext(new SqlConnectionProvider(), configuration.GetConnectionString("PreFaturamento")));

            //Application
            services.AddScoped(typeof(IBaseApp<,>), typeof(BaseServicoApp<,>));
            services.AddScoped<IApontamentoApp, ApontamentoApp>();
            services.AddScoped<IChatApp, ChatApp>();
            services.AddScoped<IChatConversasApp, ChatConversasApp>();
            services.AddScoped<ICicloAuditoriaApp, CicloAuditoriaApp>();
            services.AddScoped<IDadosAuditorApp, DadosAuditorApp>();
            services.AddScoped<IProxyApp, ProxyApp>();

            //Domínio
            services.AddScoped(typeof(IBaseServico<>), typeof(BaseServico<>));
            services.AddScoped<IApontamentoService, ApontamentoService>();
            services.AddScoped<IChatService, ChatService>();
            services.AddScoped<IChatConversasService, ChatConversasService>();
            services.AddScoped<ICicloAuditoriaService, CicloAuditoriaService>();
            services.AddScoped<IDadosAuditorService, DadosAuditorService>();

            //Repositorio
            services.AddScoped(typeof(IBaseRepositorio<>), typeof(BaseRepositorio<>));
            services.AddScoped<IApontamentoRepository, ApontamentoRepository>();
            services.AddScoped<IChatRepository, ChatRepository>();
            services.AddScoped<IChatConversasRepository, ChatConversasRepository>();
            services.AddScoped<ICicloAuditoriaRepository, CicloAuditoriaRepository>();
            services.AddScoped<IDadosAuditorRepository, DadosAuditorRepository>();
        }
    }
}
