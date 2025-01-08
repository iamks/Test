using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Test.Fixtures.Utils.ExecutorBases
{
    public abstract class DatabaseExecutorBase<TDbContext, TInput, TOutput> : ExecutorBase<TDbContext, TInput, TOutput>
        where TDbContext : DbContext
    {
        protected override Task<bool> CompareOutputs(TOutput expectedOutput, TOutput actualOutput)
        {
            actualOutput = RemoveReferenceLooping(actualOutput);

            return base.CompareOutputs(expectedOutput, actualOutput);
        }

        private static TOutput RemoveReferenceLooping(TOutput output)
        {
            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore
            };
            var serializedOutput = JsonConvert.SerializeObject(output, settings);
         
            return JsonConvert.DeserializeObject<TOutput>(serializedOutput);
        }
    }
}
