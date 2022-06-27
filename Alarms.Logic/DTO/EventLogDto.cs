namespace Alarms.Logic
{
    public class EventLogDto
    {
        public DateTime EventDateTime { get; set; }
        public bool State { get; set; }
        public string TagName { get; set; }
        public string TagDescription { get; set; }
        public int index { get; set; }


    }
}

