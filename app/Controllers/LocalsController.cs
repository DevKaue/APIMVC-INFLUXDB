using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using app.Models;
using app.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace app.Controllers
{
    public class LocalsController : Controller
    {
        private readonly ILogger<LocalsController> _logger;

        public LocalsController(ILogger<LocalsController> logger)
        {
            _logger = logger;
        }

        // public IActionResult Index()
        // {
        //     return View();
        // }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }

        //[Obsolete]
        // public async Task<IActionResult> Index([FromServices] InfluxDBService service)
        // {
        //     var results = await service.QueryAsync(async query =>
        //     {
        //         var flux = "from(bucket:\"teste-bucket\") |> range(start: 0)";
        //         var tables = await query.QueryAsync(flux, "CO2COMPANY");

        //         return tables.SelectMany(table =>
        //             table.Records.Select(record =>
        //             {
        //                 var altitudeString = record.GetValue()?.ToString();
        //                 if (altitudeString != null && int.TryParse(altitudeString, out int altitude))
        //                 {
        //                     return new AltitudeModel
        //                     {
        //                         Time = record.GetTime().ToString(),
        //                         Altitude = altitude
        //                     };
        //                 }
        //                 else
        //                 {
        //                     // Aqui você pode decidir o que fazer se o valor não puder ser convertido em um número inteiro
        //                     // Por exemplo, você pode atribuir um valor padrão ou lidar com o erro de outra forma.
        //                     return new AltitudeModel
        //                     {
        //                         Time = record.GetTime().ToString(),
        //                         Altitude = 0 // Valor padrão
        //                     };
        //                 }
        //             }));
        //     });

        //     return View(results);
        // }

        [Obsolete]
        public async Task<IActionResult> Index([FromServices] InfluxDBService service)
        {
            var results = await service.QueryAsync(async query =>
            {
                var flux = "from(bucket:\"teste-bucket\") |> range(start: 0)";
                var tables = await query.QueryAsync(flux, "CO2COMPANY");

                return tables.SelectMany(table =>
                    table.Records.Select(record =>
                    {
                        var localString = record.GetValue()?.ToString();
                        if (localString != null && int.TryParse(localString, out int local))
                        {
                            return new LocalsModel
                            {
                                Time = record.GetTime().ToString(),
                                CodeReference = local
                            };
                        }
                        else
                        {
                            // Aqui você pode decidir o que fazer se o valor não puder ser convertido em um número inteiro
                            // Por exemplo, você pode atribuir um valor padrão ou lidar com o erro de outra forma.
                            return new LocalsModel
                            {
                                Time = record.GetTime().ToString(),
                                CodeReference = 0
                            };
                        }
                    }));
            });

            return View(results);
        }
    }
}