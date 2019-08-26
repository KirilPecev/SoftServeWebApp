namespace WebApp.Services.ScoreService
{
    using Domain;
    using System.Collections.Generic;

    public interface IScoreService
    {
        IEnumerable<Rating> GetAllData();

        Dictionary<string, int> SortedRankList();
    }
}
