namespace WebApp.Web.Models.Event
{
    using System.Collections.Generic;

    public class PositionModel
    {
        public PositionModel()
        {
            Approved = new PlayerModel();
            ToBeApproved = new List<PlayerModel>();
        }

        public int EventId { get; set; }

        public int Id { get; set; }

        public int Team { get; set; }

        public string Name { get; set; }

        public PlayerModel Approved { get; set; }

        public List<PlayerModel> ToBeApproved { get; set; }
    }
}
