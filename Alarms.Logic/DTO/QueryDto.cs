namespace Alarms.Logic.DTO
{
    public class QueryDto
    {
        public List<EventLogDto> events { get; set; }
        public int PageCount { get; set; }
        public int ItemPerPage { get; set; }
        public int CurrentPage { get; set; }
        public string sortBy { get; set; }
        public string? sortedBy { get; set; }


    }
}
