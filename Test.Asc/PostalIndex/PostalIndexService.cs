using AutoMapper;
using Test.Asc.Contract.PostalIndex;
using Test.Asc.Contract.PostalIndex.So;
using Test.DataAccess.ZipCodeLookup;

namespace Test.Asc.PostalIndex
{
    public class PostalIndexService : IPostalIndexService
    {
        private readonly Esc.Contract.PostalIndex.IPostalIndexService escPostalIndexService;

        private readonly IZipCodeLookupService zipCodeLookupService;

        private readonly IMapper mapper;

        public PostalIndexService(
            Esc.Contract.PostalIndex.IPostalIndexService escPostalIndexService,
            IZipCodeLookupService zipCodeLookupService,
            IMapper mapper)
        {
            this.escPostalIndexService = escPostalIndexService;
            this.zipCodeLookupService = zipCodeLookupService;
            this.mapper = mapper;
        }

        public async Task<PostalIndexSo?> GetPostalIndexByPostalCode(string postalCode)
        {
            PostalIndexSo? responseAscSo = null;
            var escPostalIndexSo = await this.escPostalIndexService.Get(postalCode);
            if (escPostalIndexSo == null)
            {
                var zipCodeDo = await this.zipCodeLookupService.GetZipCodeDetails(postalCode);

                if (zipCodeDo != null && zipCodeDo.Places?.Any() == true)
                {
                    var requestEscSo = this.mapper.Map<Esc.Contract.PostalIndex.So.PostalIndexSo>(zipCodeDo);
                    escPostalIndexSo = await this.escPostalIndexService.Create(requestEscSo);
                }
            }

            if (escPostalIndexSo != null)
            {
                responseAscSo = this.mapper.Map<PostalIndexSo>(escPostalIndexSo);
            }

            return responseAscSo;
        }
    }
}
