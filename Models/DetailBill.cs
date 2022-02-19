﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebFastFood.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class DetailBill
    {
        public int ID { get; set; }
        [Display(Name = "Mã món ăn")]
        public string IDFood { get; set; }
        [Display(Name = "Mã hóa đơn")]
        public Nullable<int> IDBill { get; set; }
        [Display(Name = "Số lượng")]
        public Nullable<int> Amount { get; set; }
        [Display(Name = "Số bàn")]
        public string TableNumber { get; set; }
        [Display(Name = "Khuyến mãi")]
        public string Coupon { get; set; }
        [Display(Name = "Đơn giá")]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public Nullable<double> UnitPrice { get; set; }
        [Display(Name = "Thành tiền")]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public Nullable<double> IntoMoney { get; set; }
    
        public virtual Bill Bill { get; set; }
        public virtual Coupon Coupon1 { get; set; }
        public virtual Food Food { get; set; }
        public virtual TableNumber TableNumber1 { get; set; }
    }
}
