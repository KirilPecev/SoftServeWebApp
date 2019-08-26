namespace WebApp.Data.Repo.SportRepo
{
    using Domain;
    using GenericRepository;
    using System.Collections.Generic;

    public class SportRepo : Repository<Sport>, ISportRepository
    {
        public SportRepo(WebAppDbContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<Sport> GetSports()
        {
            return this.dbSet;
        }
    }
}
