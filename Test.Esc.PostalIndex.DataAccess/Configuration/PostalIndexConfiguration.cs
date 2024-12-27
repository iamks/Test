using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Test.Esc.PostalIndex.DataAccess.Do;

namespace Test.Esc.PostalIndex.DataAccess.Configuration
{
    public class PostalIndexConfiguration : IEntityTypeConfiguration<PostalIndexDo>
    {
        public void Configure(EntityTypeBuilder<PostalIndexDo> builder)
        {
            // Table name
            builder.ToTable("PostalIndexes");

            // Table Key
            builder.HasKey(t => t.Id);

            // Foreign Keys
            builder.HasMany(t => t.Places)
                .WithOne(t => t.PostalIndex)
                .HasForeignKey(t => t.PostalIndexId)
                .IsRequired();

            // Column Mapping
            builder.Property(t => t.Id).HasColumnName("postalIndexId");
            builder.Property(t => t.CreatedOn).HasDefaultValueSql("GETUTCDATE()");

            // Row Version
            builder.Property(t => t.RowVersion).IsRowVersion();
        }
    }
}
