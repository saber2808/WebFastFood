using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebFastFood.Models;

namespace WebFastFood.Controllers
{
    [Authorize(Roles = "true, false")]
    public class CategoryFoodController : Controller
    {
        // GET: CategoryFood
        WebFastFoodEntities database = new WebFastFoodEntities();
        [Authorize(Roles = "true")]
        public ActionResult Index()
        {
            return View(database.CategoryFoods.ToList());
        }
        [Authorize(Roles = "true")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CategoryFood cf)
        {
            try
            {
                database.CategoryFoods.Add(cf);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Error Create New");
            }
        }
        [Authorize(Roles = "true")]
        public ActionResult Details(int Id)
        {
            return View(database.CategoryFoods.Where(s => s.ID == Id).FirstOrDefault());
        }
        [Authorize(Roles = "true")]
        public ActionResult Edit(int Id)
        {
            return View(database.CategoryFoods.Where(s => s.ID == Id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Edit(int Id, CategoryFood cf)
        {
            database.Entry(cf).State = System.Data.Entity.EntityState.Modified;
            database.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "true")]
        public ActionResult Delete(int Id)
        {
            return View(database.CategoryFoods.Where(s => s.ID == Id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Delete(int Id, CategoryFood cf)
        {
            try
            {
                cf = database.CategoryFoods.Where(s => s.ID == Id).FirstOrDefault();
                database.CategoryFoods.Remove(cf);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("This data is using in other table, Error Delete Admin Category");
            }
        }
        public PartialViewResult CategoryPartial()
        {
            var listcate = database.CategoryFoods.ToList();
            return PartialView(listcate);
        }
        [Authorize(Roles = "true")]
        public PartialViewResult CategoryPartialAD()
        {
            var listcate = database.CategoryFoods.ToList();
            return PartialView(listcate);
        }
    }
}