using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using WebApp.Domain;
using WebApp.Services.EventService;
using WebApp.Web.Models.Event;

namespace WebApp.Web.Controllers
{
    public class HomePageController : Controller
    {
        private IEventService _eventService;

        public HomePageController(IEventService eventService)
        {
            _eventService = eventService;
        }

        public IActionResult CreateEvent()
        {
            // request to the db
            //return ReturnMainView();
            return View(new Event());
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult CreateEvent(EventBindingModel model)
        {
            try
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
                    Name = model.Name = HttpContext.Request.Form["Name"].ToString()
                };


                //model.Location = HttpContext.Request.Form["eventLocation"].ToString();
                //model.Options = HttpContext.Request.Form["exampleFormControlTextarea"].ToString();

                _eventService.CreateEvent(addedEvent);
                _eventService.SaveEvent();

                return ReturnMainView();

                //GetEvent(eventModel);
            }

            catch (Exception e)
            {

            }
            /*return this.Json(model);*/
            return ReturnMainView();
        }

        private ViewResult ReturnMainView()
        {
            HomePageBinding model = new HomePageBinding();
            return View(model);
        }

        [HttpGet]
        public JsonResult GetEvent(object model)
        {
            return Json(model);
        }
    }
}