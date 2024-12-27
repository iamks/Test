using Test.Esc.PostalIndex.DataAccess.Do;
using Test.Fixtures.Utils.DecisionTable;

namespace Test.Fixtures.Database.PostalIndex.CreatePostalIndex.Input
{
    public class RequestForPlaceInCreatePostalIndexToDatabase : DynamicDecisionTableBase<PlaceDo>
    {
        public override async Task Execute()
        {
            await base.Execute();
        }
    }
}
