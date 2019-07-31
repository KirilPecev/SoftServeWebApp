using System.Threading.Tasks;

namespace WebApp.Data.Repo
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WebAppDbContext _dbContext;

        public UnitOfWork(WebAppDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<int> SaveChanges()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
