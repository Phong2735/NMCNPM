using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace NMCNPM.Models
{
    public class Vehicle
{
    [Key]
    public int VehicleID { get; set; }

    [Required]
    public string BienSoXe { get; set; }

    public string LoaiXe { get; set; }

    [Required(ErrorMessage = "Tình trạng là bắt buộc.")]
    [RegularExpression("Available|Rented|Maintenance", 
        ErrorMessage = "Tình trạng phải là 'Available', 'Rented', hoặc 'Maintenance'.")]
    public string TinhTrang { get; set; }

    [Required(ErrorMessage = "Giá thuê là bắt buộc.")]
        [Range(0, 10000000, ErrorMessage = "Giá thuê phải lớn hơn hoặc bằng 0.")]
    public decimal GiaThue { get; set; }

    public string MoTa { get; set; }
    public string? Image {get; set;}
}
}