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



        public List<Alarm> Generate( DateTime start, DateTime end, List<string> tags)
        {
            var ListOfWords = new[] {
    new {
        text = "libero. Proin mi."
    },
    new {
        text = "Phasellus in felis."
    },
    new {
        text = "In tincidunt congue"
    },
    new {
        text = "diam luctus lobortis."
    },
    new {
        text = "vel pede blandit"
    },
    new {
        text = "Nunc ut erat."
    },
    new {
        text = "iaculis enim, sit"
    },
    new {
        text = "per inceptos hymenaeos."
    },
    new {
        text = "mauris a nunc."
    },
    new {
        text = "natoque penatibus et"
    },
    new {
        text = "in felis. Nulla"
    },
    new {
        text = "Maecenas malesuada fringilla"
    },
    new {
        text = "massa. Suspendisse eleifend."
    },
    new {
        text = "augue ut lacus."
    }
};
            List<Alarm> alarms = new List<Alarm>();
            int hashed = tags.GetHashCode();
            Random random = new Random(hashed);   

            while(start.ToOADate() <= end.ToOADate())
            {
                start = start.AddSeconds(random.Next(60, 600));

                if(start.ToOADate() > end.ToOADate())
                {
                    break;

                }
                
                string currentTag = tags[random.Next(tags.Count)];
                string description = ListOfWords[random.Next(1, 14)].ToString();

                foreach (Alarm alarm in alarms)
                {
                    if (alarm.TagName == currentTag && alarm.State == true)
                    {
                        State = false;
                    }
                    else if (alarm.TagName == currentTag && alarm.State == false)
                    {
                        State = true;
                    }
                }
                    Alarm genneratedAlarm = new Alarm()
                    {
                        TagName = currentTag,
                        State = State,
                        EventDateTime = start,
                        TagDescription = description
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
