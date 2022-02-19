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

    public partial class Bill
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Bill()
        {
            this.DetailBills = new HashSet<DetailBill>();
        }

        public int ID { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Ngày đặt hàng")]
        public Nullable<System.DateTime> DateOrder { get; set; }
        public Nullable<int> IDDetailBill { get; set; }
        [Display(Name = "Nhân viên")]
        public string IDUser { get; set; }
        [Display(Name = "Tình trạng")]
        public Nullable<bool> Status { get; set; }
        [Display(Name = "Thành tiền")]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public Nullable<double> IntoMoney { get; set; }
    
        public virtual User User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetailBill> DetailBills { get; set; }
    }
}
