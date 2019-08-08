using System;
using WebApp.Domain;

namespace WebApp.Data.Repo
{
    public interface IEventRepo : IDisposable
    {
        void CreateEvent(Event createEvent);
        void SaveEvent();
    }
}
