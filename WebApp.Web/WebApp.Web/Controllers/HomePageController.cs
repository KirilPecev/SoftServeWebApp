using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.Domain;
using WebApp.Services.EventService;
using WebApp.Web.Models.Event;
using WebApp.ImageStorage.AzureBlobStorage;
using Microsoft.AspNetCore.Identity;

namespace WebApp.Web.Controllers
{
    public class HomePageController : Controller
    {
        private readonly IEventService _eventService;
        private readonly UserManager<WebAppUser> _userManager;

        public HomePageController(IEventService eventService, UserManager<WebAppUser> userManager)
        {
            this._eventService = eventService;
            this._userManager = userManager;
        }

        public IActionResult HomePageView()
        {

            return ReturnMainView();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult HomePageView(EventBindingModel model, IFormFile eventImage)
        {
            Event newEvent = new Event();
            MapModelToDB(model, eventImage, newEvent);

            this._eventService.CreateEvent(newEvent);
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
        private static string UploadImage(IFormFile eventImage)
        {
            string imageName;
            string connectionString = "DefaultEndpointsProtocol=https;AccountName=sportapp;AccountKey=iObMXhyrN6dbB6tIcWWqJr0Z48rUubSPnYnxefhbF4Ek5UnKCvEgG0/12X/cNjdcZ2/iephB4jUcAch3ve3azA==;EndpointSuffix=core.windows.net";
            BlobManager manager = new BlobManager(connectionString, "images");
            using (var filestream = eventImage.OpenReadStream())
            {
                imageName = manager.UploadImage(eventImage.FileName, filestream);
            }
            return imageName;
        }
        private void MapModelToDB(EventBindingModel model, IFormFile eventImage, Event newEvent)
        {
            newEvent.Name = model.Title;
            newEvent.Time = model.Time;
            newEvent.SportId = model.SportId;
            if(eventImage == null)
            {
                //Set some default image
            }
            else
            {
                newEvent.Image = UploadImage(eventImage);
            }
            newEvent.AdminId = this._userManager.GetUserId(User);
        }
    }
}