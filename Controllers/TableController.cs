using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebFastFood.Models;

namespace WebFastFood.Controllers
{
    [Authorize(Roles = "true, false")]
    public class TableController : Controller
    {
        WebFastFoodEntities database = new WebFastFoodEntities();
        public List<TableNumber> listTable { get; set; }
        public ActionResult Index()
        {
            return View(database.TableNumbers.ToList());
        }
        [Authorize(Roles = "true")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(TableNumber table)
        {
            try
            {
                database.TableNumbers.Add(table);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Error Create New");
            }
        }
        public ActionResult Details(int Id)
        {
            return View(database.TableNumbers.Where(s => s.ID == Id).FirstOrDefault());
        }
        [Authorize(Roles = "true")]
        public ActionResult Edit(int Id)
        {
            return View(database.TableNumbers.Where(s => s.ID == Id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Edit(int Id, TableNumber table)
        {
            database.Entry(table).State = System.Data.Entity.EntityState.Modified;
            database.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "true")]
        public ActionResult Delete(int Id)
        {
            return View(database.TableNumbers.Where(s => s.ID == Id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Delete(int Id, TableNumber table)
        {
            try
            {
                table = database.TableNumbers.Where(s => s.ID == Id).FirstOrDefault();
                database.TableNumbers.Remove(table);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("This data is using in other table, Error Delete Admin Category");
            }
        }
    }
}