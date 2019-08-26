namespace WebApp.Data.Repo.PositionRepo
{
    using Domain;
    using GenericRepository;
    using System.Collections.Generic;

    public class PositionRepo : Repository<Position>, IPositionRepo
    {
        public PositionRepo(WebAppDbContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<Position> GetPositions()
        {
            return dbSet;
        }
    }
}
