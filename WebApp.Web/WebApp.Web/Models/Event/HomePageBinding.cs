using System.Collections.Generic;

namespace WebApp.Web.Models.Event
{
    public class HomePageBinding
    {
        public List<EventBindingModel> Events { get; set; }

        public EventBindingModel Modal { get; set; }

        public HomePageBinding()
        {
            this.Modal = new EventBindingModel();
            this.Events = new List<EventBindingModel>();
        }
    }
}
