using Alarms.Db.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alarms.Logic
{
    public class DataHandler
    {
        public AlarmsDbContext db { get; set; } = new AlarmsDbContext();
        public IList<EventLogDto> SimpleDataFetch()
        {
            //List<AlarmDto> data = new List<AlarmDto>();
            var dbData = db.EventLogs.Select(r => new EventLogDto { EventDateTime = r.EventDateTime, TagName = r.TagData.TagName, State = r.State }).ToList(); ;



            return dbData;
            
        }
        public IList<EventLogDto> DataFetchWithDates( DateTime? startDate, DateTime? endDate, string? searchPhrase)
        {
            //List<AlarmDto> data = new List<AlarmDto>();
            List<EventLogDto> dbData = new List<EventLogDto>();
            if (searchPhrase == null)
            {
                dbData = db.EventLogs.Where(x => (x.EventDateTime > startDate && x.EventDateTime < endDate) ).Select(r => new EventLogDto { EventDateTime = r.EventDateTime, TagName = r.TagData.TagName, State = r.State }).ToList(); ;

            }
            else
            {
                dbData = db.EventLogs.Where(x => (x.EventDateTime > startDate && x.EventDateTime < endDate) && x.TagData.TagName == searchPhrase || x.TagData.Description == searchPhrase).Select(r => new EventLogDto { EventDateTime = r.EventDateTime, TagName = r.TagData.TagName, State = r.State }).ToList(); ;
            }


            return dbData;

        }
    }
}
