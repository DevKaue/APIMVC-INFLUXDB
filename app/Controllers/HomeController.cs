﻿// using System.Diagnostics;
// using Microsoft.AspNetCore.Mvc;
// using app.Models;
// using app.Services;

// namespace app.Controllers;

// public class HomeController : Controller
// {
//     private readonly ILogger<HomeController> _logger;

//     public HomeController(ILogger<HomeController> logger)
//     {
//         _logger = logger;
//     }

//     // public IActionResult Index()
//     // {
//     //     return View();
//     // }

//     public IActionResult Privacy()
//     {
//         return View();
//     }

//     [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
//     public IActionResult Error()
//     {
//         return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
//     }

//     [Obsolete]
//     public async Task<IActionResult> Index([FromServices] InfluxDBService service)
//     {
//         var results = await service.QueryAsync(async query =>
//         {
//             var flux = "from(bucket:\"teste-bucket\") |> range(start: 0)";
//             var tables = await query.QueryAsync(flux, "CO2COMPANY");
//             return tables.SelectMany(table =>
//                 table.Records.Select(record =>
//                     new AltitudeModel
//                     {
//                         Time = record.GetTime().ToString(),
//                         Altitude = int.Parse(record.GetValue().ToString())
//                     }));
//         });

//         return View(results);
//     }

// }


using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using app.Models;
using app.Services;

namespace app.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
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
    }
}
