using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using WebFastFood.Models;
using Rotativa;
using Microsoft.Ajax.Utilities;
using PagedList;
using PagedList.Mvc;

namespace WebFastFood.Controllers
{
    [Authorize(Roles = "true, false")]
    public class BillManagementController : Controller
    {
        WebFastFoodEntities database = new WebFastFoodEntities();
        // GET: User
        public static List<Bill> SelectAllArticle()
        {
            var rtn = new List<Bill>();
            using (var context = new WebFastFoodEntities())
            {
                foreach (var item in context.Bills)
                {
                    rtn.Add(new Bill
                    {
                        ID = item.ID,
                        DateOrder = item.DateOrder,
                        IDDetailBill = item.IDDetailBill,
                        IDUser = item.User.Account,
                        Status = item.Status,
                        IntoMoney = item.IntoMoney
                    }); ;
                }
            }
            return rtn;
        }
        public static List<TableNumber> SelectAllArticle1()
        {
            var rtn = new List<TableNumber>();
            using (var context = new WebFastFoodEntities())
            {
                foreach (var item in context.TableNumbers)
                {
                    rtn.Add(new TableNumber
                    {
                        ID = item.ID,
                        IDTableNumber = item.IDTableNumber,
                        Number = item.Number
                    });
                }
            }
            return rtn;
        }
        public static List<User> SelectAllArticle2()
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
                        NameUser = item.NameUser
                    });
                }
            }
            return rtn;
        }
        public static List<Coupon> SelectAllArticle3()
        {
            var rtn = new List<Coupon>();
            using (var context = new WebFastFoodEntities())
            {
                foreach (var item in context.Coupons)
                {
                    rtn.Add(new Coupon
                    {
                        ID = item.ID,
                        IDCoupon = item.IDCoupon,
                        NameCoupon = item.NameCoupon,
                        DateEnd = item.DateEnd,
                        Amount = item.Amount,
                        discount = item.discount,
                        ImageCoupon = item.ImageCoupon,
                        Status = item.Status,
                        Detail = item.Detail
                    });
                }
            }
            return rtn;
        }
        public ActionResult DSBill(Bill bill,string searchString, DateTime? date, DateTime? date1, int? Id, int? page)
        {
            int pageSize = 10;
            int pageNum = (page ?? 1);
            if (searchString == null && date == null)
            {
                var list = SelectAllArticle().OrderByDescending(x => x.ID).ToList();
                Session["DSBill"] = bill.ID;
                return View(list.ToPagedList(pageNum, pageSize));
            }
            else if (searchString == null || date != null || date1 != null)
            {
                //var list = SelectAllArticle().Where(s => s.DateOrder.Value.Equals(date.Value));
                List<Bill> list = new List<Bill>();
                using (var context = new WebFastFoodEntities())
                {
                    list = context.Bills.SqlQuery("SELECT * FROM Bill where DateOrder BETWEEN '" + date.Value.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture) + "' and '" + date1.Value.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture) + "'").ToList();
                }
                Session["date"] = date.Value.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                Session["date1"] = date1.Value.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                return View(list.ToPagedList(pageNum, pageSize));
            }
            else
            {
                var list = SelectAllArticle().Where(s => s.IDUser.ToString().Contains(searchString) || s.DateOrder.Value.Month.ToString().Contains(searchString)
                || s.DateOrder.Value.ToString().Contains(searchString) || s.DateOrder.Value.Year.ToString().Contains(searchString) || s.DateOrder.Value.Day.ToString().Contains(searchString));
                return View(list.ToPagedList(pageNum, pageSize));
            }
        }

        [Authorize(Roles = "true")]
        public ActionResult Index(string searchString, DateTime? date, DateTime? date1, int? Id)
        {
            if(searchString == null && date == null)
            {
                var list = SelectAllArticle().OrderByDescending(x => x.ID).ToList();
                return View(list);
            }    
            else if (searchString == null || date != null || date1 != null)
            {
                //var list = SelectAllArticle().Where(s => s.DateOrder.Value.Equals(date.Value));
                List<Bill> list = new List<Bill>();
                using (var context = new WebFastFoodEntities())
                {
                    list = context.Bills.SqlQuery("SELECT * FROM Bill where DateOrder BETWEEN '" + date.Value.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture) + "' and '" + date1.Value.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture) + "'").ToList();
                }
                Session["date"] = date.Value.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                Session["date1"] = date1.Value.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                return View(list);
            }
            else
            {
                var list = SelectAllArticle().Where(s => s.IDUser.ToString().Contains(searchString) || s.DateOrder.Value.Month.ToString().Contains(searchString)
                || s.DateOrder.Value.ToString().Contains(searchString) || s.DateOrder.Value.Year.ToString().Contains(searchString) || s.DateOrder.Value.Day.ToString().Contains(searchString));
                return View(list);
            }
        }

        [HttpPost]
        public ActionResult Tranfer()
        {
            using (var context = new WebFastFoodEntities())
            {
                context.Database.ExecuteSqlCommand("INSERT INTO Revenue (RevenueDateStart, RevenueDateEnd, RevenueMoney) SELECT '" + Session["date"] + "', '"+ Session["date1"]+"', SUM(IntoMoney) FROM Bill WHERE DateOrder BETWEEN '"+ Session["date"] + "' and '" + Session["date1"] + "'");
                context.SaveChanges();
            }
            return View();
        }
        public ActionResult EditBill(int Id)
        {
            return View(database.Bills.Where(s => s.ID == Id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult EditBill(int Id, Bill bill)
        {
            database.Entry(bill).State = System.Data.Entity.EntityState.Modified;
            bill.IDUser = Session["Account"].ToString();
            database.SaveChanges();
            return RedirectToAction("DSBill");
        }


        public ActionResult Details(int Id)
        {
            var Details = database.DetailBills.Where(s => s.ID == Id).FirstOrDefault();
            return View(Details);
        }
        public ActionResult DetailsNV(int Id)
        {
            var Details = database.DetailBills.Where(s => s.ID == Id).FirstOrDefault();
            return View(Details);
        }

        public ActionResult Print(int Id)
        {
            var Bill = new Rotativa.ActionAsPdf("Details", new { Id = Id });
            return Bill;
        }
        //public ActionResult PrintHD(int Id)
        //{
        //    var Bill = new Rotativa.ActionAsPdf("Index", new { Id = Id });
        //    return Bill;
        //}

    }
}