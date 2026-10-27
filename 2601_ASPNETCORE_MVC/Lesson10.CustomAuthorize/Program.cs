using Lesson10.CustomAuthorize.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication("DevSecuritySchema")
    .AddCookie("DevSecuritySchema", opt =>
    {
        opt.Cookie = new CookieBuilder
        {
            HttpOnly = true,
            Name = "Devmaster.Security.Cookie",
            Path = "/",
            SameSite = SameSiteMode.Lax,
            SecurePolicy = CookieSecurePolicy.SameAsRequest
        };
        opt.LoginPath = new PathString("/Home/Login");
        opt.SlidingExpiration = true;
    });

var conn = builder.Configuration.GetConnectionString("DevXuongMocConnection");
builder.Services.AddDbContext<XuongMocContext>(opt => opt.UseSqlServer(conn));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseCookiePolicy();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();