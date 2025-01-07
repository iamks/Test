namespace Test.Fixtures.Utils.ExecutorBases
{
    public sealed class Empty
    {
        private Empty() { }

        public static Empty Instance { get; } = new Empty();
    }
}
