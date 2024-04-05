using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using appmvclibrary.Models;

namespace appmvclibrary.Areas.QuanLyDanhMuc.Controllers
{
    [Area("QuanLyDanhMuc")]
    [Route("/admin/quan-ly-danh-muc-sach/[action]/{id?}")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        // GET: QuanLyDanhMuc/Category
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Categories.Include(c => c.ParentCategory);
            return View(await appDbContext.ToListAsync());
        }

        // GET: QuanLyDanhMuc/Category/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .Include(c => c.ParentCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: QuanLyDanhMuc/Category/Create
        public IActionResult Create()
        {
            var categories = _context.Categories.ToList();
            categories.Insert(0, new Category(){
                Id = -1,
                Title = "Không có danh mục cha"
            });
            ViewData["ParentCategoryId"] = new SelectList(categories, "Id", "Title");
            return View();
        }

        // POST: QuanLyDanhMuc/Category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ParentCategoryId,Title,Content,Slug")] Category category)
        {
            if (ModelState.IsValid)
            {
                if (category.ParentCategoryId == -1) category.ParentCategoryId = null;
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var categories = _context.Categories.ToList();
            categories.Insert(0, new Category(){
                Id = -1,
                Title = "Không có danh mục cha"
            });
            ViewData["ParentCategoryId"] = new SelectList(categories, "Id", "Title");
            return View(category);
        }

        // GET: QuanLyDanhMuc/Category/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            var categories = _context.Categories.ToList();
            categories.Insert(0, new Category(){
                Id = -1,
                Title = "Không có danh mục cha"
            });
            ViewData["ParentCategoryId"] = new SelectList(categories, "Id", "Title");
            return View(category);
        }

        // POST: QuanLyDanhMuc/Category/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ParentCategoryId,Title,Content,Slug")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (category.ParentCategoryId == category.Id)
            {
                ModelState.AddModelError(string.Empty, "Bạn phải chọn danh mục khác");
            }

            if (ModelState.IsValid && category.ParentCategoryId != category.Id)
            {
                try 
                {
                    
                    if (category.ParentCategoryId == -1) category.ParentCategoryId = null;
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
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
            ViewData["ParentCategoryId"] = new SelectList(_context.Categories, "Id", "Slug", category.ParentCategoryId);
            return View(category);
        }

        // GET: QuanLyDanhMuc/Category/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .Include(c => c.ParentCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: QuanLyDanhMuc/Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.Categories
                            .Include(c => c.CategoryChildren)
                            .FirstOrDefaultAsync(m => m.Id == id);  
            if (category != null)
            {
                foreach (var childrelCate in category.CategoryChildren) 
                {
                    childrelCate.ParentCategoryId = category.ParentCategoryId;
                }
                _context.Categories.Remove(category);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}
