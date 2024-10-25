using System.ComponentModel.DataAnnotations;

namespace NMCNPM.Models
{
    
    public class KHVehicle
    {
        [Key]
        public int RentalID { get; set; }
        public int VehicleID { get; set; }
        public int CustomerID { get; set; }
        public DateTime NgayThue { get; set; }
        public DateTime? NgayTra { get; set; } // Nullable vì chưa trả có thể để null
        public string TinhTrangThue { get; set; } // Ongoing, Completed, Cancelled
    }
    
}