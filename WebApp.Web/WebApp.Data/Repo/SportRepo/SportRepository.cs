namespace WebApp.Data.Repo.SportRepo
{
    using Domain;
    using GenericRepository;
    using System.Collections.Generic;

    public class SportRepository : Repository<Sport>, ISportRepository
    {
        public SportRepository(WebAppDbContext dbContext) : base(dbContext) { }

        public IEnumerable<Sport> GetSports()
        {
            return this.dbSet;
        }
    }
}
