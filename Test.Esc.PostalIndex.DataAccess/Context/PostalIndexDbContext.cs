using Microsoft.EntityFrameworkCore;
using Test.Esc.PostalIndex.DataAccess.Configuration;
using Test.Esc.PostalIndex.DataAccess.Do;

namespace Test.Esc.PostalIndex.DataAccess.Context
{
    public class PostalIndexDbContext : DbContext
    {
        public PostalIndexDbContext(DbContextOptions<PostalIndexDbContext> options)
          : base(options)
        {
            
        }

        public DbSet<PostalIndexDo> PostalIndexes { get; set; }
        
        public DbSet<PlaceDo> Places { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new PostalIndexConfiguration().Configure(modelBuilder.Entity<PostalIndexDo>());
            new PlaceConfiguration().Configure(modelBuilder.Entity<PlaceDo>());
        }
    }
}
