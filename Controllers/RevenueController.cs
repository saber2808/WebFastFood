using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebFastFood.Models;

namespace WebFastFood.Controllers
{
    [Authorize(Roles = "true")]
    public class RevenueController : Controller
    {
        WebFastFoodEntities database = new WebFastFoodEntities();
        // GET: User
        public static List<Revenue> SelectAllArticle()
        {
            var rtn = new List<Revenue>();
            using (var context = new WebFastFoodEntities())
            {
                foreach (var item in context.Revenues)
                {
                    rtn.Add(new Revenue
                    {
                        ID = item.ID,
                        RevenueDateStart = item.RevenueDateStart,
                        RevenueDateEnd = item.RevenueDateEnd,
                        RevenueMoney = item.RevenueMoney,
                    });
                }
            }
            return rtn;
        }
        // GET: Revenue
        public ActionResult Index()
        {
            var lsRe = SelectAllArticle().ToList();
            return View(lsRe);
        }
    }
}