namespace Test.Asc.WebApi.PostalIndex.Dto
{
    public class GetPostalIndexByPostalCodeResponseDto
    {
        public PostalIndexDto? postalIndex { get; set; }

        public string? ErrorMessage { get; set; }
    }
}
