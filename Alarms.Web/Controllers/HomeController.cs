using Alarms.Logic;
using Alarms.Web.Models;
using Alarms.Web.Models.Alarms;
using Alarms.Web.Models.Home;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Alarms.Web.Controllers
{
    public class HomeController : Controller
    {


        private readonly ILogger<HomeController> _logger;

        private readonly DataHandler _handler;




        public HomeController(ILogger<HomeController> logger, DataHandler handler)
        {
            _logger = logger;
            _handler = handler;

        }

        public IActionResult Index()
        {
            var model = new DashboardModel
            {
                List1 = ListTop10occ(),
                List2 = ListTop10occ()
            };

            return View(model);
        }

        public ChartRep ListTop10occ()
        {
            var dbResult = _handler.FullEventsConversion().GroupBy(x => x.TagData.TagName).OrderByDescending(x => x.Count()).Take(10);

            List<string> labels = new();
            List<decimal> values = new();

            ChartRep chartRep = new();
            foreach (var item in dbResult)
            {
                labels.Add(item.Key + " -----> " + item.Count());
                values.Add((decimal)1);
            }
            chartRep.Labels = labels;
            chartRep.Number = values;
            return chartRep;
        }
        public IActionResult ChartDataTop10ActiveTime()
        {
            AllData data = new AllData();

            data.dbListOfData = _handler.SimpleDataFetch();
            var groupedList = data.dbListOfData.OrderBy(x => x.TagName).GroupBy(x => x.TagName);
            var orderedAndTrimedList = groupedList.OrderByDescending(x => (decimal)(x.Count())).ToList();
            ChartRep chartRep = new();


            chartRep.Labels = orderedAndTrimedList.Select(x => x.Key).Take(10).ToList();
            chartRep.Number = orderedAndTrimedList.Select(x => (decimal)(x.Count())).Take(10).ToList();


            return Json(chartRep);
        }

        public IActionResult Converter()
        {
            var dbResult = _handler.FullEventsConversion();

            Converter converter = new();
            List<FullEventRep> events = new();

            foreach (var item in dbResult)
            {
                FullEventRep ev = new()
                {
                    EventStart = item.EventStart,
                    EventEnd = item.EventEnd,
                    DurationInSec = item.DurationSeconds,
                    TagName = item.TagData.TagName
                };
                events.Add(ev);
            }

            converter.FullEvetsList = events.OrderBy(x => x.EventStart).ToList();

            return View(converter);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Whatever()
        {
            Random rnd = new Random();
            var rnd2 = new Random();

            WhateverModel whatever = new WhateverModel();
            whatever.WhateverList = Enumerable.Range(0, 10).Select(r => rnd.Next(100)).ToList();

            return View(whatever);
        }

        public IActionResult AllData()
        {

            AllData data = new AllData();
            data.dbListOfData = _handler.SimpleDataFetch();

            return View(data);
        }

        [HttpGet]
        public IActionResult DataViewer(DateTime? startDate, DateTime? endDate, string? filter, int page, string? sortBy, string? sortedBy, string? currentQuery, string ipp = "20", string? sortField = null, string? sortOrder = null)
        {

            DataViewer viewer = new DataViewer();
            viewer.pSize = Convert.ToInt32(ipp);
            sortBy = sortField + " " + sortOrder;
            viewer.queryDto = _handler.DataFetchRedesign(startDate, endDate, filter, ipp, page, sortBy);
            viewer.dbListOfData = viewer.queryDto.events;
            viewer.ipp = viewer.queryDto.ItemPerPage.ToString();
            viewer.currPage = viewer.queryDto.CurrentPage;
            viewer.totalPages = viewer.queryDto.PageCount;

            return View(viewer);
        }

        public IActionResult DataViewerTable(DateTime? startDate, DateTime? endDate, string? filter, int page, string? sortBy, string? sortedBy, string? currentQuery, string ipp = "20", string? sortField = null, string? sortOrder = null)
        {
            DataViewer partialViewer = new DataViewer();
            partialViewer.pSize = Convert.ToInt32(ipp);
            sortBy = sortField + " " + sortOrder;
            partialViewer.queryDto = _handler.DataFetchRedesign(startDate, endDate, filter, ipp, page, sortBy);
            partialViewer.dbListOfData = partialViewer.queryDto.events;
            partialViewer.ipp = partialViewer.queryDto.ItemPerPage.ToString();
            partialViewer.currPage = partialViewer.queryDto.CurrentPage;
            partialViewer.totalPages = partialViewer.queryDto.PageCount;


            return PartialView("DataViewer/_DataViewerTable", partialViewer);
        }

        //public IActionResult Top10Occ()
        //{
        //    var dbResult = _handler.FullEventsConversion();

        //    Converter converter = new();
        //    List<FullEventRep> events = new();

        //    foreach (var item in dbResult)
        //    {
        //        FullEventRep ev = new()
        //        {
        //            EventStart = item.EventStart,
        //            EventEnd = item.EventEnd,
        //            DurationInSec = item.DurationSeconds,
        //            TagName = item.TagData.TagName
        //        };
        //        events.Add(ev);
        //    }

        //    var groupped = events.GroupBy(x => x.TagName).ToList();

        //    converter.FullEvetsList = groupped.Count;

        //    return View(converter);
        //}
    }
}




