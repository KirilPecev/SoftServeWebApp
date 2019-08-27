namespace WebApp.Services.SportService
{
    using Data.Repo.SportRepo;
    using Domain;
    using System.Collections.Generic;

    public class SportService : ISportService
    {
        private readonly ISportRepository sport;

        public SportService(ISportRepository sport)
        {
            this.sport = sport;
        }
        public IEnumerable<Sport> GetSports()
        {
            return sport.GetSports();
        }
    }
}
