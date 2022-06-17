using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alarms.Db.Entities
{
    public class AlarmDto
    {
        public string TagName { get; set; }
        public string TagDescription { get; set; }
        public DateTime EventDateTime { get; set; }
        public bool State { get; set; }
    }
}
