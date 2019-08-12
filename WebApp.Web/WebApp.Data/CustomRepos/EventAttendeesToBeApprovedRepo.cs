using System.Collections.Generic;
using System.Linq;
using WebApp.Data.Repo;
using WebApp.Domain;

namespace WebApp.Data.CustomRepos
{
    public class EventAttendeesToBeApprovedRepo : Repository<EventAttendeesToBeApproved>, IEventAttendeesToBeApprovedRepo
    {
        private WebAppDbContext dbContext;
        public EventAttendeesToBeApprovedRepo(WebAppDbContext context)
        : base(context)
        {
            this.dbContext = context;
        }
        public IEnumerable<EventAttendeesToBeApproved> GetAllByUserId(string id)
        {
            var eventAttendeesToBeApproved = dbSet
                 .Where(x => x.UserId == id)
                 .ToList();

            return eventAttendeesToBeApproved;
        }
        public IEnumerable<EventAttendeesToBeApproved> GetAll()
        {
            return this.dbSet;
        }

        public void RemoveUser(string userId, int eventId, int positionId)
        {
            var remove = this.dbSet.First(u => u.UserId == userId &&
            u.PositionId == positionId &&
            u.EventId == eventId
            );
            this.dbSet.Remove(remove);
            dbContext.SaveChanges();
        }
        public void ClearUsers(string userId, int eventId)
        {
            var remove = this.dbSet.Where(u => u.UserId == userId &&
            u.EventId == eventId
            );
            foreach (var toRemove in remove)
            {
                this.dbSet.Remove(toRemove);
            }
            dbContext.SaveChanges();
        }
    }
}
