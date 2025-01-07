using System.Linq.Expressions;
using Test.Asc.Contract.PostalIndex;
using Test.Asc.Contract.PostalIndex.So;
using Test.Fixtures.Utils.ExecutorBases;

namespace Test.Fixtures.Capability.PostalIndex.GetPostalIndexByPostalCode
{
    public class CallInGetPostalIndexByPostalCodeToPostalIndex : ServiceExecutorBase<IPostalIndexService, string, PostalIndexSo?>
    {
        protected override Expression<Func<IPostalIndexService, string, Task<PostalIndexSo?>>> MethodAsync => (service, input) => service.GetPostalIndexByPostalCode(input);

        protected override Expression<Func<IPostalIndexService, string, PostalIndexSo?>> Method => null;
    }
}
