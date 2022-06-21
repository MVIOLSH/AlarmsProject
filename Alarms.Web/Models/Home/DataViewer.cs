using Alarms.Logic;

namespace Alarms.Web.Models.Home
{
    public class DataViewer
    {
        public IList<EventLogDto> dbListOfData { get; set; }
        public string tagName { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }

    }
}
