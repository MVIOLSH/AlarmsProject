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
        public  List<TagData> SaveTagsToDatabase(List<AlarmDto> PassedList)
        {
            List<TagData> tagsAsReturn = new List<TagData>();
            var db = new AlarmsDbContext();
            var listOfTagsFromDb = db.TagDatas.ToList();
            List<TagData> listOfTagsToDb = new List<TagData>();
            List<AlarmDto> tempList = PassedList;
            PassedList.RemoveAll(x => listOfTagsFromDb.Exists(a => a.TagName == x.TagName));
            
            

            foreach(var alarm in  PassedList)
            {
                TagData tag = new TagData()
                {
                    TagName = alarm.TagName,
                    Description = alarm.Description
                };
                db.TagDatas.Add(tag);
               
            }
            db.SaveChanges();


            tagsAsReturn = db.TagDatas.ToList();

            return tagsAsReturn;
        }
    }
}
