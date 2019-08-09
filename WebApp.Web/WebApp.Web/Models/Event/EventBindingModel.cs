using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Web.Models.Event
{
    public class EventBindingModel
    {
        public string Name { get; set; }
        public string Options { get; set; }
        public string Type { get; set; }
        public DateTime CurrentTime { get; set; } 
    }
}
