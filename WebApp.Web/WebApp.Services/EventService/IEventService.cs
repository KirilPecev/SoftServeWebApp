namespace WebApp.Services.EventService
{
    using System.Collections.Generic;
    using Domain;

    public interface IEventService
    {
        void CreateEvent(Event createEvent);

        IEnumerable<Event> GetAllEvents();

        Event GetEvent(int id);
    }
}
