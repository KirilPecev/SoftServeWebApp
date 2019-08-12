namespace WebApp.Services.EventService
{
    using Data.Repo;
    using Domain;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class EventService : BaseService, IEventService
    {
        private IEventRepository _eventRepository;

        public EventService(IEventRepository eventRepository, IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            this._eventRepository = eventRepository;
        }

        public void CreateEvent(Event createEvent)
        {
            this._eventRepository.CreateEvent(createEvent);
        }

        public IEnumerable<Event> GetAllEvents()
        {
            return this._eventRepository.GetAllEvents();
        }

        public Event GetEvent(int id)
        {
            Event foundEvent = this._eventRepository.GetEvent(id);
            if (foundEvent == null)
            {
                throw new ArgumentException($"No Event with ID: {id} exists!");
            }
            else
            {
                return foundEvent;
            }
        }

        //TODO: Make it asyc
        public IEnumerable<Event> GetAllEventsByUser(string id)
        {
           var allEventsByUser = this._eventRepository.GetAllEvents()
                .Where(x => x.AdminId == id && x.IsDeleted == false)
                .ToList();
                
            return allEventsByUser;
        }

        public async Task DeleteEvent(int id)
        {
            var eventToBeDeleted = this.GetEvent(id);

            eventToBeDeleted.IsDeleted = true;

            await SaveChangesAsync();
        }

        public void EditEvent(Event editEvent)
        {
            var newEvent = this.GetEvent(editEvent.Id);

            newEvent.Name = editEvent.Name;
            newEvent.Description = editEvent.Description;
            newEvent.Location = editEvent.Location;
            newEvent.SportId = editEvent.SportId;
            newEvent.Time = editEvent.Time;
            if (editEvent.Image != null)
            {
                newEvent.Image = editEvent.Image;
            }

            SaveChangesAsync().Wait();
        }
    }
}
