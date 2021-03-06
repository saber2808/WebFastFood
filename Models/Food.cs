//------------------------------------------------------------------------------
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
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web;

    public partial class Food
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Food()
        {
            this.DetailBills = new HashSet<DetailBill>(); 
            ImageFood = "~/Content/images/Error.png";
        }
        [Key]
        public int ID { get; set; }
        [Display(Name = "Mã món")]
        public string IDFood { get; set; }
        [Display(Name = "Tên món")]
        public string NameFood { get; set; }
        [Display(Name = "Mô tả")]
        public string Detail { get; set; }
        [Display(Name = "Loại món ăn")]
        public string IDCategoryFood { get; set; }
        [Display(Name = "Số lượng tồn")]
        public Nullable<int> Amount { get; set; }
        [Display(Name = "Giá")]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public Nullable<decimal> Price { get; set; }
        [Display(Name = "Hình ảnh")]
        public string ImageFood { get; set; }
        [NotMapped]
        public HttpPostedFileBase UploadImage { get; set; }

        public virtual CategoryFood CategoryFood { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetailBill> DetailBills { get; set; }
    }
}
