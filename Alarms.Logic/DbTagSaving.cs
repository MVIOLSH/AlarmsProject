using Alarms.Db.Entities;

namespace Alarms.Logic
{
    public class DbTagSaving
    {
        private AlarmsDbContext db;
        public DbTagSaving(AlarmsDbContext _db)
        {
            db = _db;

        }

        public List<TagData> SaveTagsToDatabase(List<AlarmDto> PassedList)
        {
            List<TagData> tagsAsReturn = new List<TagData>();

            var listOfTagsFromDb = db.TagDatas.ToList();
            List<TagData> listOfTagsToDb = new List<TagData>();
            List<AlarmDto> tempList = PassedList;
            PassedList.RemoveAll(x => listOfTagsFromDb.Exists(a => a.TagName == x.TagName));



            foreach (var alarm in PassedList)
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
