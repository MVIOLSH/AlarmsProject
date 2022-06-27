namespace Alarms.Db.Entities
{
    public class EventLog
    {
        public Guid EventLogId { get; set; } = new Guid();
        public DateTime EventDateTime { get; set; }
        public bool State { get; set; }
        public TagData TagData { get; set; }
        public Guid TagDataId { get; set; }


    }
}

