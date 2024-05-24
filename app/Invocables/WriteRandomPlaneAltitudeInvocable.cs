using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.Services;
using Coravel.Invocable;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Writes;

namespace app.Invocables
{
    public class WriteRandomPlaneAltitudeInvocable : IInvocable
    {
        private readonly InfluxDBService _service;
        private static readonly Random _random = new Random();

        public WriteRandomPlaneAltitudeInvocable(InfluxDBService service)
        {
            _service = service;
        }

        // [Obsolete]
        // public Task Invoke()
        // {
        //     _service.Write(write =>
        //     {
        //         var point = PointData.Measurement("altitude")
        //             .Tag("plane", "test-plane")
        //             .Field("value", _random.Next(1000, 5000))
        //             .Timestamp(DateTime.UtcNow, WritePrecision.Ns);

        //         write.WritePoint(point,"teste-bucket", "CO2COMPANY");
        //     });

        //     return Task.CompletedTask;
        // }

        // [Obsolete]
        // public Task Invoke()
        // {
        //     _service.Write(write =>
        //     {
        //         var point = PointData.Measurement("estufas")
        //             .Tag("local", "test-local")
        //             .Field("value", _random.Next(1, 5))
        //             .Timestamp(DateTime.UtcNow, WritePrecision.Ns);

        //         write.WritePoint(point, "teste-bucket", "fmhk");
        //     });

        //     return Task.CompletedTask;
        // }

        [Obsolete]
        public async Task Invoke()
        {
            var point = PointData.Measurement("estufas")
                .Tag("local", "test-local")
                .Field("value", _random.Next(1, 5))
                .Timestamp(DateTime.UtcNow, WritePrecision.Ns);

            await _service.WriteAsync(point, "teste-bucket", "fmhk");
        }


    }
}