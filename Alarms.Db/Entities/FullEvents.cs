namespace Alarms.Db.Entities
{
    public class FullEvents
    {
        public Guid FullEventsId { get; set; }
        public Guid TagDataId { get; set; }
        public DateTime EventStart { get; set; }
        public DateTime EventEnd { get; set; }
        public int DurationSeconds { get; set; }

        public virtual TagData TagData { get; set; }
    }
}
