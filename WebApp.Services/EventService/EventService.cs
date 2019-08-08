using System;
using System.Collections.Generic;
using System.Text;
using WebApp.Domain;
using WebApp.Data.Repo;

namespace WebApp.Services.EventService
{
    class EventService : IEventService
    {
        private IEventRepo _eventRepo;

        public EventService()
        {
            this._eventRepo = new EventRepo();
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
