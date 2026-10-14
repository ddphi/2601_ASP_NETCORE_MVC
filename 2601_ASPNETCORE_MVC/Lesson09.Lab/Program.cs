using Lesson09.Lab.Models;
using DevCtx = Lesson09.Lab.Models.DevXuongMoc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("BookStoreConnection");
builder.Services.AddDbContext<BookStoreContext>(x => x.UseSqlServer(connectionString));

var xuongMocConn = builder.Configuration.GetConnectionString("DevXuongMocConnection");
builder.Services.AddDbContext<DevCtx.XuongMocContext>(opt => opt.UseSqlServer(xuongMocConn));

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.Name = ".Devmaster.Session";
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();