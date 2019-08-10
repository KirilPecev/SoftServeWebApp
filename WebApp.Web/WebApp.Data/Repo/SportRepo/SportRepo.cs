using System;
using System.Collections.Generic;
using WebApp.Domain;

namespace WebApp.Data.Repo
{
    public class SportRepo : Repository<Sport>, ISportRepository
    {
        public SportRepo(WebAppDbContext dbContext):base(dbContext)
        {
        }
        public IEnumerable<Sport> GetSports()
        {
            return this.dbSet;
        }
    }
}
