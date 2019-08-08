using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Web.Models.Event
{
    public class EventBindingModel
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string Options { get; set; }
        public Type Type { get; set; }

    }

    public enum Type
    {
        Football = 1,
        Tennis = 2
    }
}
