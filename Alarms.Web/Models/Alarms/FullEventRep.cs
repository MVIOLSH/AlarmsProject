namespace Alarms.Web.Models.Alarms
{
    public class FullEventRep
    {
        public Guid FullEventRepId { get; set; }
        public DateTime EventStart { get; set; }
        public DateTime EventEnd { get; set; }
        public string TagName { get; set; }
        public int DurationInSec { get; set; }
    }
}
