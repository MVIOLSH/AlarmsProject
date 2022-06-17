using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Alarms.Db.Entities
{
    public class EventLog
    {
        public Guid EventLogId { get; set; }
        public DateTime EventDateTime { get; set; }
        public bool State { get; set; }
        public TagData TagData { get; set; }
        public Guid TagDataId { get; set; }


    }
}

