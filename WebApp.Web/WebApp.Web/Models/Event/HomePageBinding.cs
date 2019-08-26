namespace WebApp.Web.Models.Event
{
    using System.Collections.Generic;

    public class HomePageBinding
    {
        public HomePageBinding()
        {
            this.Modal = new EventBindingModel();
            this.Events = new List<EventBindingModel>();
        }

        public List<EventBindingModel> Events { get; set; }

        public EventBindingModel Modal { get; set; }
    }
}
