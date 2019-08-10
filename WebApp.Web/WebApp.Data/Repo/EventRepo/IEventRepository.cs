namespace WebApp.Data.Repo
{
    using Domain;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IEventRepository
    {
        void CreateEvent(Event createEvent); 

        IEnumerable<Event> GetAllEvents();

        Event GetEvent(int id);
    }
}
