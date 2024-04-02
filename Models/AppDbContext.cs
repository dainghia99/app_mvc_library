using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace appmvclibrary.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext (DbContextOptions<AppDbContext> options) : base (options) { }

        protected override void OnModelCreating (ModelBuilder builder) {

            base.OnModelCreating (builder);
            // Bỏ tiền tố AspNet của các bảng: mặc định
            // foreach (var entityType in builder.Model.GetEntityTypes ()) {
            //     var tableName = entityType.GetTableName ();
            //     if (tableName.StartsWith ("AspNet")) {
            //         entityType.SetTableName (tableName.Substring (6));
            //     }
            // }
        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Category> Categories {get; set;}
    }
}