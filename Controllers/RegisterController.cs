using Microsoft.AspNetCore.Mvc;
using NMCNPM.Data;
using NMCNPM.Models;

namespace NMCNPM.Controllers
{
    public class RegisterController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RegisterController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Hiển thị form đăng ký
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // POST: Xử lý đăng ký khách hàng
        [HttpPost]
        public IActionResult Register(Customer customer)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra trùng lặp số điện thoại
                var existingCustomer = _context.Customer
                    .FirstOrDefault(c => c.SDTKH == customer.SDTKH);

                if (existingCustomer != null)
                {
                    ModelState.AddModelError("SDTKH", "Số điện thoại đã được sử dụng.");
                    return View(customer);
                }
                customer.EmailKH = string.IsNullOrWhiteSpace(customer.EmailKH) ? null : customer.EmailKH;
                customer.DiaChiKH = string.IsNullOrWhiteSpace(customer.DiaChiKH) ? null : customer.DiaChiKH;
                // Lưu thông tin khách hàng vào database
                _context.Customer.Add(customer);
                _context.SaveChanges();

                // Sau khi đăng ký thành công, chuyển hướng về trang đăng nhập
                return RedirectToAction("Login", "Login");
            }

            // Nếu dữ liệu không hợp lệ, hiển thị lại form đăng ký
            return View(customer);
        }
    }
}