using Alarms.Db.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alarms.Logic
{
    public class DbTagSaving
    {
        public List<TagData> SaveTagsToDatabase(List<AlarmDto> PassedList)
        {
            List<TagData> tagsAsReturn = new List<TagData>();
            var db = new AlarmsDbContext();
            var listOfTagsFromDb = db.TagDatas.ToList();
            List<TagData> listOfTagsToDb = 
            foreach (var alarm in PassedList)
            {
                if(alarm.TagName.Any())
            }

            return tagsAsReturn;
        }
    }
}
