namespace WebApp.Data.Repo.EventAttendeesToBeApprovedRepo
{
    using Domain;
    using GenericRepository;
    using System.Collections.Generic;

    public interface IEventAttendeesToBeApprovedRepository : IRepository<EventAttendeesToBeApproved>
    {
        IEnumerable<EventAttendeesToBeApproved> GetAllByUserId(string id);

        IEnumerable<EventAttendeesToBeApproved> GetAll();

        void RemoveUser(string userId, int eventId, int positionId);

        void ClearUsers(string userId, int eventId);
    }
}

