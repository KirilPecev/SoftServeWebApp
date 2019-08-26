namespace WebApp.Web.Models.Event
{
    public class StarModel
    {
        public StarModel(int eventId, PlayerModel player)
        {
            this.EventId = eventId;
            this.Player = player;
        }
        public int EventId { get; set; }

        public PlayerModel Player { get; set; }
    }
}