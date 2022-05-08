using System;
using System.Threading.Tasks;

namespace ProjectTemplate.Infra.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task IniciarTransaction();
        Task SalvarMudancas(bool commit = true);
    }
}
