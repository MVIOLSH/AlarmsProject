using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alarms.DataView
{
    public class AlarmView
    {
        public Guid AlarmId { get; set; }
        public DateTime EventDateTime { get; set; }
        public string TagName { get; set; }
        public bool EventState { get; set; }

    }
}
