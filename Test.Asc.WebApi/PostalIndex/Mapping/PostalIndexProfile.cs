using AutoMapper;
using Test.Asc.Contract.PostalIndex.So;
using Test.Asc.WebApi.PostalIndex.Dto;

namespace Test.Asc.WebApi.PostalIndex.Mapping
{
    public class PostalIndexProfile : Profile
    {
        public PostalIndexProfile()
        {
            this.CreateMap<PostalIndexSo, PostalIndexDto>();
            this.CreateMap<PlaceSo, PlaceDto>();
        }
    }
}
