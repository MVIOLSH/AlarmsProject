using Alarms.Logic;
using Alarms.Logic.DTO;

namespace Alarms.Web.Models.Home
{
    public class DataViewer
    {
        public QueryDto queryDto { get; set; }
        public IList<EventLogDto> dbListOfData { get; set; }
        public string tagName { get; set; }
        public string description { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
        public string ipp { get; set; }
        public int pSize { get; set; }
        public int currPage { get; set; }
        public int totalPages { get; set; }
        public int index { get; set; } = 0;
        public string? sortBy { get; set; }
        public string? sortedBy { get; set; }

    }
}
