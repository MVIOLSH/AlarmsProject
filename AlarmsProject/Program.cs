using System.Text.Json;
using Alarms.GeneratorApp.Lib;
using Alarms.Db.Entities;
using Alarms.Logic;

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


var startDateConvert = Convert.ToInt32(sYear)+"/" +Convert.ToInt32(sMonth)+"/"+ Convert.ToInt32(sDay)+" "+sTime;
DateTime startDate = Convert.ToDateTime(startDateConvert);

var endDateConvert = Convert.ToInt32(eYear) + "/" + Convert.ToInt32(eMonth) + "/" + Convert.ToInt32(eDay) + " " + eTime;
DateTime endDate = Convert.ToDateTime(endDateConvert);

Console.WriteLine($"The Time span you've entered is from {startDate} To {endDate}");
Console.WriteLine("Is that correct? For yes press Y and enter: ");


var confirm = Console.ReadLine();

if (confirm == "Y" || confirm =="y")
{
    Console.WriteLine("Now the list of tags...");
    string confirmation = "N";
    List<string> tags = new List<string>();

    while(confirmation != "Y")
    {
        Console.WriteLine("Write a tag");

        string tag = Console.ReadLine();
        tags.Add(tag);

        Console.WriteLine(" Is that all the tags you wanted? Y = yes N = No");
        confirmation = Console.ReadLine();  
    }

    Console.WriteLine( "List of Tags is showed below: ");
    foreach(string tag in tags)
    {
        Console.WriteLine(tag);
    }
    Console.WriteLine("Please wait....");
    var dbContext = new AlarmsDbContext();
    var tagsFromDB = dbContext.TagDatas.Select( x=> x.TagName ).ToList();
    List<string> joinedTags = tags.Union(tagsFromDB).ToList();
    
    var Generator = new Generator();

    var FinalLinst =  Generator.Generate(startDate, endDate, joinedTags);
    Console.WriteLine( "Done");

    foreach(var alarm in FinalLinst)
    {
        Console.WriteLine($"{alarm.TagName}, {alarm.TagDescription}, {alarm.EventDateTime}, {alarm.State}");
    }
    Console.WriteLine("Do you want to save the data do Database? Y for Yes N for No");
    var saveConfirmation = Console.ReadLine();
    if(saveConfirmation == "Y")
    {
        var DbSave = new DbSavingMockData();
        var JsonList = JsonSerializer.Serialize(FinalLinst);
        DbSave.SaveDataToDb(JsonList);
    }



}

