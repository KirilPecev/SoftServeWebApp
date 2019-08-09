namespace WebApp.Data.Repo
{
    using Domain;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IEventRepository
    {
        void CreateEvent(Event createEvent); 

        void SaveEvent();

        IEnumerable<Event> GetAllEvents();
    }
}
