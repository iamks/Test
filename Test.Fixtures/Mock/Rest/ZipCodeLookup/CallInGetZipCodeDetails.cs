using Test.DataAccess.ZipCodeLookup.Do;
using Test.Fixtures.Utils;

namespace Test.Fixtures.Mock.Rest.ZipCodeLookup
{
    public class CallInGetZipCodeDetails : RestExecutorBase<Empty, ZipCodeDo?>
    {
        protected override string BaseUri { get; } = "http://zippopotam.url/";

        protected override string HttpMethod { get; } = System.Net.Http.HttpMethod.Get.Method;
    }
}
