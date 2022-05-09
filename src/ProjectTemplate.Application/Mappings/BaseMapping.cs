using AutoMapper;

namespace Orizon.Rest.Chat.Application.Mappings
{
    public class BaseMapping : Profile
    {
        public BaseMapping()
        {

            //  CreateMap<Entidade, EntidadeDTO>().ReverseMap();

            //   CreateMap<Entidade, EntidadeDTO>()
            //.ForMember(dest => dest.CampoEntidadeDTO,
            //opts => opts.MapFrom(src => src.CampoEntidade));

            // CreateMap<PaginacaoModel<Entidade>, PaginacaoModel<EntidadeDTO>>().ReverseMap();
        }
    }
}
