using Microsoft.EntityFrameworkCore;
using NMCNPM.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDistributedMemoryCache();  // Để lưu trữ session trong bộ nhớ
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);  // Thời gian session tồn tại
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;  // Bắt buộc với GDPR
});
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();  // Bật Session
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
pattern: "{controller=Login}/{action=Login}/{id?}");
app.Run();
