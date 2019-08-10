using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Web.Models.Event
{
    public class PlayerModel
    {
        public string Name { get; set; }
        public int Rating { get; set; }
        public int Id { get; set; }
        public string Position { get; set; }
        public int PositionId { get; set; }
        public int Team { get; set; }
    }
}
