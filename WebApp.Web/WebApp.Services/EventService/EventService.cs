using System;
using System.Collections.Generic;
using System.Text;
using WebApp.Data.Repo;
using WebApp.Domain;

namespace WebApp.Services.EventService
{
    public class EventService: IEventService
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

        public void SaveEvent()
        {
            this._eventRepository.SaveEvent();
        }
    }
}
