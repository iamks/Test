using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Test.Esc.PostalIndex.DataAccess.Do;

namespace Test.Esc.PostalIndex.DataAccess.Configuration
{
    public class PlaceConfiguration : IEntityTypeConfiguration<PlaceDo>
    {
        public void Configure(EntityTypeBuilder<PlaceDo> builder)
        {
            // Table Name
            builder.ToTable("Places");

            // Table Key
            builder.HasKey(t => t.Id);

            // Foreign Keys

            // Column Mapping
            builder.Property(t => t.Id).HasColumnName("placeId");
            builder.Property(t => t.CreatedOn).HasDefaultValueSql("GETUTCDATE()");

            // Row Version
            builder.Property(t => t.RowVersion).IsRowVersion();
        }
    }
}
