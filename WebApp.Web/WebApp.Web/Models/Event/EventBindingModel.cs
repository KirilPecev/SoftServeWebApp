using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Web.Models.Event
{
    public class EventBindingModel
    {
        public string Title { get; set; }
        public string ImageName { get; set; }
        public string AdminId { get; set; }
        public DateTime Time { get; set; }
        public int SportId { get; set; }
        public Dictionary<string,string> Positions { get; set; }
        public Dictionary<string, string> ToBeAprooved { get; set; }
    }
}
