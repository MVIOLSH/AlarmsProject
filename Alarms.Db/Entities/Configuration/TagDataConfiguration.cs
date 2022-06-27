using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alarms.Db.Entities.Configuration
{
    internal class TagDataConfiguration : IEntityTypeConfiguration<TagData>
    {
        public void Configure(EntityTypeBuilder<TagData> builder)
        {
            builder.Property(x => x.TagName).HasColumnType("varchar(50)");
            builder.HasMany(w => w.EventLog).WithOne(c => c.TagData).HasForeignKey(k => k.TagDataId);



        }
    }

}