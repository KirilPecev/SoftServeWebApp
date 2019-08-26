namespace WebApp.Data.Repo.EventRepo
{
    using Domain;
    using GenericRepository;
    using System.Collections.Generic;

    public interface IEventRepository : IRepository<Event>
    {
        void CreateEvent(Event createEvent);

        IEnumerable<Event> GetAllEvents();

        Event GetEvent(int id);
    }
}
