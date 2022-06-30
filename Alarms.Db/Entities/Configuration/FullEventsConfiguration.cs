using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alarms.Db.Entities.Configuration
{

    internal class FullEventsConfiguration : IEntityTypeConfiguration<FullEvents>
    {
        public void Configure(EntityTypeBuilder<FullEvents> builder)
        {
            builder.HasOne(x => x.TagData).WithMany(x => x.FullEvents).HasForeignKey(k => k.TagDataId);

        }
    }
}
