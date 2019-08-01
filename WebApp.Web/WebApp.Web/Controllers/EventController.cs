using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Web.Models.Event;

namespace WebApp.Web.Controllers
{
    public class EventController : Controller
    {
        public IActionResult EventView()
        {
            var model = new EventBindingModel();

            return this.View(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult EventView(EventBindingModel model)
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
            return this.Json(model); 
        }
    }
}