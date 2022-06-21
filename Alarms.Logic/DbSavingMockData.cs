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
            var deSerializedEvents = JsonSerializer.Deserialize<List<AlarmDto>>(Json);
            //var deSerializedTags = JsonSerializer.Deserialize<List<TagData>>(tagDataList);
            //List<TagData> ListOfTags = new List<TagData>();
            List<EventLog> EventsLogs = new List<EventLog>();
            //List<TagData> TagsFromDB = dbContext.TagDatas.ToList();


            foreach (var entity in deSerializedEvents)
            {
                EventLog log = new EventLog()
                {
                    TagDataId = entity.TagDataId,
                    EventDateTime = entity.EventDateTime,
                    State = entity.State,


                };
                

               dbContext.Add(log);


            }
            dbContext.SaveChanges();
            return Task.CompletedTask;  
        }              
           
    }
}

