using System;
using System.Collections.Generic;
using System.Text;
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
    }
}
