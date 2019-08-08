using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Domain;

namespace WebApp.Web.Repositories
{
    interface IEventRepository: IDisposable
    {
        void CreateEvent(Event createEvent);
    }
}
