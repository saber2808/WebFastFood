using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebFastFood.Models;
using PagedList;
using PagedList.Mvc;
using System.Web.Security;

namespace WebFastFood.Controllers
{
    [Authorize(Roles = "true, false")]
    public class FoodController : Controller
    {
        WebFastFoodEntities database = new WebFastFoodEntities();
        // GET: User
        public static List<Food> SelectAllArticle()
        {
            var rtn = new List<Food>();
            using (var context = new WebFastFoodEntities())
            {
                foreach (var item in context.Foods)
                {
                    rtn.Add(new Food
                    {
                        ID = item.ID,
                        IDFood = item.IDFood,
                        NameFood = item.NameFood,
                        Detail = item.Detail,
                        IDCategoryFood = item.CategoryFood.NameCategoryFood,
                        Amount = item.Amount,
                        Price = item.Price,
                        ImageFood = item.ImageFood,
                    });
                }
            }
            return rtn;
        }
        public static List<CategoryFood> SelectAllArticle1()
        {
            var rtn = new List<CategoryFood>();
            using (var context = new WebFastFoodEntities())
            {
                foreach (var item in context.CategoryFoods)
                {
                    rtn.Add(new CategoryFood
                    {
                        ID = item.ID,
                        IDCategoryFood = item.IDCategoryFood,
                        NameCategoryFood = item.NameCategoryFood
                    });
                }
            }
            return rtn;
        }

        [Authorize(Roles = "true")]
        public ActionResult Index(Food food, string category, int? page)
        {
            int pageSize = 6;
            int pageNum = (page ?? 1);
            if (category == null)
            {
                var lsFood = SelectAllArticle().ToList();
                if (food.Amount > 1)
                {
                    return View(lsFood.ToPagedList(pageNum,pageSize));
                }
                else
                {
                    Session["Amount"] = food.Amount;
                    return View(lsFood.ToPagedList(pageNum, pageSize));
                }
            }
            else
            {
                var lsFood = database.Foods.OrderByDescending(x => x.NameFood).Where(x => x.IDCategoryFood == category);
                if (food.Amount > 1)
                {
                    return View(lsFood.ToPagedList(pageNum, pageSize));
                }
                else
                {
                    Session["Amount"] = food.Amount;
                    return View(lsFood.ToPagedList(pageNum, pageSize));
                }
            }
        }
        [Authorize(Roles = "false")]
        public ActionResult DsFoodNV(Food food, string category, int? page, string searchString)
        {
            int pageSize = 6;
            int pageNum = (page ?? 1);
           
            if (searchString == null)
            {
                if (category == null)
                {
                    var lsFood = SelectAllArticle().ToList();
                    if (food.Amount > 1)
                    {
                        Session["Food"] = food.IDFood;
                        return View(lsFood.ToPagedList(pageNum, pageSize));
                    }
                    else
                    {
                        Session["Food"] = food.IDFood;
                        Session["Amount"] = food.Amount;
                        return View(lsFood.ToPagedList(pageNum, pageSize));
                    }
                }
                else
                {
                    var lsFood = database.Foods.OrderByDescending(x => x.NameFood).Where(x => x.IDCategoryFood == category);
                    if (food.Amount > 1)
                    {
                        return View(lsFood.ToPagedList(pageNum, pageSize));
                    }
                    else
                    {
                        Session["Amount"] = food.Amount;
                        return View(lsFood.ToPagedList(pageNum, pageSize));
                    }
                }
            }
            else
            {
                var list = SelectAllArticle().Where(s => s.NameFood.ToString().Contains(searchString));
                if (food.Amount > 1)
                {
                    return View(list.ToPagedList(pageNum, pageSize));
                }
                else
                {
                    Session["Amount"] = food.Amount;
                    return View(list.ToPagedList(pageNum, pageSize));
                }
            }
            
            
        }
        [Authorize(Roles = "true")]
        public ActionResult Create()
        {
            List<CategoryFood> list = SelectAllArticle1().ToList();
            ViewBag.listCF = new SelectList(list, "IDCategoryFood", "NameCategoryFood", "");
            Food food = new Food();
            return View(food);
        }
        [HttpPost]
        public ActionResult Create(Food food)
        {
            List<CategoryFood> list = SelectAllArticle1().ToList();
            try
            {
                if (food.UploadImage != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(food.UploadImage.FileName);
                    string extent = Path.GetExtension(food.UploadImage.FileName);
                    filename = filename + extent;
                    food.ImageFood = "~/Content/images/" + filename;
                    food.UploadImage.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), filename));
                }
                ViewBag.listCF = new SelectList(list, "IDCategoryFood", "NameCategoryFood", 1);
                database.Foods.Add(food);
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
            List<CategoryFood> list = SelectAllArticle1().ToList();
            ViewBag.listCF = new SelectList(list, "IDCategoryFood", "NameCategoryFood", "");
            return View(database.Foods.Where(s => s.ID == Id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Edit(int Id, Food food)
        {
            List<CategoryFood> list = SelectAllArticle1().ToList();
            database.Entry(food).State = System.Data.Entity.EntityState.Modified;
            try
            {
                if (food.UploadImage != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(food.UploadImage.FileName);
                    string extent = Path.GetExtension(food.UploadImage.FileName);
                    filename = filename + extent;
                    food.ImageFood = "~/Content/images/" + filename;
                    food.UploadImage.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), filename));
                }
                ViewBag.listCF = new SelectList(list, "IDCategoryFood", "NameCategoryFood", 1);
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
            return View(database.Foods.Where(s => s.ID == Id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Delete(int Id, Food food)
        {
            try
            {
                food = database.Foods.Where(s => s.ID == Id).FirstOrDefault();
                database.Foods.Remove(food);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Xóa nhân viên thất bại!");
            }
        }

        [Authorize(Roles = "true, false")]
        public ActionResult Details(int Id)
        {
            return View(database.Foods.Where(s => s.ID == Id).FirstOrDefault());
        }
        public ActionResult DetailsNV(int Id)
        {
            return View(database.Foods.Where(s => s.ID == Id).FirstOrDefault());
        }
    }
}