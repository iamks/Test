namespace Test.Asc.WebApi.PostalIndex.Dto
{
    public class PostalIndexDto
    {
        public string PostCode { get; set; }

        public string Country { get; set; }

        public string CountryAbbreviation { get; set; }

        public List<PlaceDto> places { get; set; }
    }
}
