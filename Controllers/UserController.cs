using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebFastFood.Models;

namespace WebFastFood.Controllers
{
    [Authorize(Roles = "true")]
    public class UserController : Controller
    {
        WebFastFoodEntities database = new WebFastFoodEntities();
        // GET: User
        public static List<User> SelectAllArticle()
        {
            var rtn = new List<User>();
            using (var context = new WebFastFoodEntities())
            {
                foreach (var item in context.Users)
                {
                    rtn.Add(new User
                    {
                        ID = item.ID,
                        Account = item.Account,
                        Pass = item.Pass,
                        IDRole = item.Role.NameRole,
                        NameUser = item.NameUser,
                        Sex = item.Sex,
                        ImageUser = item.ImageUser,
                        Phone = item.Phone,
                        Email = item.Email,
                        DateOfBirth = item.DateOfBirth,
                        Address = item.Address,

                    });
                }
            }
            return rtn;
        }
        public static List<Role> SelectAllArticle1()
        {
            var rtn = new List<Role>();
            using (var context = new WebFastFoodEntities())
            {
                foreach (var item in context.Roles)
                {
                    rtn.Add(new Role
                    {
                        ID = item.ID,
                        IDRole = item.IDRole,
                        NameRole = item.NameRole
                    });
                }
            }
            return rtn;
        }


        public ActionResult Index()
        {
            var lsNV = SelectAllArticle().ToList();
            return View(lsNV);

        }

        public ActionResult Create()
        {
            List<Role> list = SelectAllArticle1().ToList();
            ViewBag.listRole = new SelectList(list, "IDRole", "NameRole", "");
            User nv = new User();
            return View(nv);
        }
        [HttpPost]
        public ActionResult Create(User nv)
        {
            List<Role> list = SelectAllArticle1().ToList();
            try
            {
                if (nv.UploadImage != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(nv.UploadImage.FileName);
                    string extent = Path.GetExtension(nv.UploadImage.FileName);
                    filename = filename + extent;
                    nv.ImageUser = "~/Content/images/" + filename;
                    nv.UploadImage.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), filename));
                }
                ViewBag.listRole = new SelectList(list, "IDRole", "NameRole", 1);
                database.Users.Add(nv);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int Id)
        {
            List<Role> list = SelectAllArticle1().ToList();
            ViewBag.listRole = new SelectList(list, "IDRole", "NameRole", "");
            return View(database.Users.Where(s => s.ID == Id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Edit(int Id, User nv)
        {
            List<Role> list = SelectAllArticle1().ToList();
            database.Entry(nv).State = System.Data.Entity.EntityState.Modified;
            try
            {
                if (nv.UploadImage != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(nv.UploadImage.FileName);
                    string extent = Path.GetExtension(nv.UploadImage.FileName);
                    filename = filename + extent;
                    nv.ImageUser = "~/Content/images/" + filename;
                    nv.UploadImage.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), filename));
                }
                ViewBag.listRole = new SelectList(list, "IDRole", "NameRole", 1);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Delete(int Id)
        {
            return View(database.Users.Where(s => s.ID == Id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Delete(int Id, User nv)
        {
            try
            {
                nv = database.Users.Where(s => s.ID == Id).FirstOrDefault();
                database.Users.Remove(nv);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("This data is using in other table, Error Delete Employee");
            }
        }


        public ActionResult Details(int Id)
        {
            return View(database.Users.Where(s => s.ID == Id).FirstOrDefault());
        }
    }
}