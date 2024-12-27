using Microsoft.EntityFrameworkCore;

namespace Test.Fixtures.Utils
{
    public abstract class DatabaseExecutorBase<TDbContext, TInput, TOutput> : ExecutorBase<TDbContext, TInput, TOutput> 
        where TDbContext : DbContext
    {

    }
}
