using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using appmvclibrary.Models;

namespace appmvclibrary.Areas.QuanLyDonMuonTra.Controllers
{
    [Area("QuanLyDonMuonTra")]
    [Route("/order/[action]/{id?}")]
    public class DonMuonTraController : Controller
    {
        private readonly AppDbContext _context;

        public DonMuonTraController(AppDbContext context)
        {
            _context = context;
        }

        // GET: QuanLyDonMuonTra/DonMuonTra
        public async Task<IActionResult> Index()
        {
            var order = await _context.Orders
                        .Include(x => x.PhieuMuonTra)
                            .ThenInclude(x => x.sach)
                        .ToListAsync();
            return View(order);
        }

        // GET: QuanLyDonMuonTra/DonMuonTra/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: QuanLyDonMuonTra/DonMuonTra/Create
        public IActionResult Create(int? id)
        {
            return View();
        }

        // POST: QuanLyDonMuonTra/DonMuonTra/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id)
        {
            var phieuMuonTra = _context.PhieuMuonTras
                        .Include(x => x.sach)
                        .FirstOrDefault(x => x.Id == id);
            if (phieuMuonTra != null)
            {   
                phieuMuonTra.sach.Quantity = phieuMuonTra.sach.Quantity - 1;
                phieuMuonTra.TrangThai = true;
                var lichSuMuonTra = new LichSuMuonTra()
                {
                    Name = phieuMuonTra.Name,
                    MaSinhVien = phieuMuonTra.MaSinhVien,
                    Lop = phieuMuonTra.Lop,
                    PhieuMuonTras = new List<PhieuMuonTra>() {
                        phieuMuonTra
                    }
                };

                _context.LichSuMuonTras.Add(lichSuMuonTra);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(phieuMuonTra);
        }

        // GET: QuanLyDonMuonTra/DonMuonTra/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: QuanLyDonMuonTra/DonMuonTra/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
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
            return View(order);
        }

        // GET: QuanLyDonMuonTra/DonMuonTra/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: QuanLyDonMuonTra/DonMuonTra/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
