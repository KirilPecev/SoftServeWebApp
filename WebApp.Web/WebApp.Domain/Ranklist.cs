namespace WebApp.Domain
{
    public class RankList
    {
        public string UserId { get; set; }
        public virtual WebAppUser User { get; set; }

        public int SportId { get; set; }
        public virtual Sport Sport { get; set; }
    }
}
