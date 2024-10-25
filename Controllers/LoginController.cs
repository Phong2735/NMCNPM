using Microsoft.AspNetCore.Mvc;
using NMCNPM.Data;

namespace NMCNPM.Controllers
{
    public class LoginController : Controller {
     private readonly ApplicationDbContext _context;

        public LoginController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var admin = _context.Admin
                .FirstOrDefault(a => a.UserName == username && a.Password == password);

            if (admin != null)
            {
                HttpContext.Session.SetString("UserType", "Admin");
                HttpContext.Session.SetString("UserName", admin.UserName);
                return RedirectToAction("Dashboard", "Admin");
            }

            var customer = _context.Customer
                .FirstOrDefault(c => c.SDTKH == username && c.Password == password);

            if (customer != null)
            {
                HttpContext.Session.SetInt32("idKH", customer.CustomerID);
                HttpContext.Session.SetString("SDT", customer.SDTKH);
                return RedirectToAction("Dashboard", "Customer");
            }

            var employee = _context.Employee
                .FirstOrDefault(e => e.SDTNV == username && e.Password == password);

            if (employee != null)
            {
                HttpContext.Session.SetString("UserType", "Employee");
                HttpContext.Session.SetString("SDT", employee.SDTNV);
                return RedirectToAction("Dashboard", "Employee");
            }

            ModelState.AddModelError(string.Empty, "Tên đăng nhập hoặc mật khẩu không chính xác.");
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}