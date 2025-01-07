using Test.Esc.PostalIndex.DataAccess.Context;
using Test.Esc.PostalIndex.DataAccess.Do;
using Test.Fixtures.Utils.ExecutorBases;

namespace Test.Fixtures.Database.PostalIndex.CreatePostalIndex
{
    public class CallInCreatePostalIndexToDatabase : DatabaseExecutorBase<PostalIndexDbContext, List<PostalIndexDo>, Empty>
    {
        protected override Empty ExecuteCapability(PostalIndexDbContext capability, List<PostalIndexDo> input)
        {
            input.ForEach(x => capability.PostalIndexes.Add(x));
            capability.SaveChanges();

            return Empty.Instance;
        }
    }
}
