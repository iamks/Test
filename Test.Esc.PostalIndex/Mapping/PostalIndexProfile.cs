using AutoMapper;
using Test.Esc.Contract.PostalIndex.So;
using Test.Esc.PostalIndex.DataAccess.Do;

namespace Test.Esc.PostalIndex.Mapping
{
    public class PostalIndexProfile : Profile
    {
        public PostalIndexProfile()
        {
            this.CreateMap<PostalIndexDo, PostalIndexSo>().ReverseMap();
            this.CreateMap<PlaceDo, PlaceSo>().ReverseMap();
        }
    }
}
