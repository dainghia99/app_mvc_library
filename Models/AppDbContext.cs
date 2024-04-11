using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using appmvclibrary.Models.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace appmvclibrary.Models
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext (DbContextOptions<AppDbContext> options) : base (options) { }

        protected override void OnModelCreating (ModelBuilder builder) {

            base.OnModelCreating (builder);
            // Bỏ tiền tố AspNet của các bảng: mặc định
            foreach (var entityType in builder.Model.GetEntityTypes ()) {
                var tableName = entityType.GetTableName ();
                if (tableName.StartsWith ("AspNet")) {
                    entityType.SetTableName (tableName.Substring (6));
                }
            }

            builder.Entity<SachCategory>(entity => {
                entity.HasKey(x => new {x.SachId, x.CategoryId});
            });
            builder.Entity<TacGiaSach>(entity => {
                entity.HasKey(x => new {x.SachId, x.TacGiaId});
            });
        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Category> Categories {get; set;}
        public DbSet<Sach> Sachs { get; set; }
        public DbSet<SachCategory> SachCategories { get; set; }
        public DbSet<TacGia> TacGias { get; set; }
        public DbSet<TacGiaSach> TacGiaSachs { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<PhieuMuonTra> PhieuMuonTras { get; set; }
        public DbSet<LichSuMuonTra> LichSuMuonTras { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}