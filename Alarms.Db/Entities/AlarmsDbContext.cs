using Microsoft.EntityFrameworkCore;

namespace Alarms.Db.Entities
{
    public class AlarmsDbContext : DbContext
    {
        public AlarmsDbContext(DbContextOptions<AlarmsDbContext> options) : base(options)
        {

        }
        public DbSet<TagData> TagDatas { get; set; }
        public DbSet<EventLog> EventLogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb; Database=AlamrsProject;Trusted_Connection=True;");
            }

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }

}
