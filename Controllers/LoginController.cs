using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebFastFood.Models;

namespace WebFastFood.Controllers
{
    public class LoginController : Controller
    {
        WebFastFoodEntities database = new WebFastFoodEntities();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User user, string returnurl)
        {
            if (ModelState.IsValid)
            {
                UserStatus status = GetUserValidity(user);
                bool IsAdmin = false;
                //var returnurl = RedirectToAction("Index", "Home");
                string returnURL = "";
                if (status == UserStatus.AuthenticatedAdmin)
                {
                    IsAdmin = true;
                }
                else if (status == UserStatus.AuthenticatedUser)
                {
                    IsAdmin = false;
                }
                else
                {
                    ModelState.AddModelError("Pass", "Tài khoản hoặc mật khẩu không chính xác! Vui lòng kiểm tra lại");
                    return View("Index");
                }
                FormsAuthentication.SetAuthCookie(user.Account, false);
                Session["NameUser"] = user.NameUser;
                Session["Account"] = user.Account;
                Session["IsAdmin"] = IsAdmin;
                if (IsAdmin == true)
                {
                    if (returnurl == null)
                    {
                        returnurl = "/Profile/Details";
                    }
                    returnURL = returnurl;
                }
                else if (IsAdmin == false)
                {                   
                    returnURL = "/Profile/Details";
                }
                return Redirect(returnURL);
            }
            else
            {
                return View("Index");
            }
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
        public UserStatus GetUserValidity(User user)
        {
            var status = UserStatus.NonAuthenticatedUser;
            foreach (var item in database.Users.Where(x => x.Account == user.Account && x.Pass == user.Pass))
            {
                if (database.Roles.Where(x => x.IDRole == item.IDRole).FirstOrDefault().IsAdmin == "true")
                {
                    status = UserStatus.AuthenticatedAdmin;
                }
                else if (database.Roles.Where(x => x.IDRole == item.IDRole).FirstOrDefault().IsAdmin == "false")
                {
                    status = UserStatus.AuthenticatedUser;
                }
                else
                    status = UserStatus.NonAuthenticatedUser;
            }
            return status;
        }
    }
}