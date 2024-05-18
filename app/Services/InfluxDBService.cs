using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Writes;
using InfluxDB.Client.Core.Flux.Domain;
using Microsoft.Extensions.Configuration;
using app.Models;



namespace app.Services
{
    public class InfluxDBService
    {

        private readonly string _token;
        private readonly string _bucket;
        private readonly string _org;
        private readonly InfluxDBClient _client;
        // private readonly string _bucket = "teste-bucket";
        // private readonly string _org = "CO2COMPANY";
        private readonly string _url = "http://localhost:8086";

        [Obsolete]
        public InfluxDBService(IConfiguration configuration)
        {
            _token = configuration.GetValue<string>("InfluxDB:Token");
            _client = InfluxDBClientFactory.Create(_url, _token);
            _token = configuration.GetValue<string>("InfluxDB:Token");
            _bucket = configuration.GetValue<string>("InfluxDB:Bucket"); // Certifique-se de que o nome da chave está correto
            _org = configuration.GetValue<string>("InfluxDB:Org"); // Certifique-se de que o nome da chave está correto

        }

        [Obsolete]
        public void Write(Action<WriteApi> action)
        {
            using var client = InfluxDBClientFactory.Create("http://localhost:8086", _token);
            using var write = client.GetWriteApi();
            action(write);
        }

        [Obsolete]
        public async Task<T> QueryAsync<T>(Func<QueryApi, Task<T>> action)
        {
            using var client = InfluxDBClientFactory.Create("http://localhost:8086", _token);
            var query = client.GetQueryApi();
            return await action(query);
        }

    }
}