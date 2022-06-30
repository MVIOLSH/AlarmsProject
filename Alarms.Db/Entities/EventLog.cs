namespace Alarms.Db.Entities
{
    public class EventLog
    {
        public Guid EventLogId { get; set; } = new Guid();
        public Guid TagDataId { get; set; }
        public DateTime EventDateTime { get; set; }
        public bool State { get; set; }

        public virtual TagData TagData { get; set; }
    }
}

