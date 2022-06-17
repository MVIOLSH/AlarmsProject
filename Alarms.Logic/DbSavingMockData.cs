using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alarms.Db;
using Alarms.Db.Entities;
using System.Text.Json;

namespace Alarms.Logic
{
    public class DbSavingMockData
    {
        public Task SaveDataToDb(string Json)
        {
            var dbContext = new AlarmsDbContext();
            var deSerialized = JsonSerializer.Deserialize<List<AlarmDto>>(Json);
            List<TagData> ListOfTags = new List<TagData>();
            List<EventLog> EventsLogs = new List<EventLog>();

            if (deSerialized != null)
            {
                foreach (var entity in deSerialized)
                {
                    TagData tagData = new TagData()
                    {
                       
                        TagName = entity.TagName,
                        Description = entity.TagDescription,
                    };
                    EventLog log = new EventLog()
                    {
                        
                        EventDateTime = entity.EventDateTime,
                        State = entity.State,

                    };
                    //ListOfTags.Add(tagData);
                    //EventsLogs.Add(log);
                    dbContext.TagDatas.Add(tagData);
                    dbContext.EventLogs.Add(log);
                }
            }

            
            dbContext.SaveChanges();
                
           
            return Task.CompletedTask;
        }

    }
}
