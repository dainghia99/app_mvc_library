using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using appmvclibrary.Models;

namespace appmvclibrary.Areas.QuanLyLichSuMuonTra.Controllers
{
    [Area("QuanLyLichSuMuonTra")]
    [Route("/lich-su/[action]/{id?}")]
    public class LichSuController : Controller
    {
        private readonly AppDbContext _context;

        public LichSuController(AppDbContext context)
        {
            _context = context;
        }

        // GET: QuanLyLichSuMuonTra/LichSu
        public async Task<IActionResult> Index()
        {
            var lichSuMuonTras = await _context.LichSuMuonTras
                                 .Include(x => x.PhieuMuonTras)
                                    .ThenInclude(x => x.sach)
                                 .ToListAsync();
                                 
            return View(lichSuMuonTras);
        }

        // GET: QuanLyLichSuMuonTra/LichSu/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lichSuMuonTra = await _context.LichSuMuonTras
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lichSuMuonTra == null)
            {
                return NotFound();
            }

            return View(lichSuMuonTra);
        }

        // GET: QuanLyLichSuMuonTra/LichSu/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: QuanLyLichSuMuonTra/LichSu/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,MaSinhVien,Lop")] LichSuMuonTra lichSuMuonTra)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lichSuMuonTra);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lichSuMuonTra);
        }

        // GET: QuanLyLichSuMuonTra/LichSu/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lichSuMuonTra = await _context.LichSuMuonTras.FindAsync(id);
            if (lichSuMuonTra == null)
            {
                return NotFound();
            }
            return View(lichSuMuonTra);
        }

        // POST: QuanLyLichSuMuonTra/LichSu/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,MaSinhVien,Lop")] LichSuMuonTra lichSuMuonTra)
        {
            if (id != lichSuMuonTra.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lichSuMuonTra);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LichSuMuonTraExists(lichSuMuonTra.Id))
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
            return View(lichSuMuonTra);
        }

        // GET: QuanLyLichSuMuonTra/LichSu/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lichSuMuonTra = await _context.LichSuMuonTras
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lichSuMuonTra == null)
            {
                return NotFound();
            }

            return View(lichSuMuonTra);
        }

        // POST: QuanLyLichSuMuonTra/LichSu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lichSuMuonTra = await _context.LichSuMuonTras.FindAsync(id);
            if (lichSuMuonTra != null)
            {
                _context.LichSuMuonTras.Remove(lichSuMuonTra);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LichSuMuonTraExists(int id)
        {
            return _context.LichSuMuonTras.Any(e => e.Id == id);
        }
    }
}
