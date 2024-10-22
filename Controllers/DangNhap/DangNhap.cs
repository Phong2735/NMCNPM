using Microsoft.AspNetCore.Mvc;

namespace NMCNPM.Controllers.DangNhap
{
    public class DangNhap : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        // Xử lý đăng nhập
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            if (username == "admin" && password == "123") // Kiểm tra demo
            {
                return RedirectToAction("Index", "Home"); // Điều hướng về Home sau khi đăng nhập thành công
            }

            ViewBag.ErrorMessage = "Tên đăng nhập hoặc mật khẩu không đúng!";
            return View();
        }

        // Action cho màn hình Đăng Ký
        public ActionResult Register()
        {
            return View();
        }

        // Xử lý đăng ký
        [HttpPost]
        public ActionResult Register(string username, string password)
        {
            // Giả sử bạn lưu user vào cơ sở dữ liệu (chưa hiện thực chi tiết)
            ViewBag.Message = "Đăng ký thành công!";
            return View();
        }
    }
}
