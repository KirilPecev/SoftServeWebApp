using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.ImageStorage.AzureBlobStorage;

namespace WebApp.Web.Models.Event
{
    public class EventBindingModel
    {
        public EventBindingModel()
        {
            Players = new List<PlayerModel>();
            ToBeAprooved = new List<PlayerModel>();
        }
        public string Title { get; set; }
        public string ImageName { get; set; }
        public string ImageURL { get; set; }
        public string AdminId { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public DateTime Time { get; set; }
        public int SportId { get; set; }
        public List<PlayerModel> Players { get; set; }
        public List<PlayerModel> ToBeAprooved { get; set; }
    }
}
