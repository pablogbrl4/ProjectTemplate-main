using System;
using System.Threading.Tasks;

namespace Orizon.Rest.Chat.Infra.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task IniciarTransaction();
        Task SalvarMudancas(bool commit = true);
    }
}
