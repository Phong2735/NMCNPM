using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NMCNPM.Data;
using NMCNPM.Models;

namespace NMCNPM
{
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomerController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var customers = _context.Customer.ToList();
            return View(customers);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }
        public IActionResult Delete(int id)
        {
            var customer = _context.Customer.Find(id);
            return customer == null ? NotFound() : View(customer);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customer.FindAsync(id);
            if (customer != null)
            {
                _context.Customer.Remove(customer);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Dashboard()
        {
            String? SDTKH = HttpContext.Session.GetString("SDT");
            if (SDTKH == null) return RedirectToAction("Login", "Login"); 
            var customer = _context.Customer.FirstOrDefault(c => c.SDTKH == SDTKH);
            var vehicles = _context.Vehicle.ToList();
            foreach (var vehicle in vehicles)
            {
                if (string.IsNullOrEmpty(vehicle.Image))
                {
                    vehicle.Image = "/Image/default_vehicle_image.png";  
                }
            }
            var viewModel = new CustomerVehicleViewModel
            {
                Customer = customer,
                Vehicles = vehicles
            };
            return View(viewModel);
        }
        public async Task<IActionResult> Detail(int id)
        {
            String? SDTKH = HttpContext.Session.GetString("SDT");
            if (SDTKH == null) return RedirectToAction("Login", "Login"); 
            var customer = _context.Customer.FirstOrDefault(c => c.SDTKH == SDTKH);
            var vehicle = await _context.Vehicle.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound("Không tìm thấy xe yêu cầu.");
            }

            var viewModel = new Detail
            {
                Customer = customer,
                Vehicle = vehicle
            };

            return View(viewModel); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RentVehicle(int vehicleID, int customerID, DateTime NgayThue, DateTime NgayTra)
        {
            var vehicle = await _context.Vehicle.FindAsync(vehicleID);
            if (vehicle == null)
            {
                return NotFound("Xe không tồn tại.");
            }
            if (NgayTra <= NgayThue)
            {
                ModelState.AddModelError("", "Thời gian trả phải lớn hơn thời gian thuê.");
                var viewModel = new Detail
                {

                    Customer = await _context.Customer.FindAsync(customerID),
                    Vehicle = await _context.Vehicle.FindAsync(vehicleID)
                };
                return View("Detail", viewModel); 
            }
            var kH_Vehicle = new KHVehicle
            {
                VehicleID = vehicleID,
                CustomerID = customerID,
                NgayThue = NgayThue,
                NgayTra = NgayTra,
                TinhTrangThue = "Ongoing",
            };

            _context.Add(kH_Vehicle);
            await _context.SaveChangesAsync();

            TempData["Message"] = "Đặt xe thành công!";
            return RedirectToAction(nameof(PageBook));
        }
        public async Task<IActionResult> PageBook(){
            String? SDTKH = HttpContext.Session.GetString("SDT");
            if (SDTKH == null) return RedirectToAction("Login", "Login");
            var customer = await _context.Customer.FirstOrDefaultAsync(c => c.SDTKH == SDTKH);
            var khVehicles = await _context.KHVehicle
                .Where(v => v.CustomerID == customer.CustomerID)
                .ToListAsync();
            var vehicleIDs = khVehicles.Select(v => v.VehicleID).ToList();
            var vehicles = await _context.Vehicle
                .Where(v => vehicleIDs.Contains(v.VehicleID))
                .ToListAsync();
            var bookViewModel = new BookViewModel
            {
                Customer = customer,
                KHVehicles = khVehicles,
                Vehicles = vehicles 
            };

            return View(bookViewModel);
        }
        [Route("Customer/DeleteKHVehicle/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteKHVehicle(int id)
        {
            var rental = await _context.KHVehicle.FindAsync(id);
            if (rental == null)
            {
                return NotFound(new { message = "Không tìm thấy thông tin thuê xe." });
            }

            _context.KHVehicle.Remove(rental);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Xóa thành công!" });
        }
        public IActionResult UpdateInf(int id)
        {            
            var customer = _context.Customer.Find(id);
            return customer == null ? NotFound() : View(customer);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateInf(int id, Customer model)
        {
        
            if(ModelState.IsValid)
            {
                var customer = _context.Customer.Find(id);
                if (customer != null)
                {
                    customer.HoTenKH = model.HoTenKH;
                    customer.SDTKH = model.SDTKH;
                    customer.EmailKH = model.EmailKH;
                    customer.DiaChiKH = model.DiaChiKH;

                    _context.SaveChangesAsync();
                    return RedirectToAction("Dashboard", new { id = id });
                }
            }
            return View(model);
        }
        public IActionResult Search(string searchInput= "")
        {
            
            var vehicles = _context.Vehicle.AsQueryable();

            if (!string.IsNullOrEmpty(searchInput))
            {
                vehicles = vehicles.Where(v => v.LoaiXe.Contains(searchInput) || 
                                                v.BienSoXe.Contains(searchInput));
            }

            return PartialView("VehicleListSearch", vehicles.ToList());
        }
    }
}
