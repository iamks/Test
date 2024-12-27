namespace Test.Fixtures.Utils.Extensions
{
    public static class TypeExtensions
    {
        /// <summary>
        /// The types are same or type implements the otherType
        /// </summary>
        /// <param name="type">current type</param>
        /// <param name="otherType">other type</param>
        /// <returns></returns>
        public static bool IsObjectOfType(this Type type, Type otherType)
        {
            return type.Name == otherType.Name 
                || type.GetInterface(otherType.Name) != null;
        }
    }
}
