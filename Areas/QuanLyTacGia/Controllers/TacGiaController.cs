using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using appmvclibrary.Models;
using Org.BouncyCastle.Asn1.X509;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace appmvclibrary.Areas.QuanLyTacGia.Controllers
{
    [Area("QuanLyTacGia")]
    [Route("/admin/quan-ly-tac-gia/[action]/{id?}")]
    public class TacGiaController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<TacGiaController> _logger;


        public TacGiaController(AppDbContext context, ILogger<TacGiaController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: QuanLyTacGia/TacGia
        public async Task<IActionResult> Index()
        {
            var tacGias = await _context.TacGias
                          .Include(x => x.TacGiaSach)
                          .ThenInclude(x => x.Sach)
                          .ToListAsync();
            return View(tacGias);
        }

        // GET: QuanLyTacGia/TacGia/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tacGia = await _context.TacGias
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tacGia == null)
            {
                return NotFound();
            }

            return View(tacGia);
        }

        // GET: QuanLyTacGia/TacGia/Create
        public IActionResult Create()
        {
            ViewBag.Sachs =_context.Sachs
                                    .Include(x => x.TacGiaSach)
                                    .ToList();
            return View();
        }

        // POST: QuanLyTacGia/TacGia/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,NgaySinh,TieuSu")] TacGia tacGia, int[] SachId)
        {
            if (ModelState.IsValid)
            {
                tacGia.CreatedAt = DateTime.Now;
                tacGia.UpdatedAt = DateTime.Now;
                _context.Add(tacGia);
                if (SachId == null) new int {};
                foreach (var id in SachId)
                {
                    var sach = await _context.Sachs
                                .Include(x => x.TacGiaSach)
                                .Include(x => x.SachCategories)
                                .FirstOrDefaultAsync(x => x.Id == id);
                    _context.Add(new TacGiaSach()
                    {
                        TacGiaId = tacGia.Id,
                        SachId = id,
                        Sach = sach,
                        TacGia = tacGia
                    });
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tacGia);
        }

        // GET: QuanLyTacGia/TacGia/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tacGia = await _context.TacGias
                .Include(x => x.TacGiaSach)
                .ThenInclude(x => x.Sach)
                .FirstOrDefaultAsync(x => x.Id == id);
            ViewBag.SachUpdate = await _context.Sachs
                                .Include(x => x.TacGiaSach)
                                .ToListAsync();

            if (tacGia == null)
            {
                return NotFound();
            }
            
            return View(tacGia);
        }

        // POST: QuanLyTacGia/TacGia/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,NgaySinh,TieuSu")] TacGia tacGia, int [] SachId)
        {
            if (id != tacGia.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    tacGia.CreatedAt = DateTime.Now;
                    tacGia.UpdatedAt = DateTime.Now;
                    _context.Update(tacGia);

                    if (SachId == null) SachId = new int[] {};
                    
                    var tg = await _context.TacGias
                            .Include(x => x.TacGiaSach)
                            .ThenInclude(x => x.Sach)
                            .FirstOrDefaultAsync(x => x.Id == id);

                    
                    var sachCuSoHuu = tacGia.TacGiaSach?.Select(x => x.Sach?.Id).ToList();

                    if (sachCuSoHuu != null)
                    {
                        // Xoá sách cũ

                        // Lọc ra toàn bộ sách cũ thuộc tác giả để xoá
                        var sachRemove = tg.TacGiaSach
                                        .Where(s => !SachId.Contains(s.Sach.Id))
                                        .ToList();
                        _context.TacGiaSachs.RemoveRange(sachRemove);
                    }
                    
                    foreach (var idSachMoi in SachId)
                    {
                        var sach = await _context.Sachs
                                    .Include(x => x.TacGiaSach)
                                    .FirstOrDefaultAsync(x => x.Id == idSachMoi);

                        _context.TacGiaSachs.Add(new TacGiaSach() {
                            SachId = idSachMoi,
                            TacGiaId = id,
                            Sach = sach,
                            TacGia = tg
                        });
                        
                    }
                    
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TacGiaExists(tacGia.Id))
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
            return View(tacGia);
        }

        // GET: QuanLyTacGia/TacGia/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tacGia = await _context.TacGias
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tacGia == null)
            {
                return NotFound();
            }

            return View(tacGia);
        }

        // POST: QuanLyTacGia/TacGia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tacGia = await _context.TacGias.FindAsync(id);
            if (tacGia != null)
            {
                _context.TacGias.Remove(tacGia);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TacGiaExists(int id)
        {
            return _context.TacGias.Any(e => e.Id == id);
        }
    }
}
