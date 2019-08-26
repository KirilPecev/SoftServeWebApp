namespace WebApp.Data.Repo.UnitOfWork
{
    using System.Threading.Tasks;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly WebAppDbContext dbContext;

        public UnitOfWork(WebAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<int> SaveChanges()
        {
            return await dbContext.SaveChangesAsync();
        }
    }
}
