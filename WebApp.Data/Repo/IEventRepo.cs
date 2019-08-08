using System;
using System.Collections.Generic;
using System.Text;
using WebApp.Domain;

namespace WebApp.Data.Repo
{
    interface IEventRepo: IDisposable
    {
        void CreateEvent(Event createEvent);
        void SaveEvent();
    }
}
