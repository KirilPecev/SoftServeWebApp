using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.Domain;
using WebApp.Services.EventService;
using WebApp.Web.Models.Event;

namespace WebApp.Web.Controllers
{
    public class HomePageController : Controller
    {
        private readonly IEventService _eventService;
      
        public HomePageController(IEventService eventService)
        {
            this._eventService = eventService;
        }

        public IActionResult HomePageView()
        {
            // request to the db
            return ReturnMainView();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult HomePageView(EventBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = string.Empty;
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    errorMessages += error;
                }
                return BadRequest(errorMessages);
            }

            Event addedEvent = new Event
            {
                Name = model.Name = HttpContext.Request.Form["Name"].ToString(),
                Time = model.CurrentTime = DateTime.Now.Date
                //BlobManager = model.BlobManager
                //TODO Get current userID and bind to addedEvent
            };
            //model.Options = HttpContext.Request.Form["exampleFormControlTextarea"].ToString();
            //TODO Hard coded sportId, implement in HomePageView Dropdown selector
            addedEvent.SportId = 1;
            _eventService.CreateEvent(addedEvent);

            //GetEvent(eventModel);
            return ReturnMainView();
        }

        private ViewResult ReturnMainView()
        {
            HomePageBinding model = new HomePageBinding();
            var allEvents = _eventService.GetAllEvents().Select(e => new EventBindingModel()
            {
                Name = e.Name,
            });
            model.Events = allEvents;

            return View(model);
        }

        //TODO Get the created event end return it to _EventDescriptionPartial
        [HttpGet]
        public JsonResult GetEvent(object model)
        {
            return Json(model);
        }
    }
}