using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Web.Models.Event
{
    public class HomePageBinding
    {
        public IEnumerable<EventBindingModel> Events { get; set; } /*= new List<EventBindingModel>();*/

        public EventBindingModel Modal { get; set; }
    }
}
