namespace WebApp.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Services.ScoreService;

    public class RankListController : BaseController
    {
        private readonly IScoreService scoreService;

        public RankListController(IScoreService scoreService)
        {
            this.scoreService = scoreService;
        }

        [Authorize]
        public IActionResult CurrentRankList()
        {
            var sortedRankList = scoreService.SortedRankList();

            return View(sortedRankList);
        }
    }
}