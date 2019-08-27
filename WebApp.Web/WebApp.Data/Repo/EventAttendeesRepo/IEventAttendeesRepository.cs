namespace WebApp.Data.Repo.EventAttendeesRepo
{
    using Domain;
    using GenericRepository;
    using System.Collections.Generic;

    public interface IEventAttendeesRepository : IRepository<EventAttendees>
    {
        IEnumerable<EventAttendees> GetAllByUserId(string id);

        IEnumerable<EventAttendees> GetAll();

        void RemoveUser(string userId, int eventId, int positionId);

        void ClearUsers(string userId, int eventId);

    }
}
