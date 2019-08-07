using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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