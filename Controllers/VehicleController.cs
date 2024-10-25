using Microsoft.AspNetCore.Mvc;
using NMCNPM.Data;
using NMCNPM.Models;
using System.Linq;

namespace NMCNPM.Controllers
{
    public class VehicleController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VehicleController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Vehicle/Index - Hiển thị danh sách xe
        public IActionResult Index()
        {
            var vehicles = _context.Vehicle.ToList();
            return View(vehicles);
        }

        // GET: Vehicle/Create - Hiển thị form tạo xe mới
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vehicle/Create - Xử lý thêm xe mới
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(Vehicle vehicle, IFormFile Image)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra tình trạng hợp lệ (nếu cần)
                if (Image != null && Image.Length > 0)
                {
                    var filePath = Path.Combine("wwwroot/Image", Image.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await Image.CopyToAsync(stream);
                    }
                    // using (var imageStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                    // {
                    //     vehicle.Image = Image.FileName(imageStream); // VehicleImage là kiểu Image
                    // }
                     vehicle.Image = "/Image/" + Image.FileName; // Lưu URL
                }
                var validStatuses = new[] { "Available", "Rented", "Maintenance" };
                if (!validStatuses.Contains(vehicle.TinhTrang))
                {
                    ModelState.AddModelError("TinhTrang", "Tình trạng không hợp lệ.");
                    return View(vehicle);
                }

                _context.Vehicle.Add(vehicle);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vehicle);
        }

        // GET: Vehicle/Edit/5 - Hiển thị form sửa thông tin xe
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var vehicle = _context.Vehicle.Find(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            return View(vehicle);
        }

        // POST: Vehicle/Edit/5 - Xử lý cập nhật thông tin xe
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Vehicle vehicle)
        {
            if (id != vehicle.VehicleID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(vehicle);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vehicle);
        }

        // GET: Vehicle/Delete/5 - Hiển thị xác nhận xóa xe
        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                var vehicle = _context.Vehicle.Find(id);
                if (vehicle == null)
                {
                    return Json(new { success = false, message = "Không tìm thấy xe." });
                }

                _context.Vehicle.Remove(vehicle);
                _context.SaveChanges();

                return Json(new { success = true, message = "Đã xóa xe thành công." });
            }
            catch (Exception ex)
            {
                // Ghi log lỗi
                Console.WriteLine(ex.Message);
                return Json(new { success = false, message = "Lỗi khi xóa xe: " + ex.Message });
            }
        }

        // POST: Vehicle/Delete/5 - Xử lý xóa xe
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var vehicle = _context.Vehicle.Find(id);
            if (vehicle != null)
            {
                _context.Vehicle.Remove(vehicle);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
