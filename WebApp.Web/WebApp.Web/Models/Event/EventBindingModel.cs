using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.ImageStorage.AzureBlobStorage;

namespace WebApp.Web.Models.Event
{
    public class EventBindingModel
    {
        public string Title { get; set; }
        public string ImageName { get; set; }
        public string ImageURL { get; set; }
        public string AdminId { get; set; }
        public DateTime Time { get; set; }
        public int SportId { get; set; }

    }
}
