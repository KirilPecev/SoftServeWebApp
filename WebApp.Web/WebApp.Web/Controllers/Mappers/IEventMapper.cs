using Microsoft.AspNetCore.Http;
using WebApp.Domain;
using WebApp.Web.Models.Event;

namespace WebApp.Web.Controllers.Mappers
{
    public interface IEventMapper
    {
        Event MapEventToDB(EventBindingModel model, IFormFile eventImage, string adminId);

        Event MapEditEventToDB(EventBindingModel model, IFormFile eventImage, string adminId);

        EventBindingModel MapDbToEvent(Event dbEvent);
    }
}
