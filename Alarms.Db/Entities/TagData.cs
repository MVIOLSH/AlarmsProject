namespace Alarms.Db.Entities
{
    public class TagData
    {
        public Guid TagDataId { get; set; } = new Guid();
        public string TagName { get; set; }
        public string Description { get; set; }

        public virtual List<EventLog> EventLog { get; set; } = new List<EventLog>();
        public virtual List<FullEvents> FullEvents { get; set; } = new();
    }
}
