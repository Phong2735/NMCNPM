using System.ComponentModel.DataAnnotations;

namespace NMCNPM.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }  // Khóa chính

        [Required]
        [StringLength(100)]
        public string HoTenNV { get; set; }  // Họ tên nhân viên

        [StringLength(50)]
        public string ChucVu { get; set; }  // Chức vụ của nhân viên (Ví dụ: Kỹ thuật, Quản lý)

        [Phone]
        [StringLength(15)]
        public string SDTNV { get; set; }  // Số điện thoại nhân viên

        [EmailAddress]
        [StringLength(50)]
        public string EmailNV { get; set; }  // Email của nhân viên
        public string Password {get; set;}
    }
}