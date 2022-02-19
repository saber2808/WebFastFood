using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebFastFood.Models;

namespace WebFastFood.Controllers
{
    public class ProfileController : Controller
    {
        WebFastFoodEntities database = new WebFastFoodEntities();
        // GET: Profile
        public ActionResult Index(User us)
        {
            Session["ID"] = us.ID;
            Session["Account"] = us.Account;
            Session["Pass"] = us.Pass;
            Session["Name"] = us.NameUser;
            Session["ImageCustomer"] = us.ImageUser;
            Session["DateOfBirth"] = us.DateOfBirth;
            Session["Phone"] = us.Phone;
            Session["Email"] = us.Email;
            Session["Address"] = us.Address;
            Session["Sex"] = us.Sex;
            Session["Role"] = us.IDRole;
            return RedirectToAction("DSFood", "Food");
        }
        public ActionResult Details(string id)
        {
            id = (string)Session["Account"];
            return View(database.Users.Where(s => s.Account == id).FirstOrDefault());
        }
        public ActionResult Edit(int id)
        { 
            return View(database.Users.Where(s => s.ID == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Edit(User us, int id)
        {
            database.Entry(us).State = System.Data.Entity.EntityState.Modified;
            us.IDRole = database.Roles.Where(s => s.NameRole == us.IDRole).FirstOrDefault().IDRole;
            try
            {
                if (us.UploadImage != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(us.UploadImage.FileName);
                    string extent = Path.GetExtension(us.UploadImage.FileName);
                    filename = filename + extent;
                    us.ImageUser = "~/Content/images/" + filename;
                    us.UploadImage.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), filename));
                }
                database.SaveChanges();
                return RedirectToAction("Details");
            }
            catch
            {
                return View();
            }
        }
    }
}