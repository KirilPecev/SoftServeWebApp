using System;
using System.Collections.Generic;
using System.Text;
using WebApp.Domain;

namespace WebApp.Services.EventService
{
    interface IEventService
    {
        void CreateEvent(Event createEvent);
        void SaveEvent();
    }
}
