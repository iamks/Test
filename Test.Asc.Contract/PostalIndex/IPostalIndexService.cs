using Test.Asc.Contract.PostalIndex.So;

namespace Test.Asc.Contract.PostalIndex
{
    public interface IPostalIndexService
    {
        Task<PostalIndexSo?> GetPostalIndexByPostalCode(string postalCode);
    }
}
