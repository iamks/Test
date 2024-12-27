namespace Test.Fixtures.Utils.ServiceRegistrator
{
    public interface IFitnesseServiceRegistrator
    {
        Task<T> GetRequiredService<T>() where T: notnull;

        Task<IEnumerable<T>> GetServices<T>() where T: notnull;

        Task Release<T>(T service);
    }
}
