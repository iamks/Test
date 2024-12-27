using Test.Esc.PostalIndex.DataAccess.Do;

namespace Test.Esc.PostalIndex.DataAccess
{
    public interface IPostalIndexRepository
    {
        Task<PostalIndexDo?> Get(string postCode);

        Task Create(PostalIndexDo postalIndexDo);
    }
}
