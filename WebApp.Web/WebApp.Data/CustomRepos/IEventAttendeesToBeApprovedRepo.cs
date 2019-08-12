using System.Collections.Generic;
using WebApp.Data.Repo;
using WebApp.Domain;

namespace WebApp.Data.CustomRepos
{
    public interface IEventAttendeesToBeApprovedRepo : IRepository<EventAttendeesToBeApproved>
    {
        IEnumerable<EventAttendeesToBeApproved> GetAllByUserId(string id);
        IEnumerable<EventAttendeesToBeApproved> GetAll();
        void RemoveUser(string userId, int eventId, int positionId);
        void ClearUsers(string userId, int eventId);
    }
}

