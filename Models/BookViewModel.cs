namespace NMCNPM.Models
{
    public class BookViewModel
    {
        public List<Vehicle> Vehicles { get; set; } // Danh sách các xe tương ứng với đơn thuê
        public Customer Customer { get; set; }
        public List<KHVehicle> KHVehicles { get; set; }
    }
}