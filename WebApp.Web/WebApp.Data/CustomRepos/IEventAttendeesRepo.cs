using System.Collections.Generic;
using WebApp.Data.Repo;
using WebApp.Domain;

namespace WebApp.Data.CustomRepos
{
    public interface IEventAttendeesRepo : IRepository<EventAttendees>
    {
        IEnumerable<EventAttendees> GetAllByUserId(string id);
        IEnumerable<EventAttendees> GetAll();
        void RemoveUser(string userId, int eventId, int positionId);
    }
}
