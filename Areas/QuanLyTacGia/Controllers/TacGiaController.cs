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
using appmvclibrary.Areas.QuanLyTacGia.Models;
using Microsoft.AspNetCore.Authorization;

namespace appmvclibrary.Areas.QuanLyTacGia.Controllers
{
    [Area("QuanLyTacGia")]
    [Route("/quan-ly-tac-gia/[action]/{id?}")]
    [Authorize(Roles = "Administrator, ThuThu")]
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
            var sachs = _context.Sachs.ToList();
            ViewBag.DanhSachSachs = new SelectList(sachs, "Id", "TenSach");
            return View();
        }

        // POST: QuanLyTacGia/TacGia/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,NgaySinh,TieuSu, SachIds")] ThemMoiTacGiaModel tacGia)
        {
            if (ModelState.IsValid)
            {
                tacGia.CreatedAt = DateTime.Now;
                tacGia.UpdatedAt = DateTime.Now;

                if (tacGia.SachIds != null)
                {
                    foreach (var item in tacGia.SachIds)
                    {
                        _context.Add(new TacGiaSach()
                        {
                            TacGia = tacGia,
                            SachId = item
                        });
                    }
                }

                _context.Add(tacGia);
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

            var tacgia = await _context.TacGias
                        .Include(x => x.TacGiaSach)
                        .FirstOrDefaultAsync(x => x.Id == id);

            var tacGiaEdit = new ThemMoiTacGiaModel()
            {
                Id = tacgia.Id,
                Name = tacgia.Name,
                NgaySinh = tacgia.NgaySinh,
                TieuSu = tacgia.TieuSu,
                CreatedAt = tacgia.CreatedAt,
                UpdatedAt = tacgia.UpdatedAt,
                SachIds = tacgia.TacGiaSach.Select(x => x.SachId).ToArray(),
            };

            var sachs = _context.Sachs.ToList();
            ViewBag.DanhSachSachs = new SelectList(sachs, "Id", "TenSach");

            return View(tacGiaEdit);
        }

        // POST: QuanLyTacGia/TacGia/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,NgaySinh,TieuSu, SachIds")] ThemMoiTacGiaModel tacGia)
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

                    var listSachs = _context.Sachs.ToList();
                    ViewBag.DanhSachSachs = new SelectList(listSachs, "Id", "TenSach");

                    var tacGiaUpdate = await _context.TacGias
                        .Include(x => x.TacGiaSach)
                        .FirstOrDefaultAsync(x => x.Id == id);

                    if (tacGiaUpdate == null)
                    {
                        return NotFound();
                    }

                    tacGiaUpdate.Id = tacGia.Id;
                    tacGiaUpdate.Name = tacGia.Name;
                    tacGiaUpdate.NgaySinh = tacGia.NgaySinh;
                    tacGiaUpdate.TieuSu = tacGia.TieuSu;
                    tacGiaUpdate.CreatedAt = tacGia.CreatedAt;
                    tacGiaUpdate.UpdatedAt = tacGia.UpdatedAt;

                    if (tacGia.SachIds == null) tacGia.SachIds = new int[] { };

                    var oldSacIds = tacGiaUpdate.TacGiaSach.Select(x => x.SachId).ToArray();
                    var newSacIds = tacGia.SachIds;

                    var removeSachCu = from sach in tacGiaUpdate.TacGiaSach
                                       where (!newSacIds.Contains(sach.SachId))
                                       select sach;

                    _context.TacGiaSachs.RemoveRange(removeSachCu);

                    var addSachIds = from sachId in newSacIds
                                     where !oldSacIds.Contains(sachId)
                                     select sachId;

                    foreach (var addSachId in addSachIds)
                    {
                        _context.TacGiaSachs.Add(new TacGiaSach()
                        {
                            TacGia = tacGiaUpdate,
                            SachId = addSachId
                        });
                    }



                    _context.Update(tacGiaUpdate);
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
