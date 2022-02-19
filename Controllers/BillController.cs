using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls.WebParts;
using WebFastFood.Models;

namespace WebFastFood.Controllers
{
    [Authorize(Roles ="true, false")]
    public class BillController : Controller
    {
        WebFastFoodEntities database = new WebFastFoodEntities();
        // GET: Bill
     
        public static List<DetailBill> SelectAllArticle()
        {
            var rtn = new List<DetailBill>();
            using (var context = new WebFastFoodEntities())
            {
                foreach (var item in context.DetailBills)
                {
                    rtn.Add(new DetailBill
                    {
                        ID = item.ID,
                        IDFood = item.IDFood,
                        IDBill = item.IDBill,
                        Amount = item.Amount,
                        TableNumber = item.TableNumber1.IDTableNumber,
                        Coupon = item.Coupon,
                        UnitPrice = item.UnitPrice,
                        IntoMoney = item.IntoMoney,
                    });
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
        
       
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "false")]
        public ActionResult ShowBill(Coupon cp)
        {
            if (Session["Cart"] == null)
            {
                return View("EmptyBill");
            }
            Cart _cart = Session["Cart"] as Cart;
            return View(_cart);
        }
        public Cart GetCart()
        {
            Cart cart = Session["Cart"] as Cart;
            if (cart == null||Session["Cart"]==null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
        public ActionResult AddToCart(int id)
        {
            var _food = database.Foods.SingleOrDefault(s => s.ID == id);
            if(_food != null)
            {
                GetCart().Add_Product_Cart(_food);
            }
            return RedirectToAction("DsFoodNV", "Food");
        }
        public ActionResult Update_Cart_Quantity(FormCollection form)
        {
            Cart cart = Session["Cart"] as Cart;
            int id_Food = int.Parse(form["idFood"]);
            int _quantity = int.Parse(form["billQuantity"]);
            cart.Update_quanlity(id_Food, _quantity);
            return RedirectToAction("ShowBill", "Bill");
        }
        public ActionResult RemoveCart(int id)
        {
            Cart cart = Session["Cart"] as Cart;
            cart.Remove_CartItem(id);
            return RedirectToAction("ShowBill", "Bill");
        }
        public PartialViewResult ListBill()
        {
            int total_quantity_item = 0;
            Cart cart = Session["Cart"] as Cart;
            if (cart!=null)
            {
                total_quantity_item = cart.Total_quanlity();
            }
            ViewBag.QuantityCart = total_quantity_item;
            return PartialView("ListBill");
        }
        public ActionResult OrdSuccess()
        {
            return View();
        }
        [HttpPost]
        public ActionResult OrdSuccess(FormCollection form)
        {
            try
            {
                Cart cart = Session["Cart"] as Cart;
                Bill order = new Bill();
                order.DateOrder = DateTime.Now;
                string coupon = form["Coupon"];
                foreach (var cp in database.Coupons.Where(s => s.IDCoupon == coupon))
                {
                    order.IntoMoney = (double)cart.Total_money() - cp.discount* (double)cart.Total_money()/100;
                }
                order.IDUser = Session["Account"].ToString();
                database.Bills.Add(order);
                foreach (var item in cart.Items)
                {
                    DetailBill order_detail = new DetailBill();
                    order_detail.IDBill = order.ID;
                    order_detail.IDFood = item._food.IDFood;
                    order_detail.UnitPrice = (double)item._food.Price;
                    order_detail.Amount = item.quantity;
                    order_detail.TableNumber = form["TableNumber"];
                    order_detail.Coupon = form["Coupon"];
                    foreach (var cp in database.Coupons.Where(s => s.IDCoupon == coupon))
                    {
                        order_detail.IntoMoney = (double)cart.Total_money() - cp.discount * (double)cart.Total_money() / 100;
                    }
                    database.DetailBills.Add(order_detail);
                    foreach (var p in database.Foods.Where(s => s.IDFood == order_detail.IDFood))
                    {
                        var update_quan_pro = p.Amount - item.quantity;
                        p.Amount = update_quan_pro;
                    }
                    
                }
                database.SaveChanges();
                cart.ClearCart();
                return RedirectToAction("OrdSuccess", "Bill");
            }
            catch
            {
                return Content("Lỗi thanh toán, vui lòng kiểm tra lại thông tin khách hàng!");
            }
        }
        
    }
}