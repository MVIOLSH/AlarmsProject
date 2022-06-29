using Alarms.Logic;

namespace Alarms.Web.Models.Home
{
    public class AllData
    {
        public IList<EventLogDto> dbListOfData { get; set; }
        public Dictionary<string, int> MyDictionary { get; set; }


    }
}
