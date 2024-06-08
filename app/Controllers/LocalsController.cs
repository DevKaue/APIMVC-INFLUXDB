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

        [HttpGet("Locals/Infos")]
        public async Task<IActionResult> Index([FromServices] InfluxDBService service)
        {
            try
            {
                var results = await service.QueryAsync(async query =>
                {
                    var flux = "from(bucket:\"teste-bucket\") " +
                               "|> range(start: 0) " +
                               "|> filter(fn: (r) => r._measurement == \"mqtt_consumer\") " +
                               "|> filter(fn: (r) => r._field == \"uplink_message_decoded_payload_Temperatura\" or r._field == \"uplink_message_decoded_payload_Umidade\" or r._field == \"uplink_message_decoded_payload_Gas\")";

                    var tables = await query.QueryAsync(flux, "fmhk");

                    var localsList = new List<LocalsModel>();

                    foreach (var table in tables)
                    {
                        foreach (var record in table.Records)
                        {
                            var field = record.GetField();
                            var valueString = record.GetValue()?.ToString();
                            if (valueString != null && float.TryParse(valueString, out float value))
                            {
                                var time = record.GetTime().ToString();
                                var locals = localsList.FirstOrDefault(x => x.Time == time);
                                if (locals == null)
                                {
                                    locals = new LocalsModel
                                    {
                                        Time = time
                                    };
                                    localsList.Add(locals);
                                }

                                if (field == "uplink_message_decoded_payload_Temperatura")
                                    locals.Temperatura = value;
                                else if (field == "uplink_message_decoded_payload_Umidade")
                                    locals.Umidade = value;
                                else if (field == "uplink_message_decoded_payload_Gas")
                                    locals.Gas = value;
                            }
                        }
                    }

                    return localsList;
                });

                var viewModel = new LocalsViewModel
                {
                    // Locals = results ?? new List<LocalsModel>()
                    Locals = results ?? new List<LocalsModel>()
                };
                // Retorna a lista de LocalsModel diretamente para a view
                return View("Index", viewModel);
            }
            catch (Exception ex)
            {
                // Se ocorrer algum erro ao consultar o InfluxDB, retorne uma página de erro
                _logger.LogError(ex, "Ocorreu um erro ao consultar o InfluxDB");
                return View("Error");
            }
        }


    }
}