using AutoMapper;
using Test.Esc.Contract.PostalIndex;
using Test.Esc.Contract.PostalIndex.So;
using Test.Esc.PostalIndex.DataAccess;
using Test.Esc.PostalIndex.DataAccess.Do;

namespace Test.Esc.PostalIndex
{
    public class PostalIndexService : IPostalIndexService
    {
        private readonly IPostalIndexRepository postalIndexRepository;

        private readonly IMapper mapper;

        public PostalIndexService(
            IPostalIndexRepository postalIndexRepository, 
            IMapper mapper)
        {
            this.postalIndexRepository = postalIndexRepository;
            this.mapper = mapper;
        }

        public async Task<PostalIndexSo> Create(PostalIndexSo postalIndexSo)
        {
            Console.WriteLine("CreateDbC");
            var postalIndexDo = this.mapper.Map<PostalIndexDo>(postalIndexSo);
            await this.postalIndexRepository.Create(postalIndexDo);

            var createdPostCode = postalIndexDo.PostCode;
            return await this.Get(createdPostCode);
        }

        public async Task<PostalIndexSo?> Get(string postCode)
        {
            var postalIndexDo = await this.postalIndexRepository.Get(postCode);
            return this.mapper.Map<PostalIndexSo?>(postalIndexDo);
        }
    }
}
