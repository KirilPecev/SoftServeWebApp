namespace WebApp.Data.Repo.PositionRepo
{
    using Domain;
    using GenericRepository;
    using System.Collections.Generic;

    public class PositionRepository : Repository<Position>, IPositionRepository
    {
        public PositionRepository(WebAppDbContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<Position> GetPositions()
        {
            return dbSet;
        }
    }
}
