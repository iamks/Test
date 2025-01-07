using Microsoft.EntityFrameworkCore;

namespace Test.Fixtures.Utils.ExecutorBases
{
    public abstract class DatabaseExecutorBase<TDbContext, TInput, TOutput> : ExecutorBase<TDbContext, TInput, TOutput>
        where TDbContext : DbContext
    {

    }
}
