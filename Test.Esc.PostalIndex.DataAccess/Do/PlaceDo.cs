namespace Test.Esc.PostalIndex.DataAccess.Do
{
    public class PlaceDo
    {
        public int Id { get; set; }
     
        public string PlaceName { get; set; }
        
        public string Longitude { get; set; }
        
        public string State { get; set; }
        
        public string StateAbbreviation { get; set; }
        
        public string Latitude { get; set; }
        
        public byte[] RowVersion { get; set; }

        public DateTime CreatedOn { get; set; }

        public int PostalIndexId { get; set; }
        
        public PostalIndexDo PostalIndex { get; set; }
    }
}
