using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Web.Models.Event
{
    public class StarModel
    {
        public StarModel(int eventId, PlayerModel player)
        {
            this.EventId = eventId;
            this.player = player;
        }
        public int EventId { get; set; }
        public PlayerModel player { get; set; }
    }
}
