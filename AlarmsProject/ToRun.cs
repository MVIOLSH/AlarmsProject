using Alarms.GeneratorApp.Lib;
using Alarms.Logic;
using System.Text.Json;

namespace Alarms.GeneratorApp
{

    public class ToRun
    {
        private DbSavingMockData _dbSavingMockData;
        private DbTagSaving _dbTagSaving;

        public ToRun()
        {

        }

        public ToRun(DbSavingMockData dbSavingMockData, DbTagSaving dbTagSaving)
        {
            _dbSavingMockData = dbSavingMockData;
            _dbTagSaving = dbTagSaving;
        }

        public void RunIt()
        {

            Console.WriteLine("To generate the Mock Data for the Database we will a Time span");

            Console.WriteLine("Below write a day of starting Date for the generated Data: ");
            var sDay = Console.ReadLine();

            Console.WriteLine("Now the Month: ");
            var sMonth = Console.ReadLine();

            Console.WriteLine("And the year: ");
            var sYear = Console.ReadLine();

            Console.WriteLine("Hour(In the HH:MM Format): ");
            var sTime = Console.ReadLine();

            Console.WriteLine("Now lets do the end Date");

            Console.WriteLine("Below write a day of End Date for the generated Data: ");
            var eDay = Console.ReadLine();

            Console.WriteLine("Now the Month: ");
            var eMonth = Console.ReadLine();

            Console.WriteLine("And the year: ");
            var eYear = Console.ReadLine();

            Console.WriteLine("Hour(In the HH:MM Format): ");
            var eTime = Console.ReadLine();


            var startDateConvert = Convert.ToInt32(sYear) + "/" + Convert.ToInt32(sMonth) + "/" + Convert.ToInt32(sDay) + " " + sTime;
            DateTime startDate = Convert.ToDateTime(startDateConvert);

            var endDateConvert = Convert.ToInt32(eYear) + "/" + Convert.ToInt32(eMonth) + "/" + Convert.ToInt32(eDay) + " " + eTime;
            DateTime endDate = Convert.ToDateTime(endDateConvert);

            Console.WriteLine($"The Time span you've entered is from {startDate} To {endDate}");
            Console.WriteLine("Is that correct? For yes press Y and enter: ");


            var confirm = Console.ReadLine();

            if (confirm == "Y" || confirm == "y")
            {
                #region Gathering tag data
                Console.WriteLine("Now the list of tags...");
                string confirmation = "N";
                List<string> tags = new List<string>();

                while (confirmation != "Y")
                {
                    Console.WriteLine("Write a tag");

                    string tag = Console.ReadLine();
                    tags.Add(tag);

                    Console.WriteLine(" Is that all the tags you wanted? Y = yes N = No");
                    confirmation = Console.ReadLine();
                }
                #endregion

                Console.WriteLine("List of Tags is showed below: ");

                foreach (string tag in tags)
                {
                    Console.WriteLine(tag);
                }

                Console.WriteLine("Please wait....");

                #region Creating of list of tags to add a new ones to DB


                var seed = (startDate.ToString() + endDate.ToString()).GetHashCode() + tags.GetHashCode();

                #endregion


                TagGenerator TagGenerator = new TagGenerator();

                var genratedTags = TagGenerator.TagGenerate(tags, seed);

                List<AlarmDto> alarms = new List<AlarmDto>();

                foreach (var Tag in genratedTags)
                {
                    AlarmDto alarm = new AlarmDto()
                    {
                        TagName = Tag.TagName,
                        Description = Tag.Description,

                    };
                    alarms.Add(alarm);
                }




                var ListToSerialize = _dbTagSaving.SaveTagsToDatabase(alarms);
                var jsonGeneratedTags = JsonSerializer.Serialize(ListToSerialize);

                Generator genrator = new Generator();

                var FinalLinst = genrator.Generate(startDate, endDate, jsonGeneratedTags);
                Console.WriteLine("Done");

                foreach (var alarm in FinalLinst)
                {
                    Console.WriteLine($"{alarm.TagName}, {alarm.Description}, {alarm.EventDateTime}, {alarm.State}");
                }
                Console.WriteLine("Do you want to save the data do Database? Y for Yes N for No");
                var saveConfirmation = Console.ReadLine();
                if (saveConfirmation == "Y")
                {

                    var JsonList = JsonSerializer.Serialize(FinalLinst);
                    //var JsonTagDataList = JsonSerializer.Serialize(joinedTags);
                    _dbSavingMockData.SaveDataToDb(JsonList);
                }



            }
            else
            {
                Console.WriteLine("In that case we have nothing more to do here :) Goodbye");
                Console.ReadLine();
            }
        }
    }
}
