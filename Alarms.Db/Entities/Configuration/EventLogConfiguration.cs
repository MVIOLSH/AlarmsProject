using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
