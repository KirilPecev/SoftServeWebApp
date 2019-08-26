namespace WebApp.Services.SportService
{
    using Data.Repo.SportRepo;
    using Domain;
    using System.Collections.Generic;

    public class SportService : ISportService
    {
        private readonly ISportRepository sportRepository;

        public SportService(ISportRepository repository)
        {
            this.sportRepository = repository;
        }
        public IEnumerable<Sport> GetSports()
        {
            return sportRepository.GetSports();
        }
    }
}
