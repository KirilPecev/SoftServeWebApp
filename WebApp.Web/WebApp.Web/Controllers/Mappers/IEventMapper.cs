using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Domain;
using WebApp.Web.Models.Event;

namespace WebApp.Web.Controllers.Mappers
{
    public interface IEventMapper
    {
        Event MapEventToDB(EventBindingModel model, IFormFile eventImage);
        EventBindingModel MapDbToEvent(Event dbEvent);
    }
}
