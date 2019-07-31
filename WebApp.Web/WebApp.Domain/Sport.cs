namespace WebApp.Domain
{
    using System.Collections.Generic;

    public class Sport : BaseModel<int>
    {
        public Sport()
        {
            this.Positions = new HashSet<Position>();
            this.RankLists = new HashSet<RankList>();
        }

        public string Name { get; set; }

        public virtual ICollection<Position> Positions { get; set; }

        public virtual ICollection<RankList> RankLists { get; set; }
    }
}
