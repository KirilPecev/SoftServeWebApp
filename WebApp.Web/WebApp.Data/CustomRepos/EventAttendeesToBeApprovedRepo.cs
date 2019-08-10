using System.Collections.Generic;
using System.Linq;
using WebApp.Data.Repo;
using WebApp.Domain;

namespace WebApp.Data.CustomRepos
{
    public class EventAttendeesToBeApprovedRepo : Repository<EventAttendeesToBeApproved>, IEventAttendeesToBeApprovedRepo
    {
        public EventAttendeesToBeApprovedRepo(WebAppDbContext context)
        : base(context)
        {
        }

        public IEnumerable<EventAttendeesToBeApproved> GetAllByUserId(string id)
        {
            var eventAttendeesToBeApproved = dbSet
                 .Where(x => x.UserId == id)
                 .ToList();

            return eventAttendeesToBeApproved;
        }
    }
}
