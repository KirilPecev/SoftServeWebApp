using System.Collections.Generic;
using System.Linq;
using WebApp.Data.Repo;
using WebApp.Domain;

namespace WebApp.Data.CustomRepos
{
    public class EventAttendeesRepo : Repository<EventAttendees>, IEventAttendeesRepo
    {
        private WebAppDbContext dbContext;
        public EventAttendeesRepo(WebAppDbContext webAppDbContext)
            : base(webAppDbContext)
        {
            this.dbContext = webAppDbContext;
        }


        public IEnumerable<EventAttendees> GetAllByUserId(string id)
        {
            var eventAttendee = dbSet
                .Where(x => x.UserId == id)
                .ToList();

            return eventAttendee;
        }

        public IEnumerable<EventAttendees> GetAll()
        {
            return dbSet;
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
    }
}
