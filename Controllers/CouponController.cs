using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebFastFood.Models;

namespace WebFastFood.Controllers
{
    [Authorize(Roles = "true, false")]
    public class CouponController : Controller
    {
        // GET: Coupon
        WebFastFoodEntities database = new WebFastFoodEntities();
        public static List<Coupon> SelectAllArticle()
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
                        discount = item.discount,
                        Amount = item.Amount,
                        Status = item.Status,
                        ImageCoupon = item.ImageCoupon,
                        DateEnd = item.DateEnd,
                        Detail = item.Detail
                    });
                }
            }
            return rtn;
        }


        public ActionResult Index()
        {
            var lsCoupon = SelectAllArticle().ToList();
            return View(lsCoupon);

        }
        [Authorize(Roles = "true")]
        public ActionResult Create()
        {
            Coupon cp = new Coupon();
            return View(cp);
        }
        [HttpPost]
        public ActionResult Create(Coupon cp)
        { 
            try
            {
                if (cp.UploadImage != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(cp.UploadImage.FileName);
                    string extent = Path.GetExtension(cp.UploadImage.FileName);
                    filename = filename + extent;
                    cp.ImageCoupon = "~/Content/images/" + filename;
                    cp.UploadImage.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), filename));
                }
                database.Coupons.Add(cp);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Vui lòng kiểm tra ID món ăn");
            }
        }
        [Authorize(Roles = "true")]
        public ActionResult Edit(int Id)
        {
            return View(database.Coupons.Where(s => s.ID == Id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Edit(int Id, Coupon cp)
        {
            database.Entry(cp).State = System.Data.Entity.EntityState.Modified;
            try
            {
                if (cp.UploadImage != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(cp.UploadImage.FileName);
                    string extent = Path.GetExtension(cp.UploadImage.FileName);
                    filename = filename + extent;
                    cp.ImageCoupon = "~/Content/images/" + filename;
                    cp.UploadImage.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), filename));
                }
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        [Authorize(Roles = "true")]
        public ActionResult Delete(int Id)
        {
            return View(database.Coupons.Where(s => s.ID == Id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Delete(int Id, Coupon cp)
        {
            try
            {
                cp = database.Coupons.Where(s => s.ID == Id).FirstOrDefault();
                database.Coupons.Remove(cp);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Xóa nhân viên thất bại!");
            }
        }


        public ActionResult Details(int Id)
        {
            return View(database.Coupons.Where(s => s.ID == Id).FirstOrDefault());
        }

    }
}