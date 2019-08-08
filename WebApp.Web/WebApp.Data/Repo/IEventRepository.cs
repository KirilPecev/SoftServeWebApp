using System;
using System.Collections.Generic;
using System.Text;
using WebApp.Domain;

namespace WebApp.Data.Repo
{
    public interface IEventRepository
    {
        void CreateEvent(Event createEvent);
        void SaveEvent();
    }
}
