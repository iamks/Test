namespace Test.Esc.PostalIndex.DataAccess.Do
{
    public class PostalIndexDo
    {
        public int Id { get; set; }

        public string PostCode { get; set; }

        public string Country { get; set; }

        public string CountryAbbreviation { get; set; }

        public byte[] RowVersion { get; set; }

        public DateTime CreatedOn { get; set; }

        public ICollection<PlaceDo> Places { get; set; }
    }
}
