using Microsoft.EntityFrameworkCore;
using ProjectTemplate.Infra.Data.Contexto;
using System;
using Xunit;

namespace Tests
{
    public class MyReservationContextFixture : IDisposable
    {
        public BaseContexto Context { get; set; }

        public MyReservationContextFixture()
        {
            var builder = new DbContextOptionsBuilder<BaseContexto>();
            builder.UseInMemoryDatabase(databaseName: "NomeDoBanco" + Guid.NewGuid().ToString());

            var dbContextOptions = builder.Options;
            Context = new BaseContexto(dbContextOptions);

            Context.Database.EnsureDeleted();
            bool created = Context.Database.EnsureCreated();

            Assert.True(created);
        }

        public void Dispose()
        {
            Context?.Dispose();
        }
    }
}
