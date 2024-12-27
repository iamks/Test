using Test.DataAccess.ZipCodeLookup.Do;

namespace Test.DataAccess.ZipCodeLookup
{
    public interface IZipCodeLookupService
    {
        Task<ZipCodeDo?> GetZipCodeDetails(string  zipCode);
    }
}
