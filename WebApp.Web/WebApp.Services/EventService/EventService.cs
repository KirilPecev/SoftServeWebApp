namespace WebApp.Services.EventService
{
    using System;
    using System.Collections.Generic;
    using Data.Repo;
    using Domain;

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
            throw new NotImplementedException();
        }

        public void SaveEvent()
        {
            this._eventRepository.SaveEvent();
        }
    }
}
