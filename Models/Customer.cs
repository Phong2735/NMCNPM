using System.ComponentModel.DataAnnotations;

namespace NMCNPM.Models
{
    public class Customer
    {
        [Key]
        public int CustomerID { get; set; }  // Khóa chính

        [Required(ErrorMessage = "Họ tên không được để trống.")]
        [StringLength(100, ErrorMessage = "Họ tên không được vượt quá 100 ký tự.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Họ tên chỉ được chứa chữ cái và khoảng trắng.")]
        public string HoTenKH { get; set; }  // Họ tên khách hàng

        [Phone]
        [StringLength(15)]
        public string SDTKH { get; set; }  // Số điện thoại khách hàng

        [EmailAddress]
        [StringLength(50)]
        public string? EmailKH { get; set; }  // Email khách hàng

        [StringLength(255)]
        public string? DiaChiKH { get; set; }  // Địa chỉ khách hàng
        public string Password {get; set;}
    }
}