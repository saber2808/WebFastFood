using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI.WebControls;
using WebFastFood.Models;

namespace WebFastFood.Models
{
    public class CartItem
    {
        public Food _food { get; set; }
        public int quantity { get; set; }
        public int coupon { get; set; }
    }
    public class Cart
    {
        List<CartItem> items = new List<CartItem>();
        public IEnumerable<CartItem> Items
        {
            get { return items; }
        }
        public void Add_Product_Cart(Food food, int quan = 1)
        {
            var item = Items.FirstOrDefault(s => s._food.IDFood == food.IDFood);
            if (item == null)
            {
                items.Add(new CartItem
                {
                    _food = food,
                    quantity = quan
                });
            }
            else
                item.quantity += quan;
        }
        public int Total_quanlity()
        {
            return items.Sum(s => s.quantity);
        }
        public decimal Total_money()
        {
            var total = items.Sum(s => s.quantity * s._food.Price);
            return (decimal)total;
        }
        public void Update_quanlity(int id, int newquan)
        {
            var item = items.Find(s => s._food.ID == id);
            if (item != null)
            {
                if (items.Find(s => s._food.Amount >= newquan) != null)
                    item.quantity = newquan;
                else
                {
                    item.quantity = 1;
                }
            }
        }
        public void Remove_CartItem(int id)
        {
            items.RemoveAll(s => s._food.ID == id);
        }
        public void ClearCart()
        {
            items.Clear();
        }
    }
    public enum GenderTable
    {
        T01, T02, T03
    }
    public enum GenderCoupon
    {
        CP0, CP02, CP03
    }
}