using System.ComponentModel.DataAnnotations;

namespace NMCNPM.Models
{
    public class Vehicle
{
    [Key]
    public int VehicleID { get; set; }

    [Required]
    public string BienSoXe { get; set; }

    public string LoaiXe { get; set; }

    [Required]
    public string TinhTrang { get; set; }

    [Required]
    public decimal GiaThue { get; set; }

    public string MoTa { get; set; }
}
}