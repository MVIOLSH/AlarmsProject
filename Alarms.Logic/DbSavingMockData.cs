using Alarms.Db.Entities;
using System.Text.Json;

namespace Alarms.Logic
{
    public class DbSavingMockData
    {
        private AlarmsDbContext dbContext;
        public DbSavingMockData(AlarmsDbContext _dbContext)
        {
            dbContext = _dbContext;

        }

        public Task SaveDataToDb(string Json)
        {

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

