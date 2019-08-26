namespace WebApp.Web.Controllers.Mappers
{
    using Domain;
    using Microsoft.AspNetCore.Http;
    using Models.Event;

    public interface IEventMapper
    {
        Event NewEvent(EventBindingModel model, IFormFile eventImage, string adminId);

        Event ModifiedEvent(EventBindingModel model, IFormFile eventImage, string adminId);

        EventBindingModel ViewEvent(Event dbEvent);
    }
}
