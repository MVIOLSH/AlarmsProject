namespace Alarms.GeneratorApp.Lib
{
    public class Alarm
    {
        public string TagName { get; set; }
        public string Description { get; set; }
        public DateTime EventDateTime { get; set; }
        public bool State { get; set; }
        public Guid TagDataId { get; set; }
    }
}
