﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alarms.Db.Entities.Configuration
{
    internal class TagDataConfiguration : IEntityTypeConfiguration<TagData>
    {
        public void Configure(EntityTypeBuilder<TagData> builder)
        {
           
        }
    }
}