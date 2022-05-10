using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orizon.Rest.Chat.Domain.Entities;

namespace Orizon.Rest.Chat.Infra.Data.Mappings
{
    public class BaseEntidadeMap<T> : IEntityTypeConfiguration<T>
           where T : BaseEntidade
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder
                .HasKey(c => c.Id);

            //builder
            //    .Property(c => c.Id)
            //    .ValueGeneratedOnAdd()
            //    .IsRequired()
            //    .HasColumnName("Id");
        }
    }
}
