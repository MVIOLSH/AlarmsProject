using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace Alarms.GeneratorApp.Lib
{
    public class Generator
    {
        public List<Alarm> GeneratedAlarmsList { get; set; }   
        public bool State { get; set; } = true;
        public int Seed { get; set; }
        public string JSON { get; set; }



        public List<Alarm> Generate( DateTime start, DateTime end, string tags)
        {
           
            List<Alarm> alarms = new List<Alarm>();
            int hashed = tags.GetHashCode();
            Random random = new Random(hashed);   
            List<Alarm> tagsFromJson = JsonSerializer.Deserialize<List<Alarm>>(tags);

            while(start.ToOADate() <= end.ToOADate())
            {
                start = start.AddSeconds(random.Next(60, 600));

                if(start.ToOADate() > end.ToOADate())
                {
                    break;

                }
                
                var currentTag = tagsFromJson[random.Next(tagsFromJson.Count)];
            


                foreach (Alarm alarm in alarms)
                {
                    if (alarm.TagName == currentTag.TagName && alarm.State == true)
                    {
                        State = false;
                    }
                    else if (alarm.TagName == currentTag.TagName && alarm.State == false)
                    {
                        State = true;
                    }
                }
                Alarm genneratedAlarm = new Alarm()
                    {
                        TagDataId = currentTag.TagDataId,
                        TagName = currentTag.TagName,
                        State = State,
                        EventDateTime = start,
                        Description = currentTag.Description, 
                    };

                    alarms.Add(genneratedAlarm);

                
            }

            GeneratedAlarmsList = alarms;
            return alarms;
        }

        public void GenerateTags(List<string> UserInputTags)
        {


        }

        public void ToJSON()
        {
            JSON =  JsonSerializer.Serialize(GeneratedAlarmsList);
        }
        

    }
    

}
