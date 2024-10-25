using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NMCNPM.Data;
using NMCNPM.Models;

namespace NMCNPM.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Dashboard()
        {
            String? UserAdmin = HttpContext.Session.GetString("UserName");
            if (UserAdmin == null) return RedirectToAction("Login", "Login"); 
            var admin = _context.Admin.FirstOrDefault(c => c.UserName == UserAdmin);
            return View(admin);
        }
        public IActionResult ManageCustomers()
        {
            var customers = _context.Customer.ToList();
            return View(customers);
        }
        
        public IActionResult ManageVehicles()
        {
            var vehicles = _context.Vehicle.ToList();
            return View(vehicles);
        }

        // Xóa khách hàng
        public IActionResult DeleteCustomer(int id)
        {
            var customer = _context.Customer.Find(id);
            if (customer != null)
            {
                _context.Customer.Remove(customer);
                _context.SaveChanges();
            }
            return RedirectToAction("ManageCustomers");
        }

        // Xóa phương tiện
        public IActionResult DeleteVehicle(int id)
        {
            var vehicle = _context.Vehicle.Find(id);
            if (vehicle != null)
            {
                _context.Vehicle.Remove(vehicle);
                _context.SaveChanges();
            }
            return RedirectToAction("ManageVehicles");
        }
    }
}