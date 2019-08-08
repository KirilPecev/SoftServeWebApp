using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.Domain;
using WebApp.Web.Models.Event;

namespace WebApp.Web.Controllers
{
    public class HomePageController : Controller
    {
        private readonly WebAppDbContext _context;

        public IActionResult HomePageView()
        {
            // request to the db
            return ReturnMainView();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> HomePageView(EventBindingModel model)
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
                // save intothe db
                // 

                Event addedEvent = new Event
                {
                    Name = model.Name = HttpContext.Request.Form["Name"].ToString()
                };


                //model.Location = HttpContext.Request.Form["eventLocation"].ToString();
                //model.Options = HttpContext.Request.Form["exampleFormControlTextarea"].ToString();

                _context.Add(addedEvent);
                await _context.SaveChangesAsync();

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

        //[HttpPost]
        //public IActionResult JoinEvent(int id)
        //{

        //}
    }
}