using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alarms.GeneratorApp.Lib
{
    public class TagGenerator
    {
        public List<Alarm> TagGenerate(List<string> tagNames, int seed)
        {
            List<Alarm> alarms = new List<Alarm>();
            Random random = new Random(seed);
            var ListOfWords = new[] {
                                        "libero. Proin mi.", "Phasellus in felis.", "In tincidunt congue", "diam luctus lobortis.", "vel pede blandit", "Nunc ut erat.",
                                        "iaculis enim, sit","per inceptos hymenaeos.","mauris a nunc.","natoque penatibus et","in felis. Nulla","Maecenas malesuada fringilla",
                                        "massa. Suspendisse eleifend.","augue ut lacus."
                                    };

            foreach ( string tag in tagNames)
            {
                var createAlam = new Alarm()
                {
                    TagName = tag,
                    TagDescription = tagNames[random.Next(tagNames.Count)]
                    
                };
            }

             return alarms;       
            
        }
        
    }
}
