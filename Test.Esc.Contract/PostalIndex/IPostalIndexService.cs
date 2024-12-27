using Test.Esc.Contract.PostalIndex.So;

namespace Test.Esc.Contract.PostalIndex
{
    public interface IPostalIndexService
    {
        Task<PostalIndexSo?> Get(string postCode);

        Task<PostalIndexSo> Create(PostalIndexSo postalIndex);
    }
}
