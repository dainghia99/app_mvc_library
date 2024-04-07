using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using appmvclibrary.Models;
using appmvclibrary.Areas.QuanLySach.Models;
using System.Text;
using System.Globalization;

namespace appmvclibrary.Controllers
{
    [Route("thong-tin-chi-tiet/[action]/{id?}")]
    public class SachController : Controller
    {
        private readonly AppDbContext _context;

        public SachController(AppDbContext context)
        {
            _context = context;
        }

        // GET: SachController
        public async Task<IActionResult> Index(string? slug)
        {
            var sach = await _context.Sachs
                        .Include(x => x.SachCategories)
                            .ThenInclude(x => x.Category)
                        .Include(x => x.TacGiaSach)
                            .ThenInclude(x => x.TacGia)
                        .Include(x => x.Images)
                        .FirstOrDefaultAsync(x => x.Slug == slug);
                        

            ViewBag.SachHots = await _context.Sachs
                        .Include(x => x.SachCategories)
                            .ThenInclude(x => x.Category)
                        .Include(x => x.TacGiaSach)
                            .ThenInclude(x => x.TacGia)
                        .Include(x => x.Images)
                        .Where(x => x.IsPublic)
                        .ToListAsync();

            return View(sach);
        }

    }
}
