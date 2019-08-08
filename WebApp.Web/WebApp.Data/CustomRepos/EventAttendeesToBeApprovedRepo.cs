using System;
using System.Collections.Generic;
using System.Text;
using WebApp.Domain;
using WebApp.Data.CustomRepos;
using WebApp.Data.Repo;

namespace WebApp.Data.CustomRepos
{
    public class EventAttendeesToBeApprovedRepo : Repository<EventAttendeesToBeApproved>, IEventAttendeesToBeApprovedRepo
    {
        public EventAttendeesToBeApprovedRepo(WebAppDbContext context)
        : base(context)
        {
        }

    }
}
