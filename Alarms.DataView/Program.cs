using Alarms.DataView;
using Alarms.Db.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

Console.WriteLine("Welcome TO data Viewer ");
var db = new Alarms.Db.Entities.AlarmsDbContext();
var data = db.EventLogs.Include(x => x.TagData);
foreach (var item in data)
{
    Console.WriteLine($"{item.TagDataId} , {item.TagData.TagName} , {item.EventDateTime} , {item.State} ");
}
Console.ReadLine(); 
