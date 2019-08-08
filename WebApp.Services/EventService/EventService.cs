using WebApp.Data.Repo;
using WebApp.Domain;

namespace WebApp.Services.EventService
{
    public class EventService : IEventService
    {
        private IEventRepo _eventRepo;

        public EventService(IEventRepo eventRepo)
        {
            this._eventRepo = eventRepo;
        }

        public void CreateEvent(Event createEvent)
        {
            this._eventRepo.CreateEvent(createEvent);
        }

        public void SaveEvent()
        {
            this._eventRepo.SaveEvent();
        }
    }
}
