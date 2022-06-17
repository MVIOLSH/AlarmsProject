using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alarms.Db.Entities
{
    public class AlarmsDbContext : DbContext
    {
        public DbSet<TagData> TagDatas { get; set; }
        public DbSet<EventLog> EventLogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Data Source = (localdb)\MSSQLlocalDB; Catalog = AlarmProject");
            }

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 

        }
    }
    
}
