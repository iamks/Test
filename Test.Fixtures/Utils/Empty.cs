namespace Test.Fixtures.Utils
{
    public sealed class Empty
    {
        private Empty() {}

        public static Empty Instance { get; } = new Empty();
    }
}
