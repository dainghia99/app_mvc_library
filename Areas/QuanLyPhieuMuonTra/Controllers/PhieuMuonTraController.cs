using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using appmvclibrary.Models;

namespace appmvclibrary.Areas.QuanLyPhieuMuonTra.Controllers
{
    [Area("QuanLyPhieuMuonTra")]
    // [Route("/quan-ly-phieu-muon-tra/[action]/{id?}")]
    public class PhieuMuonTraController : Controller
    {
        private readonly AppDbContext _context;

        public PhieuMuonTraController(AppDbContext context)
        {
            _context = context;
        }

        // GET: QuanLyPhieuMuonTra/PhieuMuonTra
        [Route("/quan-ly-phieu-muon-tra/[action]/{id?}")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.PhieuMuonTras.ToListAsync());
        }

        // GET: QuanLyPhieuMuonTra/PhieuMuonTra/Details/5
        [Route("/quan-ly-phieu-muon-tra/[action]/{id?}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phieuMuonTra = await _context.PhieuMuonTras
                .FirstOrDefaultAsync(m => m.Id == id);
            if (phieuMuonTra == null)
            {
                return NotFound();
            }

            return View(phieuMuonTra);
        }

        // GET: QuanLyPhieuMuonTra/PhieuMuonTra/Create
        [Route("/muon-sach/{id?}")]
        public IActionResult Create(int? id)
        {
            ViewBag.Sach = _context.Sachs.FirstOrDefault(x => x.Id == id);
            return View();
        }

        // POST: QuanLyPhieuMuonTra/PhieuMuonTra/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("/muon-sach/{id?}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, [Bind("Name,Lop,MaSinhVien,NgayTra")] PhieuMuonTra phieuMuonTra)
        {
            if (ModelState.IsValid)
            {
                var sachnew = await _context.Sachs.FirstOrDefaultAsync(x => x.Id == id);
                phieuMuonTra.NgayMuon = DateTime.Now;
                phieuMuonTra.NgayTaoPhieu = DateTime.Now;
                phieuMuonTra.TrangThai = false;
                phieuMuonTra.sach = sachnew;
                _context.Add(phieuMuonTra);

                // _context.Add(new Order(){
                //     PhieuMuonTra = new List<PhieuMuonTra>()
                //     {
                //         phieuMuonTra
                //     }
                // });

                _context.Orders.Add(new Order() {
                    PhieuMuonTra = new List<PhieuMuonTra>()
                    {
                        phieuMuonTra
                    }
                });

                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            return View(phieuMuonTra);
        }

        // GET: QuanLyPhieuMuonTra/PhieuMuonTra/Delete/5
        [Route("/quan-ly-phieu-muon-tra/[action]/{id?}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phieuMuonTra = await _context.PhieuMuonTras
                .FirstOrDefaultAsync(m => m.Id == id);
            if (phieuMuonTra == null)
            {
                return NotFound();
            }

            return View(phieuMuonTra);
        }

        // POST: QuanLyPhieuMuonTra/PhieuMuonTra/Delete/5
        [Route("/quan-ly-phieu-muon-tra/[action]/{id?}")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var phieuMuonTra = await _context.PhieuMuonTras.FindAsync(id);
            if (phieuMuonTra != null)
            {
                _context.PhieuMuonTras.Remove(phieuMuonTra);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhieuMuonTraExists(int id)
        {
            return _context.PhieuMuonTras.Any(e => e.Id == id);
        }
    }
}
