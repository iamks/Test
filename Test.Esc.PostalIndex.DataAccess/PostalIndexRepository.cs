using Microsoft.EntityFrameworkCore;
using Test.Esc.PostalIndex.DataAccess.Context;
using Test.Esc.PostalIndex.DataAccess.Do;

namespace Test.Esc.PostalIndex.DataAccess
{
    public class PostalIndexRepository : IPostalIndexRepository
    {
        private readonly PostalIndexDbContext dbContext;

        public PostalIndexRepository(PostalIndexDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Create(PostalIndexDo postalIndexDo)
        {
            await this.dbContext.Set<PostalIndexDo>()
                .AddAsync(postalIndexDo);

            await this.dbContext.SaveChangesAsync();
        }

        public async Task<PostalIndexDo?> Get(string postCode)
        {
            return await dbContext.Set<PostalIndexDo>()
                .Include(x => x.Places)
                .FirstOrDefaultAsync(p => p.PostCode == postCode);
        }
    }
}
