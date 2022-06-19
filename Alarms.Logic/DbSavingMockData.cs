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
        public Task SaveDataToDb(string Json, string tagDataList)
        {
            var dbContext = new AlarmsDbContext();
            var deSerializedEvents = JsonSerializer.Deserialize<List<AlarmDto>>(Json);
            var deSerializedTags = JsonSerializer.Deserialize<List<TagData>>(tagDataList);
            List<TagData> ListOfTags = new List<TagData>();
            List<EventLog> EventsLogs = new List<EventLog>();
            List<TagData> TagsFromDB = dbContext.TagDatas.ToList();

            if(deSerializedTags != null)
            {
                foreach(var tagData in deSerializedTags)
                {
                    TagData tagDataCreate = new TagData()
                    {

                        TagName = tagData.TagName,
                        Description = tagData.Description,
                    };
                    if (!TagsFromDB.Any().Equals(tagData.TagName))
                    {
                        ListOfTags.Add(tagData);
                    }

                }
            }


            if (deSerializedEvents != null)
            {
               
                foreach (var entity in deSerializedEvents)
                {
                    
                    

                    EventLog log = new EventLog()
                    {
                        
                        EventDateTime = entity.EventDateTime,
                        State = entity.State,

                    };

                    
                    EventsLogs.Add(log);
                    
                    
                }
            }

            
            dbContext.SaveChanges();
                
           
            return Task.CompletedTask;
        }

    }
}
