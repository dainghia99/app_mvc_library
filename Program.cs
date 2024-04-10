using appmvclibrary.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();


// Đăng ký AppDbContext, sử dụng kết nối đến MS SQL Server
builder.Services.AddDbContext<AppDbContext> (options => {
    string connectstring = builder.Configuration.GetConnectionString ("DefaultConnection");
    options.UseSqlServer (connectstring);
});

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

app.UseAuthentication ();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapAreaControllerRoute(
    name: "Administrator",
    pattern: "Administrator/{action=Index}/{id?}",
    areaName: "Administrator"
);

app.Run();
