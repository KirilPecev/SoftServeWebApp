using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Web.Models.PlayerPosition
{
    public class EventDescriptionBindingModel
    {
        public IEnumerable<PositionBindingModel> Models { get; set; } = new List<PositionBindingModel>();

        public PositionBindingModel Position { get; set; }
    }
}
