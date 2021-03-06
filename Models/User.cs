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

    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            this.Bills = new HashSet<Bill>();
            ImageUser = "~/Content/images/Error.png";
        }
        public int ID { get; set; }
        [Display(Name = "Tài khoản")]
        [Required]
        public string Account { get; set; }
        [Display(Name = "Mật khẩu")]
        [Required]
        [MinLength(4, ErrorMessage = "Mật khẩu của bạn quá ngắn! Vui lòng nhập trên 4 ký tự")]
        public string Pass { get; set; }
        [Display(Name = "Chức vụ")]
        public string IDRole { get; set; }
        [Display(Name = "Tên nhân viên")]
        public string NameUser { get; set; }
        [Display(Name = "Giới tính")]
        public string Sex { get; set; }
        [Display(Name = "Avatar")]
        public string ImageUser { get; set; }
        [NotMapped]
        public HttpPostedFileBase UploadImage { get; set; }
        [Display(Name = "Số điện thoại")]
        public string Phone { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Ngày sinh")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }
        [NotMapped]
        public string ErrorInfo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bill> Bills { get; set; }
        public virtual Role Role { get; set; }
    }
    public enum Gender
    {
        Nam, Nữ
    }
}
