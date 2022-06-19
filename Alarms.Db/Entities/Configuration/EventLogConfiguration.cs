using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alarms.Db.Entities.Configuration
{
    internal class EventLogConfiguration : IEntityTypeConfiguration<EventLog>
    {
        public void Configure(EntityTypeBuilder<EventLog> builder)
        {
           // builder.HasOne( c=>c.TagData).WithOne(e =>e.EventLog).HasForeignKey<TagData>( c=>c.TagDataId);
        }
    }
}
