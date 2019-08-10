using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Web.Models.Event
{
    public class PositionModel
    {
        public PositionModel()
        {
            Aprooved = new PlayerModel();
            ToBeAprooved = new List<PlayerModel>();
        }
        public int Id { get; set; }
        public int Team { get; set; }
        public string Name { get; set; }
        public PlayerModel Aprooved { get; set; }
        public List<PlayerModel> ToBeAprooved { get; set; }
    }
}
