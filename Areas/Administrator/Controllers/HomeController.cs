using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Data;
using appmvclibrary.Models;
using appmvclibrary.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace appmvclibrary.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    [Route("/administrator/[action]/{id?}")]
    [Authorize(Roles = "Administrator, ThuThu")]
    public class HomeController : Controller
    {
        private readonly AppDbContext _dbcontext;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public HomeController(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager, AppDbContext dbcontext)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _dbcontext = dbcontext;
        }

        [TempData]
        public string StatusMessage { set; get; }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SeedDataAsync()
        {
            var roleNames = typeof(RoleName).GetFields().ToList();
            foreach (var roleName in roleNames)
            {
                var role = (string)roleName.GetRawConstantValue();
                var rolef = await _roleManager.FindByNameAsync(role);

                if (rolef == null)
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // User: admin, password: admin12345, email: admin@gmail.com
            var userAdmin = await _userManager.FindByNameAsync("admin");
            if (userAdmin == null)
            {
                userAdmin = new AppUser()
                {
                    UserName = "admin",
                    Email = "admin@gmail.com",
                    EmailConfirmed = true,
                };

                await _userManager.CreateAsync(userAdmin, "admin12345");
                await _userManager.AddToRoleAsync(userAdmin, RoleName.Administrator);
            }
            StatusMessage = "Đã tạo thành công tài khoản administrator với thông tin tài khoản là user: admin passwword: admin12345";
            return RedirectToAction("Index", "Home");
        }
    }
}