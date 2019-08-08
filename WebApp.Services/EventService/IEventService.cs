using WebApp.Domain;

namespace WebApp.Services.EventService
{
    public interface IEventService
    {
        void CreateEvent(Event createEvent);

        void SaveEvent();
    }
}
