using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using appmvclibrary.Models;
using Microsoft.AspNetCore.Identity;
using appmvclibrary.Models.User;
using Microsoft.AspNetCore.Authorization;

namespace appmvclibrary.Areas.QuanLyPhieuMuonTra.Controllers
{
    [Area("QuanLyPhieuMuonTra")]
    // [Route("/quan-ly-phieu-muon-tra/[action]/{id?}")]
    public class PhieuMuonTraController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public PhieuMuonTraController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [TempData]
        public string StatusMessage {set; get;}

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
                .Include(x => x.sach)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (phieuMuonTra == null)
            {
                return NotFound();
            }

            return View(phieuMuonTra);
        }

        // GET: QuanLyPhieuMuonTra/PhieuMuonTra/Create
        [Route("/muon-sach/{id?}")]
        public async Task<IActionResult> Create(int? id)
        {
            var user = await _userManager.GetUserAsync(this.User);
            ViewBag.Sach = _context.Sachs.FirstOrDefault(x => x.Id == id);
            ViewBag.User = user;
            return View();
        }

        // POST: QuanLyPhieuMuonTra/PhieuMuonTra/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("/muon-sach/{id?}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(int id, [Bind("Name,Lop,MaSinhVien,NgayTra")] PhieuMuonTra phieuMuonTra)
        {
            var user = await _userManager.GetUserAsync(this.User);
            if (user.FullName == null || user.Lop == null || user.StudentCode == null)
            {
                StatusMessage = "Vui lòng cập nhật thông tin tài khoản để mượn sách";
                return RedirectToAction("Create", "PhieuMuonTra", new {area = "QuanLyPhieuMuonTra"});
            }
            if (ModelState.IsValid)
            {
                var sachnew = await _context.Sachs.FirstOrDefaultAsync(x => x.Id == id);
                phieuMuonTra.NgayMuon = DateTime.Now;
                phieuMuonTra.NgayTaoPhieu = DateTime.Now;
                phieuMuonTra.TrangThai = false;
                phieuMuonTra.sach = sachnew;
                var order = new Order();
                order.PhieuMuonTra = new List<PhieuMuonTra>()
                {
                    phieuMuonTra
                };
                order.LichSuMuonTra = new LichSuMuonTra()
                {
                    Name = phieuMuonTra.Name,
                    MaSinhVien = phieuMuonTra.MaSinhVien,
                    Lop = phieuMuonTra.Lop,
                    sach = sachnew,
                    TrangThai = false, // Chưa mượn
                    Orders = new List<Order>()
                    {
                        order
                    }
                };
                phieuMuonTra.Order = order;
                _context.Add(phieuMuonTra);
                _context.Orders.Add(order);
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
