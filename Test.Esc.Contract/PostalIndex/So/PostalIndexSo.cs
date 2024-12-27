namespace Test.Esc.Contract.PostalIndex.So
{
    public class PostalIndexSo
    {
        public string PostCode { get; set; }

        public string Country { get; set; }

        public string CountryAbbreviation { get; set; }

        public List<PlaceSo> places { get; set; }
    }
}
