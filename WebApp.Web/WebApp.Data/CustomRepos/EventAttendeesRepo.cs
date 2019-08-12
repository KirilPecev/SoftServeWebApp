using System.Collections.Generic;
using System.Linq;
using WebApp.Data.Repo;
using WebApp.Domain;

namespace WebApp.Data.CustomRepos
{
    public class EventAttendeesRepo : Repository<EventAttendees>, IEventAttendeesRepo
    {
        public EventAttendeesRepo(WebAppDbContext webAppDbContext)
            : base(webAppDbContext)
        {
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
    }
}
