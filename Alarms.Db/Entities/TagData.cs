using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alarms.Db.Entities
{
    public class TagData
    {
        public Guid TagDataId { get; set; }
        public string TagName { get; set; }
        public string Description { get; set; }
    }
}
