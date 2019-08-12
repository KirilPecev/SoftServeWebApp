using System.Collections.Generic;
using System.Linq;
using WebApp.Data.Repo;
using WebApp.Domain;

namespace WebApp.Services.ScoreService
{
    public class ScoreService : IScoreService
    {
        private IScoreRepo _scores;

        public ScoreService(IScoreRepo scores)
        {
            _scores = scores;
        }

        public List<Rating> GetAllData()
        {
            return _scores.GetAllData().ToList();
        }
    }
}
