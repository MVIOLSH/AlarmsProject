using Alarms.Logic;
using Alarms.Web.Models;
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
            return View();
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
    }
}