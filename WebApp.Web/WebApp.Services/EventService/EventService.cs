namespace WebApp.Services.EventService
{
    using Data.Repo;
    using Domain;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class EventService : IEventService
    {
        private IEventRepository _eventRepository;

        public EventService(IEventRepository eventRepository)
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
            if(foundEvent == null)
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
                .Where(x => x.AdminId == id)
                .ToList();

            return allEventsByUser;
        }
    }
}
