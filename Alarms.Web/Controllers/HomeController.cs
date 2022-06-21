using Alarms.Web.Models;
using Alarms.Web.Models.Home;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Alarms.Logic;
using Microsoft.EntityFrameworkCore;

namespace Alarms.Web.Controllers
{
    public class HomeController : Controller
    {
        

        private readonly ILogger<HomeController> _logger;
       

        public HomeController(ILogger<HomeController> logger )
        {
            _logger = logger;
            
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
           DataHandler dataHandler = new DataHandler();
            AllData data = new AllData();
            data.dbListOfData = dataHandler.SimpleDataFetch();

            return View(data);
        }
        public IActionResult DataViewer(DateTime? startDate, DateTime? endDate, string? filter )
        {
            DataHandler dataHandler = new DataHandler();
            DataViewer viewer = new DataViewer();
            
            viewer.dbListOfData = dataHandler.DataFetchWithDates(startDate, endDate, filter);           
            viewer.startDate = startDate;
            viewer.endDate = endDate;
            viewer.tagName = filter;

            return View(viewer);
        }
    }
}