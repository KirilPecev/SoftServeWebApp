using System;
using System.Collections.Generic;
using System.Text;
using WebApp.Data.Repo;
using WebApp.Domain;

namespace WebApp.Services.SportService
{
    public class SportService : ISportService
    {
        private ISportRepository sportRepository;
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
