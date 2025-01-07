using Microsoft.EntityFrameworkCore;
using Test.Esc.PostalIndex.DataAccess.Context;
using Test.Esc.PostalIndex.DataAccess.Do;
using Test.Fixtures.Utils.ExecutorBases;

namespace Test.Fixtures.Database.PostalIndex.GetPostalIndexes
{
    public class CallInGetPostalIndexesToDatabase : DatabaseExecutorBase<PostalIndexDbContext, string, PostalIndexDo>
    {
        protected override PostalIndexDo? ExecuteCapability(PostalIndexDbContext capability, string input)
        {
            return capability.Set<PostalIndexDo>()
                .Include(x => x.Places)
                .FirstOrDefault(p => p.PostCode == input);
        }
    }
}
