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
using Microsoft.AspNetCore.Authorization;

namespace appmvclibrary.Areas.QuanLySach.Controllers
{
    [Area("QuanLySach")]
    [Route("/quan-ly-sach/[action]/{id?}")]
    [Authorize(Roles = "Administrator, ThuThu")]
    public class SachController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SachController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;

        }

        // GET: QuanLySach/Sach
        public async Task<IActionResult> Index()
        {
            var sachs = await _context.Sachs
                        .Include(x => x.SachCategories)
                            .ThenInclude(x => x.Category)
                        .Include(x => x.TacGiaSach)
                            .ThenInclude(x => x.TacGia)
                        .Include(x => x.Images)
                        .ToListAsync();

            return View(sachs);
        }

        // GET: QuanLySach/Sach/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sach = await _context.Sachs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sach == null)
            {
                return NotFound();
            }

            return View(sach);
        }

        // GET: QuanLySach/Sach/Create
        public IActionResult Create()
        {
            var theLoaiSach = _context.Categories.ToList();
            ViewBag.DanhSachTheLoai = new MultiSelectList(theLoaiSach, "Id", "Title");
            return View();
        }

        // POST: QuanLySach/Sach/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TenSach,MoTaNgan,Description,IsPublic,Quantity,Gia, CategoryIds")] ThemMoiSachModel sach, IFormFile[] files)
        {
            if (ModelState.IsValid)
            {

                sach.Created = DateTime.Now;
                sach.UpdatAt = DateTime.Now;
                sach.State = 1;
                var val = RemoveDiacritics(sach.TenSach);
                var slug = val.Replace(" ", "-");
                slug = slug.ToLower();
                sach.Slug = slug;
                if (sach.CategoryIds != null)
                {
                    foreach (var item in sach.CategoryIds)
                    {
                        _context.Add(new SachCategory()
                        {
                            Sach = sach,
                            CategoryId = item
                        });
                    }
                }

                foreach (var item in files)
                {
                    // Images
                    var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", item.FileName);
                    using var fileStream = new FileStream(filePath, FileMode.Create);
                    var image = new Image()
                    {
                        Sach = sach,
                        ImageUrl = "/Images/" + item.FileName
                    };
                    _context.Images.Add(image);
                    item.CopyTo(fileStream);
                }

                _context.Add(sach);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sach);
        }

        // GET: QuanLySach/Sach/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // var sach = await _context.Sachs.FindAsync(id);
            var sach = await _context.Sachs
                        .Include(x => x.SachCategories)
                        .Include(x => x.Images)
                        .FirstOrDefaultAsync(x => x.Id == id);

            if (sach == null)
            {
                return NotFound();
            }

            var postEdit = new ThemMoiSachModel()
            {
                Id = sach.Id,
                TenSach = sach.TenSach,
                MoTaNgan = sach.MoTaNgan,
                Description = sach.Description,
                IsPublic = sach.IsPublic,
                Quantity = sach.Quantity,
                Gia = sach.Gia,
                Slug = sach.Slug,
                CategoryIds = sach.SachCategories.Select(x => x.CategoryId).ToArray()
            };


            var theLoaiSach = _context.Categories.ToList();
            ViewBag.DanhSachTheLoai = new MultiSelectList(theLoaiSach, "Id", "Title");
            ViewBag.Images = sach.Images.Select(x => x.ImageUrl);
            return View(postEdit);
        }

        // POST: QuanLySach/Sach/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TenSach,MoTaNgan,Description,IsPublic,Quantity,Gia, CategoryIds")] ThemMoiSachModel sach, IFormFile[] files)
        {
            if (id != sach.Id)
            {
                return NotFound();
            }

            var theLoaiSach = _context.Categories.ToList();
            theLoaiSach.Insert(0, new Category()
            {
                Id = -1,
                Title = "Không có thể loại",
            });
            ViewBag.DanhSachTheLoai = new MultiSelectList(theLoaiSach, "Id", "Title");


            if (ModelState.IsValid)
            {
                try
                {

                    var sachUpdate = await _context.Sachs
                        .Include(x => x.SachCategories)
                        .Include(x => x.Images)
                        .FirstOrDefaultAsync(x => x.Id == id);

                    if (sachUpdate == null)
                    {
                        return NotFound();
                    }

                    var val = RemoveDiacritics(sach.TenSach);
                    var slug = val.Replace(" ", "-");
                    slug = slug.ToLower();

                    sachUpdate.Id = sach.Id;
                    sachUpdate.TenSach = sach.TenSach;
                    sachUpdate.MoTaNgan = sach.MoTaNgan;
                    sachUpdate.Description = sach.Description;
                    sachUpdate.IsPublic = sach.IsPublic;
                    sachUpdate.Quantity = sach.Quantity;
                    sachUpdate.Gia = sach.Gia;
                    sachUpdate.Slug = slug;
                    sachUpdate.Created = DateTime.Now;
                    sachUpdate.UpdatAt = DateTime.Now;

                    if (sach.CategoryIds == null) sach.CategoryIds = new int[] { };

                    var oldCateIds = sachUpdate.SachCategories.Select(x => x.CategoryId).ToArray();
                    var newCateIds = sach.CategoryIds;

                    var removeCateIds = from sachCate in sachUpdate.SachCategories
                                        where (!newCateIds.Contains(sachCate.CategoryId))
                                        select sachCate;

                    _context.SachCategories.RemoveRange(removeCateIds);

                    var addCateIds = from item in newCateIds
                                     where !oldCateIds.Contains(item)
                                     select item;

                    foreach (var item in addCateIds)
                    {
                        _context.SachCategories.Add(new SachCategory()
                        {
                            Sach = sachUpdate,
                            CategoryId = item
                        });
                    }

                    var fileCu = sachUpdate.Images.Select(x => x.ImageUrl).ToList();
                    var fileMoi = files.Select(x => x.FileName).ToList();
                    foreach (var item in files)
                    {
                        var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", item.FileName);
                        using var fileStream = new FileStream(filePath, FileMode.Create);
                        var image = new Image()
                        {
                            Sach = sach,
                            ImageUrl = "/Images/" + item.FileName
                        };
                        _context.Images.Add(image);
                        item.CopyTo(fileStream);
                    }

                    _context.Update(sachUpdate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SachExists(sach.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(sach);
        }

        // GET: QuanLySach/Sach/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sach = await _context.Sachs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sach == null)
            {
                return NotFound();
            }

            return View(sach);
        }

        // POST: QuanLySach/Sach/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sach = await _context.Sachs.FindAsync(id);
            if (sach != null)
            {
                _context.Sachs.Remove(sach);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SachExists(int id)
        {
            return _context.Sachs.Any(e => e.Id == id);
        }

        string RemoveDiacritics(string text)
        {
            string normalizedString = text.Normalize(NormalizationForm.FormD);
            StringBuilder stringBuilder = new StringBuilder();

            foreach (char c in normalizedString)
            {
                UnicodeCategory unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}
