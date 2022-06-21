using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Alarms.Logic
{
    public class EventLogDto
    {      
        public DateTime EventDateTime { get; set; }
        public bool State { get; set; }
        public string TagName { get; set; }      

    }
}

