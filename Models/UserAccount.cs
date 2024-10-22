using System.ComponentModel.DataAnnotations;

namespace NMCNPM.Models
{
    public class UserAccount
    {
        [Key]
        public int UserID { get; set; }  // Khóa chính

        [Required]
        [StringLength(50)]
        public string NameUser { get; set; }  // Tên đăng nhập (username)

        [Required]
        [StringLength(255)]
        public string Password { get; set; }  // Mật khẩu đã mã hóa

        [Required]
        [StringLength(20)]
        public string Role { get; set; }  // Vai trò (Admin, Employee, Customer)

        [DataType(DataType.Date)]
        public DateTime NgayTao { get; set; } = DateTime.Now;  // Ngày tạo tài khoản
    }
}