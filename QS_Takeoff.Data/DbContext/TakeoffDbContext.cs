using Microsoft.EntityFrameworkCore;
using QS_Takeoff.Data.Entities;

namespace QS_Takeoff.Data.DbContext {
    public class TakeoffDbContext : Microsoft.EntityFrameworkCore.DbContext {
        public DbSet<Project> Projects => Set<Project>();
        public DbSet<FileRecord> Files => Set<FileRecord>();
        public DbSet<Quantity> Quantities => Set<Quantity>();
        public DbSet<Measurement> Measurements => Set<Measurement>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlite("Data Source=takeoff.db");
        }
    }
}
