using System;
using System.Collections.Generic;
using System.Text;
using WebApp.Domain;

namespace WebApp.Data.Repo
{
    public class PositionRepo : Repository<Position>, IPositionRepo
    {
        public PositionRepo(WebAppDbContext dbContext):base(dbContext)
        {
        }
        public IEnumerable<Position> GetPositions()
        {
            return dbSet;
        }
    }
}
