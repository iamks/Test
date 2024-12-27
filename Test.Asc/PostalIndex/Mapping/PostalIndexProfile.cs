using AutoMapper;
using Test.Asc.Contract.PostalIndex.So;
using Test.DataAccess.ZipCodeLookup.Do;

namespace Test.Asc.PostalIndex.Mapping
{
    public class PostalIndexProfile : Profile
    {
        public PostalIndexProfile()
        {
            this.CreateMap<ZipCodeDo, PostalIndexSo>();
            this.CreateMap<PlaceDo, PlaceSo>();

            this.CreateMap<ZipCodeDo, Esc.Contract.PostalIndex.So.PostalIndexSo>();
            this.CreateMap<PlaceDo, Esc.Contract.PostalIndex.So.PlaceSo>();

            this.CreateMap<Esc.Contract.PostalIndex.So.PostalIndexSo, PostalIndexSo>();
            this.CreateMap<Esc.Contract.PostalIndex.So.PlaceSo, PlaceSo>();
        }
    }
}
