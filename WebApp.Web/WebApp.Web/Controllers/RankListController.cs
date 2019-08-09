using Microsoft.AspNetCore.Mvc;

namespace WebApp.Web.Controllers
{
    public class RankListController : Controller
    {
        public IActionResult CurrentRankList()
        {
            return View();
        }
    }
}