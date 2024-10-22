using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using NMCNPM.Data;
using NMCNPM.Models;

namespace NMCNPM.Controllers
{
    public class DangKy : Controller{
        private readonly ApplicationDbContext _context;

        public DangKy(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /UserAccount/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: /UserAccount/Register
        [HttpPost]
        public async Task<IActionResult> Register(UserAccount user)
        {
            if (ModelState.IsValid)
            {
                // Mã hóa mật khẩu (tùy chọn)
                user.Role = "Customer";  // Gán mặc định vai trò Customer
                user.NgayTao = DateTime.Now;  // Lưu ngày tạo hiện tại
                user.NameUser = user.NameUser;
                user.Password = user.Password;
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Login", "UserAccount");
            }

            return View(user);
        }
    }
}