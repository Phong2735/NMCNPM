using Microsoft.AspNetCore.Mvc;
using NMCNPM.Data;
using NMCNPM.Models;

namespace NMCNPM
{
    public class UserAccountController : Controller
    {
        // GET: UserAccountController
        private readonly ApplicationDbContext _context;

        public UserAccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.UserAccount.ToList());
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(UserAccount userAccount)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userAccount);
                await _context.SaveChangesAsync();
                 // Nếu role là "Customer", thêm thông tin vào bảng Customer
                if (userAccount.Role == "Customer")
                {
                    var customer = new Customer
                    {
                        HoTenKH = userAccount.NameUser,
                        EmailKH = $"{userAccount.NameUser}@example.com",  // Giả định email dựa trên tên đăng nhập
                        SDTKH = "N/A", 
                        DiaChiKH = "Chưa xác định"
                    };
                    _context.Customer.Add(customer);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(userAccount);
        }

        public IActionResult Edit(int id)
        {
            var userAccount = _context.UserAccount.Find(id);
            return userAccount == null ? NotFound() : View(userAccount);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserAccount userAccount)
        {
            if (ModelState.IsValid)
            {
                _context.Update(userAccount);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userAccount);
        }

        public IActionResult Delete(int id)
        {
            var userAccount = _context.UserAccount.Find(id);
            return userAccount == null ? NotFound() : View(userAccount);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userAccount = await _context.UserAccount.FindAsync(id);
            if (userAccount != null)
            {
                _context.UserAccount.Remove(userAccount);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
