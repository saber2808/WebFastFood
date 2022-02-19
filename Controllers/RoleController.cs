using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebFastFood.Models;

namespace WebFastFood.Controllers
{
    [Authorize(Roles = "true")]
    public class RoleController : Controller
    {
        // GET: Role
        
        WebFastFoodEntities database = new WebFastFoodEntities();
        public List<Role> listRole { get; set; }
        public ActionResult Index()
        {
            return View(database.Roles.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Role role)
        {
            try
            {
                database.Roles.Add(role);
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
            return View(database.Roles.Where(s => s.ID == Id).FirstOrDefault());
        }
        public ActionResult Edit(int Id)
        {
            return View(database.Roles.Where(s => s.ID == Id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Edit(int Id, Role role)
        {
            database.Entry(role).State = System.Data.Entity.EntityState.Modified;
            database.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int Id)
        {
            return View(database.Roles.Where(s => s.ID == Id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Delete(int Id, Role role)
        {
            try
            {
                role = database.Roles.Where(s => s.ID == Id).FirstOrDefault();
                database.Roles.Remove(role);
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